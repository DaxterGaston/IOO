using Game.Entidades;
using System;
using System.IO;
using System.Text;
using static Game.SingletonManagers.NivelesManager;

namespace Game.SingletonManagers
{
    public static class MenuManager
    {
        #region SetUp

        private static bool BotonJugar = true;
        private static bool BotonCargarPartida = false;
        private static bool BotonControles = false;
        private static bool PantallaControles = false;
        private static bool PantallaGanastePerdiste = false;
        private static readonly string texturaFlecha = "Imagenes\\flechaRoja.png";
        private static string texturaFondo = "Imagenes\\Menu.jpg";
        private static TimeSpan ts = new TimeSpan(0, 0, 0, 0, 250);
        private static readonly TimeSpan tsPantallaWL = new TimeSpan(0, 0, 2);

        #endregion

        public static void Dibujar()
        {
            Engine.Draw(texturaFondo);
            if (!PantallaControles)
            {
                if (BotonJugar)
                    Engine.Draw(texturaFlecha, 70, 85);
                else if (BotonCargarPartida)
                    Engine.Draw(texturaFlecha, 50, 240);
                else if (BotonControles)
                    Engine.Draw(texturaFlecha, 70, 400);
            }
        }

        /// <summary>
        /// Solo se puede mover de posicion con los botones arriba y abajo.
        /// </summary>
        public static void Actualizar()
        {
            if (Engine.GetKey(Keys.UP))
                PosicionCursorHaciaArriba();
            else if (Engine.GetKey(Keys.DOWN))
                PosicionCursorHaciaAbajo();
            if (Engine.GetKey(Keys.RETURN) && DateTime.Now > Program.deltaTime.Add(ts))
            {
                //Si esta sobre la opcion jugar, lo envio al nivel 1
                if (BotonJugar)
                    NivelesManager.AvanzarNivel();
                //Si quiere cargar la partida
                else if (BotonCargarPartida)
                    CargarPartida();
                else if (BotonControles)
                {
                    PantallaControles = true;
                    texturaFondo = "Imagenes\\PantallaControles.jpg";
                }
                Program.deltaTime = DateTime.Now;
            }

            //if (PantallaGanastePerdiste && DateTime.Now > Program.deltaTime.Add(tsPantallaWL))
            //{
            //    NivelesManager.AccederMenu();
            //    PantallaGanastePerdiste = false;
            //}

            //Cierra el juego.
            if (Engine.GetKey(Keys.ESCAPE))
            {
                if (DateTime.Now > Program.deltaTime.Add(ts))
                {
                    if (PantallaControles || PantallaGanastePerdiste)
                    {
                        PantallaControles = false;
                        PantallaGanastePerdiste = false;
                        BotonJugar = true;
                        texturaFondo = "Imagenes\\Menu.jpg";
                    }
                    else
                    {
                        Engine.CloseWindow();
                        Environment.Exit(0);
                    }
                    Program.deltaTime = DateTime.Now;
                }
            }
        }

        #region Posicion del Cursor

        private static void PosicionCursorHaciaArriba()
        {
            if (DateTime.Now > Program.deltaTime.Add(ts))
            {
                if (BotonJugar)
                {
                    BotonJugar = false;
                    BotonCargarPartida = false;
                    BotonControles = true;
                }
                else if (BotonCargarPartida)
                {
                    BotonJugar = true;
                    BotonCargarPartida = false;
                    BotonControles = false;
                }
                else if (BotonControles)
                {
                    BotonJugar = false;
                    BotonCargarPartida = true;
                    BotonControles = false;
                }
                Program.deltaTime = DateTime.Now;
            }
        }

        private static void PosicionCursorHaciaAbajo()
        {
            if (DateTime.Now > Program.deltaTime.Add(ts))
            {
                if (BotonJugar)
                {
                    BotonJugar = false;
                    BotonCargarPartida = true;
                    BotonControles = false;
                }
                else if (BotonCargarPartida)
                {
                    BotonJugar = false;
                    BotonCargarPartida = false;
                    BotonControles = true;
                }
                else if (BotonControles)
                {
                    BotonJugar = true;
                    BotonCargarPartida = false;
                    BotonControles = false;
                }
                Program.deltaTime = DateTime.Now;
            }
        }

        #endregion

        #region Partida

        /// <summary>
        /// Guardo el estado actual de la partida.
        /// </summary>
        public static void GuardarPartida()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}-{1}-{2}-", Program.pj.Vida, Program.pj.X, Program.pj.Y);
            sb.AppendFormat("{0}", NivelesManager.LvlActual);
            string csv = sb.ToString();
            File.WriteAllText("partida.txt", csv);
            //Luego de guardar la partida, vuelvo al menu.
            Console.WriteLine("Partida guardada!");
            NivelesManager.AccederMenu();
            //Actualizo el delta time.
            Program.deltaTime = DateTime.Now;
        }

        private static void CargarPartida()
        {
            string[] partida = File.ReadAllText("partida.txt").Split('-');

            Personaje pj;

            //Obtengo los datos del personaje del array 
            //y convierto los que no son string a sus correspondientes formatos.

            //Personaje
            int vida;
            int.TryParse(partida[0], out vida);
            float x;
            float.TryParse(partida[1], out x);
            float y;
            float.TryParse(partida[2], out y);
            pj = new Personaje(x, y, 8, vida);
            Niveles lvl = Niveles.Nivel1;
            if (partida[3] == "Nivel1")
                lvl = Niveles.Nivel1;
            if (partida[3] == "Nivel2")
                lvl = Niveles.Nivel2;
            NivelesManager.InstanciarPartidaGuardada(pj, lvl);
        }

        public static void Ganaste()
        {
            PantallaGanastePerdiste = true;
            texturaFondo = "Imagenes\\YouWin.png";
            BotonJugar = false;
            BotonCargarPartida = false;
            BotonControles = false;
            Program.deltaTime = DateTime.Now;
        }

        public static void Perdiste()
        {
            PantallaGanastePerdiste = true;
            texturaFondo = "Imagenes\\YouDied.png";
            BotonJugar = false;
            BotonCargarPartida = false;
            BotonControles = false;
            Program.deltaTime = DateTime.Now;
        }

        #endregion

    }
}
