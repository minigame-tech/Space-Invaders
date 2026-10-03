namespace Space_Invaders.Game;

/// <summary>Salva e carica il record di punteggio in un piccolo file nella cartella dati dell'utente.</summary>
public sealed class RecordStore
{
    private readonly string _percorso;

    public RecordStore()
    {
        string cartella = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "SpaceInvaders");
        _percorso = Path.Combine(cartella, "record.txt");
        Carica();
    }

    public int Valore { get; private set; }

    /// <summary>Aggiorna il record se il punteggio lo supera. Restituisce true se è un nuovo record.</summary>
    public bool Aggiorna(int punteggio)
    {
        if (punteggio <= Valore)
            return false;

        Valore = punteggio;
        Salva();
        return true;
    }

    private void Carica()
    {
        try
        {
            if (File.Exists(_percorso) && int.TryParse(File.ReadAllText(_percorso), out int valore))
                Valore = valore;
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            // Se il file non è leggibile si riparte da zero: il record non è essenziale.
        }
    }

    private void Salva()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_percorso)!);
            File.WriteAllText(_percorso, Valore.ToString());
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            // Idem: se non si riesce a salvare il gioco continua comunque.
        }
    }
}
