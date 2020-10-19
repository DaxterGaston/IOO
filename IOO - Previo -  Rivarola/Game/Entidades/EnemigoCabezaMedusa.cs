using Game.Abstracciones;
using Game.SingletonManagers;
using System;

namespace Game.Entidades
{
    public class EnemigoCabezaMedusa : Vector2D, IVector2D, IEnemigo
    {
        #region SetUp

        public EnemigoCabezaMedusa(float x, float y, float velocidad = 5) : base(x, y)
        {
            _velocidad = velocidad;
            SetearLimites();
        }

        private bool vivo = false;
        private bool DireccionDerecha = false;
        private bool DireccionArriba = false;
        private float SpawnX, SpawnY;
        private readonly float _velocidad;
        private Texture spriteCabezaIzquierda = new Texture("Imagenes\\MedusaHeadIzq.png");
        private Texture spriteCabezaDerecha = new Texture("Imagenes\\MedusaHeadDer.png");
        private int _vida = 1;

        public bool Vivo { get { return vivo; } private set { vivo = value; } }
        public DateTime Eliminacion { get; private set; }
        public float LimiteDerecha { get ; set ; }
        public float LimiteIzquierda { get ; set ; }
        public float LimiteTop { get ; set ; }
        public float LimiteBot { get ; set ; }

        #endregion

        #region Metodos Vector2D

        public void Actualizar()
        {
            SetearLimites();
            if (Vivo)
            {
                Patrullaje();
                Dibujar();
            }
            if (Colisiona())
            {
                RecibirDaño();
                if (_vida == 0)
                    MatarEnemigo();
            }
        }

        public void Dibujar()
        {
            if (DireccionDerecha)
                Engine.Draw(spriteCabezaDerecha, X, Y);
            else
                Engine.Draw(spriteCabezaIzquierda, X, Y);
        }

        #endregion

        #region Metodos Enemigo

        /// <summary>
        /// Metodo para spawnear un nuevo enemigo.
        /// </summary>
        /// <param name="x">Eje X donde se va a spawnear al enemigo.</param>
        /// <param name="y">Eje Y donde se va a spawnear al enemigo.</param>
        public void SpawnearEnemigo(float x, float y)
        {
            Vivo = true;
            SpawnX = x;
            SpawnY = y;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Mato al enemigo cuando es golpeado por un arma.
        /// </summary>
        public void MatarEnemigo()
        {
            Vivo = false;
        }

        public void RecibirDaño()
        {
            _vida -= 1;
        }

        public bool Colisiona()
        {
            return ColisionManager.ColisionaEnemigoArma(this);
        }

        public void Patrullaje()
        {
            if (DireccionArriba)
            {
                if (Y > SpawnY - 50)
                    Y -= _velocidad;
                else
                    DireccionArriba = false;
            }
            else if (!DireccionArriba)
            {
                if (Y < SpawnY + 50)
                    Y += _velocidad;
                else
                    DireccionArriba = true;
            }
            if (DireccionDerecha)
            {
                if (X < SpawnX + 100)
                    X += _velocidad;
                else
                    DireccionDerecha = false;
            }
            else if (!DireccionDerecha)
            {
                if (X > SpawnX - 100)
                    X -= _velocidad;
                else
                    DireccionDerecha = true;
            }
        }

        private void SetearLimites()
        {
            //TODO: Revisar si la propiedad de ancho y alto de Texture funciona
            LimiteTop = Y - (20);
            LimiteBot = Y + (20);
            LimiteDerecha = X + (20);
            LimiteIzquierda = X - (20);
        }

        #endregion

    }
}
