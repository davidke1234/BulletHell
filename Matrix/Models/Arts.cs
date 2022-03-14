using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Matrix
{
    /// <summary>
    /// Class for adding arts.
    /// </summary>
    static class Arts
    {
        public static Texture2D Stars;
        public static Texture2D Bomb;
        public static Texture2D Bomb2;
        public static Texture2D Boss;
        public static Texture2D Boss2;
        public static Texture2D Player;
        public static Texture2D SlowmoPlayer;
        public static Song Song1;
        public static Texture2D Bullet;
        public static Texture2D EnemyBlood;
        public static Texture2D EnemyGreen;
        public static Texture2D EnemyBlack;
        public static Texture2D EnemyBlue;
        public static Texture2D EnemyButterfly;
        public static Texture2D EnemyGrumpBird;
        public static Texture2D BulletRed;
        public static Texture2D BulletBlue;
        public static Texture2D BulletBlack;
        public static Texture2D BulletGreen;
        public static Texture2D BulletOrange;
        public static Texture2D Button;
        public static SpriteFont Font;
        public static Texture2D MainMenuBackGround;


        public static void Load(ContentManager content)
        {
            Stars = content.Load<Texture2D>("Stars");
            Bomb = content.Load<Texture2D>("bomb");
            Bomb2 = content.Load<Texture2D>("Bomb2");
            Boss = content.Load<Texture2D>("boss");
            Boss2 = content.Load<Texture2D>("boss2");
            Player = content.Load<Texture2D>("player_ship");
            SlowmoPlayer = content.Load<Texture2D>("slowmoShip");
            Song1 = content.Load<Song>("sample1");
            Bullet = content.Load<Texture2D>("Bullet");
            EnemyBlood = content.Load<Texture2D>("dngn_blood_fountain");
            EnemyBlue = content.Load<Texture2D>("dngn_blue_fountain");
            EnemyGreen = content.Load<Texture2D>("dngn_green_fountain");
            EnemyBlack = content.Load<Texture2D>("dngn_black_fountain");
            EnemyButterfly = content.Load<Texture2D>("GrumpBird");
            BulletRed = content.Load<Texture2D>("BulletRed");
            BulletBlue = content.Load<Texture2D>("BulletBlue");
            BulletBlack = content.Load<Texture2D>("BulletBlack");
            BulletGreen = content.Load<Texture2D>("BulletGreen");
            BulletOrange = content.Load<Texture2D>("BulletOrange");
            Button = content.Load<Texture2D>("Button");
            Font = content.Load<SpriteFont>("Font");
            MainMenuBackGround = content.Load<Texture2D>("MainMenu_backGround");
        }
    }
}
