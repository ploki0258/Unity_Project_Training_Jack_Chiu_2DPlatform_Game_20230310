using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[SerializeField, Header("追蹤目標")]
	Transform target;
	[SerializeField, Header("追蹤速度"), Range(0, 100)]
	float speed = 1.5f;
	[SerializeField, Header("攝影機限制上下範圍")]
	Vector2 limitUD = new Vector2(-1.7f, 1.7f);
	[SerializeField, Header("攝影機限制左右範圍")]
	Vector2 limitLR = new Vector2(-1.19f, 1.19f);
	[SerializeField, Header("攝影機物件")]
	GameObject cam1_Main, cam2_Fungus;
	[SerializeField]
	Camera MainCam;

	private void Start()
	{
		CMControl();
	}

	private void LateUpdate()
	{
		//Track();
	}

	private void CMControl()
	{
		cam1_Main.SetActive(true);
		cam2_Fungus.SetActive(false);
	}

	/// <summary>
	/// 切換攝影機
	/// </summary>
	/// <param name="isCam1">第一台攝影機是否開啟</param>
	/// <param name="isCam2">第二台攝影機是否開啟</param>
	public void CMSwitch(bool isCam1, bool isCam2)
	{
		cam1_Main.SetActive(isCam1);
		cam2_Fungus.SetActive(isCam2);
	}

	/// <summary>
	/// 物件追蹤
	/// </summary>
	void Track()
	{
		Vector3 posA = transform.position;                      // 取得攝影機座標
		Vector3 posB = target.position;                         // 取得目標座標

		posB.z = -10;
		posB.y = Mathf.Clamp(posB.y, limitUD.x, limitUD.y);     // 將 Y 軸夾在限制範圍內
		posB.x = Mathf.Clamp(posB.x, limitLR.x, limitLR.y);     // 將 X 軸夾在限制範圍內

		posA = Vector3.Lerp(posA, posB, speed * Time.deltaTime);

		transform.position = posA;
	}

	//void CMChageView()
	//{
		
	//}
}
