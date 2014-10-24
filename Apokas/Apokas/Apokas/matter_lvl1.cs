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
    public class matter_lvl1 : Matter
    {
        //Objects
        Room objRoom = new Room();
        // Rock
        public Color[] dataRock;
        public Texture2D imgRock;
        public Vector2 PosRock;
        public Rectangle rctRock;
        public int DamageRock = 0;
        // Lago
        public Color[] dataLago;
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

        public void LoadWalls(ContentManager Content, bool left, bool right, bool up, bool down)
        {
            if (left)
                imgLeft = Content.Load<Texture2D>("Walls\\left_open");
            else
                imgLeft = Content.Load<Texture2D>("Walls\\left_closed");
            if (right)
                imgRight = Content.Load<Texture2D>("Walls\\right_open");
            else
                imgRight = Content.Load<Texture2D>("Walls\\right_closed");
            if (up)
                imgUp = Content.Load<Texture2D>("Walls\\up_open");
            else
                imgUp = Content.Load<Texture2D>("Walls\\up_closed");
            if (down)
                imgDown = Content.Load<Texture2D>("Walls\\down_open");
            else
                imgDown = Content.Load<Texture2D>("Walls\\down_closed");
            //hitbox
            rctLeft = new Rectangle(0, 0, imgLeft.Width, imgLeft.Height);
            rctRight = new Rectangle(0, 0, imgRight.Width, imgRight.Height);
            rctUp = new Rectangle(0, 0, imgUp.Width, imgUp.Height);
            rctDown = new Rectangle(0, 0, imgDown.Width, imgDown.Height);
            // colour
            dataLeft = new Color[imgLeft.Width * imgLeft.Height];
            imgLeft.GetData(dataLeft);
            dataRight = new Color[imgRight.Width * imgRight.Height];
            imgRight.GetData(dataRight);
            dataUp = new Color[imgUp.Width * imgUp.Height];
            imgUp.GetData(dataUp);
            dataDown = new Color[imgDown.Width * imgDown.Height];
            imgDown.GetData(dataDown);
        }
    }
}
