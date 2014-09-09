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
        public Texture2D imgEnemy1;
        public Vector2 Enemy1Pos;
        public Rectangle rctEnemy1;
        Character objCharacter = new Character();

        public void AI()
        {
            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (Enemy1Pos.X < objCharacter.CharPos.X)
            {
                Enemy1Pos.X = Enemy1Pos.X + 3;
            }
            if (Enemy1Pos.X > objCharacter.CharPos.X)
            {
                Enemy1Pos.X = Enemy1Pos.X - 3;
            }
       
            // Checkea si su pos.y es menor y mayor y se autocorrige
            if (Enemy1Pos.Y > objCharacter.CharPos.Y)
            {
                Enemy1Pos.Y = Enemy1Pos.Y - 3;
            }
            if (Enemy1Pos.Y < objCharacter.CharPos.Y)
            {
                Enemy1Pos.Y = Enemy1Pos.Y + 3;
            }
        }
    }
}
