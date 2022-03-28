﻿using Matrix.Models;
using Matrix.Models.Factories;
using Matrix.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static Matrix.Models.Enemy;

namespace Matrix
{
    public abstract class SpriteFactoryProvider
    {
        public abstract Sprite Create(string name, Enemy.Type? basicEnemyType= null);

        private SpriteFactoryProvider GetFactory(Sprite sprite)
        {
            var enemyType = typeof(Enemy);
            var projectileType = typeof(Projectile);
            if(sprite.GetType().Name == enemyType.Name)
            {
                return new EnemyFactory();
            }
            else
            {
                return new ProjectileFactory();
            }
        }
    }
}
