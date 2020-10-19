using Game.Abstracciones;
using Game.Entidades;
using System.Collections.Generic;

namespace Game.Factory
{
    public class CabezasMedusaFactory : FactoryEnemigosBase<EnemigoCabezaMedusa>
    {
        public void Inicializar(int cantidad)
        {
            List<EnemigoCabezaMedusa> lista = new List<EnemigoCabezaMedusa>();
            for (int i = 0; i < cantidad; i++)
            {
                lista.Add(new EnemigoCabezaMedusa(0, 0));
            }
            CrearInstanciasIniciales(lista);
        }

        public EnemigoCabezaMedusa ObtenerInstancia()
        {
            return Obtener();
        }

        public void RecuperarInstancia(EnemigoCabezaMedusa e)
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
