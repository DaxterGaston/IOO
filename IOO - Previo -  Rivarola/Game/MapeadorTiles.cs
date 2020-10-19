using Game.Entidades;
using System.Collections.Generic;

namespace Game
{
    public static class MapeadorTiles
    {
        private const string RutaPrincipal = "";
        //Lista de los tiles que se usan para el nivel actual.
        private static Dictionary<int, Tile> listaActual = null;
        //Lista de los mapas correspondientes a cada nivel.
        private static List<int[]> listaDeTileMaps;

        /// <summary>
        /// Metodo para dibujar el mapa.
        /// </summary>
        public static void CrearMapaDeTiles()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Metodo para cargar los mapas que se van a usar en el nivel actual.
        /// </summary>
        /// <param name="cantidadTiles"></param>
        /// <param name="rutaTiles"></param>
        public static void CargarMapaTiles(int cantidadTiles, string rutaTiles)
        {
            listaActual = new Dictionary<int, Tile>();
            for (int i = 0; i < cantidadTiles; i++)
            {
                //Genero la ruta del tile actual
                string pathTile = RutaPrincipal + "/" + rutaTiles + "/" + i + ".png";
                Tile aux = new Tile(pathTile);
                listaActual.Add(i, aux);
            }
        }
    }
}
