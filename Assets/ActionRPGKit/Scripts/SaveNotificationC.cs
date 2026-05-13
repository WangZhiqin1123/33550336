using UnityEngine;
using System.Collections;

public class SaveNotificationC : MonoBehaviour {
	
	[Header("Settings")]
	public bool enableNotification = true;
	public float displayDuration = 2.0f;
	
	[Header("UI Settings")]
	public Color textColor = Color.green;
	public int fontSize = 24;
	public Vector2 screenPosition = new Vector2(0.5f, 0.15f);
	
	private bool isDisplaying = false;
	private float displayTimer = 0.0f;
	private string notificationText = "Auto Saved!";
	
	void Update() {
		if (!enableNotification) return;
		
		if (isDisplaying) {
			displayTimer -= Time.deltaTime;
			if (displayTimer <= 0) {
				isDisplaying = false;
			}
		}
	}
	
	public void ShowSaveNotification(string text = "Auto Saved!") {
		if (!enableNotification) return;
		
		notificationText = text;
		displayTimer = displayDuration;
		isDisplaying = true;
	}
	
	void OnGUI() {
		if (!isDisplaying || !enableNotification) return;
		
		// 设置UI样式
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = fontSize;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = textColor;
		
		// 计算位置
		float x = Screen.width * screenPosition.x;
		float y = Screen.height * screenPosition.y;
		
		// 显示通知
		GUI.Label(new Rect(x - 150, y - 20, 300, 40), notificationText, style);
	}
}