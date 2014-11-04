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
    public class Room
    {
        //Classes
        //World
        public int[,] world = new int[6, 6];
        public int Roomx;
        public int Roomy;
        public bool leftopen, downopen, rightopen, upopen;
        bool[] lado = new bool[4]; // Lado en que entra

        //World function
        public void worldgenerate(ref int[,] mundo)
        {
            Random rand1 = new Random(); //genera numero random que determina el cuarto
            // 1 = vacio, 2-5 = diferentes presets de cuartos
            for (int a = 0; a < 6; a++) //for para llenar las 2 dimensiones del cuarto
            {
                for (int b = 0; b < 6; b++)
                {
                    int room = rand1.Next(1, 5);
                    mundo[a, b] = room;
                }
            }
        }

        //Select Room, selecciona un cuarto al principio para empezar el juego
        public void Roomselect(ref int rooma, ref int roomb, int[,]mundo)
        {
            //random
            Random rand1 = new Random();
            rooma = rand1.Next(0, 6);
            roomb = rand1.Next(0, 6);
            //Para que no seleccione un cuarto vacio
            while (mundo[rooma, roomb] == 1)
            {
                rooma = rand1.Next(0, 6);
                roomb = rand1.Next(0, 6);
            }
        }

        public void doors(int[,] Mundo, int rooma, int roomb, ref bool left, ref bool down, ref bool right, ref bool up)
        {
            switch (rooma)
            {
                case 0:
                    left = false;
                    break;
                case 5:
                    right = false;
                    break;
                default:
                    // si el cuarto a la izquierda esta vacio
                    if (Mundo[rooma - 1, roomb] == 1)
                    {
                        left = false; //no agregar puerta
                    }
                    else
                    {
                        left = true; // agregar puerta
                    }
                    // Si el cuarto a la derecha esta vacio
                    if (Mundo[rooma + 1, roomb] == 1)
                    {
                        right = false; //no agregar
                    }
                    else
                    {
                        right = true; // agregar
                    }
                    break;
            }

            switch (roomb)
            { 
                case 0:
                        up = false;
                        break;
                case 5:
                        down = false;
                        break;
                default:
                     if (Mundo[rooma, roomb - 1] == 1)
                    {
                        up = false; //no agregar puerta
                    }
                    else
                    {
                        up = true; // agregar puerta
                    }
                    // Si el cuarto a la derecha esta vacio
                    if (Mundo[rooma, roomb + 1] == 1)
                    {
                        down = false; //no agregar
                    }
                    else
                    {
                        down = true; // agregar
                    }
                    break;
            }
        }
        public void RoomLoad(int[,] mundo, int rooma, int roomb, Player objPlayer, Enemy1 objEnemy1, Matter Rock, Matter Lago)
        {
            // Para ver por que puerta viene
            if (lado[0])
                objPlayer.Pos = new Vector2(930, 380);
            else if (lado[1])
                objPlayer.Pos = new Vector2(60, 380);
            else if (lado[2])
                objPlayer.Pos = new Vector2(435, 600);
            else if (lado[3])
                objPlayer.Pos = new Vector2(435, 200);
            switch (mundo[rooma, roomb])
            { 
                case 2:
                    //load room 2 textures and vectors
                    Rock.Pos = new Vector2(350, 380);
                    Lago.Pos = new Vector2(450, 300);
                    // Hitbox
                    Rock.rct = new Rectangle((int)(Rock.Pos.X), (int)(Rock.Pos.Y), (Rock.img.Width), Rock.img.Height);
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    break;
                case 3:
                    Rock.Pos = new Vector2(350, 380);
                    Lago.Pos = new Vector2(450, 300);
                    // Hitbox
                    Rock.rct = new Rectangle((int)(Rock.Pos.X), (int)(Rock.Pos.Y), (Rock.img.Width), Rock.img.Height);
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    break;
                case 4:
                    Rock.Pos = new Vector2(350, 380);
                    Lago.Pos = new Vector2(450, 300);
                    // Hitbox
                    Rock.rct = new Rectangle((int)(Rock.Pos.X), (int)(Rock.Pos.Y), (Rock.img.Width), Rock.img.Height);
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    break;
                case 5:
                    Rock.Pos = new Vector2(350, 380);
                    Lago.Pos = new Vector2(450, 300);
                    // Hitbox
                    Rock.rct = new Rectangle((int)(Rock.Pos.X), (int)(Rock.Pos.Y), (Rock.img.Width), Rock.img.Height);
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    break;
            }
            doors(world, Roomx, Roomy, ref leftopen, ref downopen, ref rightopen, ref upopen);
        }
        public void RoomChange(ref int[,] mundo, ref int rooma, ref int roomb, Player objPlayer, Enemy1 objEnemy1, Matter objLeft, Matter objRight, Matter objUp, Matter objDown, Matter Rock, Matter Pond , ref bool left, ref bool right, ref bool up, ref bool down, ContentManager Content, Matter[] skin)
        {
            if (objPlayer.Pos.X < 4)
            {
                lado[0] = true;
                lado[1] = false;
                lado[2] = false;
                lado[3] = false;
                rooma -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, Rock, Pond);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            else if (objPlayer.Pos.X > 965)
            {
                lado[0] = false;
                lado[1] = true;
                lado[2] = false;
                lado[3] = false;
                rooma += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, Rock, Pond);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            if (objPlayer.Pos.Y < 160)
            {
                lado[0] = false;
                lado[1] = false;
                lado[2] = true;
                lado[3] = false;
                roomb -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, Rock, Pond);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            else if (objPlayer.Pos.Y > 620)
            {
                // De que lado entra
                lado[0] = false;
                lado[1] = false;
                lado[2] = false;
                lado[3] = true;
                roomb += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, Rock, Pond);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
        }

        public void LoadWalls(ContentManager Content, bool left, bool right, bool up, bool down, Matter Left, Matter Right, Matter Up, Matter Down, Matter[] skins)
        {
            if (left)
            {
                Left.img = Content.Load<Texture2D>("Walls\\left_open");
                skins[0].img = Content.Load<Texture2D>("SkinWalls\\left_open");
            }
            else
            { 
                Left.img = Content.Load<Texture2D>("Walls\\left_closed");
                skins[0].img = Content.Load<Texture2D>("SkinWalls\\left_closed");
            }
            if (right)
            {
                Right.img = Content.Load<Texture2D>("Walls\\right_open");
                skins[1].img = Content.Load<Texture2D>("SkinWalls\\right_open");
            }
            else
            {
                Right.img = Content.Load<Texture2D>("Walls\\right_closed");
                skins[1].img = Content.Load<Texture2D>("SkinWalls\\right_closed");
            }
            if (up)
            {
                Up.img = Content.Load<Texture2D>("Walls\\up_open");
                skins[2].img = Content.Load<Texture2D>("SkinWalls\\up_open");
            }
            else
            {
                Up.img = Content.Load<Texture2D>("Walls\\up_closed");
                skins[2].img = Content.Load<Texture2D>("SkinWalls\\up_closed");
            }
            if (down)
            {
                Down.img = Content.Load<Texture2D>("Walls\\down_open");
                skins[3].img = Content.Load<Texture2D>("SkinWalls\\down_open");
            }
            else
            {
                Down.img = Content.Load<Texture2D>("Walls\\down_closed");
                skins[3].img = Content.Load<Texture2D>("SkinWalls\\down_closed");
            }
            //hitbox
            Left.rct = new Rectangle(0, 0, Left.img.Width, Left.img.Height);
            Right.rct = new Rectangle(0, 0, Right.img.Width, Right.img.Width);
            Up.rct = new Rectangle(0, 0, Up.img.Width, Up.img.Height);
            Down.rct = new Rectangle(0, 0, Down.img.Width, Down.img.Height);
            // colour
            Left.data = new Color[Left.img.Width * Left.img.Height];
            Left.img.GetData(Left.data);
            Right.data = new Color[Right.img.Width * Right.img.Height];
            Right.img.GetData(Right.data);
            Up.data = new Color[Up.img.Width * Up.img.Height];
            Up.img.GetData(Up.data);
            Down.data = new Color[Down.img.Width * Down.img.Height];
            Down.img.GetData(Down.data);
        }
    }
}
