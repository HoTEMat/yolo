using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Color = Microsoft.Xna.Framework.Color;

namespace yolo {
    public class AssetBank {

        private ContentManager content;

        public TextureBank Textures { get; private set; }
        public class TextureBank {
            public Texture2D Dev { get; init; }
        }

        public SpriteBank Sprites { get; private set; }
        public class SpriteBank {
            public Sprite Dev_Wall { get; init; }
            public Sprite Dev_Floor { get; init; }
        }

        public TileBank Tiles { get; private set; }
        public class TileBank {
            public Tile Dev_Wall { get; init; }
            public Tile Dev_Floor { get; init; }
        }


        public void LoadContent(ContentManager Content) {

            // Load textures
            Textures = new TextureBank {
                Dev = Content.Load<Texture2D>("assetName"),
            };

            Sprites = new SpriteBank {
                Dev_Floor = new Sprite {
                    Texture = Textures.Dev
                },
                Dev_Wall = new Sprite {
                    Texture = Textures.Dev,
                    SourceRect = new Rectangle(0, 32, 16, 32),
                },
            };

            Tiles = new TileBank {
                Dev_Wall = new Tile {
                    Sprite = Sprites.Dev_Wall,
                    Walkable = false,
                },
                Dev_Floor = new Tile {
                    Sprite = Sprites.Dev_Floor,
                    Walkable = true,
                },
            };


        }
    }
}