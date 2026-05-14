# RPG 游戏项目 (RPG Game Project)

**Author: Wang Zhiqin**

这是一个基于 Unity ActionRPGKit 的角色扮演游戏项目。

## 版本历史 (Version History)

### v1.0 - 初始版本 (Initial Version) (2026-05-13)
- 基础游戏功能
- 角色移动系统
- 战斗系统
- 任务系统
- UI界面
- 场景管理

### v1.1 - 添加冲刺功能 (Add Sprint Function) (2026-05-13)
- 实现 Shift 键冲刺功能
- 添加耐力消耗机制
- 添加冲刺速度设置（sprintSpeed）
- 优化移动速度切换逻辑

### v1.2 - 添加二段跳功能 (Add Double Jump) (2026-05-13)
- 实现空中二段跳功能
- 添加 jumpCount 跳跃计数
- 添加体力消耗（每次跳跃消耗5点体力）
- 优化跳跃手感

### v1.3 - 添加三段跳功能 (Add Triple Jump) (2026-05-13)
- 实现空中三段跳功能
- 添加 canTripleJump 三段跳开关
- 支持特定状态下三段跳
- 完善跳跃计数重置逻辑

### v1.4 - 添加浮空攻击功能 (Add Aerial Attack) (2026-05-13)
- 实现 HandleJumpAttack() 浮空攻击方法
- 跳跃后点击左键触发浮空攻击
- 优化跳跃攻击判定逻辑
- 增强空中战斗体验

### v1.5 - 添加瞬移冲刺功能 (Add Dash Teleport) (2026-05-13)
- 实现 DashForward() 瞬移冲刺方法
- 添加双击W键触发冲刺
- 添加 Shift+空格 触发冲刺
- 添加冲刺冷却时间（dashCooldown）
- 瞬移期间角色无敌（immortal = true）
- 优化冲刺距离和方向判定

### v1.6 - 添加耐力系统 (Add Stamina System) (2026-05-13)
- 实现完整的耐力管理系统
- 添加 maxStamina 最大耐力
- 添加 stamina 耐力值
- 实现耐力恢复机制（staminaRecover）
- 耐力用于冲刺、跳跃、闪避等动作

### v1.7 - 添加坠落伤害系统 (Add Fall Damage System) (2026-05-13)
- 实现 FallDamage 坠落伤害系统
- 添加 airTime 空中时间计算
- 添加最小存活坠落时间（minSurviveFall）
- 添加坠落伤害计算公式
- 优化落地伤害判定逻辑

### v1.8 - 添加全局解锁管理器 (Add Global Unlock Manager) (2026-05-13)
- 创建 `GlobalUnlockManager.cs`
- 实现迷宫解锁状态管理
- 添加 PlayerPrefs 持久化存储
- 实现单例模式
- 添加调试显示功能

### v1.9 - 添加任务完成解锁逻辑 (Add Quest Completion Unlock Logic) (2026-05-13)
- 修改 `QuestDataC.cs`
- 实现任务完成时设置全局解锁变量
- 检查两个任务完成后解锁迷宫
- 任务ID 0（哥布林）和 ID 1（蜘蛛）完成后解锁
- 添加 globalInt[99] 迷宫解锁变量

### v1.10 - 添加迷宫门控制器 (Add Maze Door Controller) (2026-05-13)
- 创建 `MazeDoorController.cs`
- 实现门的自动隐藏/显示
- 根据解锁状态控制门的可见性
- 添加持续检查解锁状态功能
- 优化门的显示动画

### v1.11 - 添加事件系统支持迷宫门 (Add Event System Support for Maze Door) (2026-05-13)
- 修改 `EventActivator.cs`
- 添加迷宫门检查功能
- 集成解锁变量检查
- 支持迷宫门事件类型
- 优化事件触发逻辑

### v1.12 - 添加对话系统支持迷宫门 (Add Dialogue System Support for Maze Door) (2026-05-13)
- 修改 `DialogueC.cs`
- 添加迷宫门解锁检查
- 实现门解锁后的场景传送
- 添加调试功能（Ctrl+U 强制解锁）
- 添加解锁状态显示（Ctrl+D）
- 可配置的目标场景和传送点

### v1.13 - 添加迷宫门交互系统 (Add Maze Gate Interaction System) (2026-05-13)
- 创建 `MazeGateController.cs`
- 完整的迷宫门交互系统
- 独立的解锁和传送逻辑
- 可配置的场景和传送点
- 优化传送判定机制

