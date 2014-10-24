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
        Texture2D red;
        //Testing
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Clases
        Player objPlayer = new Player();
        Enemies objEnemies = new Enemies();
        Enemy1 objEnemy1 = new Enemy1();
        Matter objMatter = new Matter();
        matter_lvl1 objMatter_lvl1 = new matter_lvl1();
        Room objRoom = new Room();
        //font
        SpriteFont font;
        //opacidad
        float Opacity = 1f;
        // enum
        enum GameState
        {
            MainMenu,
            Settings,
            Playing,
        }
        GameState CurrentGameState = GameState.MainMenu;

        cButton btnPlay;
        cButton btnSettings;
        

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
            //World
            
            objRoom.worldgenerate(ref objRoom.world);
            objRoom.Roomselect(ref objRoom.Roomx, ref objRoom.Roomy, objRoom.world);
            objRoom.doors(objRoom.world, objRoom.Roomx, objRoom.Roomy, ref objRoom.leftopen, ref objRoom.downopen, ref objRoom.rightopen, ref objRoom.upopen);
            // Vectors
                //Character
            objPlayer.Speed = new Vector2(0.0f, 0.0f);
            objPlayer.Pos = new Vector2(200, 200);
                //Enemy1
            objEnemy1.Pos = new Vector2(600, 600);
            objEnemy1.Speed = new Vector2(0, 0);
                //Rock
            objMatter_lvl1.PosRock = new Vector2(450, 250);
                //Lago
            objMatter_lvl1.PosLago = new Vector2(400, 400);
            //variables
            objPlayer.invencible = false;
            objPlayer.Vida = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // Images
            //mainMenu
                    IsMouseVisible = true;
                    btnPlay = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
                    btnPlay.setPosition(new Vector2(350, 300));
            //Settings
                    btnSettings = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
                    btnSettings.sSetPosition(new Vector2(350, 100));
                
                    red = Content.Load<Texture2D>("red_square");
                    objPlayer.imgattack = Content.Load<Texture2D>("character_attack");
                    objPlayer.img = Content.Load<Texture2D>("Character");
                    objEnemy1.img = Content.Load<Texture2D>("Enemy1");
                    objMatter_lvl1.imgRock = Content.Load<Texture2D>("rock");
                    objMatter_lvl1.imgLago = Content.Load<Texture2D>("lago");
                    objMatter_lvl1.LoadWalls(Content, objRoom.leftopen, objRoom.rightopen, objRoom.upopen, objRoom.downopen);
                    // Hitbox
                    objMatter_lvl1.rctRock = new Rectangle((int)(objMatter_lvl1.PosRock.X), (int)(objMatter_lvl1.PosRock.Y), (objMatter_lvl1.imgRock.Width), objMatter_lvl1.imgRock.Height);
                    objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
                    objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
                    objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
                    objMatter_lvl1.rctLago = new Rectangle((int)(objMatter_lvl1.PosLago.X), (int)(objMatter_lvl1.PosLago.Y), (objMatter_lvl1.imgLago.Width), objMatter_lvl1.imgLago.Height);
                    //walls
                    objMatter_lvl1.rctLeft = new Rectangle(0, 0, objMatter_lvl1.imgLeft.Width, objMatter_lvl1.imgLeft.Height);
                    objMatter_lvl1.rctRight = new Rectangle(0, 0, objMatter_lvl1.imgRight.Width, objMatter_lvl1.imgRight.Height);
                    objMatter_lvl1.rctUp = new Rectangle(0, 0, objMatter_lvl1.imgUp.Width, objMatter_lvl1.imgUp.Height);
                    objMatter_lvl1.rctDown = new Rectangle(0, 0, objMatter_lvl1.imgDown.Width, objMatter_lvl1.imgDown.Height);
                    //Font
                    font = Content.Load<SpriteFont>("MyFont");
                    // Collision data
                    //player
                    objPlayer.textureData = new Color[objPlayer.img.Width * objPlayer.img.Height];
                    objPlayer.img.GetData(objPlayer.textureData);
                    //enemy1
                    objEnemy1.textureData = new Color[objEnemy1.img.Width * objEnemy1.img.Height];
                    objEnemy1.img.GetData(objEnemy1.textureData);
                    //rock
                    objMatter_lvl1.dataRock = new Color[objMatter_lvl1.imgRock.Width * objMatter_lvl1.imgRock.Height];
                    objMatter_lvl1.imgRock.GetData(objMatter_lvl1.dataRock);
                    //lago
                    objMatter_lvl1.dataLago = new Color[objMatter_lvl1.imgLago.Width * objMatter_lvl1.imgLago.Height];
                    objMatter_lvl1.imgLago.GetData(objMatter_lvl1.dataLago);
                    // walls
                    objMatter_lvl1.dataLeft = new Color[objMatter_lvl1.imgLeft.Width * objMatter_lvl1.imgLeft.Height];
                    objMatter_lvl1.imgLeft.GetData(objMatter_lvl1.dataLeft);
                    objMatter_lvl1.dataRight = new Color[objMatter_lvl1.imgRight.Width * objMatter_lvl1.imgRight.Height];
                    objMatter_lvl1.imgRight.GetData(objMatter_lvl1.dataRight);
                    objMatter_lvl1.dataUp = new Color[objMatter_lvl1.imgUp.Width * objMatter_lvl1.imgUp.Height];
                    objMatter_lvl1.imgUp.GetData(objMatter_lvl1.dataUp);
                    objMatter_lvl1.dataDown = new Color[objMatter_lvl1.imgDown.Width * objMatter_lvl1.imgDown.Height];
                    objMatter_lvl1.imgDown.GetData(objMatter_lvl1.dataDown);
                   

            
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
           
            // TODO: Add your update logic here
            // switch del enum
            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.IsClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    if (btnSettings.sIsClicked == true) CurrentGameState = GameState.Settings;
                    btnSettings.Update(mouse);
                    
                    break;
                case GameState.Playing:
                    // mouse invisible para el juego

                    IsMouseVisible = false;
                    // Movimiento Jugador
                    objPlayer.Control(ref objPlayer.isAttacking);
                    //AI
                    objEnemy1.AI(objPlayer.Speed, ref objEnemy1.Speed, objPlayer.Pos, objEnemy1.Pos, ref objEnemy1.Vel);
                    //Attack
                    objPlayer.Attack(objEnemy1.rctBody, ref objEnemy1.Vida, ref objEnemy1.Speed, ref objPlayer.Attacked1);
                    //Updatea los Rectangles
                    objPlayer.rctSword.Y = (int)objPlayer.Pos.Y + objPlayer.img.Height / 2 - 5;
                    objPlayer.rctSword.X = (int)objPlayer.Pos.X + objPlayer.img.Width;
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
                    objPlayer.CollisionCharacters(objPlayer.rctBody, objEnemy1.rctBody, ref objPlayer.Vida, objEnemy1.Speed, ref Opacity, objEnemy1, objEnemy1.Damage, ref objPlayer.currentTime, ref objPlayer.invencible, gameTime, objPlayer.textureData, objEnemy1.textureData);
                    // Collision Matter
                    //Rock
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctRock, objPlayer.rctBody, objMatter_lvl1.DamageRock, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataRock);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctRock, objEnemy1.rctBody, objMatter_lvl1.DamageRock, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataRock);
                    //Lago
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctLago, objPlayer.rctBody, objMatter_lvl1.DamageLago, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataLago);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctLago, objEnemy1.rctBody, objMatter_lvl1.DamageLago, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataLago);
                    //walls
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctLeft, objPlayer.rctBody, 0, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataLeft);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctLeft, objEnemy1.rctBody, 0, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataLeft);
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctRight, objPlayer.rctBody, 0, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataRight);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctRight, objEnemy1.rctBody, 0, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataRight);
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctUp, objPlayer.rctBody, 0, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataUp);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctUp, objEnemy1.rctBody, 0, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataUp);
                    objMatter.CollisionwPlayer(ref objPlayer.Speed, objMatter_lvl1.rctDown, objPlayer.rctBody, 0, ref objPlayer.Vida, objPlayer.textureData, objMatter_lvl1.dataDown);
                    objMatter.CollisionwPlayer(ref objEnemy1.Speed, objMatter_lvl1.rctDown, objEnemy1.rctBody, 0, ref objEnemy1.Vida, objEnemy1.textureData, objMatter_lvl1.dataDown);
                    //Vida
                    if (objPlayer.Vida <= 0)
                    {
                        //objPlayer.Pos = new Vector2(100, 100);
                        objPlayer.Vida = 10;
                    }
                    // Updatea Posicion
                    //Character
                    PosUpdate(ref objPlayer.rctBody, ref objPlayer.Pos, ref objPlayer.Speed);
                    //Enemigo
                    PosUpdate(ref objEnemy1.rctBody, ref objEnemy1.Pos, ref objEnemy1.Speed);
                    // DoAttack
                    objPlayer.DoAttack(gameTime);
            
                    break;
                case GameState.Settings:
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend); //SpriteSortMode.BackToFront, BlendState.AlphaBlend
            
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("blank"), new Rectangle(0, 0, 1000, 700), Color.White);
                    btnPlay.Draw(spriteBatch);

                    break;
                case GameState.Playing:
                    // text
                    DrawText();
                    // Puertas
                    spriteBatch.Draw(objMatter_lvl1.imgLeft, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(objMatter_lvl1.imgRight, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(objMatter_lvl1.imgUp, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(objMatter_lvl1.imgDown, new Vector2(0, 0), Color.White);

                    // Obstaculos
                    spriteBatch.Draw(objMatter_lvl1.imgRock, objMatter_lvl1.PosRock, Color.White);
                    spriteBatch.Draw(objMatter_lvl1.imgLago, objMatter_lvl1.PosLago, Color.White);
                    if (objPlayer.isAttacking == true)
                    {
                        spriteBatch.Draw(objPlayer.imgattack, objPlayer.Pos, Color.White * Opacity);
                    }
                    else
                    {
                        spriteBatch.Draw(objPlayer.img, objPlayer.Pos, Color.White * Opacity);
                    }
                    spriteBatch.Draw(objEnemy1.img, objEnemy1.Pos, Color.White);
                    spriteBatch.Draw(red, new Vector2(objPlayer.rctSword.X, objPlayer.rctSword.Y), Color.White);
                    
                    break;
                case GameState.Settings:
                    spriteBatch.Draw(Content.Load<Texture2D>("Settigns"), new Rectangle(0, 0, 1000,700), Color.White);
                    btnSettings.DrawS(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
            
        }






        // -----------------------------------------------Funciones inventadas-----------------------------------------
        public void PosUpdate (ref Rectangle rctCharacter,ref Vector2 CharacterPos,ref Vector2 CharacterSpeed)
        {
            CharacterPos += CharacterSpeed; // Actualiza posicion
            rctCharacter.Y = (int)CharacterPos.Y; // Actualiza la pos de los rectangle a la pos actual del personaje
            rctCharacter.X = (int)CharacterPos.X;
        }


        private void DrawText()
        {
            spriteBatch.DrawString(font, Convert.ToString(objPlayer.Pos), new Vector2(800, 0), Color.White);
            spriteBatch.DrawString(font, "Vida: " + Convert.ToString(objPlayer.IntersectPixels(objPlayer.rctBody, objPlayer.textureData, objEnemy1.rctBody, objEnemy1.textureData)), new Vector2(400, 0), Color.White);
            spriteBatch.DrawString(font, "Time: " + Convert.ToString(objPlayer.currentTime), new Vector2(200, 0), Color.White);
            spriteBatch.DrawString(font, "bida: " + Convert.ToString(objEnemy1.Vida), new Vector2(100, 0), Color.White);
        } 
    }
}