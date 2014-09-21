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
    public class Enemy1 : Enemies
    {
        public Texture2D img;
        public Vector2 Pos;
        public Vector2 Speed;
        public Rectangle rctBody;
        public int Damage = 1;
        public int Vel = 2;
    }
}
