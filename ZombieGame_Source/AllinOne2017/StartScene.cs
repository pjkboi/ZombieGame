using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AllinOne2017
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StartScene : GameScene 
    {
        private MenuComponent menu;
        Texture2D home;
        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        private SpriteBatch spriteBatch;
        string[] menuItems = {"Start Game",
                             "Help",
                             "High Score",
                             "Credit",
                             "Quit"};

        public StartScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game, spriteBatch,
                game.Content.Load<SpriteFont>("fonts/regularFont"),
                game.Content.Load<SpriteFont>("fonts/hilightFont"),
                menuItems);
            this.Components.Add(menu);
            home = game.Content.Load<Texture2D>("images/pokemon");

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
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(home, Vector2.Zero, Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
