using UnityEngine;
using System.Collections;

public class DialogueC : MonoBehaviour {
	public TextDialogue[] message = new TextDialogue[1];

	public Transform mainModel;
	//public Texture2D button;
	public Texture2D textWindow;
	[HideInInspector]
	public bool enter = false;
	private bool showGui = false;
	[HideInInspector]
	public int s = 0;
	[HideInInspector]
	public GameObject player;
	
	[HideInInspector]
	public bool talkFinish = false;
	
	public string sendMessageWhenDone = "";
	
	// 新增：门解锁相关设置
	public bool isMazeDoor = false; // 是否是迷宫门
	public int unlockVarId = 99; // 解锁变量ID
	public string lockedMessage = "The gate is locked."; // 未解锁时显示的消息
	public string teleportScene = "Dungeon"; // 解锁后传送的场景
	public string spawnPointName = "PlayerSpawn1"; // 重生点

	public GUIStyle textStyle;
	//-------------------------
	private string[] str = new string[4];
	private int line = 0;
	
	private float wait = 0;
	public float delay = 0.05f;
	private bool begin = false;
	private int i = 0;
	private string[] wordComplete = new string[4];
	public bool freezeTime = true;
	public bool lookAtNpc = false;
	public bool npcLookPlayer = false;
	private bool rot = false;
	public bool activateSelf = true;
	
	void Update(){
		if(lookAtNpc && rot && player){
			Vector3 destinya = mainModel.position;
			destinya.y = player.transform.root.position.y;
			
			Quaternion targetRotation = Quaternion.LookRotation(destinya - player.transform.root.position);
			player.transform.root.rotation = Quaternion.Slerp(player.transform.root.rotation, targetRotation, 8 * Time.unscaledDeltaTime);
		}
		if(npcLookPlayer && rot && player){
			Vector3 destinyb = player.transform.position;
			destinyb.y = mainModel.position.y;
			
			Quaternion targetRotationa = Quaternion.LookRotation(destinyb - mainModel.position);
			mainModel.transform.rotation = Quaternion.Slerp(mainModel.rotation, targetRotationa, 8 * Time.unscaledDeltaTime);
		}
		
		// 调试：按Ctrl+U强制解锁迷宫门
		if(isMazeDoor && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U)){
			Debug.Log("=== FORCE UNLOCK MAZE DOOR (Ctrl+U) ===");
			EventSetting.globalInt[unlockVarId] = 1;
		}
		
