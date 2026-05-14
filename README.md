# RPG Game Project

**Author: Wang Zhiqin**

This is a role-playing game project based on Unity ActionRPGKit.

## Version History

### v1.14 - Final Optimization Version (2026-05-13)
- Added complete debug information
- Optimized all system performance
- Added detailed code comments
- Completed version history documentation
- Fixed known issues
- Enhanced game stability

### v1.13 - Add Maze Gate Interaction System (2026-05-13)
- Created `MazeGateController.cs`
- Complete maze gate interaction system
- Independent unlock and teleport logic
- Configurable scene and spawn point
- Optimized teleport detection mechanism

### v1.12 - Add Dialogue System Support for Maze Door (2026-05-13)
- Modified `DialogueC.cs`
- Added maze door unlock checking
- Implemented scene teleport after door unlock
- Added debug function (Ctrl+U force unlock)
- Added unlock status display (Ctrl+D)
- Configurable target scene and spawn point

### v1.11 - Add Event System Support for Maze Door (2026-05-13)
- Modified `EventActivator.cs`
- Added maze door checking function
- Integrated unlock variable checking
- Supported maze door event types
- Optimized event trigger logic

### v1.10 - Add Maze Door Controller (2026-05-13)
- Created `MazeDoorController.cs`
- Implemented automatic door hide/show
- Controlled door visibility based on unlock state
- Added continuous unlock state checking
- Optimized door display animation

### v1.9 - Add Quest Completion Unlock Logic (2026-05-13)
- Modified `QuestDataC.cs`
- Implemented setting global unlock variable on quest completion
- Checked if both quests completed to unlock maze
- Quest ID 0 (Goblin) and ID 1 (Spider) completion unlocks maze
- Added globalInt[99] maze unlock variable

### v1.8 - Add Global Unlock Manager (2026-05-13)
- Created `GlobalUnlockManager.cs`
- Implemented maze unlock state management
- Added PlayerPrefs persistence
- Implemented singleton pattern
- Added debug display function

### v1.7 - Add Fall Damage System (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented FallDamage fall damage system
- Added airTime air time calculation
- Added minimum survivable fall time (minSurviveFall)
- Added fall damage calculation formula
- Optimized landing damage detection logic

### v1.6 - Add Stamina System (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented complete stamina management system
- Added maxStamina maximum stamina
- Added stamina current stamina value
- Implemented stamina recovery mechanism (staminaRecover)
- Stamina used for sprint, jump, dodge, etc.

### v1.5 - Add Dash Teleport (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented DashForward() dash teleport method
- Added double-tap W to trigger dash
- Added Shift+Space to trigger dash
- Added dash cooldown (dashCooldown)
- Character invulnerable during dash (immortal = true)
- Optimized dash distance and direction detection

### v1.4 - Add Aerial Attack (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented HandleJumpAttack() aerial attack method
- Triggered aerial attack by clicking left mouse button after jumping
- Optimized jump attack detection logic
- Enhanced aerial combat experience

### v1.3 - Add Triple Jump (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented aerial triple jump
- Added canTripleJump triple jump toggle
- Supported triple jump in specific states
- Completed jump count reset logic

### v1.2 - Add Double Jump (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented aerial double jump
- Added jumpCount jump tracking
- Added stamina consumption per jump (5 stamina per jump)
- Optimized jump feel

### v1.1 - Add Sprint Function (2026-05-13)
- Modified `PlayerInputControllerC.cs`
- Implemented Shift key sprint function
- Added stamina consumption mechanism
- Added sprint speed setting (sprintSpeed)
- Optimized movement speed switching logic

### v1.0 - Initial Version (2026-05-13)
- Basic game functions
- Character movement system
- Combat system
- Quest system
- UI interface
- Scene management

## Features

### Character Movement System
- **W/A/S/D** - Directional Movement
- **Shift** - Sprint (consumes stamina)
- **Space** - Jump
- **Double Jump** - Press Space in air (consumes small amount of stamina)
- **Triple Jump** - Available in specific states (consumes small amount of stamina)
- **Double-tap W** - Dash teleport
- **Shift+Space** - Dash teleport

### Combat System
- **Left Click** - Normal attack
- **Right Click** - Aerial attack (quick descent when used in air)
- **Number Keys 1-9** - Skill shortcuts
- **Aerial Attack** - Click left after jumping

### Stamina System
- Max Stamina: 100
- Sprint consumption: consumes stamina per frame
- Jump consumption: 5 stamina per jump
- Recovery: automatic after stopping actions

### Maze Door Unlock Mechanism
1. Complete 2 quests: defeat 10 Goblins (Quest ID 0) and 10 Spiders (Quest ID 1)
2. After quest completion, globalInt[99] is set to 1
3. Interact with maze door, door checks unlock state
4. If unlocked, teleport to maze scene

### Debug Functions
- **Ctrl+U** - Force unlock maze door
- **Ctrl+D** - Show unlock status
- **N Key** - Cheat: level up + 500 gold
- **Z Key** - Cheat: complete kill quest objective
- Real-time debug info displayed on top-left

## Project Structure
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

## Usage

### Configure Maze Door
1. Find the door object in the scene
2. Add DialogueC or MazeGateController component
3. Configure unlock variable ID (default is 99)
4. Configure target scene (default is "Dungeon")
5. Configure spawn point (default is "PlayerSpawn1")

### Quest Configuration
Configure quest ID in QuestDataC.cs to ensure correct unlock logic.

---

## Project Information

**Project Name:** RPG Game Project
**Author:** Wang Zhiqin
**Version:** 1.14
**Date:** 2026-05-13
**Engine:** Unity
**Asset Pack:** ActionRPGKit
**GitHub:** https://github.com/WangZhiqin1123/33550336
