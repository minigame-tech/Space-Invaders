using Raylib_cs;

namespace Space_Invaders.Game;

public static class AudioSystem
{
    private static Sound _laser;
    private static Sound _explosion;
    private static Sound _hitHurt;

    public static void Init()
    {
        Raylib.InitAudioDevice();
        _laser = Raylib.LoadSound("assets/sound/laserShoot.wav");
        _explosion = Raylib.LoadSound("assets/sound/explosion.wav");
        _hitHurt = Raylib.LoadSound("assets/sound/hitHurt.wav");
        Raylib.SetSoundVolume(_laser, 0.5f);
        Raylib.SetSoundVolume(_explosion, 0.7f);
        Raylib.SetSoundVolume(_hitHurt, 0.8f);
    }

    public static void PlayLaser() => Raylib.PlaySound(_laser);
    public static void PlayExplosion() => Raylib.PlaySound(_explosion);
    public static void PlayHit() => Raylib.PlaySound(_hitHurt);

    public static void Close()
    {
        Raylib.UnloadSound(_laser);
        Raylib.UnloadSound(_explosion);
        Raylib.UnloadSound(_hitHurt);
        Raylib.CloseAudioDevice();
    }
}
