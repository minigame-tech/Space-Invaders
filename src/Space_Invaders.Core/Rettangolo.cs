namespace Space_Invaders.Core;

/// <summary>Rettangolo semplice, indipendente da Raylib, usato per le collisioni.</summary>
public readonly record struct Rettangolo(float X, float Y, float Larghezza, float Altezza)
{
    public float Destra => X + Larghezza;
    public float Basso => Y + Altezza;

    public bool Interseca(Rettangolo altro) =>
        X < altro.Destra && Destra > altro.X && Y < altro.Basso && Basso > altro.Y;
}
