using Matrix.Models;
using Matrix.Models.Enums;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Matrix
{
    public class EnemyFactory : SpriteFactoryProvider
    {
        public override Sprite Create(string name, Texture2D texture)
        {
            return CreateEnemyObject(name, texture);
        }

        private Enemy CreateEnemyObject(string name, Texture2D texture)
        {
            var type = GetAllEnemyTypes().Where(t => t.Name == name).SingleOrDefault();
            if (type != null)
            {
                return (Enemy)Activator.CreateInstance(type, texture);
            }
            else
            {
                throw new ArgumentException("Unsupported Enemy Type requestd");
            }                        
        }

        private IEnumerable<System.Type> GetAllEnemyTypes()
        {            
            System.Type enemyTypes = typeof(Enemy);
            var allTypes = Assembly.GetAssembly(enemyTypes).GetTypes().Where(TheType => TheType.IsClass && TheType.IsSubclassOf(enemyTypes));
            return allTypes;
        }
    }
}
