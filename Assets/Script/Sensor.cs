using UnityEngine;

public class Sensor : MonoBehaviour
{
	[SerializeField, Header("�b�|")] float radius = 0.5f;
	[SerializeField, Header("�j�p")] Vector2 squareSize = Vector2.one;
	[SerializeField, Header("�����ϼh")] LayerMask sensorLayer;
	[SerializeField] �������Ϊ� shape = �������Ϊ�.circle;

	public bool isOn = false;

	private void FixedUpdate()
	{
		Collider2D �����쪺�F�� = null;

		if (shape == �������Ϊ�.circle)
		{
			�����쪺�F�� = Physics2D.OverlapCircle(transform.position, radius, sensorLayer);
		}
		else if (shape == �������Ϊ�.square)
		{
			�����쪺�F�� = Physics2D.OverlapBox(transform.position, squareSize, 0f, sensorLayer);
		}

		if (�����쪺�F�� == null)
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
			if (shape == �������Ϊ�.circle)
			{
				Gizmos.DrawSphere(transform.position, radius);
			}
			else if (shape == �������Ϊ�.square)
			{
				Gizmos.DrawCube(transform.position, squareSize);
			}
		}
		else
		{
			if (shape == �������Ϊ�.circle)
			{
				Gizmos.DrawWireSphere(transform.position, radius);
			}
			else if (shape == �������Ϊ�.square)
			{
				Gizmos.DrawWireCube(transform.position, squareSize);
			}
		}
	}

	public enum �������Ϊ�
	{
		circle = 0,
		square = 1,
	}
}
