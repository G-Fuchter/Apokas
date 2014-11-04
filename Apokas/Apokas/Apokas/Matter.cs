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
    public class Matter
    {
        //Objects
        Player objPlayer = new Player();
        Room objRoom = new Room();
        // Rock
        public Color[] data;
        public Texture2D img;
        public Vector2 Pos;
        public Rectangle rct;
        public int Damage = 0;
        // Lago
       /* public Color[] dataLago;
        public Texture2D imgLago;
        public Vector2 PosLago;
        public Rectangle rctLago;
        public int DamageLago = 0;
        // Walls
            // left open
        public Color[] dataLeft;
        public Rectangle rctLeft;
        public Texture2D imgLeft;
            //right open
        public Color[] dataRight;
        public Rectangle rctRight;
        public Texture2D imgRight;
            // up open
        public Color[] dataUp;
        public Rectangle rctUp;
        public Texture2D imgUp;
            //down open
        public Color[] dataDown;
        public Rectangle rctDown;
        public Texture2D imgDown;
        // Fondo
        public Texture2D imgFondo;
        // Entrada
        */

        
        public void CollisionwPlayer(ref Vector2 speed, Rectangle rctCharacter, ref int vida, Color[] dataCharacter)
        {
            if (objPlayer.IntersectPixels(rctCharacter, dataCharacter, rct, data))
            {
                speed = new Vector2(0, 0);
                vida -= Damage;
            }
        }
    }
}