		// 调试显示：按Ctrl+D显示解锁状态
		if(isMazeDoor && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D)){
			Debug.Log("=== MAZE DOOR STATUS ===");
			Debug.Log("isMazeDoor: " + isMazeDoor);
			Debug.Log("unlockVarId: " + unlockVarId);
			Debug.Log("globalInt[" + unlockVarId + "] = " + EventSetting.globalInt[unlockVarId]);
			Debug.Log("Quest0: " + EventSetting.globalInt[0]);
			Debug.Log("Quest1: " + EventSetting.globalInt[1]);
			Debug.Log("Quest2: " + EventSetting.globalInt[2]);
		}

		/*if(Input.GetKeyDown("e") && enter && activateSelf){
			if(s == 0 && GlobalConditionC.interacting){
				return;
			}
			NextPage();
		}*/
		if(begin){
			if(wait >= delay){
				if(wordComplete[line].Length > 0)
					str[line] += wordComplete[line][i++];
				wait = 0;
				if(i >= wordComplete[line].Length && line > 2){
					begin = false;
				}else if(i >= wordComplete[line].Length){
					i = 0;
					line++;
				}
			}else{
				//wait += Time.deltaTime;
				wait += Time.unscaledDeltaTime;
			}
			
		}
	}

	IEnumerator ForceRotation(){
		rot = true;
		if(!freezeTime){
			yield return new WaitForSeconds(1);
		}else{
			yield return new WaitForSeconds(0.1f);
		}
		if(lookAtNpc){
			LookAtMe();
		}
		if(npcLookPlayer){
			LookPlayer();
		}
		rot = false;
	}
	
	public void AnimateText(string strComplete , string strComplete2 , string strComplete3 , string strComplete4){
		begin = false;
		i = 0;
		str[0] = "";
		str[1] = "";
		str[2] = "";
		str[3] = "";
		line = 0;
		wordComplete[0] = strComplete;
		wordComplete[1] = strComplete2;
		wordComplete[2] = strComplete3;
		wordComplete[3] = strComplete4;
		begin = true;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			s = 0;
			talkFinish = false;
			player = other.gameObject;
			enter = true;
			if(player.GetComponent<AttackTriggerC>())
				player.GetComponent<AttackTriggerC>().GetActivator(this.gameObject , "Talking" , "Talk");
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			s = 0;
			enter = false;
			if(player.GetComponent<AttackTriggerC>())
				player.GetComponent<AttackTriggerC>().RemoveActivator(this.gameObject);
			CloseTalk();
		}
	}

	void Talking(){
		if(!player){
			player = GlobalConditionC.mainPlayer;
		}
		
		// 如果是迷宫门，检查解锁状态
		if(isMazeDoor){
			bool isUnlocked = EventSetting.globalInt[unlockVarId] == 1;
			Debug.Log("=== Maze Door Check in DialogueC ===");
			Debug.Log("isUnlocked: " + isUnlocked);
			Debug.Log("globalInt[" + unlockVarId + "] = " + EventSetting.globalInt[unlockVarId]);
			
			if(!isUnlocked){
				// 显示锁定消息
				ShowLockedMessage();
				return;
			}
			
			// 门已解锁，执行传送
			TeleportToMaze();
			return;
		}
		
		if(s == 0 && player){
			if(Time.timeScale == 0 || GlobalConditionC.freezeAll){
				return;
			}
			StartCoroutine(ForceRotation());
		}
		NextPage();
	}
	
	// 显示锁定消息
	void ShowLockedMessage(){
		if(!player){
			player = GlobalConditionC.mainPlayer;
		}
		
		if(player){
			// 可以在这里显示锁定消息
			Debug.Log("Locked Message: " + lockedMessage);
			
			// 简单实现：直接显示消息然后关闭对话
			s = 0;
			talkFinish = true;
			CloseTalk();
		}
	}
	
	// 传送至迷宫
	void TeleportToMaze(){
		if(!player){
			player = GlobalConditionC.mainPlayer;
		}
		
		if(player){
			Debug.Log("=== Teleporting to Maze: " + teleportScene + " ===");
			
			// 设置重生点
			StatusC status = player.GetComponent<StatusC>();
			if(status){
				status.spawnPointName = spawnPointName;
			}
			
			// 销毁坐骑
			GameObject[] mounts = GameObject.FindGameObjectsWithTag("Mount");
			foreach(GameObject mount in mounts){
				mount.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
			}
			
			// 传送
			UnityEngine.SceneManagement.SceneManager.LoadScene(teleportScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
	
	public void CloseTalk(){
		showGui = false;
		Time.timeScale = 1.0f;
		//Screen.lockCursor = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		GlobalConditionC.freezeAll = false;
		GlobalConditionC.freezePlayer = false;
		GlobalConditionC.interacting = false;
		GlobalConditionC.freezeCam = false;
		s = 0;
		
		// 重置玩家状态
		if(player){
			StatusC playerStatus = player.GetComponent<StatusC>();
			if(playerStatus){
				playerStatus.canControl = true;
				playerStatus.freeze = false;
			}
			
			PlayerInputControllerC playerInput = player.GetComponent<PlayerInputControllerC>();
			if(playerInput){
				playerInput.unableToMove = false;
			}
			
			CharacterMotorC motor = player.GetComponent<CharacterMotorC>();
			if(motor){
				motor.canControl = true;
				motor.freezeGravity = false;
			}
		}
	}
	
	public void NextPage(){
		if(!enter || EventActivator.onInteracting){
			return;
		}
		if(s == 0 && player){
			StartCoroutine(ForceRotation());
		}
		if(begin){
			str[0] = wordComplete[0];
			str[1] = wordComplete[1];
			str[2] = wordComplete[2];
			str[3] = wordComplete[3];
			begin = false;
			return;
		}
		s++;
		if(s > message.Length){
			showGui = false;
			talkFinish = true;
			CloseTalk();
			if(sendMessageWhenDone != ""){
				gameObject.SendMessage(sendMessageWhenDone , SendMessageOptions.DontRequireReceiver);
			}
		}else{
			if(freezeTime){
				Time.timeScale = 0.0f;
			}else{
				GlobalConditionC.freezeAll = true;
			}
			talkFinish = false;
			//Screen.lockCursor = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			showGui = true;
			AnimateText(message[s-1].textLine1 , message[s-1].textLine2 , message[s-1].textLine3 , message[s-1].textLine4);
		}
	}
	
	void OnGUI(){
		if(!player){
			return;
		}
		/*if(enter && !showGui && !GlobalConditionC.interacting && activateSelf){
			//GUI.DrawTexture( new Rect(Screen.width / 2 - 130, Screen.height - 120, 260, 80), button);
			if (GUI.Button ( new Rect(Screen.width / 2 - 130, Screen.height - 180, 260, 80), button)){
				NextPage();
			}
		}*/
		
		if(showGui && s <= message.Length){
			GUI.DrawTexture(new Rect(Screen.width /2 - 308, Screen.height - 255, 615, 220), textWindow);
			GUI.Label(new Rect(Screen.width /2 - 263, Screen.height - 220, 500, 200), str[0] , textStyle);
			GUI.Label(new Rect(Screen.width /2 - 263, Screen.height - 190, 500, 200), str[1] , textStyle);
			GUI.Label(new Rect(Screen.width /2 - 263, Screen.height - 160, 500, 200), str[2] , textStyle);
			GUI.Label(new Rect(Screen.width /2 - 263, Screen.height - 130, 500, 200), str[3] , textStyle);
			if(GUI.Button(new Rect(Screen.width /2 + 160,Screen.height - 100,100,30), "Next")){
				NextPage();
			}
		}
	}

	void LookAtMe(){
		Vector3 lookTo = mainModel.position;
		lookTo.y = player.transform.root.position.y;
		player.transform.root.LookAt(lookTo);
	}
	
	void LookPlayer(){
		Vector3 lookTo = player.transform.position;
		lookTo.y = mainModel.position.y;
		mainModel.transform.LookAt(lookTo);
	}
}

[System.Serializable]
public class TextDialogue{
	public string textLine1 = "";
	public string textLine2 = "";
	public string textLine3 = "";
	public string textLine4 = "";
}