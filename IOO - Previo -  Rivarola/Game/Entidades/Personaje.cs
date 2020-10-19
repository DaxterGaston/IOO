using System;
using Game.Abstracciones;
using System.Collections.Generic;
using Game.SingletonManagers;

namespace Game.Entidades
{
    public class Personaje : Vector2D, IVector2D
    {
        #region SetUp

        public Personaje(float x, float y, float velocidad = 4, int vida = 3) : base(x, y)
        {
            _estadoVivo = true;
            Vida = vida;
            _velocidad = velocidad;
            CargarCuchillos();
            SetearLimites();
        }

        public float _velocidad { get; private set; }

        //Propiedades privadas del personaje.
        public int Vida { get; private set; }
        public bool _estadoVivo { get; private set; }
        private static bool _inmune = false;

        //Armas
        //private static Espada Espada { get; set; }
        //Para el fantasma, son proyectiles (bolas de fuego)
        private static List<Daga> Cuchillos = new List<Daga>();
        private static List<Daga> CuchillosEnUso = new List<Daga>();
        private TimeSpan tsDisparo = new TimeSpan(0, 0, 0, 0, 250);

        //Animacion del personaje
        Texture PersonajeIzquierda = new Texture("Imagenes\\ghostIzq.png");
        Texture PersonajeDerecha = new Texture("Imagenes\\ghostDer.png");
        Texture spriteCorazon = new Texture("Imagenes\\corazon.png");

        //private static int _indiceAnimacionIzquierda = 0, _indiceAnimacionDerecha = 0;
        //private static List<Texture> AnimacionMovimientoIzquierda { get; set; }
        //private static List<Texture> AnimacionMovimientoDerecha { get; set; }
        //private static List<Texture> AnimacionAtaque { get; set; }

        //Variable que define la direccion a la que el personaje esta mirando.
        //Se utiliza para definir la direccion en la que atacan las armas.
        private static bool _derecha = true;
        public float LimiteDerecha { get; set; }
        public float LimiteIzquierda { get; set; }
        public float LimiteTop { get; set; }
        public float LimiteBot { get; set; }

        #endregion

        #region MetodosPropios

        private void Atacar()
        {
            throw new NotImplementedException();
        }

