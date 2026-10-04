using Raylib_cs;
using Space_Invaders.Core;
using Space_Invaders.Game.UI;

namespace Space_Invaders.Game.Scenes;

/// <summary>La partita vera e propria: legge l'input, aggiorna <see cref="Partita"/> e la disegna.</summary>
public sealed class GameScene : IScene
{
    private readonly GameApp _app;
    private readonly Partita _partita = new();
    private readonly PartitaRenderer _renderer;
    private readonly VisualEffects _vfx = new();
    private bool _inPausa;

    public GameScene(GameApp app)
    {
        _app = app;
        _renderer = new PartitaRenderer(app.Sprite);
        
        _partita.OnGiocatoreSpara = () => AudioSystem.PlayLaser();
        _partita.OnInvasoreDistrutto = (x, y) => 
        {
            AudioSystem.PlayExplosion();
            _vfx.AddExplosion(x, y, new Color(0, 255, 255, 255)); // Cyan explosion
        };
        _partita.OnUfoDistrutto = (x, y) =>
        {
            AudioSystem.PlayExplosion();
            _vfx.AddExplosion(x, y, new Color(255, 0, 255, 255), 40); // Magenta explosion
            _vfx.ShakeScreen(0.4f, 10f);
        };
        _partita.OnGiocatoreColpito = () =>
        {
            AudioSystem.PlayHit();
            _vfx.ShakeScreen(0.5f, 15f);
            _vfx.AddExplosion(_partita.Giocatore.X + Core.Giocatore.Larghezza / 2, 
                              _partita.Giocatore.Y + Core.Giocatore.Altezza / 2, 
                              new Color(255, 50, 50, 255), 50);
        };
    }

    public IScene? Update(float dt)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            return new MainMenuScene(_app);

        if (Raylib.IsKeyPressed(KeyboardKey.P))
            _inPausa = !_inPausa;

        if (_inPausa)
            return null;

        float direzione = 0f;
        if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A)) direzione -= 1f;
        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D)) direzione += 1f;
        bool spara = Raylib.IsKeyDown(KeyboardKey.Space);

        _partita.Aggiorna(dt, direzione, spara);
        _vfx.Update(dt);

        if (_partita.Stato == StatoPartita.Persa)
        {
            bool nuovoRecord = _app.Record.Aggiorna(_partita.Punteggio);
            return new GameOverScene(_app, _partita.Punteggio, _partita.Livello, nuovoRecord);
        }
        return null;
    }

    public void Draw()
    {
        _vfx.BeginShake();
        _vfx.DrawBackground();
        _renderer.Disegna(_partita, _app.Record.Valore);
        _vfx.DrawForeground();
        _vfx.EndShake();

        if (_inPausa)
            Testo.Centrato("PAUSA", 270, Theme.FontTitolo, Theme.Evidenziato);
    }
}
