using Matrix.Models.Enums;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static Matrix.Models.Enemy;

namespace Matrix.Models.Factories
{
    public class ProjectileFactory: SpriteFactoryProvider
    {
        public override Sprite Create(string name, Texture2D texture)
        {
            return CreateProjectileObject(name, texture);
        }

        private Projectile CreateProjectileObject(string name, Texture2D texture)
        {
            var type = GetAllProjectiles().Where(t => t.Name == name).SingleOrDefault();
            if (type != null)
            {
                return (Projectile)Activator.CreateInstance(type, texture);
            }
            else
            {
                throw new ArgumentException("Unsupported projectile type requested");
            }
        }

        private IEnumerable<System.Type> GetAllProjectiles()
        {
            System.Type projectileType = typeof(Projectile);
            return Assembly.GetAssembly(projectileType).GetTypes().Where(TheType => TheType.IsClass && TheType.IsSubclassOf(projectileType));
        }
    }
}
