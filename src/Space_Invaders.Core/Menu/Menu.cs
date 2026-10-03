namespace Space_Invaders.Core;

/// <summary>Logica di navigazione di un menu (nessuna dipendenza da Raylib, quindi testabile).</summary>
public sealed class Menu
{
    private readonly List<MenuItem> _voci;

    public Menu(IEnumerable<MenuItem> voci)
    {
        _voci = voci.ToList();
        if (_voci.Count == 0)
            throw new ArgumentException("Il menu deve avere almeno una voce.", nameof(voci));
    }

    public IReadOnlyList<MenuItem> Voci => _voci;
    public int IndiceSelezionato { get; private set; }
    public MenuItem Selezionata => _voci[IndiceSelezionato];

    /// <summary>Sposta la selezione verso l'alto (dalla prima voce si torna all'ultima).</summary>
    public void Su() => IndiceSelezionato = (IndiceSelezionato - 1 + _voci.Count) % _voci.Count;

    /// <summary>Sposta la selezione verso il basso (dall'ultima voce si torna alla prima).</summary>
    public void Giu() => IndiceSelezionato = (IndiceSelezionato + 1) % _voci.Count;
}
