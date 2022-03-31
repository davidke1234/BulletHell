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

        public static Enemy GetEnemy(Enemy.Type type)
        {
            Enemy enemy = null;
                        
            if (type == Enemy.Type.FinalBoss)
            {
                enemy = (Enemy)enemyFactory.Create("finalBoss", Enemy.Type.FinalBoss);
            }
            else if (type == Enemy.Type.Boss)
            {
                enemy = (Enemy)enemyFactory.Create("boss", Enemy.Type.Boss);
            }
            else if (type == Enemy.Type.ButterFlyEnemies)
            {
                enemy = (Enemy)enemyFactory.Create("butterflyenemy", Enemy.Type.ButterFlyEnemies);
            }
            else if (type == Enemy.Type.BasicEnemies)
            {
                enemy = (Enemy)enemyFactory.Create("basicEnemy", Enemy.Type.BasicEnemies);
            }
                       
            return enemy;
        }

        #region Phases of game - spawing enemies
        public static IEnumerable<Sprite> GetEnemyPhase1(GameTime gameTime)
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

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase1);
        }

        public static IEnumerable<Sprite> GetEnemyPhase2(GameTime gameTime)
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

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase2);
        }    

        public static IEnumerable<Sprite> GetEnemyPhase3(GameTime gameTime)
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

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase3);
        }

        public static IEnumerable<Sprite> GetEnemyPhase4(GameTime gameTime)
        {
            if (enemiesPhase4.Count == 0)
            {
                enemiesPhase4.Add(new Spawner() { EnemyType = Enemy.Type.FinalBoss, SpawnSeconds = 121 });
           }

            return LoadEnemiesIntoPhase(gameTime, enemiesPhase4);
        }

        private static IEnumerable<Sprite> LoadEnemiesIntoPhase(GameTime gameTime, List<Spawner> spawnedEnemies)
        {
            List<Sprite> enemies = new List<Sprite>();

            //Load enemies each second
            double gameSecond = 0;
            double returnValue = Math.Round(gameTime.TotalGameTime.TotalSeconds, 2) % 1;

            if (returnValue == 0)
            {
                gameSecond = Math.Round(gameTime.TotalGameTime.TotalSeconds, 0);

                //Check if a certain enemy should be sent to view based on time
                Spawner spawner = spawnedEnemies.Find(e => e.SpawnSeconds == gameSecond);
                if (spawner != null)
                    enemies.Add(GetEnemy(spawner.EnemyType));
            }

            return enemies;
        }

        #endregion
    }

}
