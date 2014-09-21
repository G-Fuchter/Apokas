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

        public void AI(Player objPlayer, Enemy1 objEnemy1)
        {
            //Resetea La "velocidad"
            objEnemy1.Speed.X = 0;
            objEnemy1.Speed.Y = 0;
            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (objEnemy1.Pos.X < objPlayer.Pos.X - 3)
                objEnemy1.Speed.X = objEnemy1.Vel;
            else if (objEnemy1.Pos.X > objPlayer.Pos.X + 3)
                objEnemy1.Speed.X = -objEnemy1.Vel;

            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (objEnemy1.Pos.Y < objPlayer.Pos.Y - 3)
                objEnemy1.Speed.Y = objEnemy1.Vel;
            else if (objEnemy1.Pos.Y > objPlayer.Pos.Y + 3)
                objEnemy1.Speed.Y = -objEnemy1.Vel;
        }
    }
}
