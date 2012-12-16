using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ùmea_ràna
{
    class Partie
    {

        Pause pause;
        int windowHeight = 13 * 64;
        int windowWidth = 9 * 128;


        public void Initialize(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
        }

        public void Load(ContentManager Content, SpriteBatch spriteBatch)
        {
            pause = new Pause();
            pause.PauseInitialize();
            pause.PauseLoad(Content, spriteBatch);
        }


        public Game1.gameState Update(GameTime gameTime)
        {
            return Game1.gameState.PARTIE;

        }


        public void Draw(SpriteBatch spriteBatch)
        {

        }


        public Game1.gameState PauseUpdate()
        {
            return pause.PauseUpdate();
        }

        public void PauseDraw(SpriteBatch spritebatch)
        {
            pause.PauseDraw();
        }
    }
}