using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class TileIndexer2D {
        private Tile[,] tiles;

        public TileIndexer2D(Tile[,] tiles) => this.tiles = tiles;
        public Tile this[int col, int row] => tiles[col, row];
    }

    public class Tile {
        public Sprite Sprite { get; init; }
        public bool Walkable { get; init; }
    }
}