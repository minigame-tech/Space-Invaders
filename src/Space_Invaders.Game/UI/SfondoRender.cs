using System.Numerics;
using Raylib_cs;
using Space_Invaders.Core;

namespace Space_Invaders.Game.UI;

/// <summary>Disegna un'immagine di sfondo adattata alla finestra.</summary>
public static class SfondoRenderer
{
    /// <summary>
    /// Modalità "cover": l'immagine riempie tutta la finestra mantenendo le proporzioni
    /// (se non coincidono, i bordi in eccesso vengono tagliati) ed è centrata.
    /// </summary>
    /// <param name="oscuramento">0 = nessuno, 255 = nero pieno. Aiuta a leggere il testo sopra lo sfondo.</param>
    public static void Disegna(Texture2D texture, byte oscuramento = 110)
    {
        if (texture.Id == 0)
            return;  // immagine non caricata: resta lo sfondo nero

        float scala = Math.Max((float)Campo.Larghezza / texture.Width,
            (float)Campo.Altezza / texture.Height);
        float larghezza = texture.Width * scala;
        float altezza = texture.Height * scala;

        var sorgente = new Rectangle(0, 0, texture.Width, texture.Height);
        var destinazione = new Rectangle((Campo.Larghezza - larghezza) / 2f,
            (Campo.Altezza - altezza) / 2f,
            larghezza, altezza);

        Raylib.DrawTexturePro(texture, sorgente, destinazione, Vector2.Zero, 0f,
            new Color(255, 255, 255, 255));

        if (oscuramento > 0)
            Raylib.DrawRectangle(0, 0, Campo.Larghezza, Campo.Altezza, new Color(0, 0, 0, (int)oscuramento));
    }
}