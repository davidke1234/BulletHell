using Matrix.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        static EnemyFactory enemyFactory = new EnemyFactory();

        public static List<Enemy> Enemies = new List<Enemy>();

        #region Phases of game - spawing enemies
        public static void GetEnemyPhase1(GameTime gameTime, List<Sprite> _sprites)
        {
            if (enemiesPhase1.Count == 0)
            {
                for (int i = 1; i < 18; i++)
                {
                    enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = i });
                }
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.ButterFlyEnemies, SpawnSeconds = 20 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 22 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 23 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 25 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 26 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 27 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 28 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 29 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.ButterFlyEnemies, SpawnSeconds = 32 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 34 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 35 });
                enemiesPhase1.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 36 });
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(gameTime, enemiesPhase1));
        }

        public static void GetEnemyPhase2(GameTime gameTime, List<Sprite> _sprites)
        {
            if (enemiesPhase2.Count == 0)
            {
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.Boss, SpawnSeconds = 40 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 45 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 46 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 47 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 50 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 51 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 52 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 54 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 55 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 58 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 59 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 60 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 61 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 62 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 67 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 68 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 69 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 70 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 71 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 72 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 73 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 76 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 78 });
                enemiesPhase2.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 79 });
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(gameTime, enemiesPhase2));
        }

        public static void GetEnemyPhase3(GameTime gameTime, List<Sprite> _sprites)
        {
            if (enemiesPhase3.Count == 0)
            {
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 81 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 85 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 86 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 90 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 94 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 97 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 100 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 101 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 102 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 103 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 104 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 105 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 106 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 107 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 108 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 109 });
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 115});
                enemiesPhase3.Add(new Spawner() { EnemyType = Enemy.Type.BasicEnemies, SpawnSeconds = 116});
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(gameTime, enemiesPhase3));
        }

        public static void GetEnemyPhase4(GameTime gameTime, List<Sprite> _sprites)
        {
            if (enemiesPhase4.Count == 0)
            {
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 121 });
           }

            _sprites.AddRange(LoadEnemiesIntoPhase(gameTime, enemiesPhase4));
        }

        private static IEnumerable<Sprite> LoadEnemiesIntoPhase(GameTime gameTime, List<Spawner> spawnedEnemies)
        {
            List<Sprite> enemies = new List<Sprite>();
            Enemy enemy = null;

            //If gameTime is on the second with no remainder like 1.0034434, add 1 enemy
            double gameSecond = 0;
            double returnValue = Math.Round(gameTime.TotalGameTime.TotalSeconds, 2) % 1;

            if (returnValue == 0)
            {
                gameSecond = Math.Round(gameTime.TotalGameTime.TotalSeconds, 0);

                //Check if a certain enemy should be sent to view based on time
                Spawner spawner = spawnedEnemies.Find(e => e.SpawnSeconds == gameSecond);
                if (spawner != null)
                {
                    //  enemies.Add(enemyFactory.Create(spawner.EnemyType));

                    switch (spawner.EnemyType)
                    {
                        case Enemy.Type.BasicEnemies:
                            enemy = enemyFactory.CreateBasicEnemy();
                            break;
                        case Enemy.Type.ButterFlyEnemies:
                            enemy = enemyFactory.ButterFlyEnemy();
                            break;
                        case Enemy.Type.Boss:
                            enemy = enemyFactory.CreatMidBossEnemy();
                            break;
                        case Enemy.Type.FinalBoss:
                            enemy = enemyFactory.CreateFinalBossEnemy();
                            break;
                        default:
                            enemy = enemyFactory.CreateBasicEnemy();
                            break;
                    }

                    enemies.Add(enemy);

                    //store enemies big list to be used in collision detection
                   // Enemies.Add(enemy);
                }
            }         

            return enemies;
        }

        #endregion
    }

}
