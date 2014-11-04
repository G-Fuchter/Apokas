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
        public Color[] textureData;
        //Textures
        public Texture2D imgattack;
        public Texture2D img;
        //Vectors
        public Vector2 Pos;
        public Vector2 Speed;
        //Rectangles
        public Rectangle rctBody;
        public Rectangle rctSword;
        //Else
        public bool invencible = false;
        public int Vida = 10;
        public bool isAttacking;
        public const float MaxAttackTime = 0.3f;
        public float AttackTime;
        public bool AttackCollision;
        public bool Attacked1 = false;
        // player facing
        public bool[] face = new bool[4]; 
        
        // Probando invecibilidad
        public float currentTime;
        // objetos
        Enemy1 objEnemy1 = new Enemy1();
        /// <summary>
        /// Movimiento, hay 2 vectores, posición y speed. Speed predice donde vas a moverte y
        /// si hay algo, no le agrega nada pos. Si no hay nada le agrega a pos y ahí se mueve
        /// el personaje.
        /// </summary>
        public void Control(ref bool IsAttacking, ref Rectangle rctSwordo)
        {
            //Ataque
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                if (AttackTime <= 0.0f)
                {
                    isAttacking = true;
                    AttackTime = MaxAttackTime;

                }
            }
            
            //Movimiento
            bool run;
            Speed = new Vector2(0.0f, 0.0f); // Char speed esta ahi para predecir donde esta el character y la collisiones

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftShift)) // Sprint
                run = true;
            else
                run = false;
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A))
            {
                //Left
                face[0] = true;
                face[1] = false;
                face[2] = false;
                face[3] = false;
                rctSwordo.X = (int)Pos.X - rctSwordo.Width;
                rctSwordo.Y = (int)Pos.Y + img.Height / 2;
                if (run == true)
                    Speed.X = -6;
                else
                    Speed.X = -3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                //right
                face[0] = false;
                face[1] = true;
                face[2] = false;
                face[3] = false;
                rctSwordo.X = (int)Pos.X + img.Width;
                rctSwordo.Y = (int)Pos.Y + img.Height / 2;
                //sprint
                if (run == true)
                    Speed.X = 6;
                else
                    Speed.X = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                //Down
                face[0] = false;
                face[1] = false;
                face[2] = false;
                face[3] = true;
                rctSwordo.X = (int)Pos.X + img.Width/2;
                rctSwordo.Y = (int)Pos.Y + img.Height;
                //Sprint
                if (run == true)
                    Speed.Y = 6;
                else
                    Speed.Y = 3;
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                //Up
                face[0] = false;
                face[1] = false;
                face[2] = true;
                face[3] = false;
                rctSwordo.X = (int)Pos.X + img.Width / 2;
                rctSwordo.Y = (int)Pos.Y - rctSwordo.Height;
                //sprint
                if (run == true)
                    Speed.Y = -6;
                else
                    Speed.Y = -3;
            }
        }


        // Collisión con enemigos y invencibilidad.
        public void CollisionCharacters(Rectangle Player, Rectangle Enemy, ref int vida, Vector2 Charspeed, ref float opacity, Enemy1 objEnemy, int dmg, ref float current, ref bool inv, GameTime gameTime, Color[] dataplayer, Color[] dataenemy) 
        {
            if (IntersectPixels(Player, dataplayer, Enemy, dataenemy) == true) // cuando el jugador collisiona con enemigo
            {
                if (inv == false) // Si es invecible no hace daño
                {
                    Speed = new Vector2(0, 0); //atraviesa enemigos

                    vida -= dmg; // saca vida segun el daño del enemigo

                    opacity = 0.5f; // Reduce la opacidad del enemigo

                    inv = true; // lo hace invesible

                    objEnemy.Speed = new Vector2(0, 0);
                }
            }
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


        //Funcion de ATAQUE
        public void Attack(Rectangle rctenemy,ref int vidaenemy, ref Vector2 speedenemy, ref bool attackedonce)
        {
            if (rctSword.Intersects(rctenemy)) // intersecta la espada con el enemigo
            {
                if (isAttacking == true && attackedonce == false) // El usuario apreto space
                {
                     vidaenemy -= 1;
                     speedenemy = new Vector2(0, 0);
                     attackedonce = true;
                }
            }
        }

        public void DoAttack(GameTime gameTime)
        {
            if (isAttacking) // El usuario apreto space
            {
                if (AttackTime > 0.0f)
                {
                    AttackTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    
                }
                else
                {
                    isAttacking = false;
                    Attacked1 = false;
                }
            }
                else
            {
                AttackTime = 0.0f;
            }
        }


        // INTERSECCION DE PIXELES
        public bool IntersectPixels(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
 


        }
        }
    }
