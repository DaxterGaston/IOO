using System;
using Game.Entidades;
using Game.SingletonManagers;

namespace Game
{
    public class Program
    {
        //Delta Time. Se Define en program ya que va a ser utilizado en varios lugares,
        //y todos pueden acceder a esta clase.
        public static DateTime deltaTime = DateTime.Now;
        //Instancia de personaje que se esta usando.
        public static Personaje pj;
        //Esta variable define si se esta jugando o en el menu.
        public static bool Jugando = false;

        static void Main(string[] args)
        {
            Engine.Initialize("A ver q onda");
            //Cuando comienza el juego, empieza en el menu principal. Luego entra en el GameLoop
            NivelesManager.AccederMenu();
            pj = new Personaje(100F, 500F);

            while (true)
            {
                Engine.Clear();
                NivelesManager.EjecutarNivelActual();
                if (Jugando)
                {
                    
                    if (Engine.GetKey(Keys.Q))
                    {
                        MenuManager.GuardarPartida();
                    }
                }
                Engine.Show();
            }
        }
    }
}