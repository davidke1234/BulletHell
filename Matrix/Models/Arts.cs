using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Matrix.Models
{
    /// <summary>
    /// Class for adding arts.
    /// </summary>
    static class Arts
    {
        public static Texture2D Stars;
        public static Texture2D Bomb;
        public static Texture2D Bomb2;
        public static Texture2D FinalBoss;
        public static Texture2D MidBoss;
        public static Texture2D Player;
        public static Texture2D SlowmoPlayer;
        public static Song Song0;
        public static Song Song1;
        public static Song Song2;
        public static Song Song3;
        public static Song Song4;
        public static Song Song5;
        public static Song Song6;
        public static Song Song7;
        public static Texture2D Bullet;
        public static Texture2D EnemyBlood;
        public static Texture2D EnemyGreen;
        public static Texture2D EnemyBlack;
        public static Texture2D EnemyBlue;
        public static Texture2D EnemyButterfly;
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
            FinalBoss = content.Load<Texture2D>("finalBoss");
            MidBoss = content.Load<Texture2D>("midBoss");
            Player = content.Load<Texture2D>("player_ship");
            SlowmoPlayer = content.Load<Texture2D>("slowmoShip");
           // Song0 = content.Load<Song>("mixkit-medieval-show-fanfare-announcement-226");
            Song1 = content.Load<Song>("mixkit-cat-walk-371");
            Song2 = content.Load<Song>("mixkit-daredevil-80");
            Song3 = content.Load<Song>("mixkit-infected-vibes-157");
            Song4 = content.Load<Song>("mixkit-punked-up-fun-47");
            Song5 = content.Load<Song>("mixkit-rock-the-game-49");
            Song6 = content.Load<Song>("mixkit-sports-highlights-51");
            Song7 = content.Load<Song>("mixkit-techno-fights-171");
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
