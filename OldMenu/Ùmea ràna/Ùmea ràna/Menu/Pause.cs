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
    class Pause
    {
        SpriteBatch spriteBatch;
        KeyboardState keyboard;
        ContentManager content;

        Song songMenu;

        Sprite retour;
        Sprite menu;
        Sprite quitter;
        Sprite selection;
        Sprite fenetre;

        int select = 0;
        int latence;
        int mousePosX;
        int mousePosY;


        public void PauseInitialize()
        {
            //créé les sprites du menu, et donne leur position
            retour = new Sprite(700, 80);
            menu = new Sprite(700, 180);
            quitter = new Sprite(700, 280);
            selection = new Sprite(650, 80);
            fenetre = new Sprite(650, 0);
        }

        public void PauseLoad(ContentManager content, SpriteBatch spriteBatch)
        {
            //chargement des textures
            this.content = content;
            this.spriteBatch = spriteBatch;
            retour.Load(content, "pause//retour");
            menu.Load(content, "pause//menu");
            quitter.Load(content, "pause//quitter");
            selection.Load(content, "pause//selection");
            fenetre.Load(content, "pause//fenetre");

            songMenu = content.Load<Song>("songMenu");
        }

        public Game1.gameState PauseUpdate()
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

            if (mousePosX > 700 && mousePosX < 850 && mousePosY > 80 && mousePosY < 155 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                return Game1.gameState.PARTIE;
            else if (mousePosX > 700 && mousePosX < 850 && mousePosY > 180 && mousePosY < 255 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                System.Threading.Thread.Sleep(200);//pause de 200ms, sinon une partie est relancee automatiquement en arrivant au menu.
                MediaPlayer.Play(songMenu);
                return Game1.gameState.MENU; // retourne au menu
            }
            else if (mousePosX > 700 && mousePosX < 850 && mousePosY > 280 && mousePosY < 355 && Mouse.GetState().LeftButton == ButtonState.Pressed)
                return Game1.gameState.QUITTER;


            //celle ci gère le clavier
            if (latence > 0)  // la latence créé un temps d'attente avant de pouvoir changer à nouveau de boutton
                latence--;    // sinon les changements sont bien trop rapides
            else
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    if (select == 0)
                    { return Game1.gameState.PARTIE; } // retourne a la partie
                    else if (select == 1 || select == -2)
                    {
                        System.Threading.Thread.Sleep(200);//pause de 200ms, sinon une partie est relancee automatiquement en arrivant au menu.
                        MediaPlayer.Play(songMenu);
                        return Game1.gameState.MENU; // retourne au menu
                    }
                    else
                    { return Game1.gameState.QUITTER; }  // quitte le jeu
                }
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

                if (select == 0)
                    selection.Coordonnees = new Vector2(650, 80);
                else if (select == 1 || select == -2)
                    selection.Coordonnees = new Vector2(650, 180);
                else
                    selection.Coordonnees = new Vector2(650, 280);
            }

            return Game1.gameState.PAUSE;
        }

        public void PauseDraw()
        {
            fenetre.Draw(spriteBatch);
            retour.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            quitter.Draw(spriteBatch);
            selection.Draw(spriteBatch);
        }
    }
}