        [Obsolete("Utilizar cuando tenga los sprites necesarios para la animacion.")]
        private void Saltar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifica si hay algun cuchillo disponible.
        /// Si es asi, lo elimina de la lista para utilizarlo.
        /// </summary>
        /// <returns></returns>
        public void LanzarCuchillo()
        {
            if (Cuchillos.Count != 0 && DateTime.Now > Program.deltaTime.Add(tsDisparo))
            {
                for(int i = 0; i <= Cuchillos.Count; i++)
                {
                    if (!Cuchillos[i].EnVuelo)
                    {
                        Cuchillos[i].LanzarCuchillo(X, Y, _derecha);
                        CuchillosEnUso.Add(Cuchillos[i]);
                        Cuchillos.RemoveAt(i);
                        break;
                    }
                }
                Program.deltaTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Agrego una instancia ya utilizada de cuchillo.
        /// </summary>
        /// <param name="c"></param>
        public void RealmacenarCuchillo(Daga c)
        {
            CuchillosEnUso.Remove(c);
            Cuchillos.Add(c);
        }

        /// <summary>
        /// Se llama unicamente al crear al personaje.
        /// Se crea un maximo de 3 cuchillos.
        /// </summary>
        private void CargarCuchillos()
        {
            Engine.Debug("Cargue los cuchillos");
            Daga c1 = new Daga(0, 0);
            Daga c2 = new Daga(0, 0);
            Daga c3 = new Daga(0, 0);
            c1.EnVuelo = false;
            c2.EnVuelo = false;
            c3.EnVuelo = false;
            Cuchillos.Add(c1);
            Cuchillos.Add(c2);
            Cuchillos.Add(c3);
        }

        /// <summary>
        /// Agrego a una lista estatica de Texturas todas las imagenes para la animacion.
        /// </summary>
        [Obsolete("Utilizar cuando tenga los sprites necesarios para la animacion.")]
        private void CargarTexturasPersonaje()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dibujo un personaje, animando su movimiento.
        /// </summary>
        [Obsolete("Utilizar cuando tenga los sprites necesarios para la animacion.")]
        public void Animar()
        {
            if (_derecha)
            {
                //_indiceAnimacionIzquierda = 0;
                ////Dibujar(AnimacionMovimientoDerecha[_indiceAnimacionIzquierda], X, Y, Angulo, EscalaX, EscalaY);
                ////TODO: Verificar cada cuanto intervalo de tiempo deberia animar.
                //if (true)
                //    _indiceAnimacionDerecha += 1;
            }
            else
            {
                //_indiceAnimacionDerecha = 0;
                ////Dibujar(AnimacionMovimientoIzquierda[_indiceAnimacionIzquierda], X, Y, Angulo, EscalaX, EscalaY);
                ////TODO: Verificar cada cuanto intervalo de tiempo deberia animar.
                //if (true)
                //    _indiceAnimacionIzquierda += 1;
            }
            throw new NotImplementedException("Faltan sprites para animaciones.");
        }

        #endregion

        #region MetodosVector2D

        public void Actualizar()
        {
            if (Vida == 0)
            {
                _estadoVivo = false;
                NivelesManager.Perdiste();
            }
            else
            {
                Entrada();
                Dibujar();
            }

            //Actualizo las armas activas.
            if (CuchillosEnUso.Count > 0)
            {
                for (int i = 0; i < CuchillosEnUso.Count; i++)
                {
                    CuchillosEnUso[i].Actualizar();
                }
            }
            SetearLimites();
            if (Colisiona())
            {
                //ObtenerDatosColision(e1);
                Vida -= 1;
                X = 100;
                Y = 100;
            }
        }

        public void Dibujar()
        {
            if (_derecha)
                Engine.Draw(PersonajeDerecha, X, Y);
            else
                Engine.Draw(PersonajeIzquierda, X, Y);
            if (Vida == 3)
            {
                Engine.Draw(spriteCorazon, 50, 25);
                Engine.Draw(spriteCorazon, 90, 25);
                Engine.Draw(spriteCorazon, 130, 25);
            }

            if (Vida == 2)
            {
                Engine.Draw(spriteCorazon, 50, 25);
                Engine.Draw(spriteCorazon, 90, 25);
            }

            if (Vida == 1)
            {
                Engine.Draw(spriteCorazon, 50, 25);
            }
        }

        public void Entrada()
        {
            if (Engine.GetKey(Keys.LEFT) && (X - PersonajeIzquierda.Width/2) > 45)
            {
                X -= _velocidad;
                _derecha = false;
            }
            else if (Engine.GetKey(Keys.RIGHT) && (X + PersonajeIzquierda.Width / 2) < 750)
            {
                X += _velocidad;
                _derecha = true;
            }
            if (Engine.GetKey(Keys.UP))
            {
                if (Y - EscalaY > 40)
                    Y -= _velocidad;
            }
            else if (Engine.GetKey(Keys.DOWN))
            {
                if (Y + EscalaY < 565)
                    Y += _velocidad;
            }
            if (Engine.GetKey(Keys.S))
            {
                LanzarCuchillo();
            }
            //Menu
            if (Engine.GetKey(Keys.Z))
            {
                NivelesManager.AccederMenu();
            }
        }


        private bool Colisiona()
        {
            return EnemigosManager.VerificarColisionPersonajeEnemigos(this);
        }
        private void SetearLimites()
        {
            LimiteTop = Y + (23 / 2);
            LimiteBot = Y - (23 / 2);
            LimiteDerecha = X + (46 / 2);
            LimiteIzquierda = X - (46 / 2);
        }

        #endregion
    }
}
