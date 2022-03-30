using Matrix.Models;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Matrix.Models.Enemy;

namespace Matrix
{
    public class EnemyFactory : SpriteFactoryProvider
    {
        public override Sprite Create(string name, Enemy.Type? basicEnemyType)
        {
            return GetBasicEnemyType(basicEnemyType);

            //switch (name.ToLower())
            //{
            //    case "basicenemy":
            //        return GetBasicEnemyType(basicEnemyType);
            //    case "butterflyenemy":
            //        return GetBasicEnemyType(basicEnemyType);
            //    case "boss":
            //        return GetBasicEnemyType(basicEnemyType);
            //    default:
            //        throw new Exception("Invalid object type requested");
            //}
        }

        private Enemy GetBasicEnemyType(Enemy.Type? typeOfBasicEnemy)
        {
            switch (typeOfBasicEnemy)
            {
                case Enemy.Type.BasicEnemies:
                    {
                        return new BasicEnemy(GetRandomTexture(), Enemy.Type.BasicEnemies);
                    }
                case Enemy.Type.ButterFlyEnemies:
                    {
                        return new BasicEnemy(Arts.EnemyButterfly, Enemy.Type.ButterFlyEnemies);
                    }
                case Enemy.Type.Boss:
                    {
                        return new BasicEnemy(Arts.Boss2, Enemy.Type.Boss);
                    }
                case Enemy.Type.FinalBoss:
                    {
                        return new BasicEnemy(Arts.Boss, Enemy.Type.FinalBoss);
                    }
                default:
                    throw new ArgumentException("The provided type does not exist.");
            }
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
