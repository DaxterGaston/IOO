using System;
using System.Collections.Generic;
using Game.Entidades;
using Game.Factory;

namespace Game.SingletonManagers
{
    /// <summary>
    /// Clase que maneja los enemigos existentes en cada nivel.
    /// </summary>
    public static class EnemigosManager
    {
        #region SetUp

        //Variables utilizadas para crear un intervalo entre la eliminacion de un enemigo y el respawn de otro de su mismo tipo.
        private static DateTime UltimaEliminacionCabezaMedusa;
        private static DateTime UltimaEliminacionFantasma;

        private static CabezasMedusaFactory FactoryMedusas = new CabezasMedusaFactory();
        private static FantasmasFactory FactoryFantasmas = new FantasmasFactory();

        #endregion

        #region Actualizacion de enemigos

        public static void Inicializar(int cantidadFantasmas, int cantidadMedusas)
        {
            //Elimino instancias que pueden haber quedado anteriormente.
            EliminarEnemigos();

            //Inicializo los factory para que dispongan de las instancias correspondientes para cada nivel.
            FactoryFantasmas.Inicializar(cantidadFantasmas);
            FactoryMedusas.Inicializar(cantidadMedusas);
        }

        /// <summary>
        /// Actualizo los enemigos.
        /// </summary>
        private static void ActualizarEnemigos()
        {
            foreach (var item in CabezasMedusaEnUso)
                item.Actualizar();
            foreach (var item in FantasmasEnUso)
                item.Actualizar();
            if (Engine.GetKey(Keys.T))
            {
                Engine.Debug("Cantidad de cabezas de medusa: " + CabezasMedusaEnUso.Count.ToString());
                Engine.Debug("Cantidad de espectros: " + FantasmasEnUso.Count.ToString());
            }
        }

        /// <summary>
        /// Si el pool contiene cabezas de medusa disponibles, y pasaron al menos 2 segundos desde la
        /// ultima eliminacion de una cabeza de medusa, se spawnea una nueva.
        /// </summary>
        private static void RespawnearCabezaMedusa()
        {
            EnemigoCabezaMedusa e = ObtenerCabezaMedusa();
            if (e != null && UltimaEliminacionCabezaMedusa.AddSeconds(1) < DateTime.Now)
            {
                e.SpawnearEnemigo(400, 400);
                CabezasMedusaEnUso.Add(e);
            }
        }

        /// <summary>
        /// Si el pool contiene fantasmas disponibles, y pasaron al menos 2 segundos desde la
        /// ultima eliminacion de un fantasma, se spawnea uno nuevo.
        /// </summary>
        private static void RespawnearFantasma()
        {
            EnemigoFantasma e = ObtenerFantasma();
            if (e != null && UltimaEliminacionFantasma.AddSeconds(1) < DateTime.Now)
            {
                e.SpawnearEnemigo(300, 300);
                FantasmasEnUso.Add(e);
            }
        }

        #endregion

        #region Administracion Pool

        //Se utilizan estas listas para manejar las instancias que se utilizan.
        private static List<EnemigoCabezaMedusa> CabezasMedusaEnUso = new List<EnemigoCabezaMedusa>();
        private static List<EnemigoFantasma> FantasmasEnUso = new List<EnemigoFantasma>();

        /// <summary>
        /// Obtengo una instancia de una cabeza de medusa.
        /// </summary>
        /// <returns>
        /// Instancia de cabeza de medusa, si hay alguna disponible.
        /// Caso contrario, null.
        /// </returns>
        private static EnemigoCabezaMedusa ObtenerCabezaMedusa()
        {
            return FactoryMedusas.ObtenerInstancia();
        }

        /// <summary>
        /// Obtengo una instancia de un fantasma.
        /// </summary>
        /// <returns>
        /// Instancia de cabeza de medusa, si hay alguna disponible.
        /// Caso contrario, null.
        /// </returns>
        private static EnemigoFantasma ObtenerFantasma()
        {
            return FactoryFantasmas.ObtenerInstancia();
        }

        /// <summary>
        /// Reciclo las instancias de los enemigos que estan muertos para respawnear nuevos del mismo tipo.
        /// </summary>
        private static void ReciclarEnemigos()
        {
            for (int i = 0; i < CabezasMedusaEnUso.Count; i++)
            {
                if (!CabezasMedusaEnUso[i].Vivo)
                {
                    EnemigoCabezaMedusa e;
                    e = CabezasMedusaEnUso[i];
                    CabezasMedusaEnUso.RemoveAt(i);
                    FactoryMedusas.RecuperarInstancia(e);
                    UltimaEliminacionCabezaMedusa = DateTime.Now;
                }
            }

            for (int i = 0; i < FantasmasEnUso.Count; i++)
            {
                if (!FantasmasEnUso[i].Vivo)
                {
                    EnemigoFantasma e;
                    e = FantasmasEnUso[i];
                    FantasmasEnUso.RemoveAt(i);
                    FactoryFantasmas.RecuperarInstancia(e);
                    UltimaEliminacionFantasma = DateTime.Now;
                }
            }
        }

        #endregion

        /// <summary>
        /// Metodo para actualizar todo lo que refiere a enemigos.
        /// Este metodo debe ser llamado desde program, en cada ciclo del GameLoop.
        /// </summary>
        public static void Actualizar()
        {
            //Respawneo los enemigos que correspondan.
            RespawnearCabezaMedusa();
            RespawnearFantasma();

            //Actualizo los enemigos (posiciones, vida).
            ActualizarEnemigos();

            //Reciclo las instancias de enemigos que fueron matados.
            ReciclarEnemigos();
        }

        /// <summary>
        /// Elimino todos los enemigos del juego.
        /// </summary>
        public static void EliminarEnemigos()
        {
            FactoryMedusas.EliminarEnemigos();
            FactoryFantasmas.EliminarEnemigos();
            CabezasMedusaEnUso.RemoveRange(0, CabezasMedusaEnUso.Count);
            FantasmasEnUso.RemoveRange(0, FantasmasEnUso.Count);
        }

        /// <summary>
        /// Metodo para indicar que se eliminaron los enemigos y no llegaron a respawnear.
        /// </summary>
        /// <returns></returns>
        public static bool NoQuedanEnemigos()
        {
            return (CabezasMedusaEnUso.Count == 0 && FantasmasEnUso.Count == 0);
        }

        /// <summary>
        /// Verifico la colision del personaje con cualquier enemigo existente.
        /// </summary>
        /// <param name="pj"></param>
        /// <returns></returns>
        public static bool VerificarColisionPersonajeEnemigos(Personaje pj)
        {
            for (int i = 0; i < CabezasMedusaEnUso.Count; i++)
            {
                if (ColisionManager.ColisionaEnemigoPersonaje(pj, CabezasMedusaEnUso[i]))
                {
                    return true;
                }
            }
            for (int i = 0; i < FantasmasEnUso.Count; i++)
            {
                if (ColisionManager.ColisionaEnemigoPersonaje(pj, FantasmasEnUso[i]))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
