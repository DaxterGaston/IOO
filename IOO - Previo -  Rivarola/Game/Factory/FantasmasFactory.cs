using Game.Abstracciones;
using Game.Entidades;
using System;
using System.Collections.Generic;

namespace Game.Factory
{
    public class FantasmasFactory : FactoryEnemigosBase<EnemigoFantasma>
    {
        public void Inicializar(int cantidad)
        {
            List<EnemigoFantasma> lista = new List<EnemigoFantasma>();
            for (int i = 0; i < cantidad; i++)
            {
                lista.Add(new EnemigoFantasma(0, 0));
            }
            CrearInstanciasIniciales(lista);
        }

        public EnemigoFantasma ObtenerInstancia()
        {
            return Obtener();
        }

        public void RecuperarInstancia(EnemigoFantasma e)
        {
            if (e != null)
                Guardar(e);
            else
                LoggerManual.GuardarErrorLog("Guardar en el log.");
        }

        public void EliminarEnemigos()
        {
            EliminarTodo();
        }
    }
}
