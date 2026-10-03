using Space_Invaders.Core;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game.Scenes;

public sealed class GameOverScene : IScene
{
    private readonly GameApp _app;
    private readonly int _punteggio;
    private readonly int _livello;
    private readonly bool _nuovoRecord;
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

    public IScene? Update(float dt) =>
        MenuInput.Leggi(_menu) switch
        {
            MenuAzione.Gioca => new GameScene(_app),
            MenuAzione.MenuPrincipale => new MainMenuScene(_app),
            _ => null
        };

    public void Draw()
    {
        Testo.Centrato("GAME OVER", 110, Theme.FontTitolo, Theme.Errore);
        Testo.Centrato($"PUNTEGGIO  {_punteggio:D5}", 210, Theme.FontMenu, Theme.Primario);
        Testo.Centrato($"LIVELLO RAGGIUNTO  {_livello}", 250, Theme.FontHud, Theme.Primario);

        if (_nuovoRecord)
            Testo.Centrato("NUOVO RECORD!", 300, Theme.FontMenu, Theme.Evidenziato);
        else
            Testo.Centrato($"RECORD {_app.Record.Valore:D5}", 300, Theme.FontHud, Theme.Secondario);

        MenuRenderer.Disegna(_menu, 380);
    }
}
