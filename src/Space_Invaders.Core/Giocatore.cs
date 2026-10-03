namespace Space_Invaders.Core;

/// <summary>La navicella del giocatore: solo logica, niente input né disegno.</summary>
public class Giocatore
{
    public const float Larghezza = 28f;
    public const float Altezza = 32f;
    public const int VitePartenza = 3;

    private const float Velocita = 300f;               // pixel al secondo
    private const float DurataInvulnerabilita = 1.5f;  // secondi dopo essere stati colpiti

    private readonly float _xIniziale;
    private float _timerInvulnerabilita;

    public Giocatore(float x, float y)
    {
        X = _xIniziale = x;
        Y = y;
    }

    public float X { get; private set; }
    public float Y { get; }
    public int Vite { get; private set; } = VitePartenza;
    public bool Vivo => Vite > 0;
    public bool Invulnerabile => _timerInvulnerabilita > 0f;
    public Rettangolo Rettangolo => new(X, Y, Larghezza, Altezza);

    /// <param name="direzione">-1 = sinistra, 0 = fermo, +1 = destra.</param>
    public void Aggiorna(float dt, float direzione)
    {
        X = Math.Clamp(X + direzione * Velocita * dt,
                       Campo.Margine,
                       Campo.Larghezza - Campo.Margine - Larghezza);

        if (_timerInvulnerabilita > 0f)
            _timerInvulnerabilita -= dt;
    }

    public Proiettile Spara() =>
        new(X + Larghezza / 2f - Proiettile.Larghezza / 2f,
            Y - Proiettile.Altezza,
            -Proiettile.VelocitaGiocatore);

    /// <summary>Toglie una vita, riporta la nave al centro e la rende invulnerabile per poco.</summary>
    public void Colpito()
    {
        Vite--;
        X = _xIniziale;
        _timerInvulnerabilita = DurataInvulnerabilita;
    }
}
