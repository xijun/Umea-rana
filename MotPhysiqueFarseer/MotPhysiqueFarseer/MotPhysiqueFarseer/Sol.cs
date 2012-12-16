using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class Sol
    {
        //FIELDS

        Body boxSol;
        Geom geomSol;
        Texture2D _sol;
        PhysicsSimulator simulator;
        ContentManager content;
        int coorX;
        int coorY;
        


        //CONSTRUCTORS

        public Sol(PhysicsSimulator simulator,int coorx,int coory,ContentManager content)
        {
            this.simulator = simulator;
            this.coorX = coorx;
            this.coorY = coory;
            this.content = content;

            initialize();
            loadContent(content);
        }

        //METHODS

        public void initialize()
        {

            boxSol = BodyFactory.Instance.CreateRectangleBody(simulator, 200, 34,1);
            boxSol.Position = new FarseerGames.FarseerPhysics.Mathematics.Vector2(coorX, coorY);
            boxSol.IsStatic = true;

            geomSol = GeomFactory.Instance.CreateRectangleGeom(simulator, boxSol, 200, 34, 1);
            geomSol.CollisionResponseEnabled = true;
            geomSol.RestitutionCoefficient = 0.1f;
            geomSol.FrictionCoefficient = 0.5f; 
        }

        public void loadContent(ContentManager content)
        {
            _sol = content.Load<Texture2D>("sol");
        }

        private Microsoft.Xna.Framework.Vector2 CastVector2(FarseerGames.FarseerPhysics.Mathematics.Vector2 vec)
        {
            return new Microsoft.Xna.Framework.Vector2(vec.X, vec.Y);
        }

        private FarseerGames.FarseerPhysics.Mathematics.Vector2 Cast2(Vector2 vec)
        {
            return new FarseerGames.FarseerPhysics.Mathematics.Vector2(vec.X, vec.Y);
        }
         

        //UPDATE & DRAW

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sol, CastVector2(boxSol.Position), null, Color.White, boxSol.Rotation, new Vector2(_sol.Width / 2, _sol.Height / 2), 1, SpriteEffects.None, 0);

        }
    }
}
