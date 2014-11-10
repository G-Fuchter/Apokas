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
        //Objetos
        //Player ObjPlayer = new Player();
        public Color[] textureData;
        // bla
        public Texture2D img;
        public Vector2 Pos;
        public Vector2 Speed;
        public Rectangle rctBody;
        public int Damage = 1; // daño
        public int Vel = 2; // vel
        public int Vida = 5; // vida
        public bool bool_knockback; // si actúa el knockback
        public string knockbackside; // De donde le pegan
        bool once = true; // funcion knockback
        Vector2 past_pos = new Vector2();
        Vector2 cuadratica = new Vector2(0,0);
        Consola con = new Consola();

        //AI
        Vector2 playa = new Vector2();
        float Current_Time;
        float m, b;
        bool done;

        public void knockback()
        {
            if (bool_knockback) // si le pegaron
            {
                if (once) // se ejecuta solo la primer a vez
                {
                    past_pos.Y = Pos.Y;
                    once = false;
                }
                switch(knockbackside) // para ver de donde le pegaron
                {
                    case "left":
                        if (cuadratica.X > -57)
                        {
                            cuadratica.X -= 3;
                            cuadratica.Y = (float)0.025 * (cuadratica.X * cuadratica.X) + cuadratica.X;
                            Pos.X -= 4;
                            Pos.Y += cuadratica.Y;
                        }
                        else
                        { 
                            knockbackside = "false";
                            cuadratica = new Vector2(0, 0);
                        }
                        break;
                    case "right":
                        if (cuadratica.X < 57)
                        {
                            cuadratica.X += 3;
                            cuadratica.Y = (float)0.025 * (cuadratica.X * cuadratica.X) - cuadratica.X;
                            Pos.X += 4;
                            Pos.Y += cuadratica.Y;
                        }
                        else
                        {
                            knockbackside = "false";
                            cuadratica = new Vector2(0, 0);
                        }
                        break;
                    case "up":
                        if (Pos.Y > past_pos.Y - 50)
                            Pos.Y -= 4;
                        else
                            knockbackside = "false";
                        break;
                    case "down":
                        if (Pos.Y < past_pos.Y + 50)
                            Pos.Y += 4;
                        else
                            knockbackside = "false";
                        break;
                    default:
                        con.cout("ataque fallido", null, null, null);
                        bool_knockback = false;
                        break;
                }
            }
            else
                once = true;
        }

        public void AI(Vector2 PlayerSpeed, Vector2 PlayerPos, GameTime gameTime)
        {
            if (bool_knockback == false)
            {
                if (Current_Time > 1.5f && Current_Time < 2.5f)
                {
                    if (done == false)
                    {
                        done = true;
                        playa = PlayerPos;
                        m = (Pos.Y - playa.Y) / (Pos.X - playa.X);
                        b = m * Pos.X;
                        b = Pos.Y - b;
                    }
                    // Checkea si su pos.x es menor o mayor y se autocorrige
                    if (Pos.X < playa.X - 3)
                    {
                        Pos.X += 7;
                        Pos.Y = m * Pos.X + b;
                    }
                    else if (Pos.X > playa.X + 3)
                    {
                        Pos.X += -7;
                        Pos.Y = m * Pos.X + b;
                    }
                }
                else if (Current_Time > 2.5f)
                {
                    Current_Time = 0.0f;
                    done = false;
                }
                Current_Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
                Current_Time = 0f;
        }

        public void Update (GameTime gameTime, Player jugador)
        {
            knockback();
            AI(jugador.Speed, jugador.Pos,gameTime);
        }
    }
}
