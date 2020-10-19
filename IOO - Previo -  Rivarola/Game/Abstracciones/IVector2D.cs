namespace Game.Abstracciones
{
    public interface IVector2D
    {
        float LimiteDerecha { get; set; }
        float LimiteIzquierda { get; set; }
        float LimiteTop { get; set; }
        float LimiteBot { get; set; }
        float X { get; set; }
        float Y { get; set; }
        void Actualizar();
        void Dibujar();
    }
}
