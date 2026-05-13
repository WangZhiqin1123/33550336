using UnityEngine;
using System.Collections;

public class EquipmentCompareC : MonoBehaviour {
	
	[Header("Settings")]
	public bool enableCompare = true;
	
	[Header("UI Settings")]
	public Vector2 screenPosition = new Vector2(0.75f, 0.5f);
	public float panelWidth = 220f;
	public float panelHeight = 160f;
	public Color panelColor = new Color(0.15f, 0.15f, 0.15f, 0.9f);
	public Color currentColor = Color.gray;
	public Color newColor = Color.green;
	public Color positiveColor = Color.green;
	public Color negativeColor = Color.red;
	public Color neutralColor = Color.white;
	public int fontSize = 13;
	
	private bool isComparing = false;
	private string currentItemName = "Iron Sword";
	private string newItemName = "Steel Sword";
	
	void Update() {
		// 按空格键模拟装备对比
		if (Input.GetKeyDown(KeyCode.Space)) {
			isComparing = !isComparing;
		}
	}
	
	void OnGUI() {
		if (!enableCompare) return;
		
		if (!isComparing) {
			// 显示提示
			GUIStyle hintStyle = new GUIStyle();
			hintStyle.alignment = TextAnchor.MiddleCenter;
			hintStyle.fontSize = 12;
			hintStyle.normal.textColor = Color.gray;
			GUI.Label(new Rect(Screen.width * 0.75f - 100, Screen.height * 0.95f - 20, 200, 30), 
				"Press SPACE to compare equipment", hintStyle);
			return;
		}
		
		float x = Screen.width * screenPosition.x;
		float y = Screen.height * screenPosition.y;
		
		// 绘制面板背景
		GUI.color = panelColor;
		GUI.Box(new Rect(x, y, panelWidth, panelHeight), "");
		GUI.color = Color.white;
		
		GUIStyle style = new GUIStyle();
		style.fontSize = fontSize;
		style.alignment = TextAnchor.MiddleLeft;
		
		float lineHeight = 20f;
		float startY = y + 15f;
		
		// 标题
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.yellow;
		GUI.Label(new Rect(x + 10, startY, panelWidth - 20, lineHeight), "Equipment Compare", style);
		
		// 当前装备
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = currentColor;
		GUI.Label(new Rect(x + 10, startY + lineHeight, panelWidth/2 - 5, lineHeight), "Current:", style);
		
		// 新装备
		style.normal.textColor = newColor;
		GUI.Label(new Rect(x + panelWidth/2 + 5, startY + lineHeight, panelWidth/2 - 10, lineHeight), "New:", style);
		
		// 装备名称
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(x + 10, startY + lineHeight * 2, panelWidth/2 - 5, lineHeight), currentItemName, style);
		GUI.Label(new Rect(x + panelWidth/2 + 5, startY + lineHeight * 2, panelWidth/2 - 10, lineHeight), newItemName, style);
		
		// 攻击力
		style.normal.textColor = neutralColor;
		GUI.Label(new Rect(x + 10, startY + lineHeight * 3, panelWidth/2 - 5, lineHeight), "ATK: 15", style);
		GUI.Label(new Rect(x + panelWidth/2 + 5, startY + lineHeight * 3, panelWidth/2 - 10, lineHeight), "ATK: 25", style);
		style.normal.textColor = positiveColor;
		GUI.Label(new Rect(x + panelWidth - 30, startY + lineHeight * 3, 25, lineHeight), "+10", style);
		
		// 防御力
		style.normal.textColor = neutralColor;
		GUI.Label(new Rect(x + 10, startY + lineHeight * 4, panelWidth/2 - 5, lineHeight), "DEF: 5", style);
		GUI.Label(new Rect(x + panelWidth/2 + 5, startY + lineHeight * 4, panelWidth/2 - 10, lineHeight), "DEF: 3", style);
		style.normal.textColor = negativeColor;
		GUI.Label(new Rect(x + panelWidth - 30, startY + lineHeight * 4, 25, lineHeight), "-2", style);
		
		// 暴击率
		style.normal.textColor = neutralColor;
		GUI.Label(new Rect(x + 10, startY + lineHeight * 5, panelWidth/2 - 5, lineHeight), "CRIT: 5%", style);
		GUI.Label(new Rect(x + panelWidth/2 + 5, startY + lineHeight * 5, panelWidth/2 - 10, lineHeight), "CRIT: 8%", style);
		style.normal.textColor = positiveColor;
		GUI.Label(new Rect(x + panelWidth - 30, startY + lineHeight * 5, 25, lineHeight), "+3%", style);
		
		// 关闭按钮
		if (GUI.Button(new Rect(x + panelWidth - 25, y + 5, 20, 20), "X")) {
			isComparing = false;
		}
	}
}