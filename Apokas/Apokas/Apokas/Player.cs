﻿using System;
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
        public Rectangle sourceRectangle;
        public Rectangle rctSword;
        //Else
        public bool invencible = false;
        public int Vida = 5;
        public bool isAttacking;
        public const float MaxAttackTime = 0.3f;
        public float AttackTime;
        public bool AttackCollision;
        public bool Attacked1 = false;
        public bool death;
        // player facing
        public bool[] face = new bool[4];
        // Spritebatch
        public Rectangle HuddestRectangle;
        public Rectangle HudsourceRectange;
        public Texture2D imgHealth;

        // Animation counter
        float countframes;
        
        // Probando invecibilidad
        public float currentTime;
        // objetos
        Enemy1 objEnemy1 = new Enemy1();
        Consola Con = new Consola();
        Game form1 = new Game();
        KeyboardState oldstate;
        /// <summary>
        /// Movimiento, hay 2 vectores, posición y speed. Speed predice donde vas a moverte y
        /// si hay algo, no le agrega nada pos. Si no hay nada le agrega a pos y ahí se mueve
        /// el personaje.
        /// </summary>
        public void Control(ref bool IsAttacking, ref Rectangle rctSwordo, GameTime gametime)
        {
            var newState = Keyboard.GetState();
            //Ataque
            if (newState.IsKeyDown(Keys.Space))
            {
                if (AttackTime <= 0.0f)
                {
                    isAttacking = true;
                    if (face[0])
                    {
                        sourceRectangle = new Rectangle(85 * 5, 147, 85, 147);
                    }
                    else if (face[1])
                    {
                        sourceRectangle = new Rectangle(85 * 7, 147, 85, 147);
                    }
                    else if (face[2])
                    {
                        sourceRectangle = new Rectangle(85 * 3, 147, 85, 147);
                    }
                    else if (face[3])
                    {
                        sourceRectangle = new Rectangle(85 * 1, 147, 85, 147);
                    }

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
                rctSwordo.Y = (int)Pos.Y + 147 / 2;
                if (run == true)
                    Speed.X = -6;
                else
                    Speed.X = -3;
                Animacion(gametime, ref countframes, ref sourceRectangle,85 * 6, 0);
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.D))
            {
                //right
                face[0] = false;
                face[1] = true;
                face[2] = false;
                face[3] = false;
                rctSwordo.X = (int)Pos.X + 85;
                rctSwordo.Y = (int)Pos.Y + 147 / 2;
                //sprint
                if (run == true)
                    Speed.X = 6;
                else
                    Speed.X = 3;
                Animacion(gametime, ref countframes, ref sourceRectangle, 85 * 9, 0);
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                //Down
                face[0] = false;
                face[1] = false;
                face[2] = false;
                face[3] = true;
                rctSwordo.X = (int)Pos.X + 85/2;
                rctSwordo.Y = (int)Pos.Y + 147;
                //Sprint
                if (run == true)
                    Speed.Y = 6;
                else
                    Speed.Y = 3;
                Animacion(gametime, ref countframes, ref sourceRectangle, 85 * 0, 0);
            }
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.W))
            {
                //Up
                face[0] = false;
                face[1] = false;
                face[2] = true;
                face[3] = false;
                rctSwordo.X = (int)Pos.X + 85 / 2;
                rctSwordo.Y = (int)Pos.Y - 30;
                //sprint
                if (run == true)
                    Speed.Y = -6;
                else
                    Speed.Y = -3;
                Animacion(gametime, ref countframes, ref sourceRectangle, 85 * 3, 0);
            }
        }


        // Collisión con enemigos y invencibilidad.
        public void CollisionCharacters(Rectangle Player, Rectangle Enemy, ref int vida, Vector2 Charspeed, ref float opacity, Enemy1 objEnemy, int dmg, ref float current, ref bool inv, GameTime gameTime, Color[] dataplayer, Color[] dataenemy) 
        {
            if (IntersectPixels(Player, dataplayer, Enemy, dataenemy) == true) // cuando el jugador collisiona con enemigo
            {
                Con.cout("Interseccion!!!", null, null, null);
                if (inv == false) // Si es invecible no hace daño
                {
                    Speed = new Vector2(0, 0); //atraviesa enemigos

                    vida -= dmg; // saca vida segun el daño del enemigo

                    opacity = 0.5f; // Reduce la opacidad del enemigo

                    inv = true; // lo hace invesible

                    Vida_func(); // Checckea
                }
            }
            // Si es invencible
            if (inv == true)
            {
                isAttacking = false;
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
        public void Attack(Rectangle rctenemy,ref int vidaenemy, ref bool attackedonce, ref bool knock, ref string knockside)
        {
            if (rctSword.Intersects(rctenemy)) // intersecta la espada con el enemigo
            {
                if (isAttacking == true && attackedonce == false) // El usuario apreto space
                {
                     vidaenemy -= 1;
                     attackedonce = true;
                    //knocback
                     knock = true;
                     if (face[0])
                         knockside = "left";
                     else if (face[1])
                         knockside = "right";
                     else if (face[2])
                         knockside = "up";
                     else if (face[3])
                         knockside = "down";
                     Con.cout("Attack!", knockside, Convert.ToString(objEnemy1.bool_knockback), null);
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
                    sourceRectangle = new Rectangle(85, 0, 85, 147);
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

        //Vida
        public void Vida_func()
        {
            switch (Vida)
            { 
                case 6:
                    Vida = 5;
                    break;
                case 5:
                    HudsourceRectange = new Rectangle(341 * 0, 0, 341, 127);
                    break;
                case 4:
                    HudsourceRectange = new Rectangle(341 * 1, 0, 341, 127);
                    break;
                case 3:
                    HudsourceRectange = new Rectangle(341 * 2, 0, 341, 127);
                    break;
                case 2:
                    HudsourceRectange = new Rectangle(341 * 3, 0, 341, 127);
                    break;
                case 1:
                    HudsourceRectange = new Rectangle(341 * 4, 0, 341, 127);
                    break;
                default:
                    HudsourceRectange = new Rectangle(341 * 5, 0, 341, 127);
                    death = true;
                    break;
            }
        }

        //Animation
        public void Animacion(GameTime gameTime, ref float contador, ref Rectangle source, int a, int b)
        {
            contador += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (contador > 0.1f && contador < 0.2f)
            {
                source = new Rectangle(a, b, 85, 147);
            }
            else if (contador >0.2f && contador < 0.3f)
            {
                source = new Rectangle(a + 85, b, 85, 147);
            }
            else if (contador > 0.3f && contador < 0.4f)
            {
                source = new Rectangle(a + 85 * 2, b, 85, 147);
            }
            else if (contador > 0.4f && contador < 0.5f)
            {
                source = new Rectangle(a + 85, b, 85, 147);
            }
            else if (contador > 0.5f)
            {
                contador = 0f;
            }
        }
    }
}
