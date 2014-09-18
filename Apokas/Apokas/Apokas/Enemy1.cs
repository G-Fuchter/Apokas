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
    public class Enemy1
    {
        public Texture2D img;
        public Vector2 Pos;
        public Vector2 Speed;
        public Rectangle rctBody;

        public void AI(Player objPlayer)
        {
            //Resetea La "velocidad"
            Speed.X = 0;
            Speed.Y = 0;
            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (Pos.X < objPlayer.Pos.X - 3)
                Speed.X = 3;
            else if (Pos.X > objPlayer.Pos.X + 3)
                Speed.X = -3;

            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (Pos.Y < objPlayer.Pos.Y - 3)
                Speed.Y = 2;
            else if (Pos.Y > objPlayer.Pos.Y + 3)
                Speed.Y = -2;
        }
    }
}
