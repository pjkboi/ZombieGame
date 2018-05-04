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
    class Player : DrawableGameComponent
    {
        public enum Directions{up, down, left, right, space };

        const int GAMETIMERMAX = 400;     // the max length of the game

        const int PIXSIZE_WIDTH = 25;
        const int PIXSIZE_HEIGHT = 32;
        const int ZOMBIEPIXSIZE_WIDTH = 40;
        const int ZOMBIEPIXSIZE_HEIGHT = 50;
        const int SCALE = 2;
        const int STANDFRAME = 0;
        const int JUMPFRAME = 5;
        const int FIRSTRUNFRAME = 1;
        const int RUNFRAMES = 3;
        const int UPFRAMES = 3;
        const int BALLFRAMES = 3;
        private int currentFrame = STANDFRAME;
        int currentballFrame = STANDFRAME;
        const int FRAMEDELAYCOUNTER = 4;        // this is the number of screen draws between animation frames
        const int ZOMBIEFRAMECOUNTER = 8;
        int frameDelay = 0;
        string direction;
        string ball;
        SpriteBatch spriteBatch;
        ContentManager content;
        Texture2D spriteSheet;
        Background background;
        const int BALLSPEED = 10;
        const float GRAVITY = 0.02f;
        const float SPEED = 2.3f;
        public Vector2 velocity;
        Vector2 ballspeed;
        Vector2 position;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Rectangle player;
        Rectangle playerball;
        List<Rectangle> trainerwalk;
        List<Rectangle> trainerup;
        List<Rectangle> trainerdown;
        List<Rectangle> trainerleft;
        List<Rectangle> pokeball;
        public Rectangle PlayerPos
        {
            get { return player; }
        }
        public Rectangle Pokeball
        {
            get { return playerball; }
        }
        SpriteEffects spriteDirection;


        bool alive = true;
        public void Dies()
        {
            alive = false;
        }
        public bool isAlive
        {
            get { return alive; }
        }

        int gameCounter = GAMETIMERMAX;
        public int GameCountRemaining
        {
            get { return gameCounter; }
        }
        private DateTime gameStart;
        int score = 0;
        private Background b;

        public int Score
        {
            get { return score; }
            set { score += value; }         // notice that each piece will simply send their additional score!
        }

        
        

        public Player(Game game, SpriteBatch spriteBatch, ContentManager content, Background background) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.background = background;
            
            player = new Rectangle(5, 200, PIXSIZE_WIDTH*SCALE, PIXSIZE_HEIGHT*SCALE);
            
            
            trainerwalk = new List<Rectangle>();
            trainerwalk.Add(new Rectangle(16, 110, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerwalk.Add(new Rectangle(50, 110, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerwalk.Add(new Rectangle(83, 110, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerwalk.Add(new Rectangle(50, 110, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            trainerleft = new List<Rectangle>();
            trainerleft.Add(new Rectangle(15, 76, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerleft.Add(new Rectangle(48, 76, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerleft.Add(new Rectangle(81, 76, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerleft.Add(new Rectangle(48, 76, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            trainerup = new List<Rectangle>();
            trainerup.Add(new Rectangle(15, 10, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerup.Add(new Rectangle(50, 10, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerup.Add(new Rectangle(79, 10, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerup.Add(new Rectangle(15, 10, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            trainerdown = new List<Rectangle>();
            trainerdown.Add(new Rectangle(14, 42, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerdown.Add(new Rectangle(49, 42, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerdown.Add(new Rectangle(82, 42, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            trainerdown.Add(new Rectangle(49, 42, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));

            pokeball = new List<Rectangle>();
            pokeball.Add(new Rectangle(22, 292, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            pokeball.Add(new Rectangle(22, 292, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            pokeball.Add(new Rectangle(22, 292, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            pokeball.Add(new Rectangle(22, 292, PIXSIZE_WIDTH, PIXSIZE_HEIGHT));
            spriteDirection = SpriteEffects.None;

            velocity = new Vector2(0, 0);
            ballspeed = velocity;

            gameStart = DateTime.Now;

            LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
                spriteBatch.Begin();
                spriteBatch.Draw(spriteSheet, player, trainerdown.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                if (direction == "right")
                {
                    spriteBatch.Draw(spriteSheet, player, trainerwalk.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                }
                else if(direction == "up")
                {
                    spriteBatch.Draw(spriteSheet, player, trainerup.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                }
                else if (direction == "down")
                {
                    spriteBatch.Draw(spriteSheet, player, trainerdown.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                }
                if (direction == "left")
                {
                    spriteBatch.Draw(spriteSheet, player, trainerleft.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                }
                if (direction == "left")
                {
                    spriteBatch.Draw(spriteSheet, player, trainerleft.ElementAt<Rectangle>(currentFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);
                }
                if (ball == "space")
                {
                    spriteBatch.Draw(spriteSheet, playerball, pokeball.ElementAt<Rectangle>(currentballFrame), Color.White, 0f, new Vector2(0), spriteDirection, 0f);

                }
                spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            velocity.X = 0;
            velocity.Y = 0;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Right))
            {
                direction = Directions.right.ToString();
                velocity.X = SPEED;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                direction = Directions.left.ToString();
                velocity.X = -SPEED;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                direction = Directions.down.ToString();
                velocity.Y = SPEED;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                direction = Directions.up.ToString();
                velocity.Y = -SPEED;
               
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                playerball = player;
                ball = Directions.space.ToString();
                switch(direction)
                {
                    case "right":
                        ballspeed.X = BALLSPEED;
                        break;
                    case "left":
                        ballspeed.X = -BALLSPEED;
                        break;
                    case "up":
                        ballspeed.Y = -BALLSPEED;
                        break;
                    case "down":
                        ballspeed.Y = BALLSPEED;
                        break;
                }
            }

            if (velocity.X != 0) 
            {
                frameDelay++;
                if(frameDelay > FRAMEDELAYCOUNTER)
                {
                    frameDelay = 0;
                    currentFrame++;
                }
                if (currentFrame > RUNFRAMES)
                    currentFrame = FIRSTRUNFRAME;
            }
            if(velocity.Y != 0)
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

            if (ballspeed.X != 0) 
            {
                frameDelay++;
                if (frameDelay > FRAMEDELAYCOUNTER)
                {
                    frameDelay = 0;
                    currentballFrame++;
                }
                if (currentballFrame > BALLFRAMES)
                {

                    playerball = player;
                    currentballFrame = 0;
                    ballspeed.X = 0;
                    ballspeed.Y = 0;
                }
                    

            }
            if (ballspeed.Y != 0)
            {
                frameDelay++;
                if (frameDelay > FRAMEDELAYCOUNTER)
                {
                    frameDelay = 0;
                    currentballFrame++;
                }
                if (currentballFrame > BALLFRAMES)
                {

                    currentballFrame = 0;
                    playerball = player;
                    ballspeed.X = 0;
                    ballspeed.Y = 0;
                }
            }
            if (DateTime.Now > gameStart.AddSeconds(1) && gameCounter != 0)
            {
                gameCounter--;
                gameStart = DateTime.Now;
            }
            position = new Vector2(velocity.X, velocity.Y);
            playerball.X = playerball.X + (int)ballspeed.X;
            playerball.Y = playerball.Y + (int)ballspeed.Y;
            
            player.X = player.X + (int)velocity.X;
            player.Y = player.Y + (int)velocity.Y;

            base.Update(gameTime);
        }
        
        protected override void LoadContent()
        {
            spriteSheet = content.Load<Texture2D>("images/player");
            base.LoadContent();
        }
    }
}
