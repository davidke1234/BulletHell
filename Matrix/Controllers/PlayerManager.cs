using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Matrix.Controllers
{
    static class PlayerManager
    {
         public static Player GetPlayer(Texture2D player, Texture2D slowmoPlayer, int health, string keysType)
        {
            return new Player(player, slowmoPlayer)
            {
                Position = new Vector2(375, 335),
                Bullet = new Bullet(Arts.Bullet),
                Health = health,
                Score = new Score()
                {
                    PlayerName = "Player1"
                },
                GameKeys = new GameKeys(keysType)
            };
        }
        public static void DrawPlayerStatus(SpriteBatch spriteBatch, Player _player)
        {
            spriteBatch.DrawString(Arts.Font, "Player: " + _player.Score.PlayerName, new Vector2(10f, 10f), Color.White);
            spriteBatch.DrawString(Arts.Font, "Health: " + _player.Health, new Vector2(10f, 30f), Color.White);
            spriteBatch.DrawString(Arts.Font, "Score: " + _player.Score.Value, new Vector2(10f, 50f), Color.White);
        }
    }
}
