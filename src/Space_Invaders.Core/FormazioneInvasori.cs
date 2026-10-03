namespace Space_Invaders.Core;

/// <summary>
/// La griglia di invasori: si muove a scatti come nell'arcade (più veloce quando ne restano pochi)
/// e decide quale invasore spara.
/// </summary>
public class FormazioneInvasori
{
    public const int Righe = 5;
    public const int Colonne = 11;

    private const float PassoX = 12f;     // spostamento orizzontale a ogni scatto
    private const float PassoY = 22f;     // discesa quando si raggiunge il bordo
    private const float PassoGrigliaX = 60f;
    private const float PassoGrigliaY = 44f;

    // Tipo di sprite per ciascuna riga, dall'alto verso il basso.
    private static readonly int[] TipoPerRiga = { 0, 1, 1, 2, 3 };

    private readonly List<Invasore> _invasori = new();
    private readonly Random _random;
    private readonly int _livello;
    private float _timerPasso;
    private float _timerSparo;
    private int _direzione = 1;

    public FormazioneInvasori(int livello, Random random)
    {
        _livello = livello;
        _random = random;

        float larghezzaTotale = (Colonne - 1) * PassoGrigliaX + Invasore.Larghezza;
        float xInizio = (Campo.Larghezza - larghezzaTotale) / 2f;
        float yInizio = Campo.YFormazione + Math.Min(livello - 1, 5) * 20f;  // ogni livello parte più in basso

        for (int riga = 0; riga < Righe; riga++)
            for (int colonna = 0; colonna < Colonne; colonna++)
                _invasori.Add(new Invasore(TipoPerRiga[riga], riga, colonna,
                                           xInizio + colonna * PassoGrigliaX,
                                           yInizio + riga * PassoGrigliaY));

        _timerSparo = ProssimoIntervalloSparo();
    }

    public IReadOnlyList<Invasore> Invasori => _invasori;

    /// <summary>Frame di animazione corrente (0 o 1), cambia a ogni scatto.</summary>
    public int Frame { get; private set; }

    public int Vivi => _invasori.Count(i => i.Vivo);

    public void Aggiorna(float dt)
    {
        _timerPasso += dt;
        float intervallo = IntervalloPasso();
        if (_timerPasso < intervallo)
            return;

        _timerPasso = 0f;
        Passo();
    }

    /// <summary>Con una certa casualità restituisce un proiettile sparato dall'invasore più in basso di una colonna.</summary>
    public Proiettile? TentaSparo(float dt)
    {
        _timerSparo -= dt;
        if (_timerSparo > 0f)
            return null;

        _timerSparo = ProssimoIntervalloSparo();

        var tiratori = _invasori
            .Where(i => i.Vivo)
            .GroupBy(i => i.Colonna)
            .Select(g => g.OrderByDescending(i => i.Riga).First())
            .ToList();

        if (tiratori.Count == 0)
            return null;

        var tiratore = tiratori[_random.Next(tiratori.Count)];
        return new Proiettile(tiratore.CentroX - Proiettile.Larghezza / 2f,
                              tiratore.Rettangolo.Basso,
                              Proiettile.VelocitaInvasori + 20f * (_livello - 1));
    }

    private void Passo()
    {
        Frame = 1 - Frame;

        bool toccaIlBordo = _invasori.Any(i => i.Vivo &&
            (_direzione > 0
                ? i.Rettangolo.Destra + PassoX > Campo.Larghezza - Campo.Margine
                : i.X - PassoX < Campo.Margine));

        foreach (var invasore in _invasori.Where(i => i.Vivo))
        {
            if (toccaIlBordo)
                invasore.Sposta(0f, PassoY);
            else
                invasore.Sposta(_direzione * PassoX, 0f);
        }

        if (toccaIlBordo)
            _direzione = -_direzione;
    }

    // Meno invasori vivi = scatti più frequenti. Ogni livello accelera ancora un po'.
    private float IntervalloPasso()
    {
        float base_ = 0.04f + Vivi * 0.014f;  // 55 invasori ≈ 0,8 s, 1 invasore ≈ 0,05 s
        return Math.Max(0.03f, base_ * MathF.Pow(0.9f, _livello - 1));
    }

    private float ProssimoIntervalloSparo()
    {
        float minimo = Math.Max(0.25f, 0.7f - 0.05f * (_livello - 1));
        float massimo = minimo + 0.9f;
        return minimo + _random.NextSingle() * (massimo - minimo);
    }
}
