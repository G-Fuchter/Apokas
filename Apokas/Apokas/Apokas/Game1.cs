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
        Matter Roca = new Matter();
        Matter LeftWall = new Matter();
        Matter RightWall = new Matter();
        Matter UpWall = new Matter();
        Matter DownWall = new Matter();
        Matter[] Wallskin = new Matter[4];

        Texture2D red;
        //Testing
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Clases
        Player objPlayer = new Player();
        Enemies objEnemies = new Enemies();
        Enemy1 objEnemy1 = new Enemy1();
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
        cButton btnExit;

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
                //Enemy1
            objEnemy1.Pos = new Vector2(600, 600);
            objEnemy1.Speed = new Vector2(0, 0);
                //Rock
            Roca.Pos = new Vector2(450, 250);
                //Lago
            Lago.Pos = new Vector2(400, 400);
            //variables
            objPlayer.invencible = false;
            objPlayer.Vida = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);


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


            // Images
            red = Content.Load<Texture2D>("red_square"); 
            objPlayer.imgattack = Content.Load<Texture2D>("character_attack");
            objPlayer.img = Content.Load<Texture2D>("Character");
            objEnemy1.img = Content.Load<Texture2D>("Enemy1");
            Roca.img = Content.Load<Texture2D>("rock");
            Lago.img = Content.Load<Texture2D>("lago");
            objRoom.LoadWalls(Content, objRoom.leftopen, objRoom.rightopen, objRoom.upopen, objRoom.downopen, LeftWall, RightWall, UpWall, DownWall, Wallskin); //carga los hitboxes tambien
            // Hitbox
            Roca.rct = new Rectangle((int)(Roca.Pos.X), (int)(Roca.Pos.Y), (Roca.img.Width), Roca.img.Height);
            objPlayer.rctBody = new Rectangle((int)(objPlayer.Pos.X - objPlayer.img.Width / 2), (int)(objPlayer.Pos.Y - objPlayer.img.Height / 2), objPlayer.img.Width, objPlayer.img.Height);
            objEnemy1.rctBody = new Rectangle((int)(objEnemy1.Pos.X - objEnemy1.img.Width / 2), (int)(objEnemy1.Pos.Y - objEnemy1.img.Height / 2), objEnemy1.img.Width, objEnemy1.img.Height);
            objPlayer.rctSword = new Rectangle((int)(objPlayer.Pos.X + objPlayer.img.Width), (int)(objPlayer.Pos.Y), 10, 10);
            Lago.rct = new Rectangle((int)(Lago.Pos.X), (int)(Lago.Pos.Y), (Lago.img.Width), Lago.img.Height);
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
            Roca.data = new Color[Roca.img.Width * Roca.img.Height];
            Roca.img.GetData(Roca.data);
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
                    MediaPlayer.Play(GameMusic);
                    MediaPlayer.IsRepeating = true;
                    if (btnSettings.sIsClicked == true) CurrentGameState = GameState.Settings;
                    btnSettings.Update(mouse);
                    if (btnExit.eIsClicked == true) this.Exit();
                    btnExit.Update(mouse);

                    break;
                case GameState.Playing:
                    // mouse invisible para el juego

                    IsMouseVisible = false;

                    // Movimiento Jugador
                    objPlayer.Control(ref objPlayer.isAttacking, ref objPlayer.rctSword);
                    //AI
                    objEnemy1.AI(objPlayer.Speed, ref objEnemy1.Speed, objPlayer.Pos, ref objEnemy1.Pos, ref objEnemy1.Vel,objPlayer.face,objEnemy1.bool_knockback, gameTime);
                    objEnemy1.knockback(objEnemy1.bool_knockback);
                    //Attack
                    objPlayer.Attack(objEnemy1.rctBody, ref objEnemy1.Vida, ref objEnemy1.Speed, ref objPlayer.Attacked1, ref objEnemy1.bool_knockback, ref objEnemy1.knockbackside);
                    //Updatea los Rectangles
                    /*objPlayer.rctSword.Y = (int)objPlayer.Pos.Y + objPlayer.img.Height / 2 - 5;
                    objPlayer.rctSword.X = (int)objPlayer.Pos.X + objPlayer.img.Width;*/
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
                    Roca.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    //Lago
                    Lago.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    //walls
                    LeftWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    RightWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    UpWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
                    DownWall.CollisionwPlayer(ref objPlayer.Speed, objPlayer.rctBody, ref objPlayer.Vida, objPlayer.textureData);
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
                    // DoAttack
                    objPlayer.DoAttack(gameTime);
                    // Room Change
                    objRoom.RoomChange(ref objRoom.world, ref objRoom.Roomx, ref objRoom.Roomy, objPlayer, objEnemy1, LeftWall, RightWall, UpWall, DownWall, Roca, Lago, ref objRoom.leftopen, ref objRoom.rightopen, ref objRoom.upopen, ref objRoom.downopen, Content, Wallskin);
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
                    // pasto
                    spriteBatch.Draw(Content.Load<Texture2D>("Grass"), new Vector2(0, 0), Color.White);
                    // Obstaculos
                    spriteBatch.Draw(Roca.img, Roca.Pos, Color.White);
                    spriteBatch.Draw(Lago.img, Lago.Pos, Color.White);
                    //player
                    if (objPlayer.isAttacking == true)
                        spriteBatch.Draw(objPlayer.imgattack, objPlayer.Pos, Color.White * Opacity);
                    else
                        spriteBatch.Draw(objPlayer.img, objPlayer.Pos, Color.White * Opacity);

                    spriteBatch.Draw(objEnemy1.img, objEnemy1.Pos, Color.White);
                    spriteBatch.Draw(red, new Vector2(objPlayer.rctSword.X, objPlayer.rctSword.Y), Color.White);
                    //Puertas
                    spriteBatch.Draw(Wallskin[2].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[0].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[1].img, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(Wallskin[3].img, new Vector2(0, 0), Color.White);
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
            spriteBatch.DrawString(font,"Vida: " + Convert.ToString(objPlayer.IntersectPixels(objPlayer.rctBody, objPlayer.textureData, objEnemy1.rctBody, objEnemy1.textureData)), new Vector2(400, 0), Color.White);
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