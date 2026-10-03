using Raylib_cs;
using Space_Invaders.Core;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game.Scenes;

public sealed class MainMenuScene : IScene
{
    private readonly GameApp _app;
    private readonly Menu _menu = new(new[]
    {
        new MenuItem("GIOCA", MenuAzione.Gioca),
        new MenuItem("ESCI", MenuAzione.Esci)
    });

    public MainMenuScene(GameApp app) => _app = app;

    public IScene? Update(float dt)
    {
        switch (MenuInput.Leggi(_menu))
        {
            case MenuAzione.Gioca:
                return new GameScene(_app);
            case MenuAzione.Esci:
                _app.Esci();
                break;
        }
        return null;
    }

    public void Draw()
    {
        // 1. Richiama il metodo statico passandogli la texture dello sfondo
        // Sostituisci '_app.Sfondo' con la proprietà corretta di GameApp in cui hai caricato la Texture2D dello sfondo
        SfondoRenderer.Disegna(_app.Sfondo);

        // Se desideri cambiare il livello di oscuramento (di default è 110), puoi passare un secondo valore (0-255):
        // SfondoRenderer.Disegna(_app.Sfondo, 150);

        Testo.Centrato("SPACE INVADERS", 50, Theme.FontTitolo, Theme.Evidenziato);
        DisegnaTabellaPunti();
        MenuRenderer.Disegna(_menu, 400);

        Testo.Centrato($"RECORD {_app.Record.Valore:D5}", 505, Theme.FontHud, Theme.Primario);
        Testo.Centrato("FRECCE / A D: MUOVI    SPAZIO: SPARA    P: PAUSA    ESC: MENU",
                       560, Theme.FontPiccolo, Theme.Secondario);
    }

    // La classica tabella dei punti: UFO e un invasore per ogni tipo.
    private void DisegnaTabellaPunti()
    {
        const float centroSprite = 330f;
        const int xTesto = 380;
        float y = 150f;

        SpriteAtlas.DisegnaCentrato(_app.Sprite, SpriteAtlas.Ufo, centroSprite, y + 16f, 0.25f);
        Raylib.DrawText("= ??? PUNTI", xTesto, (int)y + 6, Theme.FontHud, Theme.Primario);

        for (int tipo = 0; tipo < SpriteAtlas.Invasori.Length; tipo++)
        {
            y += 40f;
            SpriteAtlas.DisegnaCentrato(_app.Sprite, SpriteAtlas.Invasori[tipo][0], centroSprite, y + 16f);
            Raylib.DrawText($"= {Invasore.PuntiDelTipo(tipo)} PUNTI", xTesto, (int)y + 6,
                            Theme.FontHud, Theme.Primario);
        }
    }
}