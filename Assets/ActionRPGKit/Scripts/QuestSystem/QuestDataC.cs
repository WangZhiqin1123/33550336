using UnityEngine;
using System.Collections;

public class QuestDataC : MonoBehaviour {
	
	public GameObject itemData;
	[System.Serializable]
	public class Quest {
		public string questName = "";
		public Texture2D icon;
		public string description;
		public int finishProgress = 5;
		public int rewardCash = 100;
		public int rewardExp = 100;
		public int[] rewardItemID;
		public int[] rewardEquipmentID;
	}
	
	public Quest[] questData = new Quest[3];
	
		// 专用于迷宫门的解锁变量ID
	private const int MAZE_DOOR_UNLOCK_VAR = 99;
	
	public void QuestClear(int id , GameObject player){
		Debug.Log("=== Quest " + id + " completed! ===");
		Debug.Log("Quest name: " + questData[id].questName);
		
		//Get Rewards
		player.GetComponent<InventoryC>().cash += questData[id].rewardCash; //Add Cash
		player.GetComponent<StatusC>().gainEXP(questData[id].rewardExp); //Get EXP
		int i = 0;
		if(questData[id].rewardItemID.Length > 0){	//Add Items
			 i = 0;
			while(i < questData[id].rewardItemID.Length){
				player.GetComponent<InventoryC>().AddItem(questData[id].rewardItemID[i] , 1);
				i++;
			}
		}
		
		if(questData[id].rewardEquipmentID.Length > 0){	//Add Equipments
			i = 0;
			while(i < questData[id].rewardEquipmentID.Length){
				player.GetComponent<InventoryC>().AddEquipment(questData[id].rewardEquipmentID[i]);
				i++;
			}
		}
		
		// 设置全局变量标记任务完成（使用任务ID作为索引）
		EventSetting.globalInt[id] = 1;
		Debug.Log("Set globalInt[" + id + "] = 1");
		
		// 打印当前所有任务状态
		Debug.Log("Current quest states - Quest0: " + EventSetting.globalInt[0] + 
		          ", Quest1: " + EventSetting.globalInt[1] + 
		          ", Quest2: " + EventSetting.globalInt[2]);
		
		// 检查是否满足解锁条件 - 使用专门的解锁变量
		// 方案：任务0和任务1都完成，或者任务2完成
		bool shouldUnlock = false;
		
		if(EventSetting.globalInt[0] == 1 && EventSetting.globalInt[1] == 1){
			shouldUnlock = true;
			Debug.Log("Both Quest 0 and 1 completed!");
		}
		else if(EventSetting.globalInt[2] == 1){
			shouldUnlock = true;
			Debug.Log("Quest 2 completed!");
		}
		
		if(shouldUnlock){
			// 设置专门的解锁变量
			EventSetting.globalInt[MAZE_DOOR_UNLOCK_VAR] = 1;
			Debug.Log("=== UNLOCKING MAZE! Setting globalInt[" + MAZE_DOOR_UNLOCK_VAR + "] = 1 ===");
			
			// 确保GlobalUnlockManager存在并解锁
			if(GlobalUnlockManager.instance == null){
				GameObject manager = new GameObject("GlobalUnlockManager");
				GlobalUnlockManager gManager = manager.AddComponent<GlobalUnlockManager>();
				Debug.Log("Created new GlobalUnlockManager");
			}
			GlobalUnlockManager.instance.UnlockMaze();
			Debug.Log("mazeUnlocked is now: " + GlobalUnlockManager.instance.mazeUnlocked);
		}else{
			Debug.Log("Not unlocking yet - need more quests. Current: globalInt[0]=" + EventSetting.globalInt[0] + 
			          ", globalInt[1]=" + EventSetting.globalInt[1] + ", globalInt[2]=" + EventSetting.globalInt[2]);
		}
	}
}