using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public class Tile {
        public Sprite Sprite { get; init; }
        public bool Walkable { get; init; } = true;
        public bool Flat { get; init; } = true;

        /// <summary>
        /// Height in game units
        /// </summary>
        public float Height => Flat ? 0 : Sprite.SourceRect.Height / 16f;

        public override string ToString() {
            return $"{Sprite.Texture.Name}: [{Sprite.SourceRect}]";
        }
    }
}