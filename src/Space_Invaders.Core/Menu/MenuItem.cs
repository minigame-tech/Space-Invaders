namespace Space_Invaders.Core;

/// <summary>Azione associata a una voce di menu.</summary>
public enum MenuAzione
{
    Gioca,
    MenuPrincipale,
    Esci
}

/// <summary>Una voce di menu: testo da mostrare e azione da eseguire alla conferma.</summary>
public sealed record MenuItem(string Etichetta, MenuAzione Azione);
