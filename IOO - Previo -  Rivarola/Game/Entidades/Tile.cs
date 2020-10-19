namespace Game.Entidades
{
    public class Tile
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Texture Textura { get; set; }
        public Tile(string pathTextura)
        {
            Textura = new Texture(pathTextura);
        }
    }
}
