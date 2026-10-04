namespace Space_Invaders.Core;

public enum StatoPartita
{
    InCorso,
    Persa
}

/// <summary>
/// Una partita completa: nave, invasori, proiettili, scudi, UFO, punteggio e livello.
/// Non conosce Raylib: riceve solo dt, la direzione di movimento e se si sta sparando.
/// </summary>
public class Partita
{
    private const int NumeroScudi = 4;
    private const int MaxProiettiliInvasori = 3;

    private readonly Random _random;
    private readonly List<Proiettile> _proiettiliInvasori = new();
    private readonly List<Scudo> _scudi = new();

    public Action? OnGiocatoreSpara { get; set; }
    public Action<float, float>? OnInvasoreDistrutto { get; set; }
    public Action<float, float>? OnUfoDistrutto { get; set; }
    public Action? OnGiocatoreColpito { get; set; }

    public Partita(Random? random = null)
    {
        _random = random ?? new Random();
        Giocatore = new Giocatore(Campo.Larghezza / 2f - Giocatore.Larghezza / 2f, Campo.YGiocatore);
        Ufo = new Ufo(_random);
        Formazione = new FormazioneInvasori(Livello, _random);
        CreaScudi();
    }

    public Giocatore Giocatore { get; }
    public FormazioneInvasori Formazione { get; private set; }
    public Ufo Ufo { get; }
    public Proiettile? ProiettileGiocatore { get; private set; }
    public IReadOnlyList<Proiettile> ProiettiliInvasori => _proiettiliInvasori;
    public IReadOnlyList<Scudo> Scudi => _scudi;
    public int Punteggio { get; private set; }
    public int Livello { get; private set; } = 1;
    public StatoPartita Stato { get; private set; } = StatoPartita.InCorso;

    /// <param name="direzione">-1 sinistra, 0 fermo, +1 destra.</param>
    /// <param name="spara">true se il tasto di sparo è premuto.</param>
    public void Aggiorna(float dt, float direzione, bool spara)
    {
        if (Stato != StatoPartita.InCorso)
            return;

        Giocatore.Aggiorna(dt, direzione);

        // Come nell'arcade: un solo colpo del giocatore alla volta.
        if (spara && ProiettileGiocatore is null)
        {
            ProiettileGiocatore = Giocatore.Spara();
            OnGiocatoreSpara?.Invoke();
        }

        ProiettileGiocatore?.Aggiorna(dt);
        foreach (var proiettile in _proiettiliInvasori)
            proiettile.Aggiorna(dt);

        Formazione.Aggiorna(dt);
        Ufo.Aggiorna(dt);

        var nuovoColpo = Formazione.TentaSparo(dt);
        if (nuovoColpo is not null && _proiettiliInvasori.Count < MaxProiettiliInvasori)
            _proiettiliInvasori.Add(nuovoColpo);

        GestisciCollisioni();
        Pulisci();
        ControllaFineLivello();
    }

    private void GestisciCollisioni()
    {
        ColpiDelGiocatore();
        ColpiDegliInvasori();
        InvasoriControScudiETerreno();
    }

    private void ColpiDelGiocatore()
    {
        var colpo = ProiettileGiocatore;
        if (colpo is null || !colpo.Attivo)
            return;

        var area = colpo.Rettangolo;

        var bersaglio = Formazione.Invasori.FirstOrDefault(i => i.Vivo && i.Rettangolo.Interseca(area));
        if (bersaglio is not null)
        {
            bersaglio.Uccidi();
            Punteggio += bersaglio.Punti;
            colpo.Distruggi();
            OnInvasoreDistrutto?.Invoke(bersaglio.CentroX, bersaglio.Y + Invasore.Altezza / 2f);
        }
        else if (Ufo.Attivo && Ufo.Rettangolo.Interseca(area))
        {
            Punteggio += Ufo.Colpisci();
            colpo.Distruggi();
            OnUfoDistrutto?.Invoke(Ufo.X + Ufo.Larghezza / 2f, Ufo.Y + Ufo.Altezza / 2f);
        }
        else if (ColpisceScudo(colpo))
        {
            colpo.Distruggi();
        }
    }

    private void ColpiDegliInvasori()
    {
        bool giocatoreColpito = false;

        foreach (var colpo in _proiettiliInvasori)
        {
            if (!colpo.Attivo)
                continue;

            if (ColpisceScudo(colpo))
            {
                colpo.Distruggi();
            }
            else if (!Giocatore.Invulnerabile && Giocatore.Rettangolo.Interseca(colpo.Rettangolo))
            {
                colpo.Distruggi();
                giocatoreColpito = true;
            }
        }

        if (!giocatoreColpito)
            return;

        Giocatore.Colpito();
        OnGiocatoreColpito?.Invoke();
        foreach (var colpo in _proiettiliInvasori)
            colpo.Distruggi();

        if (!Giocatore.Vivo)
            Stato = StatoPartita.Persa;
    }

    private void InvasoriControScudiETerreno()
    {
        foreach (var invasore in Formazione.Invasori.Where(i => i.Vivo))
        {
            foreach (var scudo in _scudi)
                scudo.DistruggiArea(invasore.Rettangolo);

            // Se gli invasori arrivano all'altezza della nave, la partita è persa.
            if (invasore.Rettangolo.Basso >= Giocatore.Rettangolo.Y)
                Stato = StatoPartita.Persa;
        }
    }

    private bool ColpisceScudo(Proiettile colpo)
    {
        foreach (var scudo in _scudi)
            if (scudo.TentaColpire(colpo.Rettangolo, colpo.DelGiocatore))
                return true;
        return false;
    }

    private void Pulisci()
    {
        _proiettiliInvasori.RemoveAll(p => !p.Attivo);
        if (ProiettileGiocatore is { Attivo: false })
            ProiettileGiocatore = null;
    }

    private void ControllaFineLivello()
    {
        if (Formazione.Vivi > 0)
            return;

        Livello++;
        Formazione = new FormazioneInvasori(Livello, _random);
        _proiettiliInvasori.Clear();
        ProiettileGiocatore = null;
        CreaScudi();
    }

    private void CreaScudi()
    {
        _scudi.Clear();
        float spazio = (Campo.Larghezza - NumeroScudi * Scudo.Larghezza) / (NumeroScudi + 1);
        for (int i = 0; i < NumeroScudi; i++)
            _scudi.Add(new Scudo(spazio + i * (Scudo.Larghezza + spazio), Campo.YScudi));
    }
}
