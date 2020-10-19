namespace Game.Abstracciones
{
    public abstract class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float EscalaX { get; private set; }
        public float EscalaY { get; private set; }
        public float Angulo { get; private set; }
        
        public Vector2D(float x, float y, float angulo = 0, float escalaX = 1, float escalaY = 1)
        {
            X = x;
            Y = y;
            Angulo = angulo;
            EscalaX = escalaX;
            EscalaY = escalaY;
        }
    }
}
