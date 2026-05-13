# 游戏优化说明

已添加的优化功能：

---

## 1. 连击显示UI (ComboDisplayC)
- 连续攻击时显示连击数
- 显示伤害加成提示
- 2秒不攻击后自动重置
- 显示位置：屏幕中央偏上

## 2. 存档通知 (SaveNotificationC)
- 存档成功后显示提示
- 2秒后自动消失
- 显示位置：屏幕下方中央

## 3. 任务追踪UI (QuestTrackerC)
- 显示当前任务列表
- 显示任务进度提示
- 固定在屏幕左上角

## 4. 装备对比UI (EquipmentCompareC)
- 对比当前装备与新装备
- 显示属性差异（绿字提升，红字下降）
- 按空格键打开/关闭

## 5. 敌人血量条 (EnemyHealthBarC)
- 在敌人头顶显示血量条
- 根据血量显示不同颜色
- 超过一定距离自动隐藏

---

## 使用方法

### 在Unity中设置：

1. **打开项目**，进入游戏场景
2. **选择玩家对象**（Player）
3. **在Inspector中点击"Add Component"**
4. **搜索并添加需要的组件：**
   - `ComboDisplayC` (连击显示)
   - `SaveNotificationC` (存档通知)
   - `QuestTrackerC` (任务追踪)
   - `EquipmentCompareC` (装备对比)

5. **选择敌人对象**（Enemy）
6. **添加组件：**
   - `EnemyHealthBarC` (敌人血量条)

### 开始游戏：

1. 点击Play按钮
2. 开始游戏
3. 连续攻击敌人 → 看到连击UI！
4. 按ESC→Save Game → 看到存档提示！
5. 屏幕左上角显示任务追踪！
6. 按空格键 → 打开装备对比！
7. 靠近敌人 → 看到敌人血量条！

---

## 自定义设置

所有组件都可以在Inspector中调整参数：
- 显示位置、颜色、字体大小等
- 可以通过勾选/取消勾选启用/禁用功能

---

## 文件说明

新增文件：
- `ComboDisplayC.cs` - 连击显示
- `SaveNotificationC.cs` - 存档通知
- `QuestTrackerC.cs` - 任务追踪
- `EquipmentCompareC.cs` - 装备对比
- `EnemyHealthBarC.cs` - 敌人血量条
- `OptimizationGuide.md` - 说明文档

修改文件：
- `AttackTriggerC.cs` - 攻击时调用连击显示
- `SaveLoadC.cs` - 存档时显示通知

所有修改都很安全，不会影响原始游戏功能！
