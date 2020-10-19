using Game.Abstracciones;
using Game.SingletonManagers;
using System;

namespace Game.Entidades
{
    public class Daga : Vector2D, IVector2D
    {
        public Daga(float x, float y) : base(x, y)
        {

        }

        private float Velocidad = 7;
        public bool EnVuelo { get; set; }
        private bool Derecha { get; set; }
        /// <summary>
        /// La coordenada X desde la cual fue lanzado el cuchillo
        /// </summary>
        public float CoordenadaLanzamiento { get; private set; }
        public bool Activa { get ; set ; }
        public float LimiteDerecha { get; set; }
        public float LimiteIzquierda { get; set; }
        public float LimiteTop { get; set; }
        public float LimiteBot { get; set; }
        private Texture SpriteProyectilDerecha = new Texture("Imagenes\\DisparoDer.png");
        private Texture SpriteProyectilIzquierda = new Texture("Imagenes\\DisparoIzq.png");

        #region MetodosPropios

        /// <summary>
        /// Metodo para lanzar cuchillos.
        /// Debe validarse si aun se poseen instancias disponibles para lanzar(MAX 2).
        /// </summary>
        public void LanzarCuchillo(float x, float y, bool derecha)
        {
            X = x;
            CoordenadaLanzamiento = x;
            Y = y;
            Derecha = derecha;
            EnVuelo = true;
            ColisionManager.AgregarArmaActiva(this);
        }

        /// <summary>
        /// Metodo para destruir un cuchillo.
        /// Se invoca al impactar con un enemigo o luego de recorrer cierta distancia.
        /// Guarda la instancia actual dentro de un pool de objetos para ser re-utilizados.
        /// </summary>
        public void DestruirCuchillo()
        {
            X = 0;
            Y = 0;
            CoordenadaLanzamiento = 0;
            EnVuelo = false;
            Program.pj.RealmacenarCuchillo(this);
            ColisionManager.EliminarArma(this);
        }

        #endregion

        #region Metodos Vector2D

        public void Actualizar()
        {
            SetearLimites();
            if (EnVuelo)
            {
                if (Derecha)
                {
                    if (X > CoordenadaLanzamiento + 200)
                    {
                        DestruirCuchillo();
                    }
                    else
                        X += Velocidad;
                }
                else
                {
                    if (X < CoordenadaLanzamiento - 200)
                    {
                        DestruirCuchillo();
                    }
                    else
                        X -= Velocidad;
                }
                Dibujar();
            }
        }

        /// <summary>
        /// Verifica si el cuchillo se encuentra en vuelo.
        /// Si es asi, lo dibuja.
        /// </summary>
        public void Dibujar()
        {
            if (Derecha)
            {
                Engine.Draw(SpriteProyectilDerecha, X, Y);
            }
            else
            {
                Engine.Draw(SpriteProyectilIzquierda, X, Y);
            }
        }

        private void SetearLimites()
        {
            LimiteTop = Y - 5;
            LimiteBot = Y + 5;
            LimiteDerecha = X + 5;
            LimiteIzquierda = X - 5;
        }

        #endregion
    }
}
