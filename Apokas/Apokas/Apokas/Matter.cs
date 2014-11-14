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
        //Objects
        Player objPlayer = new Player();
        Room objRoom = new Room();
        // Rock
        public Color[] data;
        public Texture2D img;
        public Vector2 Pos;
        public Rectangle rct;
        public int Damage = 0;
        public int counter = 0;
        public int win = 0;

        
        public void CollisionwPlayer(ref Vector2 speed, Rectangle rctCharacter, ref int vida, Color[] dataCharacter)
        {
            if (objPlayer.IntersectPixels(rctCharacter, dataCharacter, rct, data))
            {
                speed = new Vector2(0, 0);
                vida -= Damage;
                counter += 1;
                win += 1;
            }
        }
    }
}