### v1.14 - 最终优化版本 (Final Optimization Version) (2026-05-13)
- 添加完整的调试信息
- 优化所有系统性能
- 添加详细的代码注释
- 完善版本记录文档
- 修复已知问题
- 增强游戏稳定性

## 功能说明 (Features)

### 角色移动系统 (Character Movement System)
- **W/A/S/D** - 方向移动 (Directional Movement)
- **Shift** - 冲刺（消耗耐力）(Sprint - consumes stamina)
- **空格** - 跳跃 (Jump)
- **二段跳** - 空中再按空格（消耗少量耐力）(Double Jump - press Space in air)
- **三段跳** - 特定状态下可实现（消耗少量耐力）(Triple Jump - in specific states)
- **双击W** - 瞬移冲刺 (Double-tap W - dash teleport)
- **Shift+空格** - 瞬移冲刺 (Shift+Space - dash teleport)

### 战斗系统 (Combat System)
- **左键** - 普通攻击 (Left Click - normal attack)
- **右键** - 浮空攻击（空中使用时快速降落）(Right Click - aerial attack)
- **1-9数字键** - 技能快捷键 (Number Keys 1-9 - skill shortcuts)
- **浮空攻击** - 跳跃后点击左键触发 (Aerial Attack - click left after jumping)

### 耐力系统 (Stamina System)
- 耐力最大值：100点 (Max Stamina: 100)
- 冲刺消耗：每帧消耗耐力 (Sprint consumption: consumes stamina per frame)
- 跳跃消耗：每次跳跃消耗5点耐力 (Jump consumption: 5 stamina per jump)
- 恢复机制：停止动作后自动恢复 (Recovery: automatic after stopping actions)

### 迷宫门解锁机制 (Maze Door Unlock Mechanism)
1. 完成两个任务：击败10只哥布林（任务ID 0）和击败10只蜘蛛（任务ID 1）(Complete 2 quests: defeat 10 Goblins (Quest ID 0) and 10 Spiders (Quest ID 1))
2. 任务完成后，`globalInt[99]` 会被设置为1，表示迷宫已解锁 (After quest completion, globalInt[99] is set to 1)
3. 与迷宫门交互，门会检查解锁状态 (Interact with maze door, door checks unlock state)
4. 如果已解锁，传送到迷宫场景 (If unlocked, teleport to maze scene)

### 调试功能 (Debug Functions)
- **Ctrl+U** - 强制解锁迷宫门 (Force unlock maze door)
- **Ctrl+D** - 显示解锁状态信息 (Show unlock status)
- **N键** - 作弊升级（+500金币）(Cheat: level up + 500 gold)
- **Z键** - 作弊完成击杀任务目标 (Cheat: complete kill quest objective)
- 屏幕左上角会显示实时调试信息 (Real-time debug info displayed on top-left)

## 项目结构 (Project Structure)
```
Assets/
├── ActionRPGKit/
│   ├── Scripts/
│   │   ├── CanvasUI/
│   │   ├── EventSystemScript/
│   │   ├── QuestSystem/
│   │   ├── GlobalUnlockManager.cs
│   │   ├── MazeDoorController.cs
│   │   ├── MazeGateController.cs
│   │   ├── DialogueC.cs
│   │   ├── EventActivator.cs
│   │   └── ...
│   └── ...
└── ...
```

## 使用说明 (Usage)

### 配置迷宫门 (Configure Maze Door)
1. 在场景中找到门对象 (Find the door object in the scene)
2. 添加 `DialogueC` 组件或 `MazeGateController` 组件 (Add DialogueC or MazeGateController component)
3. 配置解锁变量ID（默认为99）(Configure unlock variable ID, default is 99)
4. 配置目标场景名称（默认为 "Dungeon"）(Configure target scene, default is "Dungeon")
5. 配置传送点名称（默认为 "PlayerSpawn1"）(Configure spawn point, default is "PlayerSpawn1")

### 任务配置 (Quest Configuration)
在 `QuestDataC.cs` 中配置任务ID，确保正确设置解锁逻辑。

---

## 项目信息 (Project Information)

**项目名称 (Project Name):** RPG 游戏项目 (RPG Game Project)
**作者 (Author):** Wang Zhiqin
**版本 (Version):** 1.14
**日期 (Date):** 2026-05-13
**游戏引擎 (Engine):** Unity
**素材包 (Asset Pack):** ActionRPGKit
**GitHub:** https://github.com/WangZhiqin1123/33550336
