using System;
using System.Collections.Generic;
using Raylib_cs;
using System.Numerics;

namespace Space_Invaders.Game.UI;

public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public float Life;
    public float MaxLife;
    public float Size;
}

public class Star
{
    public Vector2 Position;
    public float Speed;
    public Color Color;
    public float Size;
}

public class VisualEffects
{
    private readonly List<Particle> _particles = new();
    private readonly List<Star> _stars = new();
    private readonly Random _random = new();
    
    // Screen shake
    private float _shakeTimer;
    private float _shakeIntensity;

    public VisualEffects()
    {
        for (int i = 0; i < 100; i++)
        {
            _stars.Add(new Star
            {
                Position = new Vector2(_random.Next(0, Core.Campo.Larghezza), _random.Next(0, Core.Campo.Altezza)),
                Speed = (float)_random.NextDouble() * 50 + 10,
                Size = (float)_random.NextDouble() * 2 + 1,
                Color = new Color(255, 255, 255, _random.Next(100, 255))
            });
        }
    }

    public void Update(float dt)
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= dt;
            if (_shakeTimer < 0) _shakeTimer = 0;
        }

        foreach (var star in _stars)
        {
            star.Position.Y += star.Speed * dt;
            if (star.Position.Y > Core.Campo.Altezza)
            {
                star.Position.Y = 0;
                star.Position.X = _random.Next(0, Core.Campo.Larghezza);
            }
        }

        for (int i = _particles.Count - 1; i >= 0; i--)
        {
            var p = _particles[i];
            p.Life -= dt;
            if (p.Life <= 0)
            {
                _particles.RemoveAt(i);
                continue;
            }
            p.Position += p.Velocity * dt;
            // Gravity or drag could be added here
        }
    }

    public void AddExplosion(float x, float y, Color color, int count = 20)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = (float)(_random.NextDouble() * Math.PI * 2);
            float speed = (float)(_random.NextDouble() * 100 + 50);
            _particles.Add(new Particle
            {
                Position = new Vector2(x, y),
                Velocity = new Vector2((float)Math.Cos(angle) * speed, (float)Math.Sin(angle) * speed),
                Color = color,
                MaxLife = 0.5f + (float)_random.NextDouble() * 0.5f,
                Life = 0.5f + (float)_random.NextDouble() * 0.5f,
                Size = (float)_random.NextDouble() * 4 + 2
            });
        }
    }

    public void ShakeScreen(float duration, float intensity)
    {
        _shakeTimer = duration;
        _shakeIntensity = intensity;
    }

    public void BeginShake()
    {
        if (_shakeTimer > 0)
        {
            float offsetX = (float)(_random.NextDouble() * 2 - 1) * _shakeIntensity * (_shakeTimer / 0.5f);
            float offsetY = (float)(_random.NextDouble() * 2 - 1) * _shakeIntensity * (_shakeTimer / 0.5f);
            // Limit shake
            Raylib.BeginMode2D(new Camera2D { Target = Vector2.Zero, Offset = new Vector2(offsetX, offsetY), Rotation = 0, Zoom = 1 });
        }
    }

    public void EndShake()
    {
        if (_shakeTimer > 0)
            Raylib.EndMode2D();
    }

    public void DrawBackground()
    {
        foreach (var star in _stars)
        {
            Raylib.DrawRectangleV(star.Position, new Vector2(star.Size, star.Size), star.Color);
        }
    }

    public void DrawForeground()
    {
        // Usa Additive blending per i particellari!
        Raylib.BeginBlendMode(BlendMode.Additive);
        foreach (var p in _particles)
        {
            float alpha = p.Life / p.MaxLife;
            var c = new Color(p.Color.R, p.Color.G, p.Color.B, (byte)(255 * alpha));
            Raylib.DrawCircleV(p.Position, p.Size * alpha, c);
        }
        Raylib.EndBlendMode();
    }
}
