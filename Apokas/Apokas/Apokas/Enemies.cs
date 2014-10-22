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
        /*El clase Enemies el el pariente de todos los enemigos. Esta clase esta asi se
         puede usar objetos en funciones. Acá se pone las cosas que tienen todos los enemigos
         en común*/

        public void AI(Vector2 PlayerSpeed, ref Vector2 EnemySpeed, Vector2 PlayerPos, Vector2 EnemyPos, ref int EnemyVel)
        {
            //Resetea La "velocidad"
            EnemySpeed.X = 0;
            EnemySpeed.Y = 0;
            // Checkea si su pos.x es menor o mayor y se autocorrige
            if (EnemyPos.X < PlayerPos.X - 3)
                EnemySpeed.X = EnemyVel;
            else if (EnemyPos.X > PlayerPos.X + 3)
                EnemySpeed.X = -EnemyVel;

            // Checkea si su pos.y es menor y mayor y se autocorriges
            if (EnemyPos.Y < PlayerPos.Y - 3)
                EnemySpeed.Y = EnemyVel;
            else if (EnemyPos.Y > PlayerPos.Y + 3)
                EnemySpeed.Y = -EnemyVel;
        }
    }
}
