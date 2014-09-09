using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Apokas
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Apokas : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character objCharacter = new Character();
        Enemy1 objEnemy1 = new Enemy1();
        SpriteFont font;

        public Apokas()
        {
            // Altura y Ancho de la ventana
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false; // ojito
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            // Vectors
            objCharacter.CharSpeed = new Vector2(0.0f, 0.0f);
            objCharacter.CharPos = new Vector2(0, 0); // Vector del Personaje Principal
            objEnemy1.Enemy1Pos = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height * 0.5f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Images
            objCharacter.imgCharacter = Content.Load<Texture2D>("Character");
            objEnemy1.imgEnemy1 = Content.Load<Texture2D>("Enemy1");
            // Hitbox
            objCharacter.rctCharacter = new Rectangle((int)(objCharacter.CharPos.X - objCharacter.imgCharacter.Width / 2), (int)(objCharacter.CharPos.Y - objCharacter.imgCharacter.Height / 2), objCharacter.imgCharacter.Width, objCharacter.imgCharacter.Height);
            objEnemy1.rctEnemy1 = new Rectangle((int)(objEnemy1.Enemy1Pos.X - objEnemy1.imgEnemy1.Width / 2), (int)(objEnemy1.Enemy1Pos.Y - objEnemy1.imgEnemy1.Height / 2), objEnemy1.imgEnemy1.Width, objEnemy1.imgEnemy1.Height);
            //Font
            font = Content.Load<SpriteFont>("MyFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // Movimiento Jugador
            objCharacter.Movement();
            //Updatea los Rectangles
            objCharacter.rctCharacter.X = (int)objCharacter.CharPos.X + (int)objCharacter.CharSpeed.X;
            objCharacter.rctCharacter.Y = (int)objCharacter.CharPos.Y + (int)objCharacter.CharSpeed.Y;
            objEnemy1.rctEnemy1.X = (int)objEnemy1.Enemy1Pos.X;
            objEnemy1.rctEnemy1.Y = (int)objEnemy1.Enemy1Pos.Y;
            // Para que no se vaya de la pantalla
            if (!GraphicsDevice.Viewport.Bounds.Contains(objCharacter.rctCharacter))
                objCharacter.CharSpeed = new Vector2(0, 0);
            objEnemy1.AI();
            // Collision Enemigo
            if (objCharacter.rctCharacter.Intersects(objEnemy1.rctEnemy1))
            {
                objCharacter.CharSpeed = new Vector2(0, 0);
                objCharacter.Vida -= 1;
            }
            if (objCharacter.Vida <= 0)
            {
                objCharacter.CharPos = new Vector2(100, 100);
                objCharacter.Vida = 10.0f;
            }
            // Updatea Posicion
            objCharacter.CharPos = objCharacter.CharPos + objCharacter.CharSpeed;
            objCharacter.rctCharacter.X = (int)objCharacter.CharPos.X;
            objCharacter.rctCharacter.Y = (int)objCharacter.CharPos.Y;
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(objCharacter.imgCharacter, objCharacter.CharPos, Color.White);
            spriteBatch.Draw(objEnemy1.imgEnemy1, objEnemy1.Enemy1Pos, Color.White);
            DrawText();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawText()
        {
            spriteBatch.DrawString(font, Convert.ToString(objCharacter.CharPos), new Vector2(800, 0), Color.White);
        }
    }
}
