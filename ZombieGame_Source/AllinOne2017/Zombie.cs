using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using C3.XNA;
using Microsoft.Xna.Framework.Audio;

namespace AllinOne2017
{
    class Zombie : DrawableGameComponent
    {
        public enum Directions { up, down, left, right, space };
        const int PIXSIZE_WIDTH = 40;
        const int PIXSIZE_HEIGHT = 70;
        const int SCALE = 2;
        const int STANDFRAME = 0;
        const int FIRSTRUNFRAME = 1;
        const int RUNFRAMES = 4;
        const int SPEED = 1;
        const int FRAMEDELAYCOUNTER = 8;
        int frameDelay = 0;
        float rotation = 0f;
        string direction;
        Random ran = new Random();
        int pos;
        Game game;
        SpriteBatch spriteBatch;
        ContentManager content;
        Texture2D zombieSheet;
        Player player;
        Background background;
        Vector2 playerpos;
        Vector2 velocity;
        Vector2 zomPos;
        Vector2 acceleration = new Vector2(500, 500);
        List<Rectangle> zombieWalk;
        List<Rectangle> zombieSpawn;
        Vector2 sourcetotarget;
        SpriteEffects spriteDirection;
        Rectangle zombie;
        Rectangle target;
        private int currentFrame = STANDFRAME;
        private Texture2D zombieSprites;
        private Vector2 vector2;
        Texture2D tex;
        Player targetPlayer;
        Vector2 position;
        Background b;
        
        private Vector2 Origin
        {
            get
            {
                return new Vector2(zombieSheet.Width / 2.0f, zombieSheet.Height / 2.0f);
            }
        }
        public Zombie(Game game, SpriteBatch spriteBatch, ContentManager content, Background background) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.background = background;
            
            pos = ran.Next(200, 200);
            zombie = new Rectangle(pos, pos, PIXSIZE_WIDTH * SCALE, PIXSIZE_HEIGHT * SCALE);
            zombieWalk = new List<Rectangle>();
            zombieWalk.Add(new Rectangle(34, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(73, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(113, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(153, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(195, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(236, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(277, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieWalk.Add(new Rectangle(316, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            zombieSpawn = new List<Rectangle>();
            zombieSpawn.Add(new Rectangle(4, 100, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(39, 100, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(75, 100, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(118, 100, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(160, 100, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(34, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(73, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            zombieSpawn.Add(new Rectangle(113, 6, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            velocity = new Vector2(0, 0);
            spriteDirection = SpriteEffects.None;
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(zombieSheet, zombie, zombieWalk.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
            //spriteBatch.Draw(zombieSheet, position, zombieWalk.ElementAt<Rectangle>(currentFrame), Color.White, rotation, Origin, 1.0f, SpriteEffects.None, 0.0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;
            velocity.Y = 0;
            player = new Player(game, spriteBatch, content, b);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Vector2 moveDirection = playerPos - position;
            //moveDirection.Normalize();

            //Vector2 tempPosition = position;
            //velocity += moveDirection * SPEED * dt;

            //if (zomPos!=player.Position)
            //{
            //    velocity += tempPosition * SPEED * dt;
            //}
            //velocity += Vector2.Multiply(player.Position, (float)gameTime.ElapsedGameTime.TotalSeconds);
            //position += Vector2.Multiply(player.Position, (float)gameTime.ElapsedGameTime.TotalSeconds);
            //sourcetotarget = Vector2.Subtract(player.Position, position);
            //sourcetotarget.Normalize();
            //sourcetotarget += Vector2.Multiply(sourcetotarget, (float)gameTime.ElapsedGameTime.TotalSeconds);
            //velocity = sourcetotarget;
            //seek();
            //sourcetotarget = new Vector2(player.velocity.X, player.velocity.Y);
            //velocity = new Vector2(player.velocity.X, player.velocity.Y);

            foreach (Rectangle r in zombieWalk.ToList())
            {
                if (r.Intersects(player.Pokeball))
                {
                    zombieWalk.Remove(r);
                    player.Score = 1;
                }
            }
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity.X = -SPEED;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity.X = SPEED;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity.Y = SPEED;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity.Y = -SPEED;

            }

            if (velocity.X < 0)
                spriteDirection = SpriteEffects.None;
            else if (velocity.X > 0)
                spriteDirection = SpriteEffects.FlipHorizontally;
            if (velocity.X != 0)
            {
                frameDelay++;
                if (frameDelay > FRAMEDELAYCOUNTER)
                {
                    frameDelay = 0;
                    currentFrame++;
                }
                if (currentFrame > RUNFRAMES)
                    currentFrame = FIRSTRUNFRAME;
            }
            if (velocity.Y != 0)
            {
                frameDelay++;
                if (frameDelay > FRAMEDELAYCOUNTER)
                {
                    frameDelay = 0;
                    currentFrame++;
                }
                if (currentFrame > RUNFRAMES)
                {
                    currentFrame = FIRSTRUNFRAME;
                }
            }

            zomPos = new Vector2(velocity.X, velocity.Y);
            zombie.X = zombie.X + (int)velocity.X;
            zombie.Y = zombie.Y + (int)velocity.Y;
            base.Update(gameTime);
        }

        //private void seek()
        //{
        //    //first, find the distance to the player (and normalize)
        //    sourcetotarget = Vector2.Subtract(player.Position, position);
        //    sourcetotarget.Normalize();

        //    //second, multiply by some speed constant to get a good magnitude
        //    sourcetotarget = Vector2.Multiply(velocity, SPEED);
        //    velocity = sourcetotarget;
        //}
        protected override void LoadContent()
        {
            zombieSheet = content.Load<Texture2D>("images/zombie");

            base.LoadContent();
        }
    }
}
