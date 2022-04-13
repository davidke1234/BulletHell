using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix.Controllers
{
    static class PlayerManager
    {
        static double _spawnTimer = 0;

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

        internal static void Respawn(List<Sprite> sprites)
        {
            for (int i=0; i<sprites.Count; i++)
            {
                if (sprites[i] is Bullet || sprites[i] is Bomb)
                {
                    sprites[i].IsRemoved = true;
                }
            }         
        }

        internal static bool ResetPlayer(GameTime gameTime)
        {
            bool resetSpawning = false;

            if (_spawnTimer == 0)
            {
                _spawnTimer = gameTime.TotalGameTime.TotalSeconds;
            }
            else if (gameTime.TotalGameTime.TotalSeconds - _spawnTimer > 2)
            {
                _spawnTimer = 0;
                resetSpawning = true;
            }

            return resetSpawning;
        }

        internal static void InsertScore(string name, int score)
        {
            string retVal = "";
             DataAccessLayer.InsertHighScores(GetUserName(), score, ref retVal);
           }

        internal static string GetUserName()
        {
            return Environment.UserName;
        }
    }
}
