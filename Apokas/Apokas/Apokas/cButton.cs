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
    class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        Color mColor = new Color(255, 255, 255, 255);
        public Vector2 size;

        public cButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture; //ScreenWidth = 1000 ; ScreenHeight = 700
            size = new Vector2(graphics.Viewport.Width / 10, graphics.Viewport.Height / 14); // divido el viewport para cuando se cambie la resolucion, los valores se mantengan

        }
        bool down;
        public bool IsClicked;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1); //Rectangle del mouse

            if (mouseRectangle.Intersects(rectangle))
            {
                if (mColor.A == 255) down = false;
                if (mColor.A == 0) down = true;
                if (down) // cambia el canal alpha si el mouse esta sobre el
                {
                    mColor.A += 3;
                }
                else
                {
                    mColor.A -= 3;
                }
                //CLick
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    IsClicked = true;
                }
                else if (mColor.A < 255)
                {
                    mColor.A += 3;
                    IsClicked = false;
                }
            }
        }

        public void setPosition(Vector2 newPositon)
        {
            position = newPositon;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, mColor);
        }
    }
}
