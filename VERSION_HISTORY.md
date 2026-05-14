# 版本历史记录

**Author: Wang Zhiqin**

---

## v1.0 - 初始版本 (Initial Version)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 基础游戏功能 (Basic Game Functions)
- 角色移动系统 (Character Movement System)
- 战斗系统 (Combat System)
- 任务系统 (Quest System)
- UI界面 (UI Interface)
- 场景管理 (Scene Management)

---

## v1.1 - 添加冲刺功能 (Add Sprint Function)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现 Shift 键冲刺功能 (Implement Shift key sprint function)
- 添加耐力消耗机制 (Add stamina consumption mechanism)
- 添加冲刺速度设置（sprintSpeed）(Add sprint speed setting)
- 优化移动速度切换逻辑 (Optimize movement speed switching logic)

---

## v1.2 - 添加二段跳功能 (Add Double Jump)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现空中二段跳功能 (Implement aerial double jump)
- 添加 jumpCount 跳跃计数 (Add jump count tracking)
- 添加体力消耗（每次跳跃消耗5点体力）(Add stamina consumption per jump)
- 优化跳跃手感 (Optimize jump feel)

---

## v1.3 - 添加三段跳功能 (Add Triple Jump)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现空中三段跳功能 (Implement aerial triple jump)
- 添加 canTripleJump 三段跳开关 (Add triple jump toggle)
- 支持特定状态下三段跳 (Support triple jump in specific states)
- 完善跳跃计数重置逻辑 (Complete jump count reset logic)

---

## v1.4 - 添加浮空攻击功能 (Add Aerial Attack)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现 HandleJumpAttack() 浮空攻击方法 (Implement aerial attack method)
- 跳跃后点击左键触发浮空攻击 (Trigger aerial attack by clicking left mouse button after jumping)
- 优化跳跃攻击判定逻辑 (Optimize jump attack detection logic)
- 增强空中战斗体验 (Enhance aerial combat experience)

---

## v1.5 - 添加瞬移冲刺功能 (Add Dash Teleport)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现 DashForward() 瞬移冲刺方法 (Implement dash teleport method)
- 添加双击W键触发冲刺 (Add double-tap W to trigger dash)
- 添加 Shift+空格 触发冲刺 (Add Shift+Space to trigger dash)
- 添加冲刺冷却时间（dashCooldown）(Add dash cooldown)
- 瞬移期间角色无敌（immortal = true）(Character invulnerable during dash)
- 优化冲刺距离和方向判定 (Optimize dash distance and direction detection)

---

## v1.6 - 添加耐力系统 (Add Stamina System)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现完整的耐力管理系统 (Implement complete stamina management system)
- 添加 maxStamina 最大耐力 (Add max stamina)
- 添加 stamina 耐力值 (Add current stamina)
- 实现耐力恢复机制（staminaRecover）(Implement stamina recovery mechanism)
- 耐力用于冲刺、跳跃、闪避等动作 (Stamina used for sprint, jump, dodge, etc.)

---

## v1.7 - 添加坠落伤害系统 (Add Fall Damage System)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `PlayerInputControllerC.cs`
- 实现 FallDamage 坠落伤害系统 (Implement fall damage system)
- 添加 airTime 空中时间计算 (Add air time calculation)
- 添加最小存活坠落时间（minSurviveFall）(Add minimum survivable fall time)
- 添加坠落伤害计算公式 (Add fall damage calculation formula)
- 优化落地伤害判定逻辑 (Optimize landing damage detection logic)

---

## v1.8 - 添加全局解锁管理器 (Add Global Unlock Manager)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 创建 `GlobalUnlockManager.cs`
- 实现迷宫解锁状态管理 (Implement maze unlock state management)
- 添加 PlayerPrefs 持久化存储 (Add PlayerPrefs persistence)
- 实现单例模式 (Implement singleton pattern)
- 添加调试显示功能 (Add debug display function)

---

## v1.9 - 添加任务完成解锁逻辑 (Add Quest Completion Unlock Logic)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `QuestDataC.cs`
- 实现任务完成时设置全局解锁变量 (Set global unlock variable on quest completion)
- 检查两个任务完成后解锁迷宫 (Check if both quests completed to unlock maze)
- 任务ID 0（哥布林）和 ID 1（蜘蛛）完成后解锁 (Quest ID 0 Goblin and ID 1 Spider completion unlocks)
- 添加 globalInt[99] 迷宫解锁变量 (Add globalInt[99] maze unlock variable)

---

## v1.10 - 添加迷宫门控制器 (Add Maze Door Controller)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 创建 `MazeDoorController.cs`
- 实现门的自动隐藏/显示 (Implement automatic door hide/show)
- 根据解锁状态控制门的可见性 (Control door visibility based on unlock state)
- 添加持续检查解锁状态功能 (Add continuous unlock state checking)
- 优化门的显示动画 (Optimize door display animation)

---

## v1.11 - 添加事件系统支持迷宫门 (Add Event System Support for Maze Door)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `EventActivator.cs`
- 添加迷宫门检查功能 (Add maze door checking function)
- 集成解锁变量检查 (Integrate unlock variable checking)
- 支持迷宫门事件类型 (Support maze door event types)
- 优化事件触发逻辑 (Optimize event trigger logic)

---

## v1.12 - 添加对话系统支持迷宫门 (Add Dialogue System Support for Maze Door)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 修改 `DialogueC.cs`
- 添加迷宫门解锁检查 (Add maze door unlock checking)
- 实现门解锁后的场景传送 (Implement scene teleport after door unlock)
- 添加调试功能（Ctrl+U 强制解锁）(Add debug function: Ctrl+U force unlock)
- 添加解锁状态显示（Ctrl+D）(Add unlock status display: Ctrl+D)
- 可配置的目标场景和传送点 (Configurable target scene and spawn point)

---

## v1.13 - 添加迷宫门交互系统 (Add Maze Gate Interaction System)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 创建 `MazeGateController.cs`
- 完整的迷宫门交互系统 (Complete maze gate interaction system)
- 独立的解锁和传送逻辑 (Independent unlock and teleport logic)
- 可配置的场景和传送点 (Configurable scene and spawn point)
- 优化传送判定机制 (Optimize teleport detection mechanism)

---

## v1.14 - 最终优化版本 (Final Optimization Version)
**Date: 2026-05-13**
**Author: Wang Zhiqin**

- 添加完整的调试信息 (Add complete debug information)
- 优化所有系统性能 (Optimize all system performance)
- 添加详细的代码注释 (Add detailed code comments)
- 完善版本记录文档 (Complete version history documentation)
- 修复已知问题 (Fix known issues)
- 增强游戏稳定性 (Enhance game stability)

---

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

---

## 项目信息 (Project Information)

**项目名称 (Project Name):** RPG 游戏项目 (RPG Game Project)
**作者 (Author):** Wang Zhiqin
**版本 (Version):** 1.14
**日期 (Date):** 2026-05-13
**游戏引擎 (Engine):** Unity
**素材包 (Asset Pack):** ActionRPGKit
