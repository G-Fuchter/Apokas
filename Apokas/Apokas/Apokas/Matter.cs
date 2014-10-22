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
        Player objPlayer = new Player();
        public void CollisionwPlayer(ref Vector2 speed, Rectangle rctMatter, Rectangle rctCharacter, int damage, ref int vida, Color[] dataCharacter, Color[] dataMatter)
        {
            if (objPlayer.IntersectPixels(rctCharacter, dataCharacter, rctMatter, dataMatter))
            {
                speed = new Vector2(0, 0);
                vida -= damage;
            }
        }
    }
}
