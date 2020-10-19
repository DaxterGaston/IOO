using Game.Entidades;
using System;

namespace Game.SingletonManagers
{
    public delegate void Del();

    /// <summary>
    /// Esta clase es el nodo por el que se pasa durante todo el GameLoop.
    /// Determina en que nivel que se esta jugando actualmente.
    /// El menu es considerado un nivel, las pantallas de Ganaste/Perdiste 
    /// son consideradas parte del menu.
    /// </summary>
    public static class NivelesManager
    {
        #region SetUp

        public enum Niveles
        {
            Menu,
            Nivel1,
            Nivel2,
            Boss
        }
        public static TimeSpan tsAccionesMenu = new TimeSpan(0, 0, 0, 0, 250);
        public static TimeSpan tsAvance = new TimeSpan(0, 0, 1);
        public static Niveles LvlActual { get; private set; }

        public static Del EjecutarNivelActual = NavegarMenuPrincipal;

        #endregion

        #region Estado de la partida

        /// <summary>
        /// Metodo para avanzar de nivel cuando se llega al final de uno.
        /// </summary>
        public static void AvanzarNivel()
        {
            if (DateTime.Now > Program.deltaTime.Add(tsAvance))
            {
                if (EjecutarNivelActual == NavegarMenuPrincipal)
                {
                    EjecutarNivelActual = JugarNivel1;
                    LvlActual = Niveles.Nivel1;
                    Program.Jugando = true;
                    EnemigosManager.Inicializar(1, 1);
                }
                else if (EjecutarNivelActual == JugarNivel1)
                {
                    EjecutarNivelActual = JugarNivel2;
                    LvlActual = Niveles.Nivel2;
                    Program.Jugando = true;
                    EnemigosManager.Inicializar(1, 1);
                }
                else if (EjecutarNivelActual == JugarNivel2)
                {
                    Program.Jugando = false;
                    Ganaste();
                }
                Program.deltaTime = DateTime.Now;
            }
            //Queda esto para cuando haga un lvl con un jefe final... para otra etapa.
            //if (lvlActual == Niveles.Nivel2)
            //    lvlActual = Niveles.Boss;
            //if (lvlActual == Niveles.Boss)
            //    Ganaste();
        }

        public static void Ganaste()
        {
            EjecutarNivelActual = NavegarMenuPrincipal;
            MenuManager.Ganaste();
            Program.deltaTime = DateTime.Now;
        }

        public static void Perdiste()
        {
            EjecutarNivelActual = NavegarMenuPrincipal;
            MenuManager.Perdiste();
            Program.deltaTime = DateTime.Now;
        }
        
        #endregion

        #region Ejecucion de Niveles

        private static void JugarNivel1()
        {
            Engine.Draw("Imagenes\\Nivel1.jpg");
            Program.pj.Actualizar();
            EnemigosManager.Actualizar();
            if (EnemigosManager.NoQuedanEnemigos() && Program.Jugando)
                AvanzarNivel();
        }

        private static void JugarNivel2()
        {
            Engine.Draw("Imagenes\\Nivel2.jpg");
            Program.pj.Actualizar();
            EnemigosManager.Actualizar();
            if (EnemigosManager.NoQuedanEnemigos() && Program.Jugando)
                AvanzarNivel();
        }

        #endregion

        #region Menu

        public static void AccederMenu()
        {
            EjecutarNivelActual = NavegarMenuPrincipal;
            LvlActual = Niveles.Menu;
            Program.Jugando = false;
            EnemigosManager.EliminarEnemigos();
        }

        private static void NavegarMenuPrincipal()
        {
            MenuManager.Actualizar();
            MenuManager.Dibujar();
            Engine.Show();
        }

        #endregion

        #region Manejo de partida

        /// <summary>
        /// Metodo para cargar la partida.
        /// </summary>
        /// <param name="pj">Personaje.</param>
        /// <param name="lvlPartida">Nivel en el que estaba.</param>
        public static void InstanciarPartidaGuardada(Personaje pj, Niveles lvlPartida)
        {
            
        }

        public static void SetearNivel(Niveles lvl)
        {
            if (lvl == Niveles.Nivel1)
                EjecutarNivelActual = JugarNivel1;
            else if (lvl == Niveles.Nivel2)
                EjecutarNivelActual = JugarNivel2;
            else if (lvl == Niveles.Boss)
                throw new NotImplementedException("Falta implementar nivel de nuevo jefe.");
        }

        #endregion

    }
}
