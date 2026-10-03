using System.Numerics;
using Raylib_cs;

namespace Space_Invaders.Game.UI;

/// <summary>
/// Posizione di ogni sprite dentro invaders2.png (x, y, larghezza, altezza in pixel).
/// Se cambi immagine, basta aggiornare i numeri qui.
/// </summary>
public static class SpriteAtlas
{
    public const string Percorso = "assets/img/invaders2.png";

    /// <summary>Quattro tipi di invasore, ciascuno con due frame di animazione.</summary>
    public static readonly Rectangle[][] Invasori =
    {
        new[] { new Rectangle(56, 1890, 52, 32),  new Rectangle(136, 1890, 52, 32) },  // tipo 0 (viola)
        new[] { new Rectangle(214, 1896, 48, 32), new Rectangle(294, 1896, 48, 32) },  // tipo 1 (verde)
        new[] { new Rectangle(58, 1950, 48, 32),  new Rectangle(138, 1950, 48, 32) },  // tipo 2 (giallo)
        new[] { new Rectangle(208, 1948, 56, 32), new Rectangle(292, 1948, 56, 32) },  // tipo 3 (arancione)
    };

    public static readonly Rectangle Giocatore = new(428, 1940, 28, 32);
    public static readonly Rectangle Laser = new(442, 1896, 4, 28);

    /// <summary>UFO rosso in versione grande (192x84): va disegnato con scala 0.25.</summary>
    public static readonly Rectangle Ufo = new(391, 783, 192, 84);

    /// <summary>Disegna uno sprite centrato nel punto indicato, con una scala opzionale.</summary>
    public static void DisegnaCentrato(Texture2D texture, Rectangle sorgente,
                                       float centroX, float centroY, float scala = 1f)
    {
        float larghezza = sorgente.Width * scala;
        float altezza = sorgente.Height * scala;
        var destinazione = new Rectangle(centroX - larghezza / 2f, centroY - altezza / 2f, larghezza, altezza);
        Raylib.DrawTexturePro(texture, sorgente, destinazione, Vector2.Zero, 0f, new Color(255, 255, 255, 255));
    }
}
