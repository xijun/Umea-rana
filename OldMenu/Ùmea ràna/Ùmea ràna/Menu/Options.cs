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
    class Options
    {
        SpriteBatch spriteBatch;
        Rectangle rectangleBG;
        KeyboardState keyboard;
        ContentManager content;

        Sprite background;
        Sprite menu;
        Sprite son;
        Sprite selection2;
        Sprite selectionSon;

        int select = 1;
        int latence = 0;
        int mousePosX;
        int mousePosY;

        public Options()
        {
            menu = new Sprite(500, 180);
            son = new Sprite(510, 280);
            selection2 = new Sprite(0, 180);
            selectionSon = new Sprite(0, 280);
            //créé les sprites des options, et donne leur position

            //le rectangle est la place accordée au fond d'écran, soit la taille de la fenètre
            rectangleBG = new Rectangle(0, 0, 9 * 128, 11 * 64);
            background = new Sprite(rectangleBG);
        }

        public void Load(ContentManager content, SpriteBatch spriteBatch)
        {
            //chargement des textures
            this.content = content;
            this.spriteBatch = spriteBatch;
            background.Load(content, "menu//background menu");
            son.Load(content, "Options//son");
            menu.Load(content, "pause//menu");
            selection2.Load(content, "options//selection2");
            selectionSon.Load(content, "options//selectionSon");
        }

        public Game1.gameState Update()
        {
            keyboard = Keyboard.GetState();
            mousePosX = Mouse.GetState().X;
            mousePosY = Mouse.GetState().Y;

            //cette partie gère la selection à la souris
            if (mousePosX > 500 && mousePosX < 650 && mousePosY > 180 && mousePosY < 255)
                select = 1;
            else if (mousePosX > 500 && mousePosX < 600 && mousePosY > 280 && mousePosY < 355)
                select = -1;

            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;
            else if (mousePosX > 500 && mousePosX < 650 && mousePosY > 180 && mousePosY < 255 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                System.Threading.Thread.Sleep(200); /* retourne au menu */
                return Game1.gameState.MENU;
            }
            else if (mousePosX > 630 && mousePosX < 680 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                latence = 20;
                Music.vol = 0.0f;  /* desactive le son*/
            }
            else if (mousePosX > 715 && mousePosX < 765 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                latence = 20;
                Music.vol = 0.25f;
            }
            else if (mousePosX > 820 && mousePosX < 870 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                latence = 20;
                Music.vol = 0.5f;
            }
            else if (mousePosX > 920 && mousePosX < 970 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                latence = 20;
                Music.vol = 0.75f;
            }
            else if (mousePosX > 1015 && mousePosX < 1070 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                latence = 20;
                Music.vol = 1.0f;
            }

            //celle ci le clavier
            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;    // sinon les changements sont bien trop rapides
            else if (keyboard.IsKeyDown(Keys.Enter))
            {
                latence = 20;
                if (select == 1)
                {
                    System.Threading.Thread.Sleep(200); /* retourne au menu */
                    return Game1.gameState.MENU;
                }
                else
                {
                    if (Music.vol == 1.0f)
                        Music.vol = 0.0f;  /* active/desactive le son*/
                    else if (Music.vol == 0.0f)
                        Music.vol = 0.25f;
                    else if (Music.vol == 0.25f)
                        Music.vol = 0.5f;
                    else if (Music.vol == 0.5f)
                        Music.vol = 0.75f;
                    else if (Music.vol == 0.75f)
                        Music.vol = 1.0f;
                }
            }
            else if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.Up))  //la selection est faite grâce à un modulo égal au nombre total de bouttons
            {
                select *= -1;
                latence = 20;
            }

            if (select == 1)
                selection2.Coordonnees = new Vector2(450, 180);
            else
                selection2.Coordonnees = new Vector2(450, 280);

            if (Music.vol == 0.0f)
                selectionSon.Coordonnees = new Vector2(510, 280);
            else if (Music.vol == 0.25f)
                selectionSon.Coordonnees = new Vector2(595, 280);
            else if (Music.vol == 0.5f)
                selectionSon.Coordonnees = new Vector2(695, 280);
            else if (Music.vol == 0.75f)
                selectionSon.Coordonnees = new Vector2(795, 280);
            else if (Music.vol == 1.0f)
                selectionSon.Coordonnees = new Vector2(900, 280);

            Music.Volume();
            return Game1.gameState.OPTIONS;
        }

        public void Draw()
        {
            background.Draw(spriteBatch);
            son.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            selection2.Draw(spriteBatch);
            selectionSon.Draw(spriteBatch);
        }
    }
}
