using Raylib_cs;
using Space_Invaders.Core;

namespace Space_Invaders.Game.UI;

public static class MenuInput
{
    /// <summary>
    /// Gestisce frecce/W/S per spostare la selezione. Se si preme Invio restituisce l'azione scelta,
    /// altrimenti null.
    /// </summary>
    public static MenuAzione? Leggi(Menu menu)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Up) || Raylib.IsKeyPressed(KeyboardKey.W))
            menu.Su();
        if (Raylib.IsKeyPressed(KeyboardKey.Down) || Raylib.IsKeyPressed(KeyboardKey.S))
            menu.Giu();

        if (Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.KpEnter))
            return menu.Selezionata.Azione;

        return null;
    }
}
