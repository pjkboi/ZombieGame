using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AllinOne2017
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ActionScene : GameScene 
    {
        const int SCREENWIDTH = 1200;
        const int SCREENHEIGHT = 500;
        private SpriteBatch spriteBatch;
        Background b;
        //private Bat bat;
        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            ContentManager Content = game.Content;
            //bat = new Bat(game, spriteBatch, 
            //    game.Content.Load<Texture2D>("images/Bat"));
            //this.Components.Add(bat);
            b = new Background(game, spriteBatch, Content, SCREENWIDTH, SCREENHEIGHT);
            Components.Add(b);

            Player p = new Player(game, spriteBatch, Content, b);
            Components.Add(p);
            Zombie z = new Zombie(game, spriteBatch, Content, b);
            Components.Add(z);
            HUD hud = new HUD(game, spriteBatch, Content, b, p);
            Components.Add(hud);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
