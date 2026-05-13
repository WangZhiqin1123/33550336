using UnityEngine;
using System.Collections;

public class EnemyHealthBarC : MonoBehaviour {
	
	[Header("Settings")]
	public bool enableHealthBar = true;
	public float maxDistance = 15f;
	public float barWidth = 80f;
	public float barHeight = 6f;
	
	[Header("UI Settings")]
	public Color healthColor = Color.green;
	public Color damagedColor = Color.yellow;
	public Color criticalColor = Color.red;
	public Color bgColor = Color.black;
	
	private StatusC targetStatus;
	private Camera mainCamera;
	
	void Start() {
		targetStatus = GetComponent<StatusC>();
		mainCamera = Camera.main;
	}
	
	void OnGUI() {
		if (!enableHealthBar) return;
		if (targetStatus == null) return;
		if (mainCamera == null) mainCamera = Camera.main;
		
		// 计算与相机的距离
		float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
		if (distance > maxDistance) return;
		
		// 将世界坐标转换为屏幕坐标
		Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position);
		
		// 如果在屏幕外，不显示
		if (screenPos.z < 0) return;
		if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height) return;
		
		// 计算血量百分比
		float healthPercent = (float)targetStatus.health / targetStatus.maxHealth;
		
		// 根据血量选择颜色
		Color barColor = healthColor;
		if (healthPercent < 0.3f) barColor = criticalColor;
		else if (healthPercent < 0.6f) barColor = damagedColor;
		
		// 血量条位置（在敌人头顶上方）
		float x = screenPos.x - barWidth / 2;
		float y = Screen.height - screenPos.y - 30f;
		
		// 绘制背景
		GUI.color = bgColor;
		GUI.Box(new Rect(x, y, barWidth, barHeight), "");
		
		// 绘制血量
		GUI.color = barColor;
		GUI.Box(new Rect(x, y, barWidth * healthPercent, barHeight), "");
		
		// 绘制边框
		GUI.color = Color.white;
		GUI.Box(new Rect(x, y, barWidth, barHeight), "", new GUIStyle() { border = new RectOffset(1, 1, 1, 1) });
		
		// 显示血量数值
		GUIStyle textStyle = new GUIStyle();
		textStyle.alignment = TextAnchor.MiddleCenter;
		textStyle.fontSize = 10;
		textStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(x, y - 15, barWidth, 15), 
			targetStatus.health + "/" + targetStatus.maxHealth, textStyle);
	}
}