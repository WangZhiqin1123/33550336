=============================
新增游戏功能使用指南
=============================

1. 击杀奖励系统 (KillRewardC)
=============================
- 添加到敌人对象上
- 配置项：
  * goldReward - 击杀获得的金币
  * expBonus - 击杀获得的经验
  * expMultiplier - 经验倍率
  * specialDrops - 特殊掉落物品数组（带概率）
  * killEffect - 击杀特效
  * killSound - 击杀音效

2. 击杀统计系统 (KillStatsC)
=============================
- 添加到玩家对象上
- 自动记录击杀数据
- 配置连击奖励参数

3. 元素克制系统 (ElementSystemC)
=============================
- 创建空对象，添加此组件
- 预设了9种元素和克制关系：
  * 火克冰、风
  * 冰克火、土
  * 土克雷、风
  * 雷克水、土
  * 水克火、雷
  * 风光克暗、暗克光
- 在StatusC中设置角色元素属性
- 在BulletStatusC中设置攻击元素

4. 装备锻造系统 (ForgeSystemC)
=============================
- 添加到锻造台/铁匠NPC上
- 配置锻造配方
- 支持成功率、材料消耗

5. 连击计数器 (ComboCounterC)
=============================
- 添加到玩家对象上
- 连击超时、奖励倍率
- 连击里程碑：5/10/15/20/30连击
- 每次命中敌人时调用AddHit()

6. 技能连击系统 (SkillComboSystemC)
=============================
- 添加到玩家对象上
- 配置技能连击序列
- 支持按键组合释放强化技能

7. 成就系统 (AchievementSystemC)
=============================
- 持久化存储
- 成就解锁通知
- 奖励金币和经验

8. 任务系统 (QuestSystemC)
=============================
- 添加到NPC/任务NPC上
- 追踪击杀目标
- 自动更新任务进度

使用示例：
=============================
1. 玩家对象组件：
   - KillStatsC
   - ComboCounterC
   - SkillComboSystemC
   - QuestSystemC

2. 敌人对象组件：
   - KillRewardC

3. 全局对象：
   - ElementSystemC
   - AchievementSystemC

注意事项：
=============================
- 确保ElementSystemC是单例（DontDestroyOnLoad）
- KillRewardC需要配合StatusC使用
- ComboCounterC需要在每次命中时调用AddHit()
