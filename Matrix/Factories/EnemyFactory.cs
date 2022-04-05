using Matrix.Models;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Matrix
{
    public class EnemyFactory : SpriteFactoryProvider
    {
        public override Sprite Create(string name, Enemy.Type? basicEnemyType)
        {
            return Create(basicEnemyType);
        }

        public Enemy Create(Enemy.Type? typeOfBasicEnemy)
        {
            switch (typeOfBasicEnemy)
            {
                case Enemy.Type.BasicEnemies:
                    {
                        return new BasicEnemy(GetRandomTexture(), Enemy.Type.BasicEnemies);
                    }
                case Enemy.Type.ButterFlyEnemies:
                    {
                        return new ButterflyEnemy(Arts.EnemyButterfly);
                    }
                case Enemy.Type.MidBoss:
                    {
                        return new MidBoss(Arts.MidBoss);
                    }
                case Enemy.Type.FinalBoss:
                    {
                        return new FinalBoss(Arts.FinalBoss);
                    }
                default:
                    throw new ArgumentException("The provided type does not exist.");
            }
        }

        public Enemy CreateBasicEnemy()
        {
            return new BasicEnemy(GetRandomTexture(), Enemy.Type.BasicEnemies);
        }
        public Enemy ButterFlyEnemy()
        {
            return new ButterflyEnemy(Arts.EnemyButterfly);
        }
        public Enemy CreatMidBossEnemy()
        {
            return new MidBoss(Arts.MidBoss);
        }
        public Enemy CreateFinalBossEnemy()
        {
            return new FinalBoss(Arts.FinalBoss);
        }



        private Texture2D GetRandomTexture()
        {
            Random random = new Random();
            List<Texture2D> textures = new List<Texture2D>()
            {
                Arts.EnemyBlack,
                Arts.EnemyBlood,
                Arts.EnemyBlue,
                Arts.EnemyGreen,
            };
            return textures[random.Next(0, textures.Count)];
        }
    }
}
