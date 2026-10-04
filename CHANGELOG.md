# Changelog - Space Invaders

## [v0.9.0-beta] - 2026-10-04
### Added
- **Audio System**: Implemented sound effects for shooting (`laserShoot.wav`), explosions (`explosion.wav`), and player damage (`hitHurt.wav`).
- **Visual Effects System (VFX)**:
  - **Screen Shake**: Added camera shake when the player fires, gets hit, or when large explosions occur (like UFO destruction).
  - **Particles**: Cyan and Magenta explosion particles with additive blending when destroying enemies and UFOs.
  - **Starfield Background**: A parallax scrolling starfield to give depth to the void of space.
- **Visual Polish**:
  - Added additive blending glow to both player and enemy projectiles for a striking neon/arcade aesthetic.

### Changed
- Game rendering pipeline separated into foreground and background passes to properly composite stars, sprites, and glowing effects.
- Main logic events updated to broadcast actions for audio and particle triggers asynchronously.

### Platform Support
- Fully cross-platform architecture utilizing MonoGame/Raylib-cs, tested and optimized for Windows and Linux natively.
