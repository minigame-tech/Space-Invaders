namespace Space_Invaders.Game.Scenes;

/// <summary>Una schermata del gioco (menu, partita, game over...).</summary>
public interface IScene
{
    /// <summary>Aggiorna la scena. Restituisce la scena successiva, oppure null per restare in questa.</summary>
    IScene? Update(float dt);

    void Draw();
}
