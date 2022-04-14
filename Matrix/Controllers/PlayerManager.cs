using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix.Controllers
{
    static class PlayerManager
    {
        static SpriteFactoryProvider bulletFactory;
        static double _spawnTimer = 0;
        static Player _player = null;

        public static Player GetPlayer(Texture2D player, Texture2D slowmoPlayer, int health, string keysType)
        {

            bulletFactory = SpriteFactoryProvider.GetFactory(typeof(Bullet).Name);
            _player = new Player(player, slowmoPlayer)
            {
                Position = new Vector2(375, 335),
                Bullet = bulletFactory.Create("Bullet", Arts.Bullet),
                Health = health,
                Score = new Score()
                {  },
                PlayerName = GetUserName()      ,
                GameKeys = new GameKeys(keysType)
            };
            return _player;
        }
        public static void DrawPlayerStatus(SpriteBatch spriteBatch, Player _player)
        {
            spriteBatch.DrawString(Arts.Font, "Player: " + _player.PlayerName, new Vector2(10f, 10f), Color.White);
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

        internal static void InsertScore(int score)
        {
            string retVal = "";
             DataAccessLayer.InsertHighScores(_player.PlayerName, score, ref retVal);
        }

        private static string GetUserName()
        {
            string name = Environment.UserName;
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
            }
            return name;
        }
    }
}
