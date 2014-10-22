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
    public class matter_lvl1 : Matter
    {
        // Rock
        public Color[] dataRock;
        public Texture2D imgRock;
        public Vector2 PosRock;
        public Rectangle rctRock;
        public int DamageRock = 0;
        // Lago
        public Color[] dataLago;
        public Texture2D imgLago;
        public Vector2 PosLago;
        public Rectangle rctLago;
        public int DamageLago = 0;
        // Hitbox Fondo
        public Color[] dataHbFondo;
        public Texture2D imgHbFondo;
        public Vector2 PosHbFondo;
        public Rectangle rctHbFondo;
        public int DamageHbFondo;
        // Fondo
        public Texture2D imgFondo;
    }
}
