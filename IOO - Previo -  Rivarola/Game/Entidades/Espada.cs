using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Abstracciones;

namespace Game.Entidades
{
    public class Espada : Vector2D, IVector2D
    {
        public Espada(float x, float y) : base(x, y)
        {

        }
        public bool Desenfundada { get; set; }
        public float LimiteDerecha { get ; set ; }
        public float LimiteIzquierda { get ; set ; }
        public float LimiteTop { get ; set ; }
        public float LimiteBot { get ; set ; }

        #region Metodos Vector2D

        public void Actualizar()
        {
            throw new NotImplementedException();
        }

        public void Dibujar()
        {
            throw new NotImplementedException();
        }

        public void Entrada()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
