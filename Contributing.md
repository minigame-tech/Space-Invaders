# 🤝 Contributing to Space Invaders: C# Remake

First off, thank you for taking the time to contribute! 🎉
Every kind of help is welcome: bug reports, feature ideas, documentation, code and graphics.

By participating in this project you agree to abide by our [Code of Conduct](CODE_OF_CONDUCT.md).

## 📋 Ways to Contribute

*   🐛 **Report a bug**: open an issue with clear steps to reproduce it.
*   💡 **Suggest a feature**: open an issue describing the idea and why it would be useful.
*   📝 **Improve the docs**: fix typos, clarify instructions, add screenshots.
*   💻 **Submit code**: fix a bug or implement a feature from the [Roadmap](README.md#-roadmap).

For security issues, **do not** open a public issue: follow the [Security Policy](SECURITY.md) instead.

## 🛠️ Development Setup

### Prerequisites

*   The **.NET SDK** version specified in [`global.json`](global.json)
*   Git
*   An IDE of your choice (JetBrains Rider, Visual Studio, VS Code with C# Dev Kit)

### Getting Started

1. **Fork** the repository and clone your fork:
   ```bash
   git clone https://github.com/<your-username>/SpaceInvaders.git
   cd SpaceInvaders
   ```
2. Build the solution:
   ```bash
   dotnet build
   ```
3. Run the game:
   ```bash
   dotnet run --project src/Space_Invaders.Game
   ```
4. Run the tests:
   ```bash
   dotnet test
   ```

## 🗂️ Project Structure

| Folder                         | Purpose                                                    |
| ------------------------------ | ---------------------------------------------------------- |
| `src/Space_Invaders.Core`      | Game logic. Keep it free of rendering dependencies.        |
| `src/Space_Invaders.Game`      | Raylib-cs front-end, input, rendering, assets, entry point |
| `test/SpaceInvaders.Core.Tests`| Unit tests for the core logic                              |
| `docs/`, `scripts/`            | Documentation and helper scripts                           |

## 🌿 Workflow

1. Create a branch from `master` with a descriptive name:
   ```bash
   git switch -c feature/invader-grid
   ```
   Suggested prefixes: `feature/`, `fix/`, `docs/`, `refactor/`, `chore/`.
2. Make your changes in small, focused commits.
3. Make sure the project builds and all tests pass.
4. Push your branch and open a **Pull Request** against `master`.
5. Address review comments. Once approved, the branch is merged with a merge commit (`--no-ff`) to preserve the history.

## ✍️ Commit Messages

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification:

```text
<type>(<scope>): <short description in imperative mood>
```

*   **Types**: `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `build`, `ci`, `chore`.
*   **Scope** (optional): the area you touched, e.g. `player`, `assets`, `game`, `core`.
*   Keep the first line under about 72 characters, with no trailing period.
*   Use the body to explain *why*, not just *what*.

Examples:

```text
feat(player): add shooting with cooldown
fix(player): clamp movement to screen bounds
docs: update installation instructions
```

## 🎨 Code Style

*   Follow the rules defined in [`.editorconfig`](.editorconfig); your IDE should pick them up automatically.
*   Use the standard C# naming conventions: `PascalCase` for types and public members, `_camelCase` for private fields, `camelCase` for locals and parameters.
*   Keep game logic in `Core` and rendering/input in `Game`.
*   Use delta time (`Raylib.GetFrameTime()`) for movement and timers, never frame counts.
*   Prefer small classes with a single responsibility, and add XML doc comments to public APIs.
*   Format your code before committing:
    ```bash
    dotnet format
    ```

## ✅ Tests

*   Add or update unit tests in `test/SpaceInvaders.Core.Tests` for any change to the game logic.
*   Run `dotnet test` before opening a Pull Request.

## 🖼️ Assets

*   Only add images, audio or fonts that you created yourself or that you have the right to redistribute, and state their source and license in the Pull Request.
*   Original *Space Invaders* artwork is the property of Taito Corporation; see the disclaimer in the [README](README.md#-assets--disclaimer).
*   Place assets under `src/Space_Invaders.Game/assets/` (`img/` for images, `audio/` for sounds).

## 🔀 Pull Request Checklist

Before submitting, please check that:

- [ ] The code builds without errors or new warnings.
- [ ] All tests pass (`dotnet test`).
- [ ] The code is formatted (`dotnet format`).
- [ ] Commit messages follow Conventional Commits.
- [ ] Documentation is updated if behavior changed.
- [ ] The PR description explains *what* changed and *why*.

## 📜 License

By contributing, you agree that your contributions will be licensed under the [MIT License](LICENSE).