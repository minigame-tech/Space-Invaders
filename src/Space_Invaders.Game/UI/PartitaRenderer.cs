using Raylib_cs;
using Space_Invaders.Core;

namespace Space_Invaders.Game.UI;

/// <summary>Disegna lo stato di una <see cref="Partita"/>: scudi, invasori, nave, proiettili e HUD.</summary>
public sealed class PartitaRenderer
{
    private readonly Texture2D _sprite;

    public PartitaRenderer(Texture2D sprite) => _sprite = sprite;

    public void Disegna(Partita partita, int record)
    {
        DisegnaScudi(partita);
        DisegnaInvasori(partita);
        DisegnaUfo(partita.Ufo);
        DisegnaGiocatore(partita.Giocatore);
        DisegnaProiettili(partita);
        DisegnaHud(partita, record);
    }

    private static void DisegnaScudi(Partita partita)
    {
        int cella = (int)Scudo.Cella;
        foreach (var scudo in partita.Scudi)
            for (int c = 0; c < Scudo.Colonne; c++)
                for (int r = 0; r < Scudo.Righe; r++)
                    if (scudo.CellaViva(c, r))
                        Raylib.DrawRectangle((int)(scudo.X + c * Scudo.Cella),
                                             (int)(scudo.Y + r * Scudo.Cella),
                                             cella, cella, Theme.ColoreScudo);
    }

    private void DisegnaInvasori(Partita partita)
    {
        int frame = partita.Formazione.Frame;
        foreach (var invasore in partita.Formazione.Invasori)
        {
            if (!invasore.Vivo)
                continue;

            SpriteAtlas.DisegnaCentrato(_sprite, SpriteAtlas.Invasori[invasore.Tipo][frame],
                                        invasore.CentroX, invasore.Y + Invasore.Altezza / 2f);
        }
    }

    private void DisegnaUfo(Ufo ufo)
    {
        if (!ufo.Attivo)
            return;

        SpriteAtlas.DisegnaCentrato(_sprite, SpriteAtlas.Ufo,
                                    ufo.X + Ufo.Larghezza / 2f, ufo.Y + Ufo.Altezza / 2f, 0.25f);
    }

    private void DisegnaGiocatore(Giocatore giocatore)
    {
        // Dopo essere stati colpiti la nave lampeggia.
        bool visibile = !giocatore.Invulnerabile || (int)(Raylib.GetTime() * 12) % 2 == 0;
        if (!visibile)
            return;

        SpriteAtlas.DisegnaCentrato(_sprite, SpriteAtlas.Giocatore,
                                    giocatore.X + Giocatore.Larghezza / 2f,
                                    giocatore.Y + Giocatore.Altezza / 2f);
    }

    private void DisegnaProiettili(Partita partita)
    {
        Raylib.BeginBlendMode(BlendMode.Additive);

        if (partita.ProiettileGiocatore is { } colpoGiocatore)
        {
            // Glow effect
            Raylib.DrawRectangle((int)colpoGiocatore.X - 4, (int)colpoGiocatore.Y - 4,
                (int)Proiettile.Larghezza + 8, (int)Proiettile.Altezza + 8,
                new Color(0, 255, 255, 100)); // Cyan glow

            var destinazione = new Rectangle(colpoGiocatore.X, colpoGiocatore.Y,
                Proiettile.Larghezza, Proiettile.Altezza);
            Raylib.DrawTexturePro(_sprite, SpriteAtlas.Laser, destinazione,
                System.Numerics.Vector2.Zero, 0f, new Color(255, 255, 255, 255));
        }

        foreach (var colpoInvasore in partita.ProiettiliInvasori)
        {
            // Glow effect
            Raylib.DrawRectangle((int)colpoInvasore.X - 4, (int)colpoInvasore.Y - 4,
                (int)Proiettile.Larghezza + 8, (int)Proiettile.Altezza + 8,
                new Color((int)Theme.ColoreProiettileInvasori.R, (int)Theme.ColoreProiettileInvasori.G, (int)Theme.ColoreProiettileInvasori.B, 100));

            Raylib.DrawRectangle((int)colpoInvasore.X, (int)colpoInvasore.Y,
                (int)Proiettile.Larghezza, (int)Proiettile.Altezza,
                Theme.ColoreProiettileInvasori);
        }
        
        Raylib.EndBlendMode();
    }

    private void DisegnaHud(Partita partita, int record)
    {
        Raylib.DrawText($"PUNTI {partita.Punteggio:D5}", 20, 10, Theme.FontHud, Theme.Primario);
        Testo.Centrato($"RECORD {Math.Max(record, partita.Punteggio):D5}", 10, Theme.FontHud, Theme.Primario);

        string livello = $"LIVELLO {partita.Livello}";
        int larghezzaLivello = Raylib.MeasureText(livello, Theme.FontHud);
        Raylib.DrawText(livello, Campo.Larghezza - 20 - larghezzaLivello, 10, Theme.FontHud, Theme.Primario);

        // Linea del terreno e vite rimaste.
        Raylib.DrawLine(0, (int)Campo.YTerreno, Campo.Larghezza, (int)Campo.YTerreno, Theme.ColoreTerreno);
        Raylib.DrawText("VITE", 20, 582, Theme.FontPiccolo, Theme.Primario);
        for (int i = 0; i < partita.Giocatore.Vite; i++)
            SpriteAtlas.DisegnaCentrato(_sprite, SpriteAtlas.Giocatore, 85f + i * 24f, 591f, 0.5f);
    }
}
