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
        // start button
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;
        // settings button
        Texture2D sTexture;
        Vector2 sPosition;
        Rectangle sRectangle;

        public Vector2 size;


        public cButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture; //ScreenWidth = 1000 ; ScreenHeight = 70
            size = new Vector2(graphics.Viewport.Width / 10, graphics.Viewport.Height / 14); // divido el viewport para cuando se cambie la resolucion, los valores se mantengan

        }
        public void SettingsButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            sTexture = newTexture; //ScreenWidth = 1000 ; ScreenHeight = 70
            size = new Vector2(graphics.Viewport.Width / 10, graphics.Viewport.Height / 14); // divido el viewport para cuando se cambie la resolucion, los valores se mantengan

        }
        bool down;
        public bool IsClicked;

        public bool sIsClicked;

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            sRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1); //Rectangle del mouse

            if (mouseRectangle.Intersects(rectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    IsClicked = true;
                }
                else
                {
                    IsClicked = false;
                }
            }

            if (mouseRectangle.Intersects(sRectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    sIsClicked = true;
                }
                else
                {
                    sIsClicked = false;
                }
            }
        }

        public void setPosition(Vector2 newPositon)
        {
            position = newPositon;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

        }
    }
}
