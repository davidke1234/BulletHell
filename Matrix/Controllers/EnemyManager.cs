using Matrix.Controllers;
using Matrix.Models;
using Matrix.Models.Enums;
using Matrix.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NLog;
using System;
using System.Collections.Generic;

namespace Matrix
{
    static class EnemyManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        static List<Spawner> enemiesPhase1 = new List<Spawner>();
        static List<Spawner> enemiesPhase2 = new List<Spawner>();
        static List<Spawner> enemiesPhase3 = new List<Spawner>();
        static List<Spawner> enemiesPhase4 = new List<Spawner>();

        public static List<Enemy> Enemies = new List<Enemy>();
        internal static bool UsedEnabledKillEnemiesCheat;

        #region Phases of game - spawing enemies
        public static void GetEnemyPhase1(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase1.Count == 0)
            {
                for (int i = 1; i < 38; i++)
                {
                    if (i == 5 || i == 10 || i == 20 || i == 32)
                    {
                        enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.ButterflyEnemy, SpawnSeconds = i });
                    }
                    else
                    {
                        enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = i });
                    }
                }
            }
            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase1, currentStartGameSeconds));
        }

        public static void GetEnemyPhase2(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase2.Count == 0)
            {
                for (int i = 39; i < 80; i++)
                {
                    if (i == 40)
                    {
                        enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.MidBoss, SpawnSeconds = i });
                    }
                    else
                    {
                        enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = i });
                    }
                }
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase2, currentStartGameSeconds));
        }

        public static void GetEnemyPhase3(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase3.Count == 0)
            {
                for (int i = 82; i < 119; i++)
                {
                    if (i == 88)
                    {
                        enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.FinalBoss, SpawnSeconds = i });
                    }
                    else
                    {
                        enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = i });
                    }
                }
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase3, currentStartGameSeconds));
        }

        public static void GetEnemyPhase4(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase4.Count == 0)
            {
                enemiesPhase4.Add(new Spawner() { EnemyType = EnemyType.FinalBoss, SpawnSeconds = 121 });
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase4, currentStartGameSeconds));
        }

        private static IEnumerable<Sprite> LoadEnemiesIntoPhase(List<Spawner> spawnedEnemies, double currentStartGameSeconds)
        {
            SpriteFactoryProvider enemyFactory = SpriteFactoryProvider.GetFactory(typeof(Enemy).Name);
            List<Sprite> enemies = new List<Sprite>();
            Sprite enemy = null;

            //If gameTime is on the second with no remainder like 1.0034434, add 1 enemy
            double gameSecond = 0;
            double returnValue = Math.Round(currentStartGameSeconds, 2) % 1;

            if (returnValue == 0)
            {
                gameSecond = Math.Round(currentStartGameSeconds, 0);

                //Check if a certain enemy should be sent to view based on time
                Spawner spawner = spawnedEnemies.Find(e => e.SpawnSeconds == gameSecond);
                if (spawner != null)
                {
                    switch (spawner.EnemyType)
                    {
                        case EnemyType.BasicEnemy:
                            enemy = enemyFactory.Create(spawner.EnemyType.ToString(), MatrixExtensions.GetRandomTexture());
                            break;
                        case EnemyType.ButterflyEnemy:
                            enemy = enemyFactory.Create(spawner.EnemyType.ToString(), Arts.EnemyButterfly);
                            break;
                        case EnemyType.MidBoss:
                            enemy = MidBoss.GetInstance;
                            break;
                        case EnemyType.FinalBoss:
                            enemy = FinalBoss.GetInstance;
                            break;
                        default:
                            enemy = enemyFactory.Create(spawner.EnemyType.ToString(), MatrixExtensions.GetRandomTexture());
                            break;
                    }

                    enemies.Add(enemy);
                }
            }

            return enemies;
        }

        public static void CheckForKillEnemiesCheat(List<Sprite> sprites)
        {
            if (GameManager.EnabledKillEnemiesCheat && Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                // kill all enemies on the screen and add to score
                //if (!EnemyManager.UsedEnabledKillEnemiesCheat)
                {
                    EnemyManager.UsedEnabledKillEnemiesCheat = true;
                    Sprite playerSprite = sprites.Find(p => p is Player);

                    foreach (Sprite sprite in sprites)
                    {
                        if (sprite is Enemy && !sprite.IsRemoved)
                        {
                            int score = GetEnemyScoreValue(sprite.Name);
                            PlayerManager.AddToPlayerScore(score);
                            sprite.IsRemoved = true;
                        }
                    }
                }
            }
        }
    

        public static int GetEnemyScoreValue(string name)
        {
            int scoreValue;
            name = name.ToLower();
            switch (name)
            {
                case "finalboss":
                    scoreValue = 15;
                    break;
                case "midboss":
                    scoreValue = 10;
                    break;
                case "grumpbird":
                    scoreValue = 5;
                    break;
                default:
                    scoreValue = 1;
                    break;
            }

            return scoreValue;
        }

        #endregion
    }

}
