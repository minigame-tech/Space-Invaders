namespace Space_Invaders.Core;

/// <summary>Bunker distruttibile, rappresentato come una griglia di piccole celle.</summary>
public class Scudo
{
    public const int Colonne = 22;
    public const int Righe = 16;
    public const float Cella = 3f;  // lato di una cella in pixel
    public const float Larghezza = Colonne * Cella;
    public const float Altezza = Righe * Cella;

    private readonly bool[,] _celle = new bool[Colonne, Righe];

    public Scudo(float x, float y)
    {
        X = x;
        Y = y;
        for (int c = 0; c < Colonne; c++)
            for (int r = 0; r < Righe; r++)
                _celle[c, r] = Forma(c, r);
    }

    public float X { get; }
    public float Y { get; }
    public Rettangolo Rettangolo => new(X, Y, Larghezza, Altezza);

    public bool CellaViva(int colonna, int riga) => _celle[colonna, riga];

    /// <summary>
    /// Se il rettangolo tocca una cella viva, la distrugge (con le vicine) e restituisce true.
    /// </summary>
    /// <param name="dalBasso">true per proiettili che salgono (si parte dalle celle più in basso).</param>
    public bool TentaColpire(Rettangolo area, bool dalBasso)
    {
        if (!Rettangolo.Interseca(area))
            return false;

        GetIntervallo(area, out int c0, out int c1, out int r0, out int r1);

        for (int i = 0; i <= r1 - r0; i++)
        {
            int riga = dalBasso ? r1 - i : r0 + i;
            for (int colonna = c0; colonna <= c1; colonna++)
            {
                if (_celle[colonna, riga])
                {
                    Esplodi(colonna, riga);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>Cancella tutte le celle sotto il rettangolo (usato quando un invasore ci passa sopra).</summary>
    public void DistruggiArea(Rettangolo area)
    {
        if (!Rettangolo.Interseca(area))
            return;

        GetIntervallo(area, out int c0, out int c1, out int r0, out int r1);
        for (int c = c0; c <= c1; c++)
            for (int r = r0; r <= r1; r++)
                _celle[c, r] = false;
    }

    private void Esplodi(int colonna, int riga)
    {
        for (int c = colonna - 2; c <= colonna + 2; c++)
        {
            for (int r = riga - 2; r <= riga + 2; r++)
            {
                if (c < 0 || c >= Colonne || r < 0 || r >= Righe)
                    continue;
                int dc = c - colonna;
                int dr = r - riga;
                if (dc * dc + dr * dr <= 6)
                    _celle[c, r] = false;
            }
        }
    }

    private void GetIntervallo(Rettangolo area, out int c0, out int c1, out int r0, out int r1)
    {
        c0 = Math.Max(0, (int)MathF.Floor((area.X - X) / Cella));
        c1 = Math.Min(Colonne - 1, (int)MathF.Floor((area.Destra - X) / Cella));
        r0 = Math.Max(0, (int)MathF.Floor((area.Y - Y) / Cella));
        r1 = Math.Min(Righe - 1, (int)MathF.Floor((area.Basso - Y) / Cella));
    }

    // Forma classica: angoli superiori smussati e un arco in basso al centro.
    private static bool Forma(int colonna, int riga)
    {
        int taglio = 5 - riga;
        if (taglio > 0 && (colonna < taglio || colonna >= Colonne - taglio))
            return false;

        bool arco = riga >= Righe - 5 && colonna >= 7 && colonna < Colonne - 7;
        return !arco;
    }
}
