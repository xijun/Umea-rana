using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Ùmea_ràna
{
    class Sprite
    {
        protected Texture2D texture;
        public Vector2 coordonnees;
        protected Rectangle rect = Rectangle.Empty;

        public Sprite()
        {
        }

        public Sprite(Rectangle rectangle)
        {
            rectangle = rect;
        }

        public Sprite(int x, int y)
        {
            coordonnees = new Vector2(x, y);
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Coordonnees
        {
            get { return coordonnees; }
            set { coordonnees = value; }
        }

        public void Load(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //le rectangle indique la place allouée au sprite
            //il est null par défaut, et n'a donc aucun effet si il n'est pas initialisé
            if (rect != Rectangle.Empty)
                spriteBatch.Draw(texture, rect, Color.White);
            else
                spriteBatch.Draw(texture, coordonnees, Color.White);
        }
    }
}
