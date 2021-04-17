using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Tileset
    {
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public Texture2D Texture { get; private set; }

        public Tileset(Texture2D texture, int tileWidth, int tileHeight)
        {
            Texture = texture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Rectangle GetTile(int id)
        {
            int W = Texture.Width / TileWidth;

            return new Rectangle((id % W) * TileWidth, (id / W) * TileHeight, TileWidth, TileHeight);
        }
    }
}