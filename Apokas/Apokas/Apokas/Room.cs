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
        public int[,] world = new int[4, 5];
        public int Roomx;
        public int Roomy;
        public bool leftopen, downopen, rightopen, upopen;

        //World function
        public void worldgenerate(ref int[,] mundo)
        {
            Random rand1 = new Random(); //genera numero random que determina el cuarto
            // 1 = vacio, 2-5 = diferentes presets de cuartos
            for (int a = 0; a < 4; a++) //for para llenar las 2 dimensiones del cuarto
            {
                for (int b = 0; b < 5; b++)
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
            rooma = rand1.Next(0, 3);
            roomb = rand1.Next(0, 4);
            //Para que no seleccione un cuarto vacio
            while (mundo[rooma, roomb] == 1)
            {
                rooma = rand1.Next(0, 3);
                roomb = rand1.Next(0, 4);
            }
        }

        public void doors(int[,] Mundo, int rooma, int roomb, ref bool left, ref bool down, ref bool right, ref bool up)
        {
            switch (rooma)
            {
                case 0:
                    left = false;
                    break;
                case 3:
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
                case 4:
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
        public void RoomLoad(int[,] mundo, int rooma, int roomb, Player objPlayer, Enemy1 objEnemy1, matter_lvl1 objMatter_lvl1)
        {
            switch (mundo[rooma, roomb])
            { 
                case 2:
                    //load room 2 textures and vectors
                    objPlayer.Pos = new Vector2(60, 380);
                    objMatter_lvl1.PosRock = new Vector2(350, 380);
                    objMatter_lvl1.PosLago = new Vector2(450, 300);
                    // Hitbox
                    objMatter_lvl1.rctRock = new Rectangle((int)(objMatter_lvl1.PosRock.X), (int)(objMatter_lvl1.PosRock.Y), (objMatter_lvl1.imgRock.Width), objMatter_lvl1.imgRock.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    objMatter_lvl1.rctLago = new Rectangle((int)(objMatter_lvl1.PosLago.X), (int)(objMatter_lvl1.PosLago.Y), (objMatter_lvl1.imgLago.Width), objMatter_lvl1.imgLago.Height);
                    break;
                case 3:
                    objPlayer.Pos = new Vector2(60, 380);
                    objMatter_lvl1.PosRock = new Vector2(350, 380);
                    objMatter_lvl1.PosLago = new Vector2(450, 300);
                    // Hitbox
                    objMatter_lvl1.rctRock = new Rectangle((int)(objMatter_lvl1.PosRock.X), (int)(objMatter_lvl1.PosRock.Y), (objMatter_lvl1.imgRock.Width), objMatter_lvl1.imgRock.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    objMatter_lvl1.rctLago = new Rectangle((int)(objMatter_lvl1.PosLago.X), (int)(objMatter_lvl1.PosLago.Y), (objMatter_lvl1.imgLago.Width), objMatter_lvl1.imgLago.Height);
                    break;
                case 4:
                    objPlayer.Pos = new Vector2(60, 380);
                    objMatter_lvl1.PosRock = new Vector2(350, 380);
                    objMatter_lvl1.PosLago = new Vector2(450, 300);
                    // Hitbox
                    objMatter_lvl1.rctRock = new Rectangle((int)(objMatter_lvl1.PosRock.X), (int)(objMatter_lvl1.PosRock.Y), (objMatter_lvl1.imgRock.Width), objMatter_lvl1.imgRock.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    objMatter_lvl1.rctLago = new Rectangle((int)(objMatter_lvl1.PosLago.X), (int)(objMatter_lvl1.PosLago.Y), (objMatter_lvl1.imgLago.Width), objMatter_lvl1.imgLago.Height);
                    break;
                case 5:
                    objPlayer.Pos = new Vector2(60, 380);
                    objMatter_lvl1.PosRock = new Vector2(350, 380);
                    objMatter_lvl1.PosLago = new Vector2(450, 300);
                    // Hitbox
                    objMatter_lvl1.rctRock = new Rectangle((int)(objMatter_lvl1.PosRock.X), (int)(objMatter_lvl1.PosRock.Y), (objMatter_lvl1.imgRock.Width), objMatter_lvl1.imgRock.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    objMatter_lvl1.rctLago = new Rectangle((int)(objMatter_lvl1.PosLago.X), (int)(objMatter_lvl1.PosLago.Y), (objMatter_lvl1.imgLago.Width), objMatter_lvl1.imgLago.Height);
                    break;
            }
            doors(world, Roomx, Roomy, ref leftopen, ref downopen, ref rightopen, ref upopen);
        }
        public void RoomChange(ref int[,] mundo, ref int rooma, ref int roomb, Player objPlayer, Enemy1 objEnemy1, matter_lvl1 objMatterlvl1, ref bool left, ref bool right, ref bool up, ref bool down, ContentManager Content)
        {
            if (objPlayer.Pos.X < 4)
            {
                rooma -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, objMatterlvl1);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                objMatterlvl1.LoadWalls(Content, left, right, up, down);
            }
            else if (objPlayer.Pos.X > 965)
            {
                rooma += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, objMatterlvl1);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                objMatterlvl1.LoadWalls(Content, left, right, up, down);
            }
            if (objPlayer.Pos.Y < 160)
            {
                roomb -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, objMatterlvl1);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                objMatterlvl1.LoadWalls(Content, left, right, up, down);
            }
            else if (objPlayer.Pos.Y > 620)
            {
                roomb += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, objEnemy1, objMatterlvl1);
                doors(mundo, rooma, roomb, ref leftopen, ref downopen, ref rightopen, ref upopen);
                objMatterlvl1.LoadWalls(Content, left, right, up, down);
            }
        }
    }
}
