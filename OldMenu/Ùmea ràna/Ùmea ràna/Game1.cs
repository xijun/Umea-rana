using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ùmea_ràna
{
   
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Partie partie;
        Menu menu;
        gameState gameState1 = gameState.MENU;
        MouseState ms = Mouse.GetState();
        Music music;
        Viewport _defaultViewport;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.Window.Title = "Le Sixiéme Sens";
            graphics.IsFullScreen = true;
        }

       
        protected override void Initialize()
        {
            music = new Music();
            menu = new Menu();
            music.MusicLoad(Content);
            menu.MenuInitialize();  // initialise les options avec leurs valeurs par defaut

            _defaultViewport = GraphicsDevice.Viewport;

            int h = _defaultViewport.Height;
            int l = _defaultViewport.Width;

            graphics.PreferredBackBufferHeight = h;
            graphics.PreferredBackBufferWidth = l;
            graphics.ApplyChanges();

            base.Initialize();

        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            menu.MenuLoad(Content, spriteBatch);
            
        }

       
        protected override void UnloadContent()
        {
            
        }

       
        protected override void Update(GameTime gameTime)
        {

            if (gameState1 == gameState.PARTIE)
            {
                gameState1 = partie.Update(gameTime);
            }
            else if (gameState1 == gameState.MENU)
                gameState1 = menu.MenuUpdate();
            else if (gameState1 == gameState.PAUSE)
            {
                gameState1 = partie.PauseUpdate();
            }
            else if (gameState1 == gameState.OPTIONS)
                gameState1 = menu.OptionsUpdate();
            else if (gameState1 == gameState.CHARGER_PARTIE)
            {
                music.Stop();
                partie = new Partie();
                partie.Load(Content, spriteBatch);
                gameState1 = gameState.PARTIE;
            }
            else if (gameState1 == gameState.QUITTER)
                this.Exit();
           
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (gameState1 == gameState.MENU)
                menu.MenuDraw();
            else if (gameState1 == gameState.PARTIE)
            {
                partie.Draw(spriteBatch);
            }
            else if (gameState1 == gameState.PAUSE)
            {
                partie.Draw(spriteBatch);
                partie.PauseDraw(spriteBatch);
            }
            else if (gameState1 == gameState.OPTIONS)
                menu.OptionsDraw();

            spriteBatch.End(); 
            base.Draw(gameTime);
        }

        public enum gameState
        {
            MENU,
            OPTIONS,
            PAUSE,
            QUITTER,
            PARTIE,
            CHARGER_PARTIE,
            DECHARGER_PARTIE,
        }
    }
}
