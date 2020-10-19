using Game.Abstracciones;
using Game.Entidades;
using System;
using System.Collections.Generic;

namespace Game.SingletonManagers
{
    public static class ColisionManager
    {
        private static List<Daga> ArmasActivas = new List<Daga>();

        /// <summary>
        /// Agrego un arma a la lista de armas activas, para verificar las colisiones 
        /// con un enemigo especifico.
        /// </summary>
        /// <param name="arma">Arma que esta en estado activa.</param>
        public static void AgregarArmaActiva(Daga arma)
        {
            if (arma != null)
                ArmasActivas.Add(arma);
            else
                throw new ArgumentNullException();
        }

        public static void EliminarArma(Daga c)
        {
            ArmasActivas.Remove(c);
        }

        public static bool ColisionaEnemigoPersonaje(IVector2D pj, IVector2D enemigo)
        {
            return (VerificarColisionEnX(pj, enemigo) && VerificarColisionEnY(pj, enemigo));
        }

        public static bool ColisionaEnemigoArma(IVector2D e)
        {
            foreach (var item in ArmasActivas)
            {
                if (VerificarColisionEnX(ArmasActivas[0], e) && VerificarColisionEnY(ArmasActivas[0], e))
                {

                    Engine.Debug("Colisiono: Arma");
                    Engine.Debug(ArmasActivas[0].LimiteBot.ToString());
                    Engine.Debug(ArmasActivas[0].LimiteTop.ToString());
                    Engine.Debug(ArmasActivas[0].LimiteIzquierda.ToString());
                    Engine.Debug(ArmasActivas[0].LimiteDerecha.ToString());

                    Engine.Debug("Colisiono: Enemigo");
                    Engine.Debug(e.LimiteBot.ToString());
                    Engine.Debug(e.LimiteTop.ToString());
                    Engine.Debug(e.LimiteIzquierda.ToString());
                    Engine.Debug(e.LimiteDerecha.ToString());

                    return true;
                }
            }
            return false;
        }

        #region Funciones privadas

        //Corresponde que este metodo sea privado, lo dejo publico solo para poder correr los tests.
        private static bool VerificarColisionEnX(IVector2D v1, IVector2D v2)
        {
            if ((v1.LimiteDerecha < v2.LimiteDerecha && v1.LimiteIzquierda > v2.LimiteIzquierda) 
                || (v2.LimiteDerecha < v1.LimiteDerecha && v2.LimiteIzquierda > v1.LimiteIzquierda)
                || (v1.X == v2.X))
                return true; //Colisiona por completo

            if ((v1.X < v2.X && v1.LimiteDerecha > v2.LimiteIzquierda) 
                || (v2.X < v1.X && v2.LimiteDerecha > v1.LimiteIzquierda))
                return true; //Colision parcial
            return false;
        }

        //Corresponde que este metodo sea privado, lo dejo publico solo para poder correr los tests.
        private static bool VerificarColisionEnY(IVector2D v1, IVector2D v2)
        {
            if ((v1.LimiteTop < v2.LimiteTop && v1.LimiteBot > v2.LimiteBot) 
                || (v2.LimiteTop < v1.LimiteTop && v2.LimiteBot > v1.LimiteBot)
                || (v1.Y == v2.Y))
                return true; //Colisiona por completo

            if ((v1.Y < v2.Y && v1.LimiteTop > v2.LimiteBot) 
                || (v2.Y < v1.Y && v2.LimiteTop > v1.LimiteBot))
                return true; //Colision parcial
            return false;
        }

        #endregion

    }
}
