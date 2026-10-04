using Raylib_cs;
using Space_Invaders.Core;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game.Scenes;

public sealed class MainMenuScene : IScene
{
    private readonly GameApp _app;
    private readonly VisualEffects _vfx = new();
    private readonly Menu _menu = new(new[]
    {
        new MenuItem("GIOCA", MenuAzione.Gioca),
        new MenuItem("ESCI", MenuAzione.Esci)
    });

    public MainMenuScene(GameApp app) => _app = app;

    public IScene? Update(float dt)
    {
        _vfx.Update(dt);
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
        _vfx.DrawBackground();
        SfondoRenderer.Disegna(_app.Sfondo, 150);

        // Effetto pulsante per il titolo
        Color coloreTitolo = Theme.Evidenziato;
        coloreTitolo.A = (byte)(200 + 55 * MathF.Sin((float)Raylib.GetTime() * 3f));
        Testo.Centrato("SPACE INVADERS", 50, Theme.FontTitolo, coloreTitolo);
        
        DisegnaTabellaPunti();
        MenuRenderer.Disegna(_menu, 400);

        Testo.Centrato($"RECORD {_app.Record.Valore:D5}", 505, Theme.FontHud, Theme.Primario);
        Testo.Centrato("FRECCE / A D: MUOVI    SPAZIO: SPARA    P: PAUSA    ESC: MENU",
                       560, Theme.FontPiccolo, Theme.Secondario);
                       
        // Versione
        Raylib.DrawText("v1.0.0", Campo.Larghezza - 65, Campo.Altezza - 25, Theme.FontPiccolo, Theme.Secondario);
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