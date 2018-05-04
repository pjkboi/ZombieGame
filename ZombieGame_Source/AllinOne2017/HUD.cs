using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace AllinOne2017
{
    class HUD : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        ContentManager content;
        Player player;
        SpriteFont HUDfont;
        SpriteFont GameOverFont;
        Background background;

        public HUD(Game game, SpriteBatch spriteBatch, ContentManager content, Background background, Player p) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.player = p;
            this.background = background;

            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(HUDfont, "SCORE", new Vector2(50, 10), Color.Red);
            string playerString = string.Format("{0,12}", player.Score);  //later we will print the numeric score to this string
            spriteBatch.DrawString(HUDfont, playerString, new Vector2(50, 32), Color.Red);

            spriteBatch.DrawString(HUDfont, "TIME", new Vector2(435, 10), Color.Red);
            playerString = string.Format("{0,7}", player.GameCountRemaining);  //later we will print the numeric level to this string
            spriteBatch.DrawString(HUDfont, playerString, new Vector2(445, 32), Color.Red);


            //if(!player.isAlive)
            //{
            //    spriteBatch.DrawString(GameOverFont, "Game Over", new Vector2(120, 140), Color.White);
            //    background.StopBackgroundMusic();
            //    gameOverSound.Play();
            //}

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            HUDfont = content.Load<SpriteFont>("fonts/HUD");

            base.LoadContent();
        }
    }
}
