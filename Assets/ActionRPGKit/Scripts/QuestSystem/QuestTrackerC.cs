using UnityEngine;
using System.Collections;

public class QuestTrackerC : MonoBehaviour {
	
	public GameObject questDataBase;
	public GameObject targetMarkerPrefab;
	public float markerHeight = 2.0f;
	public Color markerColor = Color.yellow;
	
	private GameObject currentMarker;
	private GameObject currentTarget;
	private int currentQuestId = -1;
	
	void Start(){
		if(!targetMarkerPrefab){
			// 如果没有预制体，创建一个简单的三角形
			CreateDefaultMarker();
		}
	}
	
	void Update(){
		UpdateQuestTracker();
	}
	
	void CreateDefaultMarker(){
		// 创建一个三角形标记
		GameObject marker = new GameObject("QuestTargetMarker");
		
		// 创建三角形网格
		MeshFilter meshFilter = marker.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = marker.AddComponent<MeshRenderer>();
		
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[3];
		int[] triangles = new int[3];
		
		// 三角形顶点
		vertices[0] = new Vector3(0, 0.5f, 0);
		vertices[1] = new Vector3(-0.3f, -0.5f, 0);
		vertices[2] = new Vector3(0.3f, -0.5f, 0);
		
		triangles[0] = 0;
		triangles[1] = 1;
		triangles[2] = 2;
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		
		meshFilter.mesh = mesh;
		
		// 设置材质
		Material mat = new Material(Shader.Find("Standard"));
		mat.color = markerColor;
		mat.EnableKeyword("_EMISSION");
		mat.SetColor("_EmissionColor", markerColor * 2);
		meshRenderer.material = mat;
		
		// 添加旋转动画
		marker.AddComponent<QuestMarkerRotateC>();
		
		targetMarkerPrefab = marker;
		marker.SetActive(false);
	}
	
	void UpdateQuestTracker(){
		GameObject player = GameObject.FindWithTag("Player");
		if(!player) return;
		
		QuestStatC questStat = player.GetComponent<QuestStatC>();
		if(!questStat) return;
		
		// 查找当前激活的任务
		int activeQuestId = -1;
		GameObject targetObj = null;
		
		for(int i = 0; i < questStat.questSlot.Length; i++){
			int questId = questStat.questSlot[i];
			if(questId > 0){
				// 检查任务是否已完成
				QuestDataC questData = questDataBase.GetComponent<QuestDataC>();
				if(questData && questStat.questProgress[questId] < questData.questData[questId].finishProgress){
					activeQuestId = questId;
					// 查找任务目标
					targetObj = FindQuestTarget(questId);
					break;
				}
			}
		}
		
		// 更新标记
		if(activeQuestId != currentQuestId || targetObj != currentTarget){
			currentQuestId = activeQuestId;
			currentTarget = targetObj;
			
			if(currentTarget && targetMarkerPrefab){
				if(!currentMarker){
					currentMarker = Instantiate(targetMarkerPrefab);
				}
				currentMarker.SetActive(true);
			}else if(currentMarker){
				currentMarker.SetActive(false);
			}
		}
		
		// 更新标记位置
		if(currentMarker && currentTarget){
			Vector3 pos = currentTarget.transform.position;
			pos.y += markerHeight;
			currentMarker.transform.position = pos;
			
			// 让标记朝向玩家
			if(player){
				currentMarker.transform.LookAt(player.transform.position);
				currentMarker.transform.Rotate(0, 180, 0);
			}
		}
	}
	
	GameObject FindQuestTarget(int questId){
		// 查找带有 QuestTriggerC 或 QuestClientC 的对象
		QuestTriggerC[] triggers = FindObjectsOfType<QuestTriggerC>();
		foreach(QuestTriggerC trigger in triggers){
			if(trigger.questId == questId){
				return trigger.gameObject;
			}
		}
		
		QuestClientC[] clients = FindObjectsOfType<QuestClientC>();
		foreach(QuestClientC client in clients){
			if(client.questId == questId){
				return client.gameObject;
			}
		}
		
		return null;
	}
	
	// 显示任务完成弹窗
	public void ShowQuestComplete(int questId){
		StartCoroutine(QuestCompletePopup(questId));
	}
	
	IEnumerator QuestCompletePopup(int questId){
		// 创建弹窗
		GameObject popup = new GameObject("QuestCompletePopup");
		popup.AddComponent<QuestCompletePopupC>().questId = questId;
		
		yield return new WaitForSeconds(3f);
		
		Destroy(popup);
	}
}

// 标记旋转动画
public class QuestMarkerRotateC : MonoBehaviour {
	void Update(){
		transform.Rotate(0, 100 * Time.deltaTime, 0);
	}
}

// 任务完成弹窗
public class QuestCompletePopupC : MonoBehaviour {
	public int questId = 0;
	private GUIStyle style;
	private float alpha = 1f;
	
	void Start(){
		style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Bold;
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.yellow;
	}
	
	void OnGUI(){
		if(alpha <= 0) return;
		
		Color c = style.normal.textColor;
		c.a = alpha;
		style.normal.textColor = c;
		
		GUI.Label(new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200), 
			"任务完成!\nQuest Complete!", style);
		
		alpha -= Time.deltaTime * 0.5f;
	}
}
