using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof (CharacterMotorC))]

public class PlayerInputControllerC : MonoBehaviour {

	public bool unableToMove = false;
	private GameObject mainModel;
	public float walkSpeed = 6.0f;
	public float sprintSpeed = 12.0f;
	public bool canSprint = true;
	private bool sprint = false;
	[HideInInspector]
	public bool recover = false;
	private float staminaRecover = 1.4f;
	private float useStamina = 0.04f;
	[HideInInspector]
		public bool dodging = false;
	
	public Texture2D staminaGauge;
	public Texture2D staminaBorder;
	
	public float maxStamina = 100.0f;
	public float stamina = 100.0f;
	
	private float lastTime = 0.0f;
	[HideInInspector]
	public float recoverStamina = 0.0f;
	private Vector3 dir = Vector3.forward;

	public bool doubleJump = false;
	private bool airJump = false;
	private bool airMove = false;
	private int jumpCount = 0;
	private bool canTripleJump = false;
	private bool heavyAttack = false;
	private bool ignoreFallDamage = false;
	private bool bowHovering = false;
	
	// 瞬移冲刺功能
	public bool canDash = true;
	public float dashDistance = 20f;
	public float dashCooldown = 0.5f;
	private bool isDashing = false;
	private float lastDashTime = 0f;
	private float lastWPressTime = 0f;
	private bool wKeyHeld = false;
	
	private bool useMecanim = true;
	private bool mobileMode = false;
	[HideInInspector]
	public bool mobileJumping = false;

	[System.Serializable]
	public class DodgeSetting{
		public bool canDodgeRoll = false;
		public int staminaUse = 10;
		
		public AnimationClip dodgeForward;
		public AnimationClip dodgeLeft;
		public AnimationClip dodgeRight;
		public AnimationClip dodgeBack;
	}
	public DodgeSetting dodgeRollSetting;
	public FallDamage fallingDamage;
	
	private CharacterMotorC motor;
	private CharacterController controller;

	public JoystickCanvas joyStick;// For Mobile
	private float moveHorizontal;
	private float moveVertical;

	[System.Serializable]
	public class CanvasObj{
		public bool useCanvas = false;
		public GameObject staminaBorder;
		public Image staminaBar;
	}
	public CanvasObj canvasElement;

	// Use this for initialization
	void Start(){
		motor = GetComponent<CharacterMotorC>();
		controller = GetComponent<CharacterController>();
		stamina = maxStamina;
		if(!mainModel){
			mainModel = GetComponent<StatusC>().mainModel;
		}
		useMecanim = GetComponent<AttackTriggerC>().useMecanim;
		mobileMode = GetComponent<AttackTriggerC>().mobileMode;
	}
	
