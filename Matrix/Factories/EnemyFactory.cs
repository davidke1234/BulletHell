﻿using Matrix.Models;
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
            switch(name.ToLower())
            {
                case "basicenemy":
                    Enemy newBasicEnemy = GetBasicEnemyType(basicEnemyType);
                    return newBasicEnemy;
                case "butterflyenemy":
                    return new ButterflyEnemy(Arts.EnemyButterfly);
                default:
                    throw new Exception("Invalid object type requested");
            }
        }

        private Enemy GetBasicEnemyType(Enemy.Type? typeOfBasicEnemy)
        {
            switch (typeOfBasicEnemy)
            {
                case Enemy.Type.BasicEnemies:
                    {
                        return new BasicEnemy(Arts.EnemyBlack, Enemy.Type.BasicEnemies);
                    }
                case Enemy.Type.ButterFlyEnemies:
                    {
                        return new BasicEnemy(Arts.EnemyBlack, Enemy.Type.ButterFlyEnemies);
                    }
                case Enemy.Type.Boss:
                    {
                        return new BasicEnemy(Arts.EnemyBlack, Enemy.Type.Boss);
                    }
                case Enemy.Type.FinalBoss:
                    {
                        return new BasicEnemy(Arts.EnemyBlack, Enemy.Type.FinalBoss);
                    }
                default:
                    throw new ArgumentException("The provided type does not exist.");
            }
        }
    }
}
