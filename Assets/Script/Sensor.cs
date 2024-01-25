using UnityEngine;

public class Sensor : MonoBehaviour
{
	[SerializeField, Header("半徑")] float radius = 0.5f;
	[SerializeField, Header("大小")] Vector2 squareSize = Vector2.one;
	[SerializeField, Header("偵測圖層")] LayerMask sensorLayer;
	[SerializeField] 偵測器形狀 shape = 偵測器形狀.circle;

	public bool isOn = false;

	private void FixedUpdate()
	{
		Collider2D 偵測到的東西 = null;

		if (shape == 偵測器形狀.circle)
		{
			偵測到的東西 = Physics2D.OverlapCircle(transform.position, radius, sensorLayer);
		}
		else if (shape == 偵測器形狀.square)
		{
			偵測到的東西 = Physics2D.OverlapBox(transform.position, squareSize, 0f, sensorLayer);
		}

		if (偵測到的東西 == null)
		{
			isOn = false;
		}
		else
		{
			isOn = true;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;

		if (isOn)
		{
			if (shape == 偵測器形狀.circle)
			{
				Gizmos.DrawSphere(transform.position, radius);
			}
			else if (shape == 偵測器形狀.square)
			{
				Gizmos.DrawCube(transform.position, squareSize);
			}
		}
		else
		{
			if (shape == 偵測器形狀.circle)
			{
				Gizmos.DrawWireSphere(transform.position, radius);
			}
			else if (shape == 偵測器形狀.square)
			{
				Gizmos.DrawWireCube(transform.position, squareSize);
			}
		}
	}

	public enum 偵測器形狀
	{
		circle = 0,
		square = 1,
	}
}
