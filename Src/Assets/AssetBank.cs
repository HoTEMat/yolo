using Microsoft.Xna.Framework.Graphics;
using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Color = Microsoft.Xna.Framework.Color;

namespace yolo
{
    public class AssetBank
    {
        private ContentManager content;

        public TextureBank Textures { get; private set; }

        public class TextureBank
        {
            public Texture2D Dev { get; init; }
            public Texture2D Main { get; init; }
        }

        public SpriteBank Sprites { get; private set; }

        public class SpriteBank
        {
            public Sprite Dev_Wall { get; init; }
            public Sprite Dev_Floor { get; init; }

            public Sprite PondLU { get; init; }
            public Sprite PondRU { get; init; }
            public Sprite PondLD { get; init; }
            public Sprite PondRD { get; init; }

            public Sprite TrashcanUp { get; init; }
            public Sprite TrashcanDown { get; init; }

            public Sprite Duck1 { get; init; }
            public Sprite Duck2 { get; init; }

            public Sprite ParkMisc1 { get; init; }
            public Sprite ParkMisc2 { get; init; }
            public Sprite ParkMisc3 { get; init; }
            public Sprite ParkMisc4 { get; init; }
            public Sprite ParkBush { get; init; }
            
            public Sprite Person1Down { get; init; }
            public Sprite Person1Down1 { get; init; }
            public Sprite Person1Down2 { get; init; }
            public Sprite Person1Up { get; init; }
            public Sprite Person1Up1 { get; init; }
            public Sprite Person1Up2 { get; init; }
            public Sprite Person1Right { get; init; }
            public Sprite Person1Right1 { get; init; }
            public Sprite Person1Right2 { get; init; }
            public Sprite Person1Left { get; init; }
            public Sprite Person1Left1 { get; init; }
            public Sprite Person1Left2 { get; init; }
            
            public Sprite Person2Down { get; init; }
            public Sprite Person2Down1 { get; init; }
            public Sprite Person2Down2 { get; init; }
            public Sprite Person2Up { get; init; }
            public Sprite Person2Up1 { get; init; }
            public Sprite Person2Up2 { get; init; }
            public Sprite Person2Right { get; init; }
            public Sprite Person2Right1 { get; init; }
            public Sprite Person2Right2 { get; init; }
            public Sprite Person2Left { get; init; }
            public Sprite Person2Left1 { get; init; }
            public Sprite Person2Left2 { get; init; }
            
            public Sprite Person3Down { get; init; }
            public Sprite Person3Down1 { get; init; }
            public Sprite Person3Down2 { get; init; }
            public Sprite Person3Up { get; init; }
            public Sprite Person3Up1 { get; init; }
            public Sprite Person3Up2 { get; init; }
            public Sprite Person3Right { get; init; }
            public Sprite Person3Right1 { get; init; }
            public Sprite Person3Right2 { get; init; }
            public Sprite Person3Left { get; init; }
            public Sprite Person3Left1 { get; init; }
            public Sprite Person3Left2 { get; init; }
        }

        public TileBank Tiles { get; private set; }

        public class TileBank
        {
            public Tile Dev_Wall { get; init; }
            public Tile Dev_Floor { get; init; }

            //POND
            public Tile PondLU { get; init; }
            public Tile PondRU { get; init; }
            public Tile PondLD { get; init; }
            public Tile PondRD { get; init; }
        }


        public void LoadContent(ContentManager Content)
        {
            // Load textures
            Textures = new TextureBank
            {
                Dev = Content.Load<Texture2D>("assetName"),
                Main = Content.Load<Texture2D>("main"),
            };

            Sprites = new SpriteBank
            {
                Dev_Floor = new Sprite {Texture = Textures.Dev},
                Dev_Wall = new Sprite {Texture = Textures.Dev, SourceRect = new Rectangle(0, 32, 16, 32)},

                PondLU = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 0, 16, 16)},
                PondRU = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(48, 0, 16, 16)},
                PondLD = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 16, 16, 16)},
                PondRD = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(48, 16, 16, 16)},

                TrashcanUp = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 0, 16, 16)},
                TrashcanDown = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 16, 16, 16)},

                Duck1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 0, 16, 16)},
                Duck2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 16, 16, 16)},

                ParkMisc1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 0, 16, 16)},
                ParkMisc2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(96, 0, 16, 16)},
                ParkMisc3 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 16, 16, 16)},
                ParkMisc4 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(96, 16, 16, 16)},
                ParkBush = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 48, 16, 16)},
                
                Person1Down = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 32, 16, 16)},
                Person1Down1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 32, 16, 16)},
                Person1Down2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 48, 16, 16)},
                Person1Up = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 48, 16, 16)},
                Person1Up1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 64, 16, 16)},
                Person1Up2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 64, 16, 16)},
                Person1Right = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 64, 16, 16)},
                Person1Right1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 48, 16, 16)},
                Person1Right2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 32, 16, 16)},
                Person1Left = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 64, 16, 16)},
                Person1Left1 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 48, 16, 16)},
                Person1Left2 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 32, 16, 16)},
                
                Person2Down = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 80, 16, 16)},
                Person2Down1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 80, 16, 16)},
                Person2Down2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 96, 16, 16)},
                Person2Up = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 96, 16, 16)},
                Person2Up1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 112, 16, 16)},
                Person2Up2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 112, 16, 16)},
                Person2Right = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 112, 16, 16)},
                Person2Right1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 96, 16, 16)},
                Person2Right2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 80, 16, 16)},
                Person2Left = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 112, 16, 16)},
                Person2Left1 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 96, 16, 16)},
                Person2Left2 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 80, 16, 16)},
                
                Person3Down = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 128, 16, 16)},
                Person3Down1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 128, 16, 16)},
                Person3Down2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 144, 16, 16)},
                Person3Up = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 144, 16, 16)},
                Person3Up1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 160, 16, 16)},
                Person3Up2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 160, 16, 16)},
                Person3Right = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 160, 16, 16)},
                Person3Right1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 144, 16, 16)},
                Person3Right2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 128, 16, 16)},
                Person3Left = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 160, 16, 16)},
                Person3Left1 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 144, 16, 16)},
                Person3Left2 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 128, 16, 16)},
            };

            Tiles = new TileBank
            {
                Dev_Wall = new Tile {Sprite = Sprites.Dev_Wall, Walkable = false},
                Dev_Floor = new Tile {Sprite = Sprites.Dev_Floor, Walkable = true},

                PondLU = new Tile {Sprite = Sprites.PondLU, Walkable = false},
                PondRU = new Tile {Sprite = Sprites.PondRU, Walkable = false},
                PondLD = new Tile {Sprite = Sprites.PondLD, Walkable = false},
                PondRD = new Tile {Sprite = Sprites.PondRD, Walkable = false},
            };
            
            
        }
    }
}