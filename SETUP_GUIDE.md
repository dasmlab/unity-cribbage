# Unity Cribbage Game Setup Guide (Unity 6.1+)

## Prerequisites

1. **Unity 6.1** installed
2. **Go backend server** running on `localhost:8001`
3. **TextMeshPro** package (included with Unity 6.1)

## Step 1: Create New Unity Project

1. Open Unity Hub
2. Click "New Project"
3. Select "2D Core" template
4. Name it "CribbageGame"
5. Choose location and create project

## Step 2: Input System Setup (Unity 6.1+)

1. In the Project window, locate the default `InputSystem_Actions.inputactions` asset in `Assets/`.
2. Double-click to open it in the Input Actions editor.
3. Under the **Player** action map, add these actions:
   - **Click**: Action Type = Button, Binding = Mouse/Left Button
   - **Drag**: Action Type = Button, Binding = Mouse/Left Button (can use interaction/hold if desired)
   - **Cancel**: Action Type = Button, Binding = Keyboard/Escape
4. Save the asset. Unity will auto-generate a C# class called `InputSystem_Actions`.
5. (Optional) Under the **UI** action map, ensure you have standard UI navigation actions.

## Step 3: Import Project Structure

1. Copy all scripts from `Assets/Scripts/` to your Unity project.
2. Create the following folder structure in your Unity project:
   ```
   Assets/
   ├── Scripts/
   │   ├── Networking/
   │   ├── Game/
   │   ├── UI/
   │   └── Utils/
   ├── Prefabs/
   ├── Sprites/
   └── Scenes/
   ```

## Step 4: Configure InputManager Script

1. The `InputManager` script now uses the generated `InputSystem_Actions` class.
2. No need to assign an InputActionAsset in the Inspector.
3. The script will subscribe to `Player/Click`, `Player/Drag`, and `Player/Cancel` actions in `OnEnable` and unsubscribe in `OnDisable`.
4. All input handling is routed through the generated class.

## Step 5: Create Main Scene

1. Create a new scene: **File > New Scene**
2. Save as "GameScene"

### Scene Hierarchy Setup:

```
GameScene
├── GameManager
├── NetworkManager
├── InputManager
├── Canvas (UI)
│   ├── TopPanel (Remote Player Hand)
│   ├── BottomPanel (Current Player Hand)
│   ├── CenterPanel (Cribbage Board)
│   └── RightPanel (Controls)
└── Main Camera
```

## Step 6: Create UI Layout

### Canvas Setup:
1. Create Canvas: **GameObject > UI > Canvas**
2. Set Canvas Scaler to "Scale With Screen Size"
3. Reference resolution: 1920x1080

### Panel Layout:
1. **TopPanel** (Remote Player):
   - Anchor: Top
   - Height: 200px
   - Add Horizontal Layout Group for cards

2. **BottomPanel** (Current Player):
   - Anchor: Bottom
   - Height: 200px
   - Add Horizontal Layout Group for cards

3. **CenterPanel** (Cribbage Board):
   - Anchor: Center
   - Size: 400x300px
   - Add CribbageBoard component

4. **RightPanel** (Controls):
   - Anchor: Right
   - Width: 300px
   - Add buttons: Deal, Reset
   - Add status text

## Step 7: Create Card Prefab

1. Create empty GameObject
2. Add **Image** component (card background)
3. Add **CardDisplay** script
4. Add child objects:
   - **RankText** (TextMeshPro)
   - **SuitText** (TextMeshPro)
   - **CardBack** (Image)
5. Configure CardDisplay component
6. Create Prefab from GameObject

## Step 8: Create Peg Prefab

1. Create empty GameObject
2. Add **Image** component (peg sprite)
3. Add **Rigidbody2D** (if needed for physics)
4. Create Prefab from GameObject

## Step 9: Configure Components

### GameManager:
- Assign references to UIManager, CribbageBoard
- Set status update interval

### UIManager:
- Assign card prefab
- Assign hand containers (TopPanel, BottomPanel)
- Assign UI text elements
- Assign buttons

### CribbageBoard:
- Assign peg prefab
- Assign player tracks
- Assign score text elements

### InputManager:
- No need to assign InputActionAsset. The script uses the generated class.

## Step 10: Test Backend Connection

1. Ensure Go server is running:
   ```bash
   cd card-suite/main-app
   go run main.go
   ```

2. Test API endpoints:
   - `GET http://localhost:8001/deal`
   - `GET http://localhost:8001/status`
   - `POST http://localhost:8001/reset`

## Step 11: Run and Test

1. Press Play in Unity
2. Click "Deal" button
3. Verify cards appear in correct positions
4. Test card dragging
5. Verify score updates on cribbage board

## Troubleshooting

### Common Issues:

1. **Script compilation errors**: Ensure all scripts are in correct namespaces
2. **Missing references**: Check all SerializeField assignments in Inspector
3. **Input System errors**: Make sure InputSystem_Actions is generated and up to date
4. **Network errors**: Check server URL and CORS settings
5. **UI layout issues**: Verify Canvas Scaler settings

### Debug Tips:

1. Check Console for error messages
2. Use Debug.Log statements in scripts
3. Verify API responses in Network tab
4. Test UI interactions in Scene view

## Next Steps

1. **Add card sprites** for better visuals
2. **Implement drag-and-drop** for card interactions
3. **Add sound effects** for card movements
4. **Create animations** for scoring
5. **Add multiplayer** features
6. **Implement game rules** and validation 