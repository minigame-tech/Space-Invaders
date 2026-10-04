using Raylib_cs;
using Space_Invaders.Core;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game.Scenes;

public sealed class GameOverScene : IScene
{
    private readonly GameApp _app;
    private readonly int _punteggio;
    private readonly int _livello;
    private readonly bool _nuovoRecord;
    private readonly VisualEffects _vfx = new();
    private readonly Menu _menu = new(new[]
    {
        new MenuItem("RIGIOCA", MenuAzione.Gioca),
        new MenuItem("MENU PRINCIPALE", MenuAzione.MenuPrincipale)
    });

    public GameOverScene(GameApp app, int punteggio, int livello, bool nuovoRecord)
    {
        _app = app;
        _punteggio = punteggio;
        _livello = livello;
        _nuovoRecord = nuovoRecord;
    }

    public IScene? Update(float dt)
    {
        _vfx.Update(dt);
        return MenuInput.Leggi(_menu) switch
        {
            MenuAzione.Gioca => new GameScene(_app),
            MenuAzione.MenuPrincipale => new MainMenuScene(_app),
            _ => null
        };
    }

    public void Draw()
    {
        _vfx.DrawBackground();

        Color coloreTitolo = Theme.Errore;
        coloreTitolo.A = (byte)(200 + 55 * MathF.Sin((float)Raylib.GetTime() * 4f));
        Testo.Centrato("GAME OVER", 110, Theme.FontTitolo, coloreTitolo);
        
        Testo.Centrato($"PUNTEGGIO  {_punteggio:D5}", 210, Theme.FontMenu, Theme.Primario);
        Testo.Centrato($"LIVELLO RAGGIUNTO  {_livello}", 250, Theme.FontHud, Theme.Primario);

        if (_nuovoRecord)
        {
            Color coloreRecord = Theme.Evidenziato;
            coloreRecord.A = (byte)(200 + 55 * MathF.Sin((float)Raylib.GetTime() * 8f));
            Testo.Centrato("NUOVO RECORD!", 300, Theme.FontMenu, coloreRecord);
        }
        else
            Testo.Centrato($"RECORD {_app.Record.Valore:D5}", 300, Theme.FontHud, Theme.Secondario);

        MenuRenderer.Disegna(_menu, 380);
        
        // Versione
        Raylib.DrawText("v1.0.0", Campo.Larghezza - 65, Campo.Altezza - 25, Theme.FontPiccolo, Theme.Secondario);
    }
}
