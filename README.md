# RPG 游戏项目

这是一个基于 Unity ActionRPGKit 的角色扮演游戏项目。

## 版本历史

### v1.0 - 初始版本 (2026-05-13)
- 基础游戏功能
- 角色移动系统
- 战斗系统
- 任务系统
- UI界面
- 场景管理

### v1.1 - 添加全局解锁管理器 (2026-05-13)
- 创建 `GlobalUnlockManager.cs`
- 实现迷宫解锁状态管理
- 添加调试显示功能
- 集成全局解锁机制

### v1.2 - 任务完成解锁逻辑 (2026-05-13)
- 修改 `QuestDataC.cs`
- 实现任务完成时设置全局解锁变量
- 检查两个任务完成后解锁迷宫
- 任务ID 0（哥布林）和 ID 1（蜘蛛）完成后解锁

### v1.3 - 迷宫门控制器 (2026-05-13)
- 创建 `MazeDoorController.cs`
- 实现门的自动隐藏/显示
- 根据解锁状态控制门的可见性
- 添加持续检查解锁状态功能

### v1.4 - 事件系统支持迷宫门 (2026-05-13)
- 修改 `EventActivator.cs`
- 添加迷宫门检查功能
- 集成解锁变量检查
- 支持迷宫门事件类型

### v1.5 - 对话系统支持迷宫门 (2026-05-13)
- 修改 `DialogueC.cs`
- 添加迷宫门解锁检查
- 实现门解锁后的场景传送
- 添加调试功能（Ctrl+U 强制解锁）
- 添加解锁状态显示（Ctrl+D）

### v1.6 - 迷宫门控制器 (2026-05-13)
- 创建 `MazeGateController.cs`
- 完整的迷宫门交互系统
- 独立的解锁和传送逻辑
- 可配置的场景和传送点

### v1.7 - 最终优化版本 (2026-05-13)
- 添加完整的调试信息
- 优化解锁逻辑
- 添加详细的代码注释
- 完善版本记录文档

## 功能说明

### 迷宫门解锁机制
1. 完成两个任务：击败10只哥布林（任务ID 0）和击败10只蜘蛛（任务ID 1）
2. 任务完成后，`globalInt[99]` 会被设置为1，表示迷宫已解锁
3. 与迷宫门交互，门会检查解锁状态
4. 如果已解锁，传送到迷宫场景

### 调试功能
- `Ctrl+U` - 强制解锁迷宫门
- `Ctrl+D` - 显示解锁状态信息
- 屏幕左上角会显示实时调试信息

## 项目结构
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

## 使用说明

### 配置迷宫门
1. 在场景中找到门对象
2. 添加 `DialogueC` 组件或 `MazeGateController` 组件
3. 配置解锁变量ID（默认为99）
4. 配置目标场景名称（默认为 "Dungeon"）
5. 配置传送点名称（默认为 "PlayerSpawn1"）

### 任务配置
在 `QuestDataC.cs` 中配置任务ID，确保正确设置解锁逻辑。

## Git 使用

### 查看版本历史
```bash
git log --oneline --graph
```

### 查看特定版本
```bash
git checkout v1.0  # 查看v1.0版本
git checkout master  # 返回最新版本
```

### 推送代码到GitHub
1. 在GitHub创建仓库
2. 添加远程仓库
3. 推送代码和标签
```bash
git remote add origin <你的仓库URL>
git push -u origin master
git push origin --tags
```

## 开发工具
- Unity 2022
- C#
- Git

## 作者
RPG 游戏项目开发团队
