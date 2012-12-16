using System;
using System.Collections.Generic;
using System.Linq;

#region XNA 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;

namespace MotPhysiqueFarseer
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PhysicsSimulator simulator;
        Texture2D player,fond;
        Body boxP;     
        Geom geomP;
        KeyboardState _keyboardstate;
        KeyboardState _oldkeyboardstate;
        Sol sol1;
        List<Sol> _sol;
        Viewport defaultViewport;
        int hauteur, largeur;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

     
        protected override void Initialize()  
        {
            

            defaultViewport = GraphicsDevice.Viewport;
            hauteur = defaultViewport.Height;
            largeur = defaultViewport.Width;

            graphics.PreferredBackBufferHeight = hauteur;
            graphics.PreferredBackBufferWidth = largeur;
            graphics.ApplyChanges();

            IsMouseVisible = true;    

            simulator = new PhysicsSimulator(new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, 1500f));

            boxP = BodyFactory.Instance.CreateRectangleBody(simulator, 50, 50,1);
            boxP.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(150, 300);
            
            boxP.LinearDragCoefficient = 1.5f;

            geomP = GeomFactory.Instance.CreateRectangleGeom(simulator, boxP, 50, 50, 1);
            geomP.CollisionResponseEnabled = true;

            sol1 = new Sol(simulator, 200, 450, base.Content);

            _sol = new List<Sol>();

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = base.Content.Load<Texture2D>("P1");
            fond = base.Content.Load<Texture2D>("Desert");
        }

        
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {
            simulator.Update(gameTime.ElapsedGameTime.Milliseconds * 0.001f);

            _keyboardstate = Keyboard.GetState();

            if (_keyboardstate.IsKeyDown(Keys.Left))
                boxP.ApplyForce(new FarseerGames.FarseerPhysics.Mathematics.Vector2(-600, 0));

            if (_keyboardstate.IsKeyDown(Keys.Right))
                boxP.ApplyForce(new FarseerGames.FarseerPhysics.Mathematics.Vector2(600, 0));
             
            if (_keyboardstate.IsKeyDown(Keys.Up) && _oldkeyboardstate.IsKeyUp(Keys.Up))
                boxP.ApplyImpulse(new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, -400));

            if (_keyboardstate.IsKeyDown(Keys.Down))
                boxP.ApplyForce(new FarseerGames.FarseerPhysics.Mathematics.Vector2(0, 200));
             
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _sol.Add(new Sol(simulator,Mouse.GetState().X,Mouse.GetState().Y,base.Content));

           
            _oldkeyboardstate = _keyboardstate;  

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(fond,new Vector2(),Color.White);
            spriteBatch.Draw(player, CastVector2(boxP.Position), null,Color.White, 0f, new Vector2(player.Width / 2, player.Height / 2), 1,SpriteEffects.None, 0);
            foreach (Sol sol in _sol)
            {
                sol.Draw(spriteBatch);
            }
            sol1.Draw(spriteBatch);
            
            spriteBatch.End();

           

            base.Draw(gameTime);
        }

        private Microsoft.Xna.Framework.Vector2  CastVector2(FarseerGames.FarseerPhysics.Mathematics.Vector2 vec)

        {
            return new Microsoft.Xna.Framework.Vector2(vec.X, vec.Y);
        }

        private FarseerGames.FarseerPhysics.Mathematics.Vector2 Cast2(Vector2 vec)
        {
            return new FarseerGames.FarseerPhysics.Mathematics.Vector2(vec.X, vec.Y);
        }
    }
}
