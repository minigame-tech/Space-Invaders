using Raylib_cs;
using Space_Invaders.Core;

namespace Space_Invaders.Game.UI;

public static class MenuRenderer
{
    /// <summary>Disegna le voci del menu centrate, evidenziando quella selezionata.</summary>
    public static void Disegna(Menu menu, int yInizio, int spaziatura = 48)
    {
        for (int i = 0; i < menu.Voci.Count; i++)
        {
            bool selezionata = i == menu.IndiceSelezionato;
            string etichetta = menu.Voci[i].Etichetta;
            Color colore = selezionata ? Theme.Evidenziato : Theme.Primario;

            int larghezza = Raylib.MeasureText(etichetta, Theme.FontMenu);
            int x = (Campo.Larghezza - larghezza) / 2;
            int y = yInizio + i * spaziatura;

            Raylib.DrawText(etichetta, x, y, Theme.FontMenu, colore);
            if (selezionata)
                Raylib.DrawText(">", x - 32, y, Theme.FontMenu, colore);
        }
    }
}
