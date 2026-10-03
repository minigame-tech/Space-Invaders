using Raylib_cs;
using Space_Invaders.Core;
using Space_Invaders.Game.Scenes;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game;

/// <summary>Crea la finestra, carica le risorse e fa girare il ciclo principale delle scene.</summary>
public sealed class GameApp
{
    private IScene _scena = null!;
    private bool _esci;

    public Texture2D Sprite { get; private set; }
    public RecordStore Record { get; } = new();

    public void Esegui()
    {
        Raylib.InitWindow(Campo.Larghezza, Campo.Altezza, "Space Invaders");
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.Null);  // ESC non chiude più la finestra: serve per pausa/menu

        Sprite = Raylib.LoadTexture(SpriteAtlas.Percorso);
        if (Sprite.Id == 0)
            Console.WriteLine($"ATTENZIONE: sprite non trovato ({SpriteAtlas.Percorso}). " +
                              "Controlla 'Copy to output directory' nel .csproj.");
        Raylib.SetTextureFilter(Sprite, TextureFilter.Point);  // pixel art nitida

        _scena = new MainMenuScene(this);

        while (!_esci && !Raylib.WindowShouldClose())
        {
            // Limito dt per evitare salti enormi se la finestra viene trascinata o il gioco si blocca.
            float dt = Math.Min(Raylib.GetFrameTime(), 0.033f);

            var prossima = _scena.Update(dt);
            if (prossima is not null)
                _scena = prossima;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Theme.Sfondo);
            _scena.Draw();
            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(Sprite);
        Raylib.CloseWindow();
    }

    public void Esci() => _esci = true;
}
