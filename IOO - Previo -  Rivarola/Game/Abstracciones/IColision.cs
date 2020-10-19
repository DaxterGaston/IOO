using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Abstracciones
{
    /// <summary>
    /// Interfaz que contiene los metodos y las propiedades necesarias 
    /// para controlar las colisiones de objetos.
    /// </summary>
    public interface IColision
    {
        

        bool Colisiona();
    }
}