	// Update is called once per frame
	void Update(){
		// 作弊按钮 N：升级+500金币
		if(Input.GetKeyDown(KeyCode.N)){
			CheatLevelUp();
		}
		
		// 作弊按钮 Z：单步完成任务（视为完成击杀一个任务目标）
		if(Input.GetKeyDown(KeyCode.Z)){
			CompleteQuestKill();
		}
		
		// 处理跳跃攻击
		HandleJumpAttack();
		
		StatusC stat = GetComponent<StatusC>();
		if(recover && !sprint && !dodging){
			if(recoverStamina >= staminaRecover){
				StaminaRecovery();
			}else{
				recoverStamina += Time.deltaTime;
			}
		}
		if(sprint || recover || dodging){
			if(canvasElement.useCanvas && canvasElement.staminaBar){
				if(!canvasElement.staminaBorder.activeSelf){
					canvasElement.staminaBorder.SetActive(true);
				}
				float curSt = stamina/maxStamina;
				canvasElement.staminaBar.fillAmount = curSt;
			}
		}
		if(stamina >= maxStamina && canvasElement.useCanvas && canvasElement.staminaBar || GlobalConditionC.freezeAll && canvasElement.useCanvas){
			if(canvasElement.staminaBorder.activeSelf){
				canvasElement.staminaBorder.SetActive(false);
			}
		}

		if(stat.freeze || GlobalConditionC.freezeAll || GlobalConditionC.freezePlayer || !stat.canControl){
			motor.inputMoveDirection = new Vector3(0,0,0);
			if(sprint){
				sprint = false;
				recover = true;
				motor.movement.maxForwardSpeed = walkSpeed;
				motor.movement.maxSidewaysSpeed = walkSpeed;
				recoverStamina = 0.0f;
			}
			return;
		}
		if(Time.timeScale == 0.0f){
			return;
		}
		if(dodging && !unableToMove){
			Vector3 fwd = transform.TransformDirection(dir);
			controller.Move(fwd * 8 * Time.deltaTime);
			return;
		}
		
		if(dodgeRollSetting.canDodgeRoll){
			//Dodge Forward
			if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0 && (controller.collisionFlags & CollisionFlags.Below) != 0 && Input.GetAxis("Horizontal") == 0){
				if(Input.GetButtonDown ("Vertical") && (Time.time - lastTime) < 0.4f && Input.GetButtonDown ("Vertical") && (Time.time - lastTime) > 0.1f && Input.GetAxis("Vertical") > 0.03f){
					lastTime = Time.time;
					dir = Vector3.forward;
					StartCoroutine(DodgeRoll(dodgeRollSetting.dodgeForward));
				}else
					lastTime = Time.time;
			}
			//Dodge Backward
			if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0 && (controller.collisionFlags & CollisionFlags.Below) != 0 && Input.GetAxis("Horizontal") == 0){
				if(Input.GetButtonDown ("Vertical") && (Time.time - lastTime) < 0.4f && Input.GetButtonDown ("Vertical") && (Time.time - lastTime) > 0.1f && Input.GetAxis("Vertical") < -0.03f){
					lastTime = Time.time;
					dir = Vector3.back;
					StartCoroutine(DodgeRoll(dodgeRollSetting.dodgeBack));
				}else
					lastTime = Time.time;
			}
			//Dodge Left
			if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") < 0 && (controller.collisionFlags & CollisionFlags.Below) != 0 && !Input.GetButton("Vertical")){
				if(Input.GetButtonDown ("Horizontal") && (Time.time - lastTime) < 0.3f && Input.GetButtonDown ("Horizontal") && (Time.time - lastTime) > 0.15f && Input.GetAxis("Horizontal") < -0.03f){
					lastTime = Time.time;
					dir = Vector3.left;
					StartCoroutine(DodgeRoll(dodgeRollSetting.dodgeLeft));
				}else
					lastTime = Time.time;
			}
			//Dodge Right
			if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal") > 0 && (controller.collisionFlags & CollisionFlags.Below) != 0 && !Input.GetButton("Vertical")){
				if(Input.GetButtonDown ("Horizontal") && (Time.time - lastTime) < 0.3f && Input.GetButtonDown ("Horizontal") && (Time.time - lastTime) > 0.15f && Input.GetAxis("Horizontal") > 0.03f){
					lastTime = Time.time;
					dir = Vector3.right;
					StartCoroutine(DodgeRoll(dodgeRollSetting.dodgeRight));
				}else
					lastTime = Time.time;
			}
		}
		
		//Cancel Sprint
		if(sprint && Input.GetAxis("Vertical") < 0.02f || sprint && stamina <= 0 || sprint && Input.GetButtonDown("Fire1") || sprint && Input.GetKeyUp(KeyCode.LeftShift)){
			sprint = false;
			recover = true;
			motor.movement.maxForwardSpeed = walkSpeed;
			motor.movement.maxSidewaysSpeed = walkSpeed;
			recoverStamina = 0.0f;
		}
		if(fallingDamage.enable){
			//if(!controller.isGrounded){
			if(!motor.grounded){
				airTime += Time.deltaTime;
			}else{
				yPos = transform.position.y;
			}
		}

		if(airJump){
			//Double Jump
			Vector3 aj = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") , 2 , Input.GetAxis("Vertical")));
			controller.Move(aj * 4 * Time.deltaTime);
			return;
		}
		// 跳跃浮空（左键）优先级最高
		if(!bowHovering){
			// Multi Jump: 二段跳（所有武器），三段跳（所有状态）
			if(Input.GetButtonDown("Jump") && !motor.grounded){
				// 二段跳或三段跳消耗少量体力
				if(stamina > 5f){
					stamina -= 5f;
					if(jumpCount == 0){
						// 二段跳
						StartCoroutine(DoubleJumping());
						jumpCount = 1;
					}else if(jumpCount == 1 && canTripleJump){
						// 三段跳（所有状态）
						StartCoroutine(DoubleJumping());
						jumpCount = 2;
					}
				}
			}
			
			// Ground Crush技能：跳跃后点击右键快速降落到地面（不清空体力条）
			if(!motor.grounded && Input.GetMouseButtonDown(1)){
				// 快速降落到地面
				motor.movement.maxFallSpeed = 50f;
				// 落地时免伤
				ignoreFallDamage = true;
				// 触发技能
				TriggerGroundCrush();
			}
		}
		
		// 瞬移冲刺：双击W键触发
		if(Input.GetKeyDown(KeyCode.W)){
			if(wKeyHeld && Time.time - lastWPressTime < 0.3f && canDash && Time.time - lastDashTime > dashCooldown){
				StartCoroutine(DashForward());
			}
			lastWPressTime = Time.time;
			wKeyHeld = true;
		}
		if(Input.GetKeyUp(KeyCode.W)){
			wKeyHeld = false;
		}
		
		// 瞬移冲刺：Shift+空格触发
		if(canDash && Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.LeftShift) && Time.time - lastDashTime > dashCooldown){
			StartCoroutine(DashForward());
		}
		
		// 瞬移冲刺期间无敌
		if(isDashing){
			GetComponent<StatusC>().immortal = true;
			if(Time.time - lastDashTime > 0.2f){
				isDashing = false;
				GetComponent<StatusC>().immortal = false;
			}
		}

		if(joyStick){
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
				moveHorizontal = Input.GetAxis("Horizontal");
				moveVertical = Input.GetAxis("Vertical");
			}else{
				moveHorizontal = joyStick.position.x;
				moveVertical = joyStick.position.y;
			}
		}else{
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}

		if(motor.grounded){
			if(Input.GetButton("Jump") && !stat.freeze){
				if(fallingDamage.enable){
					airTime = -0.75f;
				}
			}
			// 重置跳跃计数
			jumpCount = 0;
			heavyAttack = false;
			bowHovering = false;
			// 检查是否装备剑（支持三段跳）
			CheckWeaponType();
		}

		//Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		if(!unableToMove){
			Vector3 directionVector = new Vector3(moveHorizontal, 0, moveVertical);

			if(directionVector != Vector3.zero) {
				float directionLength = directionVector.magnitude;
				directionVector = directionVector / directionLength;

				directionLength = Mathf.Min(1, directionLength);

				directionLength = directionLength * directionLength;
				directionVector = directionVector * directionLength;
			}

			// Apply the direction to the CharacterMotor
			motor.inputMoveDirection = transform.rotation * directionVector;
		}

		if(!mobileMode){
			motor.inputJump = Input.GetButton("Jump");
		}else{
			motor.inputJump = mobileJumping;
		}
		
		if(sprint){
			motor.movement.maxForwardSpeed = sprintSpeed;
			motor.movement.maxSidewaysSpeed = sprintSpeed;
			return;
		}
		//Activate Sprint
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && (controller.collisionFlags & CollisionFlags.Below) != 0 && canSprint && stamina > 0){
			sprint = true;
			StartCoroutine(Dasher());
		}
	}
	
	void OnGUI(){
		if(canvasElement.useCanvas){
			return;
		}
		if(sprint || recover || dodging){
			float staminaPercent = stamina * 100 / maxStamina *3;
			//GUI.DrawTexture ( new Rect((Screen.width /2) -150,Screen.height - 120,stamina *3,10), staminaGauge);
			GUI.DrawTexture ( new Rect((Screen.width /2) -150,Screen.height - 120, staminaPercent ,10), staminaGauge);
			GUI.DrawTexture ( new Rect((Screen.width /2) -153,Screen.height - 123, 306 ,16), staminaBorder);
		}
		
	}

	public void MobileJump(){
		mobileJumping = true;
	}
	public void MobileJumpRelease(){
		mobileJumping = false;
	}


	IEnumerator DoubleJumping(){
		airMove = true;
		airJump = true;
		//Double Jump Animation
		if(!useMecanim){
			GetComponent<PlayerAnimationC>().DoubleJumpAnimation();
		}
		motor.freezeGravity = true;
		yield return new WaitForSeconds(0.25f);
		motor.freezeGravity = false;
		airJump = false;
		// 短暂延迟后允许下一次跳跃
		yield return new WaitForSeconds(0.1f);
		airMove = false;
	}
	
	void OnControllerColliderHit(ControllerColliderHit col){
		CharacterController controller = GetComponent<CharacterController>();
		if(airMove && (controller.collisionFlags & CollisionFlags.Below) != 0){
			airMove = false;
			motor.freezeGravity = false;
		}
	}
	
	IEnumerator Dasher(){
		while (sprint){
			yield return new WaitForSeconds(useStamina);
			if(stamina > 0){
				stamina -= 1;
			}else{
				stamina = 0;
			}
		}
	}
	
	// 瞬移冲刺
	IEnumerator DashForward(){
		isDashing = true;
		lastDashTime = Time.time;
		
		Vector3 dashDir = transform.forward;
		dashDir.y = 0;
		dashDir.Normalize();
		
		Vector3 startPos = transform.position;
		Vector3 endPos = transform.position + dashDir * dashDistance;
		endPos.y = transform.position.y;
		
		// 检测冲刺路径上的碰撞
		RaycastHit hit;
		if(Physics.Linecast(startPos, endPos, out hit)){
			// 如果碰撞到东西，停在碰撞点前面一点
			endPos = hit.point - dashDir * 0.5f;
		}
		
		transform.position = endPos;
		
		yield return new WaitForSeconds(0.2f);
		
		isDashing = false;
		GetComponent<StatusC>().immortal = false;
	}
	
	void StaminaRecovery(){
		stamina += 1;
		if(stamina >= maxStamina){
			stamina = maxStamina;
			recoverStamina = 0.0f;
			recover = false;
		}else{
			recoverStamina = staminaRecover - 0.02f;
		}
	}
	
	IEnumerator DodgeRoll(AnimationClip anim){
		if(stamina >= 25 && !dodging && motor.canControl){
			if(!useMecanim){
				//For Legacy Animation
				mainModel.GetComponent<Animation>()[anim.name].layer = 18;
				mainModel.GetComponent<Animation>().PlayQueued(anim.name, QueueMode.PlayNow);
			}else{
				//For Mecanim Animation
				if(GetComponent<PlayerMecanimAnimationC>()){
					GetComponent<PlayerMecanimAnimationC>().AttackAnimation(anim.name);
				}
			}
			dodging = true;
			stamina -= dodgeRollSetting.staminaUse;
			GetComponent<StatusC>().dodge = true;
			motor.canControl = false;
			yield return new WaitForSeconds(0.5f);
			GetComponent<StatusC>().dodge = false;
			recover = true;
			motor.canControl = true;
			dodging = false;
			recoverStamina = 0.0f;
		}
	}

	private float airTime = 0;
	private float yPos = 0;
	public void OnLand(){
		// 重置下落速度为默认值
		motor.movement.maxFallSpeed = motor.movement.defaultMaxFallSpeed;
		
		if(!fallingDamage.enable){
			return;
		}
		//print(airTime);
		StatusC stat = GetComponent<StatusC>();
		// 如果是重击落地，忽略落地伤害
		if(!ignoreFallDamage && airTime > fallingDamage.minSurviveFall && transform.position.y < yPos - fallingDamage.surviveHeight){
			float df = fallingDamage.minSurviveFall / 2;
			float aa = airTime - df;
			float dmg = (float)fallingDamage.damageForSeconds * aa;
			stat.FallingDamage((int)dmg);
			if(fallingDamage.hitEffect){
				Instantiate(fallingDamage.hitEffect , transform.position , fallingDamage.hitEffect.rotation);
			}
		}
		// 重置落地伤害豁免标志
		ignoreFallDamage = false;
		airTime = 0;
	}
	
	// 作弊函数：升级+500金币
	void CheatLevelUp(){
		StatusC stat = GetComponent<StatusC>();
		InventoryC inventory = GetComponent<InventoryC>();
		
		if(stat){
			// 直接升级
			stat.LevelUp(0);
			Debug.Log("=== CHEAT: Level Up! ===");
		}
		
		if(inventory){
			// 增加500金币
			inventory.cash += 500;
			Debug.Log("=== CHEAT: +500 Gold! Total: " + inventory.cash + " ===");
		}
	}
	
	// 检查武器类型
	void CheckWeaponType(){
		// 所有状态下都可以进行三段跳
		canTripleJump = true;
	}
	
	// 跳跃攻击处理
	void HandleJumpAttack(){
		// 跳跃攻击：在空中点击鼠标左键触发滞空（任何武器都可以）
		if(!motor.grounded && Input.GetMouseButtonDown(0)){
			// 滞空效果：静止在空中直到松开鼠标
			bowHovering = true;
		}
		
		// 滞空处理：松开鼠标左键结束滞空
		if(bowHovering && !Input.GetMouseButton(0)){
			bowHovering = false;
			motor.movement.maxFallSpeed = motor.movement.defaultMaxFallSpeed;
			motor.freezeGravity = false;
		}
		
		// 滞空时保持静止（冻结重力和下落速度）
		if(bowHovering){
			motor.movement.maxFallSpeed = 0f;
			motor.freezeGravity = true;
		}
	}
	
	// 单步完成任务击杀（Z键）- 视为完成击杀一个任务目标
	void CompleteQuestKill(){
		QuestStatC questStat = GetComponent<QuestStatC>();
		if(questStat){
			// 找到第一个未完成的任务
			for(int i = 0; i < questStat.questSlot.Length; i++){
				int questId = questStat.questSlot[i];
				if(questId > 0){
					// 增加任务击杀进度
					questStat.questProgress[questId] += 1;
					Debug.Log("=== CHEAT: Quest " + questId + " kill count +1 (" + questStat.questProgress[questId] + ") ===");
					break;
				}
			}
		}
	}
	
	// 完成所有任务
	void CompleteAllQuests(QuestStatC questStat){
		int completedCount = 0;
		
		// 先保存所有任务ID
		int[] questsToComplete = new int[questStat.questSlot.Length];
		for(int i = 0; i < questStat.questSlot.Length; i++){
			questsToComplete[i] = questStat.questSlot[i];
		}
		
		// 使用 Clear 方法完成任务（这会正确地从任务栏移除）
		for(int i = 0; i < questsToComplete.Length; i++){
			int questId = questsToComplete[i];
			if(questId > 0){
				// 设置任务进度为完成
				questStat.questProgress[questId] = 999; // 设置为很高的值确保完成
				// 调用 Clear 方法完成任务
				questStat.Clear(questId);
				completedCount++;
			}
		}
		
		Debug.Log("=== CHEAT: Completed " + completedCount + " quests! ===");
	}
	
	// Ground Crush技能：快速降落到地面并造成伤害
	void TriggerGroundCrush(){
		Debug.Log("=== Ground Crush Triggered! ===");
		// 获取AttackTriggerC组件，触发技能攻击
		AttackTriggerC attackTrigger = GetComponent<AttackTriggerC>();
		if(attackTrigger){
			// 设置重击标记
			attackTrigger.heavyAttack = true;
			// 可以在这里添加技能特效、范围伤害等逻辑
		}
	}
}

[System.Serializable]
public class FallDamage{
	public bool enable = false;
	public float minSurviveFall = 0.45f;
	public int damageForSeconds = 30;
	private CharacterController controller;
	public float surviveHeight = 0.3f;
	public Transform hitEffect;
}
