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
    }
}
