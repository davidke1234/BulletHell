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
        public override Sprite Create(string name, Enemy.Type? basicEnemyType = null)
        {
            switch (name.ToLower())
            {
                case "bullet":
                    return new Bullet(Arts.Bullet);
                case "orangebullet":
                    return new Bullet(Arts.BulletOrange);
                case "bomb":
                    return new Bomb(Arts.Bomb);
                case "bomb2":
                    return new Bomb(Arts.Bomb2);
                default:
                    throw new Exception("Invalid object type requested");
            }
        }

        private IEnumerable<System.Type> GetAllProjectiles()
        {
            System.Type projectileType = typeof(Projectile);
            return Assembly.GetAssembly(projectileType).GetTypes().Where(TheType => TheType.IsClass && TheType.IsSubclassOf(projectileType));
        }
    }
}
