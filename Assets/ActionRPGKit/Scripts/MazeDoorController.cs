using UnityEngine;
using System.Collections;

public class MazeDoorController : MonoBehaviour {
	
	public GameObject doorObject; // 门对象
	public bool keepChecking = true; // 持续检查
	public bool startUnlocked = false; // 初始是否解锁
	
	void Start(){
		if(startUnlocked && doorObject){
			doorObject.SetActive(false); // 如果初始解锁，就隐藏门
		}
		
		CheckCondition();
	}
	
	void Update(){
		if(keepChecking){
			CheckCondition();
		}
	}
	
	public void CheckCondition(){
		// 检查GlobalUnlockManager
		if(GlobalUnlockManager.instance){
			if(GlobalUnlockManager.instance.mazeUnlocked){
				// 解锁！
				if(doorObject){
					doorObject.SetActive(false); // 隐藏门
				}
			}else{
				// 未解锁，显示门
				if(doorObject){
					doorObject.SetActive(true);
				}
			}
		}
		// 也检查EventSetting的两个任务是否都完成
		else if(EventSetting.globalInt[0] == 1 && EventSetting.globalInt[1] == 1){
			if(doorObject){
				doorObject.SetActive(false);
			}
			// 如果两个任务完成了但GlobalUnlockManager还没解锁，强制解锁
			if(GlobalUnlockManager.instance){
				GlobalUnlockManager.instance.UnlockMaze();
			}
		}
	}
}
