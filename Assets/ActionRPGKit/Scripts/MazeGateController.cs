using UnityEngine;
using System.Collections;

// 迷宫门控制器 - 处理门的交互逻辑
public class MazeGateController : MonoBehaviour {
	
	public string lockedMessage = "The gate is locked."; // 未解锁时的消息
	public string unlockedMessage = "The gate opens..."; // 解锁后的消息
	
	public int unlockVarId = 99; // 解锁变量ID
	public string teleportToScene = "Dungeon"; // 传送目标场景
	public string spawnPointName = "PlayerSpawn1"; // 重生点
	
	public GameObject doorObject; // 门对象（用于播放动画）
	public AnimationClip openAnimation; // 开门动画
	
	private bool playerInRange = false;
	private GameObject player;
	private bool isUnlocked = false;
	
	void Update(){
		// 更新解锁状态
		isUnlocked = EventSetting.globalInt[unlockVarId] == 1;
		
		// 调试：按K键强制解锁
		if(Input.GetKeyDown(KeyCode.K)){
			EventSetting.globalInt[unlockVarId] = 1;
			isUnlocked = true;
			Debug.Log("=== FORCE UNLOCKED! globalInt[" + unlockVarId + "] = 1 ===");
		}
		
		// 如果玩家在范围内按E键
		if(playerInRange && Input.GetKeyDown(KeyCode.E) && !GlobalConditionC.freezeAll && Time.timeScale != 0){
			InteractWithGate();
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			player = other.gameObject;
			playerInRange = true;
			Debug.Log("Player entered gate trigger");
			
			// 显示交互提示
			if(player.GetComponent<AttackTriggerC>()){
				player.GetComponent<AttackTriggerC>().GetActivator(this.gameObject, "InteractWithGate", "Open Gate");
			}
		}
	}
	
	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			playerInRange = false;
			
			if(player && player.GetComponent<AttackTriggerC>()){
				player.GetComponent<AttackTriggerC>().RemoveActivator(this.gameObject);
			}
		}
	}
	
	public void InteractWithGate(){
		if(!player){
			player = GlobalConditionC.mainPlayer;
		}
		
		if(!player){
			return;
		}
		
		// 检查是否解锁
		isUnlocked = EventSetting.globalInt[unlockVarId] == 1;
		
		Debug.Log("=== Gate Interaction ===");
		Debug.Log("isUnlocked: " + isUnlocked);
		Debug.Log("globalInt[" + unlockVarId + "] = " + EventSetting.globalInt[unlockVarId]);
		
		if(!isUnlocked){
			// 显示未解锁消息
			ShowMessage(lockedMessage);
			Debug.Log("Gate is locked!");
			return;
		}
		
		// 门已解锁，执行开门逻辑
		OpenGate();
	}
	
	void ShowMessage(string message){
		// 使用对话系统显示消息
		if(player.GetComponent<AttackTriggerC>()){
			// 显示简单消息
			Debug.Log("Message: " + message);
			
			// 如果有显示文本的UI，可以在这里调用
			// 这里只是一个简单的实现
		}
	}
	
	void OpenGate(){
		Debug.Log("=== Opening Gate ===");
		
		// 播放开门动画
		if(doorObject && openAnimation){
			doorObject.GetComponent<Animation>().Play(openAnimation.name);
		}
		
		// 显示解锁消息
		ShowMessage(unlockedMessage);
		
		// 延迟传送
		StartCoroutine(DelayedTeleport(1.5f)); // 1.5秒后传送
	}
	
	IEnumerator DelayedTeleport(float delay){
		yield return new WaitForSeconds(delay);
		
		// 设置重生点
		if(player.GetComponent<StatusC>()){
			player.GetComponent<StatusC>().spawnPointName = spawnPointName;
		}
		
		// 销毁坐骑
		GameObject[] mounts = GameObject.FindGameObjectsWithTag("Mount");
		foreach(GameObject mount in mounts){
			mount.SendMessage("DestroySelf", SendMessageOptions.DontRequireReceiver);
		}
		
		// 传送
		Debug.Log("Teleporting to: " + teleportToScene);
		UnityEngine.SceneManagement.SceneManager.LoadScene(teleportToScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}
	
	// OnGUI用于调试显示
	void OnGUI(){
		GUI.color = Color.white;
		GUI.Label(new Rect(10, 10, 400, 20), "Maze Gate - Unlock: " + isUnlocked);
		GUI.Label(new Rect(10, 35, 400, 20), "globalInt[" + unlockVarId + "] = " + EventSetting.globalInt[unlockVarId]);
		GUI.Label(new Rect(10, 60, 400, 20), "Press K to force unlock");
	}
}