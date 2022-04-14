using Matrix.Models;
using Matrix.Models.Enums;
using Matrix.Utilities;
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
        public static void GetEnemyPhase1(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase1.Count == 0)
            {
                for (int i = 1; i < 18; i++)
                {
                    enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = i });
                }
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.ButterflyEnemy, SpawnSeconds = 20 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 22 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 23 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 25 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 26 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 27 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 28 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 29 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.ButterflyEnemy, SpawnSeconds = 32 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 34 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 35 });
                enemiesPhase1.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 36 });
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase1, currentStartGameSeconds));
        }

        public static void GetEnemyPhase2(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase2.Count == 0)
            {
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.MidBoss, SpawnSeconds = 40 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 45 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 46 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 47 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 50 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 51 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 52 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 54 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 55 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 58 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 59 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 60 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 61 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 62 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 67 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 68 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 69 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 70 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 71 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 72 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 73 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 76 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 78 });
                enemiesPhase2.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 79 });
            }

            _sprites.AddRange(LoadEnemiesIntoPhase(enemiesPhase2, currentStartGameSeconds));
        }

        public static void GetEnemyPhase3(List<Sprite> _sprites, double currentStartGameSeconds)
        {
            if (enemiesPhase3.Count == 0)
            {
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.FinalBoss, SpawnSeconds = 81 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 85 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 86 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 90 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 94 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 97 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 100 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 101 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 102 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 103 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 104 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 105 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 106 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 107 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 108 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 109 });
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 115});
                enemiesPhase3.Add(new Spawner() { EnemyType = EnemyType.BasicEnemy, SpawnSeconds = 116});
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

        #endregion
    }

}
