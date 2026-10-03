namespace Space_Invaders.Core;

/// <summary>L'astronave bonus che attraversa la parte alta dello schermo ogni tanto.</summary>
public class Ufo
{
    public const float Larghezza = 48f;
    public const float Altezza = 21f;

    private const float Velocita = 130f;
    private static readonly int[] PuntiPossibili = { 50, 100, 150, 300 };

    private readonly Random _random;
    private float _timerComparsa;
    private int _direzione;

    public Ufo(Random random)
    {
        _random = random;
        _timerComparsa = ProssimaComparsa();
    }

    public bool Attivo { get; private set; }
    public float X { get; private set; }
    public float Y => Campo.YUfo;
    public Rettangolo Rettangolo => new(X, Y, Larghezza, Altezza);

    public void Aggiorna(float dt)
    {
        if (!Attivo)
        {
            _timerComparsa -= dt;
            if (_timerComparsa <= 0f)
                Avvia();
            return;
        }

        X += _direzione * Velocita * dt;
        if (X > Campo.Larghezza || X + Larghezza < 0f)
            Disattiva();
    }

    /// <summary>Chiamalo quando l'UFO viene colpito: lo fa sparire e restituisce i punti guadagnati.</summary>
    public int Colpisci()
    {
        Disattiva();
        return PuntiPossibili[_random.Next(PuntiPossibili.Length)];
    }

    private void Avvia()
    {
        Attivo = true;
        _direzione = _random.Next(2) == 0 ? 1 : -1;
        X = _direzione > 0 ? -Larghezza : Campo.Larghezza;
    }

    private void Disattiva()
    {
        Attivo = false;
        _timerComparsa = ProssimaComparsa();
    }

    private float ProssimaComparsa() => 15f + _random.NextSingle() * 10f;
}
