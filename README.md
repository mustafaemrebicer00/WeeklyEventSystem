# 🗓️ Weekly Event System

A modular, time-limited event framework for Unity games. Designed to support multiple weekly events (e.g. Star Event, Blaze Event) with dynamic difficulty, persistent progress, and full UI integration. Built using ScriptableObjects for flexible configuration and mobile-friendly architecture.



## 🧠 Features

- **Weekly Scheduling**
  - Star Event → Monday to Wednesday
  - Blaze Event → Thursday to Sunday
  - Events activate automatically based on system date (`System.DateTime.Now.DayOfWeek`)

- **ScriptableObject-Based Configuration**
  Each event is defined via `EventData.asset` with:
  `eventName`, `eventIcon`, `goal`, `rewardAmount`, `eventWords`, `startDayOfWeek`, `endDayOfWeek`

- **Dynamic Star Distribution**
  - 3 stars per tile if progress < 25%
  - 2 stars if progress between 25–75%
  - 1 star if progress > 75%

- **Progress Persistence**
  - Stars saved per event using `PlayerPrefs`
  - Progress resets with each new event cycle

- **UI Integration**
  - Event popup, progress bar, reward screen, quit warning
  - Responsive layout for mobile screens

- **Soft Currency Reward System**
  - Completing an event grants configurable currency

- **Debug Tools**
  - Manual date override
  - Progress simulation
  - Event switching



## 🧱 Architecture & Design Reasoning

### 🔹 Core Scripts

| Script                     | Purpose                                           |
|---------------------------|---------------------------------------------------|
| `EventData.cs`            | Defines event parameters via ScriptableObject     |
| `EventManager.cs`         | Selects active event based on system date         |
| `EventUIController.cs`    | Binds event data to UI                            |
| `WordValidator.cs`        | Handles word input, validation, and star logic    |
| `HintPanelController.cs`  | Displays masked hints and marks found words       |
| `StarSpawner.cs`          | Spawns tiles with adaptive star count             |
| `CurrencyManager.cs`      | Manages soft currency                             |
| `LevelQuitPopupController.cs` | Handles quit confirmation                    |
| `DebugPanel.cs`           | Developer tool for testing progress and switching |

### 🔹 Design Principles

- **Modularity**: All systems are prefab-compatible and decoupled
- **Scalability**: Easily extendable to support new event types
- **Responsiveness**: UI scales across devices
- **Maintainability**: Clear separation of concerns and naming conventions
- **Edge Case Handling**: Expired events mid-level, quit loss warnings, invalid configs gracefully skipped



## 🖼️ UI Components

- Event Popup (title, icon, progress bar)
- Hint Panel with masked words
- Tile Grid with star indicators
- Result Text for feedback
- Level End Popup with reward
- Quit Confirmation Popup
- Debug Panel for testing



## 🧪 Testing Strategy

- Star Event activates Monday–Wednesday
- Blaze Event activates Thursday–Sunday
- Progress persists across sessions
- Word validation blocks duplicates
- UI updates dynamically
- Debug panel reflects system date and progress override
- Edge case: event expiration mid-level → stars still granted if level completed



## 📁 Folder Structure
Assets/ ├── Scripts/ │   ├── EventSystem/ │   ├── GameLogic/ │   └── UI/ ├── Resources/ │   └── Events/ ├── Prefabs/ ├── Scenes/ ├── Art/ ├── Settings/




## 🚀 Setup Instructions

1. Create `StarEvent.asset` and `BlazeEvent.asset` under `Resources/Events/`
2. Assign values: name, icon, goal, reward, word list, start/end days
3. Add `EventManager` to scene and link both events
4. Add `EventUIController` and bind UI elements
5. Add `WordValidator`, `HintPanelController`, and other systems
6. Test using `DebugPanel` or real-time system date



## 📌 Notes

- Unity version: **2021.3.42f** (required)
- All functionality is local to Unity client—no external APIs
- Tweening libraries like DOTween are supported
- Designed for mobile scaling and prefab reuse
- All code is written in C# and fully explainable



## 📞 Contact

Developed by **Mustafa Emre Biçer**  
Role: Indie Game Developer, Technical Architect  
Location: Istanbul, Turkey  
Email: mustafa1999bicer1907fb@gmail.com  
GitHub: [github.com/mustafaemrebicer00](https://github.com/mustafaemrebicer00)
