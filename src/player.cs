using System.Numerics;
using Raylib_cs;

namespace Space_Invaders;

/// <summary>Proiettile sparato dal giocatore: sale dritto verso l'alto.</summary>
public class Proiettile
{
    public const int Larghezza   = 4;
    public const int Altezza     = 12;
    private const float Velocita = 500f; //Pixel al secondo
    
    public float X { get; }
    public float Y { get; private set; }
    public bool Attivo { get; private set; } = true;

    public Proiettile(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void Aggiorna(float dt)
    {
        Y -= Velocita * dt;
        if (Y + Altezza < 0)
        {
            Attivo = false; //Uscita dallo schermo
        }
    }

    /// <summary>Funzione richiamata quando il proiettile distrugge qualcosa</summary>
    public void Distruggi() => Attivo = false;

    public Rectangle Rettangolo() => new(X, Y, Larghezza, Altezza);

    public void Disegna() =>
        Raylib.DrawRectangle((int)X, (int)Y, Larghezza, Altezza, Color.White);
}

/// <summary>Classe che rappresenta la navicella controllata dal giocatore</summary>
public class Giocatore
{
    // -----------------------------------------------------------------
    // Costanti di gioco
    // -----------------------------------------------------------------
    public const int Larghezza           = 48;  //Dimensione a schermo
    public const int Altezza             = 32;
    private const float Velocita         = 300f; //Pixel al secondo
    private const float CoolDownSparo    = 0.4f; //Secondi tra 2 spari
    private const float DurataFrameSparo = 0.1f;
    private const int VitePartenza       = 3;
    
    // -----------------------------------------------------------------
    // Stato globale
    // -----------------------------------------------------------------
    private readonly Texture2D _texture;
    private readonly Rectangle _frameFermo; //Porzione dello sprite sheet
    private readonly Rectangle _frameSparo;
    private readonly float _startX;
    private readonly float _startY;

    private float _timerSparo;              //Tempo rimasto al prossimo sparo
    private float _timerAnimazione;         //Tempo rimasto al frame "Sparo"
    
    public float X { get; private set; }
    public float Y { get; private set; }
    public int Vite { get; private set; } = VitePartenza;
    public bool Vivo { get; private set; } = true;
    public List<Proiettile> Proiettili { get; } = new();
    
    // -----------------------------------------------------------------
    // Costruzione
    // -----------------------------------------------------------------
    /// <param name="texture">Texture già caricata (Raylib.LoadTexture) dopo InitWindow.</param>
    /// <param name="frameFermo">Rettangolo dello sprite a riposo nello sprite sheet.</param>
    /// <param name="frameSparo">Rettangolo dello sprite mentre spara.</param>
    public Giocatore(Texture2D texture, Rectangle frameFermo, Rectangle frameSparo,
        float startX, float startY)
    {
        _texture = texture;
        _frameFermo = frameFermo;
        _frameSparo = frameSparo;
        _startX = startX;
        _startY = startY;
    }
    
    // -----------------------------------------------------------------
    // Input
    // -----------------------------------------------------------------
    public void GestisciInput(float dt, int larghezzaSchermo)
    {
        float direzione = 0f;
        if (Raylib.IsKeyDown(KeyboardKey.Left) || Raylib.IsKeyDown(KeyboardKey.A))
            direzione -= 1f;
        if (Raylib.IsKeyDown(KeyboardKey.Right) || Raylib.IsKeyDown(KeyboardKey.D))
            direzione += 1f;

        X = Math.Clamp(X + direzione * Velocita * dt, 0, larghezzaSchermo - Larghezza);

        if (Raylib.IsKeyPressed(KeyboardKey.Space) && _timerSparo <= 0f)
            Spara();
    }

    private void Spara()
    {
        float bx = X + Larghezza / 2f - Proiettile.Larghezza / 2f;
        Proiettili.Add(new Proiettile(bx, Y - Proiettile.Altezza));
        _timerSparo = CoolDownSparo;
        _timerAnimazione = DurataFrameSparo;
    }
    
    // -----------------------------------------------------------------
    // Aggiornamento
    // -----------------------------------------------------------------
    public void Aggiorna(float dt)
    {
        if (_timerSparo > 0f) _timerSparo -= dt;
        if (_timerAnimazione > 0f) _timerAnimazione -= dt;

        foreach (var p in Proiettili)
            p.Aggiorna(dt);
        Proiettili.RemoveAll(p => !p.Attivo);
    }

    public void Muori()
    {
        Vite--;
        if (Vite <= 0)
            Vivo = false;
        else
            Respawn();
    }

    public void Respawn()
    {
        X = _startX;
        Y = _startY;
        _timerSparo = 0f;
        _timerAnimazione = 0f;
        Proiettili.Clear();
    }
    
    // -----------------------------------------------------------------
    // Rettangolo di collisione
    // -----------------------------------------------------------------
    public Rectangle Rettangolo()
    {
        const float margine = 4f;
        return new Rectangle(X + margine, Y + margine,
            Larghezza - margine * 2, Altezza - margine * 2);
    }
    
    // -----------------------------------------------------------------
    // Disegno
    // -----------------------------------------------------------------
    public void Disegna()
    {
        Rectangle sorgente = _timerAnimazione > 0f ? _frameSparo : _frameFermo;
        var destinazione = new Rectangle(X, Y, Larghezza, Altezza);
        
        Raylib.DrawTexturePro(_texture, sorgente, destinazione, Vector2.Zero, 0f, Color.White);

        foreach (var p in Proiettili)
            p.Disegna();
    }
}