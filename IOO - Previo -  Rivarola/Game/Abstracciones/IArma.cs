using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Abstracciones
{
    public interface IArma : IVector2D
    {
        bool Activa { get; set; }
        void Atacar();
    }
}
