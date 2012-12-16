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
    class Music
    {
        Song songMenu;
        ContentManager content;
        public static float vol = 1.0f;

        public static void Volume()
        {
            MediaPlayer.Volume = vol;
        }

        public void Menu(ContentManager content)
        {
            songMenu = content.Load<Song>("songMenu");
            MediaPlayer.Play(songMenu);
        }

       

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void MusicLoad(ContentManager content)
        {
            Menu(content);
        }
    }
}
