using UnityEngine;
using System.Collections;

public class GlobalUnlockManager : MonoBehaviour {
	
	public static GlobalUnlockManager instance;
	
	public bool mazeUnlocked = false;
	public bool dungeonUnlocked = false;
	
	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}
	
	void Start(){
		// 尝试从PlayerPrefs加载解锁状态
		mazeUnlocked = PlayerPrefs.GetInt("MazeUnlocked", 0) == 1;
		dungeonUnlocked = PlayerPrefs.GetInt("DungeonUnlocked", 0) == 1;
		Debug.Log("=== GlobalUnlockManager loaded ===");
		Debug.Log("mazeUnlocked = " + mazeUnlocked);
	}
	
	void Update(){
		// 调试用：按L键强制解锁迷宫
		if(Input.GetKeyDown(KeyCode.L)){
			Debug.Log("=== MANUAL UNLOCK TRIGGERED ===");
			UnlockMaze();
		}
	}
	
	public void UnlockMaze(){
		mazeUnlocked = true;
		PlayerPrefs.SetInt("MazeUnlocked", 1);
		PlayerPrefs.Save();
		Debug.Log("=== MAZE UNLOCKED! ===");
	}
	
	public void UnlockDungeon(){
		dungeonUnlocked = true;
		PlayerPrefs.SetInt("DungeonUnlocked", 1);
		PlayerPrefs.Save();
		Debug.Log("=== DUNGEON UNLOCKED! ===");
	}
	
	public void LockMaze(){
		mazeUnlocked = false;
		PlayerPrefs.SetInt("MazeUnlocked", 0);
		PlayerPrefs.Save();
	}
	
	public void LockDungeon(){
		dungeonUnlocked = false;
		PlayerPrefs.SetInt("DungeonUnlocked", 0);
		PlayerPrefs.Save();
	}
}

// 迷宫入口触发器 - 需要解锁才能进入
public class MazeEntranceC : MonoBehaviour {

	public string teleportToMap = "Dungeon"; // 默认场景名改为Dungeon
	public string spawnPointName = "PlayerSpawn1"; // 重生点名称
	public bool allowMountEnter = true;
	public GameObject doorObject; // 门对象（可选）
	public bool requireUnlock = true; // 是否需要解锁
	public int debugVarId = -1; // 调试用：指定要检查的globalInt变量ID
	
	// 调试显示
	void OnGUI(){
		if(GlobalUnlockManager.instance || debugVarId >= 0){
			GUI.color = Color.white;
			
			// 显示GlobalUnlockManager状态
			string managerStatus = "GlobalUnlockManager: ";
			if(GlobalUnlockManager.instance){
				managerStatus += "EXISTS (mazeUnlocked=" + GlobalUnlockManager.instance.mazeUnlocked + ")";
			}else{
				managerStatus += "NULL";
			}
			GUI.Label(new Rect(10, 10, 600, 20), managerStatus);
			
			// 显示globalInt状态
			GUI.Label(new Rect(10, 35, 600, 20), "globalInt[0]=" + EventSetting.globalInt[0] + 
			          ", globalInt[1]=" + EventSetting.globalInt[1] + 
			          ", globalInt[2]=" + EventSetting.globalInt[2] +
			          ", globalInt[99]=" + EventSetting.globalInt[99]);
			
			// 显示门的状态
			string doorStatus = requireUnlock ? 
				(GlobalUnlockManager.instance && GlobalUnlockManager.instance.mazeUnlocked ? "DOOR: UNLOCKED" : "DOOR: LOCKED") :
				"DOOR: ALWAYS OPEN";
			GUI.Label(new Rect(10, 60, 600, 20), doorStatus);
			
			// 如果指定了调试变量ID
			if(debugVarId >= 0 && debugVarId < EventSetting.globalInt.Length){
				GUI.Label(new Rect(10, 85, 600, 20), "globalInt[" + debugVarId + "] = " + EventSetting.globalInt[debugVarId]);
			}
			
			GUI.Label(new Rect(10, 110, 600, 20), "Press Ctrl+U to force unlock");
		}
	}
	
	void Update(){
		// 按左Ctrl键强制解锁（不会和其他功能冲突）
		if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U)){
			Debug.Log("=== MANUAL UNLOCK TRIGGERED (Ctrl+U) ===");
			if(GlobalUnlockManager.instance == null){
				GameObject manager = new GameObject("GlobalUnlockManager");
				GlobalUnlockManager gManager = manager.AddComponent<GlobalUnlockManager>();
			}
			GlobalUnlockManager.instance.UnlockMaze();
			EventSetting.globalInt[99] = 1;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Debug.Log("=== Player entered Maze Portal trigger ===");
			Debug.Log("mazeUnlocked = " + (GlobalUnlockManager.instance ? GlobalUnlockManager.instance.mazeUnlocked : "null"));
			Debug.Log("globalInt[99] = " + EventSetting.globalInt[99]);
			
			// 检查是否需要解锁
			if(requireUnlock){
				// 同时检查两种方式：GlobalUnlockManager和globalInt[99]
				bool isUnlocked = (GlobalUnlockManager.instance && GlobalUnlockManager.instance.mazeUnlocked) 
				                  || EventSetting.globalInt[99] == 1;
				
				if(!isUnlocked){
					Debug.Log("=== MAZE IS LOCKED! Complete the quests first. ===");
					return;
				}
			}
			
			// 检查坐骑
			if(!allowMountEnter && GlobalConditionC.freezePlayer){
				return;
			}
			
			// 设置玩家的重生点
			other.GetComponent<StatusC>().spawnPointName = spawnPointName;
			
			// 传送！
			Debug.Log("=== TELEPORTING TO MAZE ===");
			ChangeMap();
		}
	}
	
	void ChangeMap(){
		// 销毁所有坐骑
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Mount");
		if(gos.Length > 0){
			foreach(GameObject go in gos){ 
				go.SendMessage("DestroySelf" , SendMessageOptions.DontRequireReceiver);
			}
		}
		
		// 加载地图
		Debug.Log("Loading scene: " + teleportToMap);
		UnityEngine.SceneManagement.SceneManager.LoadScene(teleportToMap, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
}
