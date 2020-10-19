using System.Collections.Generic;

namespace Game.Abstracciones
{
    public abstract class FactoryEnemigosBase<T> where T : class, IEnemigo
    {
        private List<T> Disponibles = new List<T>();

        /// <summary>
        /// Cuando el nivel comienza, se crean las instancias que vayan a ser utilizadas en el mismo.
        /// </summary>
        /// <param name="cantidad">
        /// Cantidad de ese tipo especifico de enemigo que se necesitan.
        /// </param>
        protected void CrearInstanciasIniciales(List<T> lista)
        {
            Disponibles.AddRange(lista);
        }

        protected T Obtener()
        {
            if (Disponibles.Count > 0)
            {
                T e;
                e = Disponibles[0];
                Disponibles.RemoveAt(0);
                return e;
            }
            return default(T);
        }

        protected void Guardar(T e)
        {
            Disponibles.Add(e);
        }

        protected void EliminarTodo()
        {
            Disponibles.RemoveRange(0, Disponibles.Count);
        }
    }
}
