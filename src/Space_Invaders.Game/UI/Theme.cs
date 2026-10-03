using Raylib_cs;

namespace Space_Invaders.Game.UI;

/// <summary>Colori e dimensioni dei font in un unico posto: per cambiare stile basta modificare questo file.</summary>
public static class Theme
{
    public static readonly Color Sfondo = new(0, 0, 0, 255);
    public static readonly Color Primario = new(255, 255, 255, 255);
    public static readonly Color Secondario = new(150, 150, 150, 255);
    public static readonly Color Evidenziato = new(0, 255, 0, 255);
    public static readonly Color Errore = new(255, 70, 70, 255);

    public static readonly Color ColoreScudo = new(204, 204, 170, 255);
    public static readonly Color ColoreProiettileInvasori = new(255, 235, 90, 255);
    public static readonly Color ColoreTerreno = new(0, 255, 0, 255);

    public const int FontTitolo = 56;
    public const int FontMenu = 28;
    public const int FontHud = 20;
    public const int FontPiccolo = 16;
}
