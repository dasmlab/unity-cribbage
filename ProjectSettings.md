# Unity Project Settings Configuration

## Player Settings

### General Settings:
- **Company Name**: DASMLAB
- **Product Name**: Cribbage Game
- **Version**: 1.0.0

### Other Settings:
- **Active Input Handling**: Input System Package (New)
- **Scripting Backend**: IL2CPP
- **API Compatibility Level**: .NET Standard 2.1

## Input System Settings

### Input Action Asset Configuration:
1. Create "GameInput" asset
2. Add Action Maps:
   - **Gameplay**
   - **UI**

### Gameplay Actions:
- **Click** (Mouse/leftButton)
- **Drag** (Mouse/leftButton)
- **Cancel** (Keyboard/escape)

### UI Actions:
- **Navigate** (Arrow keys)
- **Submit** (Enter/Space)
- **Cancel** (Escape)

## Graphics Settings

### Quality Settings:
- **Anti Aliasing**: 4x MSAA
- **Texture Quality**: Full Res
- **Anisotropic Textures**: Per Texture

### 2D Settings:
- **Sprite Atlas**: Enabled
- **Sprite Packer Mode**: Always Enabled

## Audio Settings

### Audio Configuration:
- **System Sample Rate**: 48000 Hz
- **DSP Buffer Size**: Best Performance
- **Virtual Voices**: 32
- **Real Voices**: 24

## Physics Settings

### 2D Physics:
- **Default Material**: Default
- **Gravity**: (0, -9.81, 0)
- **Velocity Iterations**: 8
- **Position Iterations**: 3

## Tags and Layers

### Tags:
- Card
- Peg
- UI
- Background

### Layers:
- Default
- UI
- Cards
- Board

## Build Settings

### Scenes in Build:
1. GameScene

### Platform Settings:
- **Target Platform**: PC, Mac & Linux Standalone
- **Architecture**: x86_64
- **Compression Method**: LZ4HC

## Package Manager

### Required Packages:
- **Input System**: 1.6.3
- **TextMeshPro**: 3.0.6
- **2D Sprite**: 1.0.0
- **2D Animation**: 9.0.5

## Editor Settings

### General:
- **Auto Save**: Enabled
- **Asset Serialization**: Force Text
- **Version Control**: Visible Meta Files

### 2D:
- **Sprite Packer**: Enabled
- **Sprite Atlas**: Enabled 