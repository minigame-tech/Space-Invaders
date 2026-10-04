#!/usr/bin/env bash
# Run from the ROOT of the SpaceInvaders repo:  bash /path/to/upgrade/apply.sh
set -euo pipefail
SRC="$(cd "$(dirname "$0")" && pwd)"
EMAIL="cipriano.salvatore@outlook.com"

# Case-only renames need a temp name on Windows/macOS file systems.
rename_ci() { [ -f "$1" ] && git mv "$1" "$1.tmp" && git mv "$1.tmp" "$2" || true; }

rename_ci Security.md SECURITY.md
rename_ci Code_of_Conduct.md CODE_OF_CONDUCT.md
rename_ci Contributing.md CONTRIBUTING.md
git commit -m "docs: rename community files to canonical uppercase names" \
  -m "Fixes broken links on case-sensitive systems (Linux) and lets GitHub detect the files."

printf '\n# IDE and publish output\n.idea/\n.vs/\ndist/\n' >> .gitignore
git rm -r -q --cached .idea 2>/dev/null || true
git add .gitignore
git commit -m "chore: ignore IDE folders and publish output"

mkdir -p src/Space_Invaders.Game/UI
cp "$SRC/src/Space_Invaders.Game/UI/Starfield.cs" src/Space_Invaders.Game/UI/
git add src/Space_Invaders.Game/UI/Starfield.cs
git commit -m "feat(game): add parallax starfield with twinkling stars"

cp "$SRC/src/Space_Invaders.Game/UI/Particelle.cs" src/Space_Invaders.Game/UI/
git add src/Space_Invaders.Game/UI/Particelle.cs
git commit -m "feat(game): add particle system for explosions"

cp "$SRC/src/Space_Invaders.Core/EventoPartita.cs" "$SRC/src/Space_Invaders.Core/Partita.cs" src/Space_Invaders.Core/
git add src/Space_Invaders.Core
git commit -m "feat(core): raise gameplay events from Partita" \
  -m "Lets the front-end react to kills, UFO hits, player hits and level changes without coupling the core to rendering."

cp "$SRC/src/Space_Invaders.Game/Scenes/GameScene.cs" src/Space_Invaders.Game/Scenes/
git add src/Space_Invaders.Game/Scenes/GameScene.cs
git commit -m "feat(game): add screen shake, popups and level banner" \
  -m "Wires starfield and particles into the game scene and reacts to Partita events."

cp "$SRC/README.md" README.md
git add README.md
git commit -m "docs: rewrite README with accurate features, controls and roadmap"

sed -i "s/your-email@example.com/$EMAIL/g" SECURITY.md CODE_OF_CONDUCT.md
git add SECURITY.md CODE_OF_CONDUCT.md
git commit -m "docs: replace placeholder contact email in policies"

echo "Done. Review with: git log --oneline"
