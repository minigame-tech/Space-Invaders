namespace Space_Invaders.Core;

/// <summary>Proiettile verticale, sparato dal giocatore (verso l'alto) o dagli invasori (verso il basso).</summary>
public class Proiettile
{
    public const float Larghezza = 4f;
    public const float Altezza = 20f;
    public const float VelocitaGiocatore = 550f;  // pixel al secondo
    public const float VelocitaInvasori = 250f;

    public Proiettile(float x, float y, float velocitaY)
    {
        X = x;
        Y = y;
        VelocitaY = velocitaY;
    }

    public float X { get; }
    public float Y { get; private set; }
    public float VelocitaY { get; }
    public bool Attivo { get; private set; } = true;
    public bool DelGiocatore => VelocitaY < 0f;
    public Rettangolo Rettangolo => new(X, Y, Larghezza, Altezza);

    public void Aggiorna(float dt)
    {
        Y += VelocitaY * dt;
        if (Y + Altezza < 0f || Y > Campo.Altezza)
            Attivo = false;
    }

    public void Distruggi() => Attivo = false;
}
