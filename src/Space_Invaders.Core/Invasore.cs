namespace Space_Invaders.Core;

/// <summary>Un singolo invasore della formazione.</summary>
public class Invasore
{
    public const float Larghezza = 44f;
    public const float Altezza = 32f;

    private static readonly int[] PuntiPerTipo = { 40, 30, 20, 10 };

    public Invasore(int tipo, int riga, int colonna, float x, float y)
    {
        Tipo = tipo;
        Riga = riga;
        Colonna = colonna;
        X = x;
        Y = y;
    }

    public int Tipo { get; }
    public int Riga { get; }
    public int Colonna { get; }
    public float X { get; private set; }
    public float Y { get; private set; }
    public bool Vivo { get; private set; } = true;
    public int Punti => PuntiPerTipo[Tipo];
    public float CentroX => X + Larghezza / 2f;
    public Rettangolo Rettangolo => new(X, Y, Larghezza, Altezza);

    public static int PuntiDelTipo(int tipo) => PuntiPerTipo[tipo];

    public void Sposta(float dx, float dy)
    {
        X += dx;
        Y += dy;
    }

    public void Uccidi() => Vivo = false;
}
