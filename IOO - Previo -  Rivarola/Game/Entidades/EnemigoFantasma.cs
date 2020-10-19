using Game.Abstracciones;
using Game.SingletonManagers;
using System;

namespace Game.Entidades
{
    public class EnemigoFantasma : Vector2D, IEnemigo, IVector2D
    {
        #region SetUp

        public EnemigoFantasma(float x, float y, float velocidad = 5) : base(x, y)
        {
            X = x;
            Y = y;
            _velocidad = velocidad;
            SetearLimites();
        }

        //Seteo valor por defecto para vivo como false.
        private bool vivo = false;
        private bool DireccionDerecha = false;
        private bool DireccionArriba = false;
        private float SpawnX, SpawnY;
        private readonly float _velocidad;
        private Texture spriteFantasmaIzquierda = new Texture("Imagenes\\EnemigoEspectroIzq.png");
        private Texture spriteFantasmaDerecha = new Texture("Imagenes\\EnemigoEspectroDer.png");
        private int _vida = 2;

        public bool Vivo { get { return vivo; } private set { vivo = value; } }
        public DateTime Eliminacion { get; private set; }
        public float LimiteDerecha { get; set; }
        public float LimiteIzquierda { get; set; }
        public float LimiteTop { get; set; }
        public float LimiteBot { get; set; }

        #endregion

        #region Metodos Vector 2D

        public void Actualizar()
        {
            SetearLimites();
            if (vivo)
            {
                Patrullaje();

                //Si el enemigo colisiona con algun arma, retrocede, ya que tiene 2 vidas.
                if (Colisiona())
                {
                    RecibirDaño();
                    if (_vida == 0)
                        MatarEnemigo();
                }

                //Dibujo el fantasma
                Dibujar();
            }
        }

        public void Dibujar()
        {
            if (DireccionDerecha)
                Engine.Draw(spriteFantasmaDerecha, X, Y);
            else
                Engine.Draw(spriteFantasmaIzquierda, X, Y);
        }

        /// <summary>
        /// Este metodo no se utiliza en los enemigos, ya que patrullan automaticamente.
        /// </summary>
        [Obsolete]
        public void Entrada()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Metodos Enemigo

        public void SpawnearEnemigo(float x, float y)
        {
            vivo = true;
            DireccionArriba = true;
            SpawnX = x;
            SpawnY = y;
            X = x;
            Y = y;
        }

        public void MatarEnemigo()
        {
            vivo = false;
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
            LimiteTop = Y - (20);
            LimiteBot = Y + (20);
            LimiteDerecha = X + (20);
            LimiteIzquierda = X - (20);
        }

        #endregion

    }
}
