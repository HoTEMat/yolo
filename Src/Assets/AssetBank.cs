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

            // OTHER
            public Sprite TrashcanUp { get; init; }
            public Sprite TrashcanDown { get; init; }
            public Sprite Grafitti { get; init; }
            public Sprite IcecreamStand { get; init; }
            public Sprite Fountain { get; init; }

            // PARK
            public Sprite ParkDuck1 { get; init; }
            public Sprite ParkDuck2 { get; init; }
            public Sprite ParkMisc1 { get; init; }
            public Sprite ParkMisc2 { get; init; }
            public Sprite ParkMisc3 { get; init; }
            public Sprite ParkMisc4 { get; init; }
            public Sprite ParkBush { get; init; }
            public Sprite ParkPondLU { get; init; }
            public Sprite ParkPondRU { get; init; }
            public Sprite ParkPondLD { get; init; }
            public Sprite ParkPondRD { get; init; }
            public Sprite ParkGrass { get; init; }
            
            // PEOPLE
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
            
            public Sprite Person4Down { get; init; }
            public Sprite Person4Down1 { get; init; }
            public Sprite Person4Down2 { get; init; }
            public Sprite Person4Up { get; init; }
            public Sprite Person4Up1 { get; init; }
            public Sprite Person4Up2 { get; init; }
            public Sprite Person4Right { get; init; }
            public Sprite Person4Right1 { get; init; }
            public Sprite Person4Right2 { get; init; }
            public Sprite Person4Left { get; init; }
            public Sprite Person4Left1 { get; init; }
            public Sprite Person4Left2 { get; init; }
            
            // HOUSES
            public Sprite House1L { get; init; }
            public Sprite House2L { get; init; }
            public Sprite House3L { get; init; }
            public Sprite House4L { get; init; }
            public Sprite House5L { get; init; }
            public Sprite House6L { get; init; }
            public Sprite House7L { get; init; }
            public Sprite House1R { get; init; }
            public Sprite House2R { get; init; }
            public Sprite House3R { get; init; }
            public Sprite House4R { get; init; }
            public Sprite House5R { get; init; }
            public Sprite House6R { get; init; }
            public Sprite House7R { get; init; }
            public Sprite HospitalL { get; init; }
            public Sprite HospitalR { get; init; }
            public Sprite MarketL { get; init; }
            public Sprite MarketR { get; init; }
            
            // EFFECTS
            public Sprite HeartGood { get; init; }
            public Sprite HeartBad { get; init; }
            
            // INTERIORS
            public Sprite HospitalWindow1 { get; init; }
            public Sprite HospitalWindow2 { get; init; }
            public Sprite HospitalWall { get; init; }
            public Sprite HospitalTable { get; init; }
            public Sprite HospitalFloor { get; init; }
            
            public Sprite MarketIsle { get; init; }
            public Sprite MarketCheckout { get; init; }
            public Sprite MarketWall { get; init; }
            public Sprite MarketFloor { get; init; }
            
            public Sprite HouseStairL { get; init; }
            public Sprite HouseStairR { get; init; }
            public Sprite HouseElevatorL { get; init; }
            public Sprite HouseElevatorR { get; init; }
            public Sprite HouseFloor { get; init; }
        }

        public TileBank Tiles { get; private set; }

        public class TileBank
        {
            public Tile Dev_Wall { get; init; }
            public Tile Dev_Floor { get; init; }

            // PARK
            public Tile ParkPondLU { get; init; }
            public Tile ParkPondRU { get; init; }
            public Tile ParkPondLD { get; init; }
            public Tile ParkPondRD { get; init; }
            public Tile ParkGrass { get; init; }
            public Tile HospitalFloor { get; init; }
            public Tile MarketFloor { get; init; }
            public Tile HouseFloor { get; init; }
            
            // HOUSES
            public Tile House1L { get; init; }
            public Tile House2L { get; init; }
            public Tile House3L { get; init; }
            public Tile House4L { get; init; }
            public Tile House5L { get; init; }
            public Tile House6L { get; init; }
            public Tile House7L { get; init; }
            public Tile House1R { get; init; }
            public Tile House2R { get; init; }
            public Tile House3R { get; init; }
            public Tile House4R { get; init; }
            public Tile House5R { get; init; }
            public Tile House6R { get; init; }
            public Tile House7R { get; init; }
            public Tile HospitalL { get; init; }
            public Tile HospitalR { get; init; }
            public Tile MarketL { get; init; }
            public Tile MarketR { get; init; }
        }
        
        public TimedSpriteSetBank TimedSprites { get; private set; }
        
        public class TimedSpriteSetBank {
            public TimedSpriteSet Person1Down { get; init; }
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

                // OTHER
                TrashcanUp = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 0, 16, 16)},
                TrashcanDown = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 16, 16, 16)},
                Grafitti = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(160, 64, 32, 16)},
                IcecreamStand = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(304, 112, 16, 16)},
                Fountain = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(304, 160, 32, 16)},

                // PARK
                ParkDuck1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 0, 16, 16)},
                ParkDuck2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 16, 16, 16)},
                ParkMisc1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 0, 16, 16)},
                ParkMisc2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(96, 0, 16, 16)},
                ParkMisc3 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 16, 16, 16)},
                ParkMisc4 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(96, 16, 16, 16)},
                ParkPondLU = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 0, 16, 16)},
                ParkPondRU = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(48, 0, 16, 16)},
                ParkPondLD = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 16, 16, 16)},
                ParkPondRD = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(48, 16, 16, 16)},
                ParkBush = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(80, 48, 16, 16)},
                ParkGrass = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(64, 0, 16, 16)},
                
                // PEOPLE
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
                
                Person4Down = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 176, 16, 16)},
                Person4Down1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 176, 16, 16)},
                Person4Down2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 192, 16, 16)},
                Person4Up = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 192, 16, 16)},
                Person4Up1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(0, 208, 16, 16)},
                Person4Up2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(16, 208, 16, 16)},
                Person4Right = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 208, 16, 16)},
                Person4Right1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 192, 16, 16)},
                Person4Right2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(32, 176, 16, 16)},
                Person4Left = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 208, 16, 16)},
                Person4Left1 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 192, 16, 16)},
                Person4Left2 = new Sprite {Effects = SpriteEffects.FlipHorizontally, Texture = Textures.Main, SourceRect = new Rectangle(32, 176, 16, 16)},
                
                // HOUSES
                House1L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(160, 0, 16, 32)},
                House1R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(176, 0, 16, 32)},
                House2L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(192, 0, 16, 32)},
                House2R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(208, 0, 16, 32)},
                House3L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(224, 0, 16, 32)},
                House3R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(240, 0, 16, 32)},
                House4L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(160, 32, 16, 32)},
                House4R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(176, 32, 16, 32)},
                House5L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(336, 112, 16, 32)},
                House5R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(352, 112, 16, 32)},
                House6L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(336, 144, 16, 32)},
                House6R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(352, 144, 16, 32)},
                House7L = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(368, 144, 16, 32)},
                House7R = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(384, 144, 16, 32)},
                HospitalL = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(368, 0, 16, 32)},
                HospitalR = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(384, 0, 16, 32)},
                MarketL = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(400, 48, 16, 32)},
                MarketR = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(416, 48, 16, 32)},
                
                // EFFECTS
                HeartGood = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(192, 64, 16, 16)},
                HeartBad = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(208, 64, 16, 16)},
                
                // INTERIORS
                HospitalWindow1 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(272, 0, 16, 32)},
                HospitalWindow2 = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(288, 0, 16, 32)},
                HospitalTable = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(288, 0, 32, 32)},
                HospitalWall = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(352, 0, 16, 32)},
                HospitalFloor = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(272, 32, 16, 16)},
                
                MarketWall = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(384, 24, 16, 32)},
                MarketFloor = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(272, 32, 16, 16)},
                MarketCheckout = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(352, 24, 16, 32)},
                MarketIsle = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(320, 24, 16, 32)},
                
                HouseStairL = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(160, 80, 16, 32)},
                HouseStairR = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(176, 80, 16, 32)},
                HouseElevatorL = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(192, 80, 16, 32)},
                HouseElevatorR = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(208, 80, 16, 32)},
                HouseFloor = new Sprite {Texture = Textures.Main, SourceRect = new Rectangle(160, 112, 16, 16)},
            };

            Tiles = new TileBank
            {
                Dev_Wall = new Tile {Sprite = Sprites.Dev_Wall, Walkable = false},
                Dev_Floor = new Tile {Sprite = Sprites.Dev_Floor},

                ParkPondLU = new Tile {Sprite = Sprites.ParkPondLU, Walkable = false},
                ParkPondRU = new Tile {Sprite = Sprites.ParkPondRU, Walkable = false},
                ParkPondLD = new Tile {Sprite = Sprites.ParkPondLD, Walkable = false},
                ParkPondRD = new Tile {Sprite = Sprites.ParkPondRD, Walkable = false},
                ParkGrass = new Tile {Sprite = Sprites.ParkGrass},
                
                HospitalFloor = new Tile {Sprite = Sprites.HospitalFloor},
                MarketFloor = new Tile {Sprite = Sprites.MarketFloor},
                HouseFloor = new Tile {Sprite = Sprites.HouseFloor},
                
                // HOUSES
                House1L = new Tile {Sprite = Sprites.House1L, Walkable=false, Flat = false},
                House2L = new Tile {Sprite = Sprites.House2L, Walkable=false, Flat = false},
                House3L = new Tile {Sprite = Sprites.House3L, Walkable=false, Flat = false},
                House4L = new Tile {Sprite = Sprites.House4L, Walkable=false, Flat = false},
                House5L = new Tile {Sprite = Sprites.House5L, Walkable=false, Flat = false},
                House6L = new Tile {Sprite = Sprites.House6L, Walkable=false, Flat = false},
                House7L = new Tile {Sprite = Sprites.House7L, Walkable=false, Flat = false},
                House1R = new Tile {Sprite = Sprites.House1L, Walkable=false, Flat = false},
                House2R = new Tile {Sprite = Sprites.House2L, Walkable=false, Flat = false},
                House3R = new Tile {Sprite = Sprites.House3L, Walkable=false, Flat = false},
                House4R = new Tile {Sprite = Sprites.House4L, Walkable=false, Flat = false},
                House5R = new Tile {Sprite = Sprites.House5L, Walkable=false, Flat = false},
                House6R = new Tile {Sprite = Sprites.House6L, Walkable=false, Flat = false},
                House7R = new Tile {Sprite = Sprites.House7L, Walkable=false, Flat = false},
                HospitalL = new Tile {Sprite = Sprites.HospitalL, Walkable=false, Flat = false},
                HospitalR = new Tile {Sprite = Sprites.HospitalR, Walkable=false, Flat = false},
                MarketL = new Tile {Sprite = Sprites.MarketL, Walkable=false, Flat = false},
                MarketR = new Tile {Sprite = Sprites.MarketR, Walkable=false, Flat = false},
            };
            
            int personWalkPeriod = 300;
            TimedSprites = new TimedSpriteSetBank
            {
                Person1Down = new TimedSpriteSet {
                    Sprites = new []{Sprites.Person1Down, Sprites.Person1Down1, Sprites.Person1Down2},
                    Loops = true,
                    Period = personWalkPeriod
                }
            };
            
        }
    }
}