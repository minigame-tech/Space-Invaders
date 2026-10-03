using Raylib_cs;
using Space_Invaders.Core;

namespace Space_Invaders.Game.UI;

public static class Testo
{
    /// <summary>Disegna il testo centrato orizzontalmente nella finestra.</summary>
    public static void Centrato(string testo, int y, int dimensione, Color colore)
    {
        int larghezza = Raylib.MeasureText(testo, dimensione);
        Raylib.DrawText(testo, (Campo.Larghezza - larghezza) / 2, y, dimensione, colore);
    }
}
