using System;
using System.Linq;
using System.Runtime.InteropServices;
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

        // objeto
        Consola objConosla = new Consola();
        Matter Lago = new Matter();
        Matter[] Roca = new Matter [4];
        Matter LeftWall = new Matter();
        Matter RightWall = new Matter();
        Matter UpWall = new Matter();
        Matter DownWall = new Matter();
        Matter[] Wallskin = new Matter[4];
        Player objPlayer = new Player();
        Enemy1[] objEnemy1 = new Enemy1 [3];
        Room objRoom = new Room();

        //Testing
        Texture2D red;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
        cButton btnExit;
        //death
        cButton btnQuit;
        cButton btnTry;

        Song GameMusic;
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
            //Wallskin
            for (int a = 0; a < 4; a++)
                Wallskin[a] = new Matter();
            objRoom.worldgenerate(ref objRoom.world);
            objRoom.Roomselect(ref objRoom.Roomx, ref objRoom.Roomy, objRoom.world);
            objRoom.doors(objRoom.world, objRoom.Roomx, objRoom.Roomy, ref objRoom.leftopen, ref objRoom.downopen, ref objRoom.rightopen, ref objRoom.upopen);
            // Vectors
                //Character
            objPlayer.Speed = new Vector2(0.0f, 0.0f);
            objPlayer.Pos = new Vector2(200, 200);
                //Lago
            Lago.Pos = new Vector2(400, 400);
            //variables
            objPlayer.invencible = false;
            // Rectangle
            objPlayer.HuddestRectangle = new Rectangle(0, 0, 341, 127);
            objPlayer.HudsourceRectange = new Rectangle(341 * 0, 0, 341, 127);
            //mouse
            Mouse.WindowHandle = Window.Handle;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // BOTONES
            //mainMenu
            IsMouseVisible = true;
            btnPlay = new cButton(Content.Load<Texture2D>("PlayButton"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(450, 250));
            //Settings
            btnSettings = new cButton(Content.Load<Texture2D>("SettingsButton"), graphics.GraphicsDevice);
            btnSettings.setPosition(new Vector2(450, 375));
            //Exit
            btnExit = new cButton(Content.Load<Texture2D>("ExitButton"), graphics.GraphicsDevice);
            btnExit.setPosition(new Vector2(450, 500));
            //Death
            btnQuit = new cButton(Content.Load<Texture2D>("QuitButton"), graphics.GraphicsDevice);
            btnQuit.setPosition(new Vector2(450, 450));
            btnTry = new cButton(Content.Load<Texture2D>("TryButton"), graphics.GraphicsDevice);
            btnTry.setPosition(new Vector2(450, 350));

            // Images
            red = Content.Load<Texture2D>("red_square"); 
            objPlayer.imgattack = Content.Load<Texture2D>("character_attack");
            objPlayer.img = Content.Load<Texture2D>("Character");
            Lago.img = Content.Load<Texture2D>("lago");
            objPlayer.imgHealth = Content.Load<Texture2D>("health");
            objRoom.LoadWalls(Content, objRoom.leftopen, objRoom.rightopen, objRoom.upopen, objRoom.downopen, LeftWall, RightWall, UpWall, DownWall, Wallskin); //carga los hitboxes tambien
            //hitbox
            objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
            //Font
            font = Content.Load<SpriteFont>("MyFont");
            // Collision data
                //player
            objPlayer.textureData = new Color[objPlayer.img.Width * objPlayer.img.Height];
            objPlayer.img.GetData(objPlayer.textureData);
                //lago
            Lago.data = new Color[Lago.img.Width * Lago.img.Height];
            Lago.img.GetData(Lago.data);
            //Write
            objConosla.Write(objRoom.world, objRoom);
            // musica
            GameMusic = Content.Load<Song>("GameMusic");
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

            MouseState mouse = Mouse.GetState();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.IsClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    //MediaPlayer.Play(GameMusic); // Activa la canci�n
                    //MediaPlayer.IsRepeating = true;
                    if (btnSettings.sIsClicked == true) CurrentGameState = GameState.Settings;
                    btnSettings.Update(mouse);
                    if (btnExit.eIsClicked == true) this.Exit();
                    btnExit.Update(mouse);
                    break;

                case GameState.Playing: // Ingame
                    // mouse invisible para el juego
                    if (objPlayer.death == false)
                    {
                        IsMouseVisible = false;
                    }
                    // ENEMIGOS
                    for (int a = 0; a < 3; a++) // 3 enemigos
                    {
                        if (objEnemy1[a] != null)
                        {
                            objPlayer.CollisionCharacters(objPlayer.rctBody, objEnemy1[a].rctBody, ref objPlayer.Vida, objPlayer.Speed, ref Opacity, objEnemy1[a], objEnemy1[a].Damage, ref objPlayer.currentTime, ref objPlayer.invencible, gameTime, objPlayer.textureData, objEnemy1[a].textureData);
                            objPlayer.Attack(objEnemy1[a].rctBody, ref objEnemy1[a].Vida, ref objPlayer.Attacked1, ref objEnemy1[a].bool_knockback, ref objEnemy1[a].knockbackside);
                            objPlayer.DoAttack(gameTime);
                            objEnemy1[a].Update(gameTime, objPlayer);
                            objEnemy1[a].rctBody.X = (int)objEnemy1[a].Pos.X + (int)objEnemy1[a].Speed.X;
                            objEnemy1[a].rctBody.Y = (int)objEnemy1[a].Pos.Y + (int)objEnemy1[a].Speed.Y;
                            if (objEnemy1[a].Vida <= 0)
                                objEnemy1[a] = null;
                        }
                    }

                    
                        //death
                    if (objPlayer.death)
                    {
                        IsMouseVisible = true;

                        if (btnQuit.qIsClicked == true) this.Exit();
                        

                        if (btnTry.tIsClicked == true)
                        {
                            // TRY AGAIN
                        }
                        btnTry.Update(mouse);

                    }

                    // Movimiento Jugador
                    objPlayer.Control(ref objPlayer.isAttacking, ref objPlayer.rctSword);

                    //Updatea los Rectangles
                    objPlayer.rctBody.X = (int)objPlayer.Pos.X + (int)objPlayer.Speed.X;
                    objPlayer.rctBody.Y = (int)objPlayer.Pos.Y + (int)objPlayer.Speed.Y;

                    // Para que no se vaya de la pantalla
                    if (!GraphicsDevice.Viewport.Bounds.Contains(objPlayer.rctBody))
                        objPlayer.Speed = new Vector2(0, 0);

                    // Collision Matter
                    //Lago
                    Lago.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);

                    //ROCAS
                    for (int a = 0; a < 4; a++)
                    {
                        if (Roca[a] != null)
                        {
                            Roca[a].CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                        }
                    }


                    //walls
                    LeftWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    RightWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    UpWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    DownWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);

                    // Updatea Posicion
                    PosUpdate(ref objPlayer.rctBody, ref objPlayer.Pos, ref objPlayer.Speed);

                    // Cambio de cuarto
                    objRoom.RoomChange(ref objRoom.world, ref objRoom.Roomx, ref objRoom.Roomy, objPlayer, ref objEnemy1, LeftWall, RightWall, UpWall, DownWall,ref Roca, Lago, ref objRoom.leftopen, ref objRoom.rightopen, ref objRoom.upopen, ref objRoom.downopen, Content, Wallskin);

                    break;
                case GameState.Settings:
                    // poner el back buton
                    break;
            }
            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend); //SpriteSortMode.BackToFront, BlendState.AlphaBlend

            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("Main Menu"), new Rectangle(0, 0, 1000, 700), Color.White);
                    btnSettings.Draw(spriteBatch);
                    btnPlay.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    // PASTO
                    spriteBatch.Draw(Content.Load<Texture2D>("Grass"), new Vector2(0, 0), Color.White);

                    // Obstaculos
                        //ROCA
                    for (int a = 0; a < 4; a++)
                    {
                        if (Roca[a] != null)
                        {
                            spriteBatch.Draw(Roca[a].img, Roca[a].Pos, Color.White);
                            spriteBatch.Draw(red, new Vector2(Roca[a].rct.X, Roca[a].rct.Y), Color.White);
                        }
                    }
                    spriteBatch.Draw(Lago.img, Lago.Pos, Color.White);

                    //JUGADOR
                    if (objPlayer.isAttacking == true)
                        spriteBatch.Draw(objPlayer.imgattack, objPlayer.Pos, Color.White * Opacity);
                    else
                        spriteBatch.Draw(objPlayer.img, objPlayer.Pos, Color.White * Opacity);

                    // Enemigos
                    for (int a = 0; a < 3; a++) // Dibujando enemigos
                    {
                        if (objEnemy1[a] != null)
                        {
                            spriteBatch.Draw(objEnemy1[a].img, objEnemy1[a].Pos, Color.White);
                        }
                    }

                    // TEST ESPADA
                    spriteBatch.Draw(red, new Vector2(objPlayer.rctSword.X, objPlayer.rctSword.Y), Color.White);
                    spriteBatch.Draw(red, new Vector2(objPlayer.rctBody.X, objPlayer.rctBody.Y), Color.White);

                    //PUERTAS
                    spriteBatch.Draw(Wallskin[2].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[0].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[1].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[3].img, new Vector2(0, 0), Color.White);

                    //HUD
                    spriteBatch.Draw(objPlayer.imgHealth, objPlayer.HuddestRectangle, objPlayer.HudsourceRectange, Color.White);

                    // MUERTE
                    if (objPlayer.death)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("DeathMenu"), new Rectangle(0, 0, 1000, 700), Color.White);
                        btnQuit.Draw(spriteBatch);
                        btnTry.Draw(spriteBatch);
                       
                    }

                    //TEXTO
                    DrawText();

                    break;
                case GameState.Settings:
                    spriteBatch.Draw(Content.Load<Texture2D>("SettingsMenu"), new Rectangle(0, 0, 1000, 700), Color.White);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
        private void DrawText()
        {
            spriteBatch.DrawString(font, Convert.ToString(objPlayer.Pos), new Vector2(800, 0), Color.White);
            spriteBatch.DrawString(font,"Vida: " + Convert.ToString(objPlayer.Vida), new Vector2(400, 0), Color.White);
            spriteBatch.DrawString(font, "Time: " + Convert.ToString(objPlayer.currentTime), new Vector2(200, 0), Color.White);
            spriteBatch.DrawString(font, Convert.ToString(objRoom.Roomx) + Convert.ToString(objRoom.Roomy), new Vector2(150, 0), Color.White);
            //spriteBatch.DrawString(font, Convert.ToString(objRoom.world[objRoom.Roomx - 1, objRoom.Roomy]) + Convert.ToString(objRoom.world[objRoom.Roomx, objRoom.Roomy] + Convert.ToString(objRoom.world[objRoom.Roomx + 1, objRoom.Roomy])), new Vector2(100, 0), Color.White);
        }




        // -----------------------------------------------Funciones inventadas-----------------------------------------
        public void PosUpdate (ref Rectangle rctCharacter,ref Vector2 CharacterPos,ref Vector2 CharacterSpeed)
        {
            CharacterPos += CharacterSpeed; // Actualiza posicion
            rctCharacter.Y = (int)CharacterPos.Y; // Actualiza la pos de los rectangle a la pos actual del personaje
            rctCharacter.X = (int)CharacterPos.X;
        }
    }
}