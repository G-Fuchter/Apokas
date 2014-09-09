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

namespace Apokas
{
    public class Character
    {
        public float Vida = 10.0f;
        public Texture2D imgCharacter;
        public Vector2 CharPos;
        public Vector2 CharSpeed;
        public Rectangle rctCharacter;

        public void Movement()
        {
            bool run;
            CharSpeed = new Vector2(0.0f, 0.0f); // Char speed esta ahi para predecir donde esta el character y la collisiones

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftShift)) // Sprint
                run = true;
            else
                run = false;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                if (run == true)
                    CharSpeed.X = -6;
                else
                    CharSpeed.X = -3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                if (run == true)
                    CharSpeed.X = 6;
                else
                    CharSpeed.X = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                if (run == true)
                    CharSpeed.Y = 6;
                else
                    CharSpeed.Y = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                if (run == true)
                    CharSpeed.Y = -6;
                else
                    CharSpeed.Y = -3;
            }
        }
    }
}
