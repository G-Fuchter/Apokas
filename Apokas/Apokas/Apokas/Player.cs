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
    public class Player
    {
        public float Vida = 10.0f;
        public Texture2D img;
        public Vector2 Pos;
        public Vector2 Speed;
        public Rectangle rctBody;
        public bool invencible;
        public float lastInvenciblitity = 0f;
        public int ValorAlfa = 1;
        public int FadeIncrement = 3;
        public double FadeDelay = .035;


        public void Movement()
        {
            bool run;
            Speed = new Vector2(0.0f, 0.0f); // Char speed esta ahi para predecir donde esta el character y la collisiones

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftShift)) // Sprint
                run = true;
            else
                run = false;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                if (run == true)
                    Speed.X = -6;
                else
                    Speed.X = -3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                if (run == true)
                    Speed.X = 6;
                else
                    Speed.X = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                if (run == true)
                    Speed.Y = 6;
                else
                    Speed.Y = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                if (run == true)
                    Speed.Y = -6;
                else
                    Speed.Y = -3;
            }
        }
    }
}
