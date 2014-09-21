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
    public class Player
    {
        public Texture2D img;
        public Vector2 Pos;
        public Vector2 Speed;
        public Rectangle rctBody;
        public bool invencible = false;
        public float Vida = 10.0f;
        // Probando invecibilidad
        public float currentTime;

        /// <summary>
        /// Movimiento, hay 2 vectores, posición y speed. Speed predice donde vas a moverte y
        /// si hay algo, no le agrega nada pos. Si no hay nada le agrega a pos y ahí se mueve
        /// el personaje.
        /// </summary>
        public void Movement()
        {
            bool run;
            Speed = new Vector2(0.0f, 0.0f); // Char speed esta ahi para predecir donde esta el character y la collisiones

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftShift)) // Sprint
                run = true;
            else
                run = false;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                if (run == true)
                    Speed.X = -6;
                else
                    Speed.X = -3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                if (run == true)
                    Speed.X = 6;
                else
                    Speed.X = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                if (run == true)
                    Speed.Y = 6;
                else
                    Speed.Y = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                if (run == true)
                    Speed.Y = -6;
                else
                    Speed.Y = -3;
            }
        }
        // Collisión con enemigos y invencibilidad.
        public void CollisionCharacters(Rectangle Player, Rectangle Enemy, ref float vida, Vector2 Charspeed, ref float opacity, Enemy1 objEnemy, int dmg, ref float current, ref bool inv, GameTime gameTime) 
        {
            if (Player.Intersects(objEnemy.rctBody)) // cuando el jugador collisiona con enemigo
            {
                if (inv == false) // Si es invecible no hace daño
                {
                    Speed = new Vector2(0, 0); //atraviesa enemigos
                    vida -= dmg; // saca vida segun el daño del enemigo
                    opacity = 0.5f; // Reduce la opacidad del enemigo
                    inv = true; // lo hace invesible
                }
            }
            if (Enemy.Intersects(rctBody)) // cuando el enemigo coliciona con el jugador
                objEnemy.Speed = new Vector2(0, 0);
            // Si es invencible
            if (inv == true)
            { 
                current += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (current >= 2f)
                {
                    inv = false;
                    opacity = 1f;
                    current = 0f;
                }
            }
        }
    }
}
