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
        public bool[,] Clear = new bool[6, 6];
        public int enemies;
        public int Roomx;
        public int Roomy;
        public bool leftopen, downopen, rightopen, upopen;
        bool[] lado = new bool[4]; // Lado en que entra
        Random rnd = new Random();

        //World function
        public void worldgenerate(ref int[,] mundo, ref bool[,] despejado)
        {
            Random rand1 = new Random(); //genera numero random que determina el cuarto
            // 1 = vacio, 2-5 = diferentes presets de cuartos
            for (int a = 0; a < 6; a++) //for para llenar las 2 dimensiones del cuarto
            {
                for (int b = 0; b < 6; b++)
                {
                    int room = rand1.Next(1, 5);
                    mundo[a, b] = room;
                    despejado[a, b] = false;
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
        public void RoomLoad(int[,] mundo, int rooma, int roomb, Player objPlayer, ref Enemy1[] objEnemy1, ref Matter[] Rock, Matter Lago, ContentManager Content, ref Matter Memory, ref Matter Healthpack)
        {
            // Para ver por que puerta viene
            if (lado[0])
                objPlayer.Pos = new Vector2(850, 330);
            else if (lado[1])
                objPlayer.Pos = new Vector2(110, 330);
            else if (lado[2])
                objPlayer.Pos = new Vector2(435, 500);
            else if (lado[3])
                objPlayer.Pos = new Vector2(435, 200);
            objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + 85), (int)(objPlayer.Pos.Y), 10, 10);
            objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X), (int)(objPlayer.Pos.Y), 85, 127);
            int enemyx = rnd.Next(100, 800);
            int enemyy = rnd.Next(100, 500);
            int memo = rnd.Next(0, 10);
            int healthpack = rnd.Next(0, 10);

            switch (mundo[rooma, roomb])
            {

                    //------------//
                    //--CUARTO 2--//
                    //------------//

                case 2:
                    // ROCAS
                    if (Clear[rooma, roomb] == false) //Si no está vacio
                    {
                        Rock[0] = new Matter();
                        Rock[1] = new Matter();
                        Rock[2] = new Matter();
                        Rock[3] = new Matter();
                        //POSICION
                        Rock[0].Pos = new Vector2(0, 380);
                        Rock[1].Pos = new Vector2(950, 380);
                        Rock[2].Pos = new Vector2(435, 150);
                        Rock[3].Pos = new Vector2(435, 650);
                        for (int a = 0; a < 4; a++)
                        {
                            Rock[a].img = Content.Load<Texture2D>("rock");
                            Rock[a].rct = new Rectangle((int)(Rock[a].Pos.X), (int)(Rock[a].Pos.Y), (Rock[a].img.Width), Rock[a].img.Height);
                            Rock[a].data = new Color[Rock[a].img.Width * Rock[a].img.Height];
                            Rock[a].img.GetData(Rock[a].data);
                        }

                        // Enemigos
                        enemies = 2;
                        objEnemy1[0] = new Enemy1();
                        objEnemy1[1] = new Enemy1();
                        // Posiciones
                        objEnemy1[0].Pos = new Vector2(enemyx, enemyy);
                        enemyx = rnd.Next(100, 900);
                        enemyy = rnd.Next(100, 600);
                        objEnemy1[0].Pos = new Vector2(enemyx, enemyy);
                        for (int a = 0; a < 2; a++)
                        {
                            objEnemy1[a].img = Content.Load<Texture2D>("Fantasmita");
                            objEnemy1[a].rctBody = new Rectangle((int)(objEnemy1[a].Pos.X), (int)(objEnemy1[a].Pos.Y), 82, 135);
                            objEnemy1[a].textureData = new Color[objEnemy1[a].img.Width * objEnemy1[a].img.Height];
                            objEnemy1[a].img.GetData(objEnemy1[a].textureData);
                        }

                        //Vida y Memo
                        if (memo > 4)
                        {
                            Memory = new Matter();
                            Memory.Pos = new Vector2(100,400);
                            Memory.img = Content.Load<Texture2D>("Io");
                            Memory.rct = new Rectangle((int)Memory.Pos.X, (int)Memory.Pos.Y, Memory.img.Width, Memory.img.Height);
                            Memory.data = new Color[Memory.img.Width * Memory.img.Height];
                            Memory.img.GetData(Memory.data);
                        }
                        if (healthpack > 4)
                        {
                            Healthpack = new Matter();
                            Healthpack.Pos = new Vector2(120, 340);
                            Healthpack.Damage = -1;
                            Healthpack.img = Content.Load<Texture2D>("healthpack");
                            Healthpack.rct = new Rectangle((int)Healthpack.Pos.X, (int)Healthpack.Pos.Y, Healthpack.img.Width, Healthpack.img.Height);
                            Healthpack.data = new Color[Healthpack.img.Width * Healthpack.img.Height];
                            Healthpack.img.GetData(Healthpack.data);
                        }
                    }
                    
                    Lago.Pos = new Vector2(550, 300);
                    // Hitbox
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    break;

                    //------------//
                    //--CUARTO 3--//
                    //------------//

                case 3:
                    if (Clear[rooma, roomb] == false) // Si esta vacio
                    {
                        // ROCAS
                        Rock[0] = new Matter();
                        Rock[1] = new Matter();
                        Rock[2] = new Matter();
                        Rock[3] = new Matter();
                        //POSICION
                        Rock[0].Pos = new Vector2(0, 380);
                        Rock[1].Pos = new Vector2(950, 380);
                        Rock[2].Pos = new Vector2(435, 150);
                        Rock[3].Pos = new Vector2(435, 650);
                        for (int a = 0; a < 4; a++)
                        {
                            Rock[a].img = Content.Load<Texture2D>("rock");
                            Rock[a].rct = new Rectangle((int)(Rock[a].Pos.X), (int)(Rock[a].Pos.Y), (Rock[a].img.Width), Rock[a].img.Height);
                            Rock[a].data = new Color[Rock[a].img.Width * Rock[a].img.Height];
                            Rock[a].img.GetData(Rock[a].data);
                        }

                        // Enemigos
                        enemies = 1;
                        objEnemy1[0] = new Enemy1();
                        // Posiciones
                        objEnemy1[0].Pos = new Vector2(enemyx, enemyy);
                        for (int a = 0; a < 1; a++)
                        {
                            objEnemy1[a].img = Content.Load<Texture2D>("Fantasmita");
                            objEnemy1[a].rctBody = new Rectangle((int)(objEnemy1[a].Pos.X), (int)(objEnemy1[a].Pos.Y), 82, 135);
                            objEnemy1[a].textureData = new Color[objEnemy1[a].img.Width * objEnemy1[a].img.Height];
                            objEnemy1[a].img.GetData(objEnemy1[a].textureData);
                        }

                        //Vida y Memo
                        if (memo > 4)
                        {
                            Memory = new Matter();
                            Memory.Pos = new Vector2(100, 400);
                            Memory.img = Content.Load<Texture2D>("Io");
                            Memory.rct = new Rectangle((int)Memory.Pos.X, (int)Memory.Pos.Y, Memory.img.Width, Memory.img.Height);
                            Memory.data = new Color[Memory.img.Width * Memory.img.Height];
                            Memory.img.GetData(Memory.data);
                        }
                        if (healthpack > 4)
                        {
                            Healthpack = new Matter();
                            Healthpack.Pos = new Vector2(120, 340);
                            Healthpack.Damage = -1;
                            Healthpack.img = Content.Load<Texture2D>("healthpack");
                            Healthpack.rct = new Rectangle((int)Healthpack.Pos.X, (int)Healthpack.Pos.Y, Healthpack.img.Width, Healthpack.img.Height);
                            Healthpack.data = new Color[Healthpack.img.Width * Healthpack.img.Height];
                            Healthpack.img.GetData(Healthpack.data);
                        }
                    }


                    Lago.Pos = new Vector2(550, 300);
                    // Hitbox
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    break;

                    //------------//
                    //--CUARTO 4--//
                    //------------//

                case 4:
                    if (Clear[rooma, roomb] == false)
                    {
                        // ROCAS
                        Rock[0] = new Matter();
                        Rock[1] = new Matter();
                        Rock[2] = new Matter();
                        Rock[3] = new Matter();
                        //POSICION
                        Rock[0].Pos = new Vector2(0, 380);
                        Rock[1].Pos = new Vector2(950, 380);
                        Rock[2].Pos = new Vector2(435, 150);
                        Rock[3].Pos = new Vector2(435, 650);
                        for (int a = 0; a < 4; a++)
                        {
                            Rock[a].img = Content.Load<Texture2D>("rock");
                            Rock[a].rct = new Rectangle((int)(Rock[a].Pos.X), (int)(Rock[a].Pos.Y), (Rock[a].img.Width), Rock[a].img.Height);
                            Rock[a].data = new Color[Rock[a].img.Width * Rock[a].img.Height];
                            Rock[a].img.GetData(Rock[a].data);
                        }

                        // Enemigos
                        enemies = 3;
                        objEnemy1[0] = new Enemy1();
                        objEnemy1[1] = new Enemy1();
                        objEnemy1[2] = new Enemy1();
                        // Posiciones
                        objEnemy1[0].Pos = new Vector2(enemyx, enemyy);
                        enemyx = rnd.Next(100, 900);
                        enemyy = rnd.Next(100, 600);
                        objEnemy1[1].Pos = new Vector2(enemyx, enemyy);
                        enemyx = rnd.Next(100, 900);
                        enemyy = rnd.Next(100, 600);
                        objEnemy1[2].Pos = new Vector2(enemyx, enemyy);
                        for (int a = 0; a < 3; a++)
                        {
                            objEnemy1[a].img = Content.Load<Texture2D>("Fantasmita");
                            objEnemy1[a].rctBody = new Rectangle((int)(objEnemy1[a].Pos.X), (int)(objEnemy1[a].Pos.Y), 82, 135);
                            objEnemy1[a].sourceRectangle = new Rectangle(a + 82 * 2, 0, 82, 135);
                            objEnemy1[a].textureData = new Color[objEnemy1[a].img.Width * objEnemy1[a].img.Height];
                            objEnemy1[a].img.GetData(objEnemy1[a].textureData);
                        }

                        //Vida y Memo
                        if (memo > 4)
                        {
                            Memory = new Matter();
                            Memory.Pos = new Vector2(100, 400);
                            Memory.img = Content.Load<Texture2D>("Io");
                            Memory.rct = new Rectangle((int)Memory.Pos.X, (int)Memory.Pos.Y, Memory.img.Width, Memory.img.Height);
                            Memory.data = new Color[Memory.img.Width * Memory.img.Height];
                            Memory.img.GetData(Memory.data);
                        }
                        if (healthpack > 4)
                        {
                            Healthpack = new Matter();
                            Healthpack.Pos = new Vector2(120, 340);
                            Healthpack.Damage = -1;
                            Healthpack.img = Content.Load<Texture2D>("healthpack");
                            Healthpack.rct = new Rectangle((int)Healthpack.Pos.X, (int)Healthpack.Pos.Y, Healthpack.img.Width, Healthpack.img.Height);
                            Healthpack.data = new Color[Healthpack.img.Width * Healthpack.img.Height];
                            Healthpack.img.GetData(Healthpack.data);
                        }

                    }
                    Lago.Pos = new Vector2(550, 300);
                    // Hitbox
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    break;

                    //------------//
                    //--CUARTO 5--//
                    //------------//

                case 5:
                    if (Clear[rooma, roomb] == false)
                    {
                        // ROCAS
                        Rock[0] = new Matter();
                        Rock[1] = new Matter();
                        Rock[2] = new Matter();
                        Rock[3] = new Matter();
                        //POSICION
                        Rock[0].Pos = new Vector2(0, 380);
                        Rock[1].Pos = new Vector2(950, 380);
                        Rock[2].Pos = new Vector2(435, 150);
                        Rock[3].Pos = new Vector2(435, 650);
                        for (int a = 0; a < 4; a++)
                        {
                            Rock[a].img = Content.Load<Texture2D>("rock");
                            Rock[a].rct = new Rectangle((int)(Rock[a].Pos.X), (int)(Rock[a].Pos.Y), (Rock[a].img.Width), Rock[a].img.Height);
                            Rock[a].data = new Color[Rock[a].img.Width * Rock[a].img.Height];
                            Rock[a].img.GetData(Rock[a].data);
                        }

                        // Enemigos
                        enemies = 2;
                        objEnemy1[0] = new Enemy1();
                        objEnemy1[1] = new Enemy1();
                        // Posiciones
                        objEnemy1[0].Pos = new Vector2(enemyx, enemyy);
                        enemyx = rnd.Next(100, 900); // otros numeros random
                        enemyy = rnd.Next(100, 600); // otros numeros random
                        objEnemy1[1].Pos = new Vector2(enemyx, enemyy);
                        for (int a = 0; a < 2; a++)
                        {
                            objEnemy1[a].img = Content.Load<Texture2D>("Fantasmita");
                            objEnemy1[a].rctBody = new Rectangle((int)(objEnemy1[a].Pos.X), (int)(objEnemy1[a].Pos.Y), 82, 135);
                            objEnemy1[a].textureData = new Color[objEnemy1[a].img.Width * objEnemy1[a].img.Height];
                            objEnemy1[a].img.GetData(objEnemy1[a].textureData);
                        }

                        //Vida y Memo
                        if (memo > 4)
                        {
                            Memory = new Matter();
                            Memory.Pos = new Vector2(100, 400);
                            Memory.img = Content.Load<Texture2D>("Io");
                            Memory.rct = new Rectangle((int)Memory.Pos.X, (int)Memory.Pos.Y, Memory.img.Width, Memory.img.Height);
                            Memory.data = new Color[Memory.img.Width * Memory.img.Height];
                            Memory.img.GetData(Memory.data);
                        }
                        if (healthpack > 4)
                        {
                            Healthpack = new Matter();
                            Healthpack.Pos = new Vector2(120, 340);
                            Healthpack.Damage = -1;
                            Healthpack.img = Content.Load<Texture2D>("healthpack");
                            Healthpack.rct = new Rectangle((int)Healthpack.Pos.X, (int)Healthpack.Pos.Y, Healthpack.img.Width, Healthpack.img.Height);
                            Healthpack.data = new Color[Healthpack.img.Width * Healthpack.img.Height];
                            Healthpack.img.GetData(Healthpack.data);
                        }
                    }
                    Lago.Pos = new Vector2(550, 300);
                    // Hitbox
                    Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
                    break;
            }
            doors(world, Roomx, Roomy, ref leftopen, ref downopen, ref rightopen, ref upopen);
        }
        public void RoomChange(ref int[,] mundo, ref int rooma, ref int roomb, Player objPlayer, ref Enemy1[] objEnemy1,Matter objLeft, Matter objRight, Matter objUp, Matter objDown, ref Matter[] Rock, Matter Pond , ref bool left, ref bool right, ref bool up, ref bool down, ContentManager Content, Matter[] skin, ref Matter memo, ref Matter health)
        {
            if (objPlayer.Pos.X < 4)
            {
                lado[0] = true;
                lado[1] = false;
                lado[2] = false;
                lado[3] = false;
                rooma -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, ref objEnemy1,ref Rock, Pond, Content, ref memo, ref health);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            else if (objPlayer.Pos.X > 905)
            {
                lado[0] = false;
                lado[1] = true;
                lado[2] = false;
                lado[3] = false;
                rooma += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, ref objEnemy1, ref Rock, Pond, Content, ref memo, ref health);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            if (objPlayer.Pos.Y < 160)
            {
                lado[0] = false;
                lado[1] = false;
                lado[2] = true;
                lado[3] = false;
                roomb -= 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, ref objEnemy1, ref Rock, Pond, Content, ref memo, ref health);
                LoadWalls(Content, left, right, up, down, objLeft, objRight, objUp, objDown, skin);
            }
            else if (objPlayer.Pos.Y > 550)
            {
                // De que lado entra
                lado[0] = false;
                lado[1] = false;
                lado[2] = false;
                lado[3] = true;
                roomb += 1;
                RoomLoad(mundo, rooma, roomb, objPlayer, ref objEnemy1, ref Rock, Pond, Content, ref memo, ref health);
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
