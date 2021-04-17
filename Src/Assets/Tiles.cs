using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Tile {
        public Sprite Sprite { get; init; }
        public bool Walkable { get; init; } = true;
        public bool Flat { get; init; } = true;

        public override string ToString() {
            return $"{Sprite.Texture.Name}: [{Sprite.SourceRect}]";
        }
    }
}