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
    class Menu
    {
        SpriteBatch spriteBatch;
        Rectangle rectangleBG;
        KeyboardState keyboard;
        ContentManager content;
        Options menuOptions;

        Sprite background;
        Sprite jouer;
        Sprite options;
        Sprite quitter;
        Sprite selection;

        int select = 0;
        int latence = 0;
        int mousePosX;
        int mousePosY;

        public Menu()
        {
        }

        public void MenuInitialize()
        {
            //créé les sprites du menu, et donne leur position
            jouer = new Sprite(700, 80);
            options = new Sprite(700, 180);
            quitter = new Sprite(700, 280);
            selection = new Sprite(650, 80);

            //le rectangle est la place accordée au fond d'écran, soit la taille de la fenètre
            rectangleBG = new Rectangle(0, 0, 9 * 128, 11 * 64);
            background = new Sprite(rectangleBG);

            menuOptions = new Options();
        }

        public void MenuLoad(ContentManager content, SpriteBatch spriteBatch)
        {
            //chargement des textures
            this.content = content;
            this.spriteBatch = spriteBatch;
            background.Load(content, "menu//background menu");
            jouer.Load(content, "menu//jouer");
            options.Load(content, "menu//options");
            quitter.Load(content, "menu//quitter");
            selection.Load(content, "menu//selection");

            menuOptions.Load(content, spriteBatch);

        }

        public Game1.gameState MenuUpdate()
        {
            keyboard = Keyboard.GetState();
            mousePosX = Mouse.GetState().X;
            mousePosY = Mouse.GetState().Y;

            //cette partie gère la selection et validation à la souris
            if (mousePosX > 700 && mousePosX < 850 && mousePosY > 80 && mousePosY < 155)
                select = 0;
            else if (mousePosX > 700 && mousePosX < 850 && mousePosY > 180 && mousePosY < 255)
                select = 1;
            else if (mousePosX > 700 && mousePosX < 850 && mousePosY > 280 && mousePosY < 355)
                select = 2;

            if (select == 0 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                return Game1.gameState.CHARGER_PARTIE;
            else if (select == 1 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                System.Threading.Thread.Sleep(200); // va aux options
                return Game1.gameState.OPTIONS;
            }
            else if (select == 2 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                return Game1.gameState.QUITTER;


            //celle ci le clavier
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                if (select == 0)
                {
                    return Game1.gameState.CHARGER_PARTIE;
                } // lance la partie
                else if (select == 1 || select == -2)
                {
                    System.Threading.Thread.Sleep(200); // va aux options
                    return Game1.gameState.OPTIONS;
                }
                else
                { return Game1.gameState.QUITTER; }  // quitte le jeu
            }

            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;    // sinon les changements sont bien trop rapides
            else
            {
                if (keyboard.IsKeyDown(Keys.Down))  //la selection est faite grâce à un modulo égal au nombre total de bouttons
                {
                    select = (select + 1) % 3;
                    latence = 10;
                }
                else if (keyboard.IsKeyDown(Keys.Up))
                {
                    select = (select - 1) % 3;
                    latence = 10;
                }

                if (select == 0)                                    //place le sprite "selection" au bon endroit
                    selection.Coordonnees = new Vector2(650, 80);
                else if (select == 1 || select == -2)
                    selection.Coordonnees = new Vector2(650, 180);
                else
                    selection.Coordonnees = new Vector2(650, 280);
            }

            return Game1.gameState.MENU;
        }

        public void MenuDraw()
        {
            background.Draw(spriteBatch);
            jouer.Draw(spriteBatch);
            options.Draw(spriteBatch);
            quitter.Draw(spriteBatch);
            selection.Draw(spriteBatch);
        }

        public Game1.gameState OptionsUpdate()
        {
            return menuOptions.Update();
        }

        public void OptionsDraw()
        {
            menuOptions.Draw();
        }
    }
}
