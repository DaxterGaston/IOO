using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Abstracciones
{
    public interface IEnemigo
    {
        void SpawnearEnemigo(float x, float y);
        void RecibirDaño();
        void MatarEnemigo();
        bool Colisiona();
        void Patrullaje();
    }
}
