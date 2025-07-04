# Unity Cribbage Game

A 2D multiplayer Cribbage game built with Unity 6.1 and the new Input System.

## Project Structure

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

## Backend Integration

This Unity project connects to the Go-based Cribbage server running on `localhost:8001`.

### API Endpoints
- `GET /deal` - Deal new hands
- `GET /status` - Get current game state
- `POST /reset` - Reset game

## Setup Instructions

1. Create new Unity 6.1 project
2. Import this project structure
3. Configure the new Input System
4. Set up networking components
5. Create UI layout for 2D card game

## Game Layout

- **Top**: Remote player's hand (cards face down)
- **Bottom**: Current player's hand (cards visible)
- **Center**: Cribbage board with animated pegs
- **Right**: Game status and controls 