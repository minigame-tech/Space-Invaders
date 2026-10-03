# 👾 Space Invaders: C# Remake

![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Platforms: Windows | Linux](https://img.shields.io/badge/platforms-Windows%20%7C%20Linux-lightgrey.svg)
![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)
![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)

A remake of the classic arcade game **Space Invaders**, developed entirely in **C#** on **.NET**.
The project uses **Raylib-cs**, the C# binding for [raylib](https://www.raylib.com/), to handle rendering, input and audio, with a clean separation between game logic and presentation.

> 🚧 **Work in progress**: the player ship is already playable (movement and shooting). Invaders, shields and scoring are on the way. See the [Roadmap](#-roadmap).

<!--
TIPS: Add a gameplay screenshot or GIF here!
![Gameplay Screenshot](docs/gameplay-preview.png)
-->

## 🚀 Key Features

*   **Classic Gameplay**: Mechanics inspired by the original arcade version: a ship at the bottom of the screen, bullets with a firing cooldown and a limited number of lives.
*   **Frame-rate Independent**: Movement and timers are based on delta time, so the game feels the same on any machine.
*   **Clean Architecture**: Game logic (`Core`) is kept separate from rendering and input (`Game`), which makes it easier to maintain and to unit test.
*   **Cross-Platform**: Runs on **Windows 11** and **Linux** from the same codebase.

## 🎮 How to Play

*   **Move**: <kbd>←</kbd> <kbd>→</kbd> or <kbd>A</kbd> <kbd>D</kbd>
*   **Shoot**: <kbd>Space</kbd>
*   **Objective**: Destroy the invaders before they reach the bottom of the screen. If they hit you, you lose a life!

## 🛠️ Installation & Getting Started

### Prerequisites

*   The **.NET SDK** version specified in [`global.json`](global.json) ([download](https://dotnet.microsoft.com/download))
*   Git

### Option 1: Run from Source (Windows & Linux)

1. Clone the repository:
   ```bash
   git clone https://github.com/minigame-tech/SpaceInvaders.git
   cd SpaceInvaders
   ```
2. Restore dependencies and run the game:
   ```bash
   dotnet run --project src/Space_Invaders.Game
   ```

### Option 2: Standalone Executable

You can publish a self-contained build that doesn't require the .NET runtime on the target machine:

```bash
# Windows
dotnet publish src/Space_Invaders.Game -c Release -r win-x64 --self-contained -o dist/win-x64

# Linux
dotnet publish src/Space_Invaders.Game -c Release -r linux-x64 --self-contained -o dist/linux-x64
```

The executable will be available in the corresponding `dist/` folder.

### Running the Tests

```bash
dotnet test
```

## 🗂️ Project Structure

```text
.
├── .github/                    # Issue/PR templates and workflows
├── docs/                       # Documentation
├── scripts/                    # Helper scripts
├── src/
│   ├── Space_Invaders.Core/    # Game logic (no rendering dependencies)
│   └── Space_Invaders.Game/    # Raylib-cs front-end, assets and entry point
│       └── assets/             # Images and audio
└── test/
    └── SpaceInvaders.Core.Tests/
```

## 🤝 Community & Contributing

We welcome contributions of all kinds, whether it's reporting bugs, suggesting new features, or submitting pull requests!

*   **[Contributing Guide](CONTRIBUTING.md)**: Learn how to set up the project and submit changes.
*   **[Code of Conduct](CODE_OF_CONDUCT.md)**: Our community standards and expectations.
*   **[Security Policy](SECURITY.md)**: How to responsibly report security vulnerabilities.

## 📈 Roadmap

### 👾 Core Gameplay
- [x] **Player ship:** horizontal movement, shooting with cooldown, lives.
- [ ] **Invader grid:** formation that moves sideways and descends, with two animation frames.
- [ ] **Collisions:** bullets vs. invaders, invader bombs vs. the player.
- [ ] **Shields:** destructible bunkers.
- [ ] **Mystery UFO:** bonus ship crossing the top of the screen.
- [ ] **Score & levels:** score counter, high score and increasing difficulty.

### 🎨 Polish
- [ ] **Sound effects & music:** shooting, explosions and the classic marching beat.
- [ ] **Main menu & Game Over screen.**
- [ ] **Animations:** explosions and smooth screen transitions.

## 🖼️ Assets & Disclaimer

*Space Invaders* is a trademark and copyrighted work of **Taito Corporation**. This is a non-commercial, fan-made educational project and is not affiliated with or endorsed by Taito. Original game artwork and sprites remain the property of their respective owners and are **not** covered by this project's MIT License.

## 📜 Credits & License

*   **Developed by**: Cipriano Salvatore / [minigame-tech](https://github.com/minigame-tech)
*   **Game Library**: [Raylib-cs](https://github.com/raylib-cs/raylib-cs) (C# bindings) built on [raylib](https://www.raylib.com/) by Ramon Santamaria (@raysan5)

The source code of this project is licensed under the terms of the **MIT License**. Check the [`LICENSE`](LICENSE) file for more details.