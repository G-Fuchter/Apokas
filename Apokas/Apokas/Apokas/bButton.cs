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
    class bButton
    {
        Texture2D mTexture;
        Vector2 mPosition;
        Rectangle mRectangle;
        public Vector2 mSize;
        public bool IsClicked;

        public void setPosition(Vector2 newPositon)
        {
            mPosition = newPositon;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mRectangle, Color.White);
        }

        public bButton(Texture2D newTexture, GraphicsDevice graphics)
        {
            mTexture = newTexture;
            mSize = new Vector2(100,50);
        }
        
        public void Update(MouseState mouse)
        {
            mRectangle = new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)mSize.X, (int)mSize.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1); //Rectangle del mouse

            if (mouseRectangle.Intersects(mRectangle))
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
        }


        
    }
}

