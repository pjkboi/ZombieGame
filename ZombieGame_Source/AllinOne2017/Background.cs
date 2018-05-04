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
using Microsoft.Xna.Framework.Media;

namespace AllinOne2017
{
    class Background : DrawableGameComponent
    {
        const int YOFFSET = 0;              // we don't have scrolling on Y (yet)
        const int SCROLLSENSTIVITY = 1;
        const int HORIZSCROLLBORDER = 75;
        const int BGIMAGEWIDTH = 1280;
        const int BGIMAGEHEIGHT = 800;
        //const int FARBGIMAGEHEIGHT = 260;

        public void StopBackgroundMusic()
        {
            MediaPlayer.Stop();
        }

        public void StartBackgroundMusic()
        {
            MediaPlayer.Play(backgroundSoundTrack);
        }
        Song backgroundSoundTrack;

        Rectangle playerPos;
        Vector2 playerVelocity;

        int screenWidth;
        public int ScreenWidth
        {
        get { return screenWidth; }
        }

        int screenHeight;
        public int ScreenHeight
        {
            get { return screenHeight; }
        }

        public int CurrentXOffset
        {
            get { return currentXoffset; }
        }

        public bool SendPlayerPos(Rectangle PlayerRectangle, Vector2 velocity)
        {
            bool returnValue = false;       //this means no shifting of bg instead of shifting player.

            playerPos = PlayerRectangle;
            playerVelocity = velocity;
                                                                 
            if (playerVelocity.X > 0 && currentXoffset !=  -upperXScrollBoundary)
                if (playerPos.Intersects(rightHorizontalBound))
                    returnValue = true;

            if (playerVelocity.X < 0 && currentXoffset != 0)
                if (playerPos.Intersects(leftHorizontalBound))
                    returnValue = true;

            return returnValue;
        }

        Rectangle leftHorizontalBound;
        Rectangle rightHorizontalBound;

        int currentXoffset;
        int previousXoffset;
        int upperXScrollBoundary;
        int farBackgroundCurrentXOffset;        //this is the scrolling offset for the far background
                                                //it will move slower than the stuff in the foreground
        Texture2D farBackground;
        Texture2D background;
        SpriteBatch spriteBatch;
        ContentManager content;
        SpriteFont font;

        public Background(Game game, SpriteBatch spriteBatch, ContentManager content, int screenWidth, int screenHeight) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.content = content;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            

            playerPos = new Rectangle(0, 0, 0, 0);      //will be overwritten by Player call
            playerVelocity = new Vector2(0);            //will be overwritten by Player call

            leftHorizontalBound = new Rectangle(HORIZSCROLLBORDER- SCROLLSENSTIVITY, 25, SCROLLSENSTIVITY, screenHeight-30);
            rightHorizontalBound = new Rectangle(screenWidth - HORIZSCROLLBORDER, 25, SCROLLSENSTIVITY, screenHeight-30);
            upperXScrollBoundary = BGIMAGEWIDTH - screenWidth;

            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(currentXoffset, YOFFSET, BGIMAGEWIDTH, BGIMAGEHEIGHT), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
           
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            background = content.Load<Texture2D>("images/d");


            backgroundSoundTrack = content.Load<Song>("sounds/background");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(backgroundSoundTrack);

            base.LoadContent();
        }
    }
}
