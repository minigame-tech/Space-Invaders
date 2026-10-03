using Raylib_cs;
using Space_Invaders;

const int LarghezzaSchermo = 800;
const int AltezzaSchermo   = 600;
const int FPS = 60;

Raylib.InitWindow(LarghezzaSchermo, AltezzaSchermo, "Space Invaders - test");
Raylib.SetTargetFPS(FPS);

//Caricamento delle Texture
Texture2D sprite = Raylib.LoadTexture("assets/img/invaders2.png");
if (sprite.Id == 0)
    Console.WriteLine("ATTENZIONE: sprite non trovato. Controlla il percorso e 'Copy to output directory'");
    
// Pixel art nitida (niente sfocatura quando viene ridimensionata).
Raylib.SetTextureFilter(sprite, TextureFilter.Point);
 
// Cannone verde dentro invaders2.png: x=356, y=1161, 104x64 px.
// Non abbiamo un secondo frame, quindi usiamo lo stesso rettangolo per entrambi.
var cannone = new Rectangle(356, 1161, 104, 64);
 
var giocatore = new Giocatore(
    sprite,
    frameFermo: cannone,
    frameSparo: cannone,
    startX: LarghezzaSchermo / 2f - Giocatore.Larghezza / 2f,
    startY: AltezzaSchermo - Giocatore.Altezza - 20);
 
while (!Raylib.WindowShouldClose())
{
    float dt = Raylib.GetFrameTime();
 
    giocatore.GestisciInput(dt, LarghezzaSchermo);
    giocatore.Aggiorna(dt);
 
    // Tasto di prova: K per perdere una vita e testare Muori()/Respawn().
    if (Raylib.IsKeyPressed(KeyboardKey.K))
        giocatore.Muori();
 
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
 
    if (giocatore.Vivo)
        giocatore.Disegna();
    else
        Raylib.DrawText("GAME OVER", 300, 280, 40, Color.Red);
 
    Raylib.DrawText($"Vite: {giocatore.Vite}", 10, 10, 20, Color.White);
    Raylib.DrawText("Frecce/A-D: muovi | Spazio: spara | K: muori", 10, 570, 16, Color.Gray);
    Raylib.DrawFPS(LarghezzaSchermo - 90, 10);
    Raylib.EndDrawing();
}
 
Raylib.UnloadTexture(sprite);
Raylib.CloseWindow();