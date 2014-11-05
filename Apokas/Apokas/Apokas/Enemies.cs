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
    public class Enemies
    {
        Vector2 playa = new Vector2();
        float Current_Time;
        float m, b;
        bool done;
        /*El clase Enemies el el pariente de todos los enemigos. Esta clase esta asi se
         puede usar objetos en funciones. Acá se pone las cosas que tienen todos los enemigos
         en común*/

        public void AI(Vector2 PlayerSpeed, ref Vector2 EnemySpeed, Vector2 PlayerPos, ref Vector2 EnemyPos, ref int EnemyVel,bool[] cara ,bool knock,GameTime gameTime)
        {
            if (knock == false)
            {
                if (Current_Time > 2.5f && Current_Time < 4f)
                {
                    if (done == false)
                    {
                        done = true;
                        playa = PlayerPos;
                        m = (EnemyPos.Y - playa.Y) / (EnemyPos.X - playa.X);
                        b = m * EnemyPos.X;
                        b = EnemyPos.Y - b;
                    }
                    //Resetea La "velocidad"
                    EnemySpeed.X = 0;
                    EnemySpeed.Y = 0;
                    // Checkea si su pos.x es menor o mayor y se autocorrige
                    if (EnemyPos.X < playa.X - 3)
                    {
                        EnemyPos.X += 7;
                        EnemyPos.Y = m * EnemyPos.X + b;
                    }
                    else if (EnemyPos.X > playa.X + 3)
                    {
                        EnemyPos.X += -7;
                        EnemyPos.Y = m * EnemyPos.X + b;
                    }
                    /*
                    // Checkea si su pos.y es menor y mayor y se autocorriges
                    if (EnemyPos.Y < playa.Y - 3)
                        EnemySpeed.Y = 7;
                    if (EnemyPos.Y > playa.Y + 3)
                        EnemySpeed.Y = -7;
                    // Resetea el reloj
                    if ((EnemyPos.Y > playa.Y - 3 && EnemyPos.Y < playa.Y + 3) && (EnemyPos.X > playa.X - 3 && EnemyPos.X < playa.X + 3))
                        Current_Time = 0.0f;*/
                }
                else if (Current_Time > 4f)
                {
                    Current_Time = 0.0f;
                    done = false;
                }
                Current_Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
                Current_Time = 0f;
        }
    }
}
