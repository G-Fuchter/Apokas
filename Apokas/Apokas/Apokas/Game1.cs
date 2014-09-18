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
        Player objPlayer = new Player();
        Enemy1 objEnemy1 = new Enemy1();
        SpriteFont font;
        float Opacity = 1f;

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
            //Character
            objPlayer.Speed = new Vector2(0.0f, 0.0f);
            objPlayer.Pos = new Vector2(0, 0);
            //Enemy1
            objEnemy1.Pos = new Vector2(500, 500);
            objEnemy1.Speed = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Images
            objPlayer.img = Content.Load<Texture2D>("Character");
            objEnemy1.img = Content.Load<Texture2D>("Enemy1");
            // Hitbox
            objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
            objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
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
            objPlayer.Movement();
            //AI
            objEnemy1.AI(objPlayer);
            //Updatea los Rectangles
            objPlayer.rctBody.X = (int)objPlayer.Pos.X + (int)objPlayer.Speed.X;
            objPlayer.rctBody.Y = (int)objPlayer.Pos.Y + (int)objPlayer.Speed.Y;
            objEnemy1.rctBody.X = (int)objEnemy1.Pos.X + (int)objEnemy1.Speed.X;
            objEnemy1.rctBody.Y = (int)objEnemy1.Pos.Y + (int)objEnemy1.Speed.Y;
            // Para que no se vaya de la pantalla
            if (!GraphicsDevice.Viewport.Bounds.Contains(objPlayer.rctBody))
                objPlayer.Speed = new Vector2(0, 0);
            if (!GraphicsDevice.Viewport.Bounds.Contains(objEnemy1.rctBody))
                objEnemy1.Speed = new Vector2(0, 0);
            // Collision Enemigo
            CollisionCharacters(objPlayer.rctBody, objEnemy1.rctBody, ref objPlayer.Vida, objEnemy1.Speed, ref Opacity);
            //Vida
            if (objPlayer.Vida <= 0)
            {
                //objPlayer.Pos = new Vector2(100, 100);
                objPlayer.Vida = 10.0f;
            }
            // Updatea Posicion
                //Character
            PosUpdate(ref objPlayer.rctBody,ref objPlayer.Pos,ref objPlayer.Speed);
                //Enemigo
            PosUpdate(ref objEnemy1.rctBody,ref objEnemy1.Pos,ref objEnemy1.Speed);          
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(objPlayer.img, objPlayer.Pos, Color.White * Opacity);
            spriteBatch.Draw(objEnemy1.img, objEnemy1.Pos, Color.White);
            DrawText();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawText()
        {
            spriteBatch.DrawString(font, Convert.ToString(objPlayer.Pos), new Vector2(800, 0), Color.White);
            spriteBatch.DrawString(font,"Vida: " + Convert.ToString(objPlayer.Vida), new Vector2(400, 0), Color.White);
        }




        // -----------------------------------------------Funciones inventadas-----------------------------------------
        public void PosUpdate (ref Rectangle rctCharacter,ref Vector2 CharacterPos,ref Vector2 CharacterSpeed)
        {
            CharacterPos += CharacterSpeed; // Actualiza posicion
            rctCharacter.Y = (int)CharacterPos.Y; // Actualiza la pos de los rectangle a la pos actual del personaje
            rctCharacter.X = (int)CharacterPos.X;
        }
        public void CollisionCharacters( Rectangle Player, Rectangle Character, ref float vida, Vector2 Charspeed, ref float opacity)
        {
            if (Player.Intersects(objEnemy1.rctBody))
            {
                if (objPlayer.invencible == false)
                {
                    objPlayer.Speed = new Vector2(0, 0);
                    vida -= 1;
                    /*opacity = 0.5f; PONER EN FUNCION
                    objPlayer.FadeDelay -= GameTime.ElapsedGameTime.Totalseconds;
                    if (objPlayer.FadeDelay <= 0)
                    {
                        objPlayer.FadeDelay = .035;
                        objPlayer.ValorAlfa += objPlayer.FadeIncrement;

                        if (objPlayer.ValorAlfa >= 255 || objPlayer.ValorAlfa <= 0)
                        {
                            objPlayer.FadeIncrement *= -1;
                        }
                    }
                     */
                    objPlayer.invencible = true;
                    objPlayer.lastInvenciblitity += (float)GameTime.ElapsedGameTime.TotalSeconds;
                    if (objPlayer.lastInvenciblitity > 3f)
                    {
                        objPlayer.invencible = false;
                    }
                }
            
            if (Character.Intersects(objPlayer.rctBody))
                objEnemy1.Speed = new Vector2(0, 0);
        }
    }
}
