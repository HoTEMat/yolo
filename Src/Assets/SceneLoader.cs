using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace yolo
{
    public interface ISceneLoader
    {
        Scene LoadScene(Context context);
    }

    public class MainSceneLoader : ISceneLoader
    {
        private List<Vector3> PersonTargets = new List<Vector3>()
        {
            // points between which NPCs are moving
            new(1, 14, 0), new(40, 9, 0), new(20, 28, 0), new(33, 25, 0),
            new(33, 18, 0), new(21, 2, 0), new(25, 13, 0)
        };

        private List<Vector3> PersonInitial = new List<Vector3>()
        {
            // points where NPCs are generated
            new(30, 27, 0), new(22, 24, 0), new(5, 14, 0), new(19, 9, 0),
            new(28, 17, 0), new(22, 9, 0), new(29, 13, 0), new(35, 9, 0),
            new(40, 8, 0), new(15, 15, 0), new(20, 3, 0), new(9, 15, 0)
        };

        private List<Vector3> IceCreamPositions = new List<Vector3>()
        {
            // parc and square
            new(30, 26, 0), new(31, 16, 0)
        };

        private List<Vector3> BinPositions = new List<Vector3>()
        {
            new(3, 13, 0), new(14, 15, 0), new(38, 10, 0), new(22, 4, 0),
            new(20, 10, 0), new(27, 13, 0)
        };

        private const int NPCCount = 20;

        public Scene LoadScene(Context context)
        {
            List<Entity> entities = new List<Entity>();

            Scene scene = new Scene("main", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.MainScene, context.Assets), context);

            Entity fountain = new Entity(context)
            {
                Position = new Vector3(24, 12, 0),
                Animation = new Animation(context.Assets.Sprites.Fountain),
                Behavior = null,
            };
            fountain.Collider = new RectangleCollider(fountain, false, 2, 0.1f);
            scene.AddEntity(fountain);

            generateNPCs(scene, context);
            generateIceCream(scene, context);
            generateBins(scene, context);
            generatePark(scene, context);
            
            TileMapLoader.AddTileColliders(scene, context);
            return scene;
        }

        private void generatePark(Scene scene, Context context)
        {
            int minX = 19;
            int minY = 21;
            int maxX = 33;
            int maxY=  29;

            int miscCount = 150;
            
            Sprite[] misc = new[]
            {
                context.Assets.Sprites.ParkMisc1,
                context.Assets.Sprites.ParkMisc2,
                context.Assets.Sprites.ParkMisc3,
                context.Assets.Sprites.ParkMisc4,
            };
            
            for (int i = 0; i < miscCount; i++)
            {
                float x = Utils.Random.Next(minX, maxX) + (float)Utils.Random.NextDouble();
                float y = Utils.Random.Next(minY, maxY) + (float)Utils.Random.NextDouble();

                if ((x > 26 && x < 28 && y > 23 && y < 25))
                    continue;
                
                Entity msc = new Entity(context)
                {
                    
                    Position = new Vector3( x, y, 0),
                    Animation = new Animation(misc[Utils.Random.Next(0, misc.Length)]),
                    Behavior = null,
                };
                scene.AddEntity(msc);
            }
            
            Entity pond = new Entity(context)
            {
                Position = new Vector3(27, 24, -0.01f),
                Animation = new Animation(context.Assets.Sprites.ParkPond),
                IsFlat = true,
            };
            pond.Collider = new CircleCollider(pond, false, 1);
            pond.Behavior = new Pond(pond);
            scene.AddEntity(pond);

            List<Vector3> treePositions = new List<Vector3>()
            {
                new Vector3(21, 24, 0),
                new Vector3(23, 27, 0),
                new Vector3(28, 28, 0),
                new Vector3(31, 23, 0),
                new Vector3(29, 22, 0),
            };
            
            Sprite[] trees = {
                context.Assets.Sprites.ParkTreeSmall,
                context.Assets.Sprites.ParkTreeLarge,
            };

            foreach (var treePosition in treePositions)
            {
                int r = Utils.Random.Next(0, trees.Length);
                
                Entity msc = new Entity(context)
                {
                    Position = treePosition,
                    Animation = new Animation(trees[r]),
                }; 
            
                ICollider[] treeCols = new[]
                {
                    new RectangleCollider(msc, false, 0.25f, 0.01f),
                    new RectangleCollider(msc, false, 0.5f, 0.01f),
                };

                msc.Collider = treeCols[r];
                msc.Behavior = new Tree(msc);
                scene.AddEntity(msc);
            }
        }

        private void generateNPCs(Scene scene, Context ctx)
        {
            for (int i = 0; i < NPCCount; i++)
            {
                int idx = i % PersonInitial.Count;

                var person = new Entity(ctx)
                {
                    Position = PersonInitial[idx],
                };

                person.Behavior =
                    new PersonBehavior(idx % 4 + 1, PersonTargets, person); // idx % 4 - chooses one of 4 person sprites
                scene.AddEntity(person);
            }
        }

        private void generateIceCream(Scene scene, Context ctx)
        {
            var icecream = new Entity(ctx)
            {
                Position = Utils.RandChoice(IceCreamPositions),
                Animation = new Animation(ctx.Assets.Sprites.IcecreamStand)
            };

            icecream.Behavior = new IceCreamStand(icecream);
            scene.AddEntity(icecream);
        }

        private void generateBins(Scene scene, Context ctx)
        {
            int binStartIndex = ctx.Random.Next(0, BinPositions.Count - 1);
            for (int i = 0; i < 3; i++)
            {
                var bin = new Entity(ctx)
                {
                    Position = BinPositions[(binStartIndex + i) % BinPositions.Count],
                };
                
                bin.Behavior =new Bin(ctx.Player.IsGood, bin);  // if player is good - generate overturned bins
                scene.AddEntity(bin);
            }
        }
    }

    public class DumSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            Scene scene = new Scene("dum", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.DumScene, context.Assets), context);
            
            TileMapLoader.AddTileColliders(scene, context);
            return scene;
        }
    }

    public class NemocniceSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            Scene scene = new Scene("nemocnice", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.NemocniceScene, context.Assets), context);
            
            TileMapLoader.AddTileColliders(scene, context);
            return scene;
        }
    }

    public class ObchodSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            Scene scene = new Scene("obchod", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.ObchodScene, context.Assets), context);
            
            TileMapLoader.AddTileColliders(scene, context);
            return scene;
        }
    }

    static class TileMapLoader
    {
        /// <summary>
        /// Adds colliders to each non-walkable tile of the scene.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="context"></param>
        public static void AddTileColliders(Scene scene, Context context)
        {
            for (int i = 0; i < scene.Tiles.Width; i++)
            {
                for (int j = 0; j < scene.Tiles.Height; j++)
                {
                    if (!scene.Tiles[i, j].Walkable)
                    {
                        Entity dummy = new Entity(context) {Position = new Vector3(i + 0.5f, j + 0.5f, 0)};
                        dummy.Collider = new RectangleCollider(dummy, false, 1, 1);
                        scene.AddEntity(dummy);
                    }
                }
            }
        }

        
        public static Indexer2D<Tile> LoadIndexer(Texture2D src, AssetBank assets)
        {
            Dictionary<uint, Tile> map = new Dictionary<uint, Tile>
            {
                {0x005784u, assets.Tiles.HospitalFloor},

                {0xae9a9au, assets.Tiles.HospitalL},
                {0xa19a9au, assets.Tiles.HospitalR},
                {0xe4e4e4u, assets.Tiles.HospitalWindow1},
                {0xadadadu, assets.Tiles.HospitalWindow2},
                {0x584848u, assets.Tiles.HospitalWall},
                {0x817676u, assets.Tiles.HospitalTableL},
                {0x6d6262u, assets.Tiles.HospitalTableR},
                {0x423535u, assets.Tiles.HospitalDoctor},

                {0x5a4a34u, assets.Tiles.HouseFloor},
                {0x185122u, assets.Tiles.HouseStairL},
                {0x257332u, assets.Tiles.HouseStairR},
                {0x7bfc91u, assets.Tiles.HouseElevatorL},
                {0x80ca8cu, assets.Tiles.HouseElevatorR},
                {0x1dff43u, assets.Tiles.HouseWall},

                {0x197653u, assets.Tiles.MarketL},
                {0x0d5b3du, assets.Tiles.MarketR},
                {0x534129u, assets.Tiles.MarketIsleL},
                {0x352714u, assets.Tiles.MarketIsleR},
                {0xdb8717u, assets.Tiles.MarketCheckoutL},
                {0xbf740fu, assets.Tiles.MarketCheckoutR},
                {0xd2a05du, assets.Tiles.MarketWall},
                {0x9d9d9du, assets.Tiles.MarketFloor},

                {0x428b1du, assets.Tiles.ParkGrass},
                {0x08668cu, assets.Tiles.ParkFence},

                {0x383838u, assets.Tiles.Cobble},
                {0x402b2bu, assets.Tiles.Cobble1},
                {0x361b1bu, assets.Tiles.Cobble2},
                {0x963030u, assets.Tiles.Cobble4},
                {0x840a0au, assets.Tiles.Cobble7},
                {0xcc7a7au, assets.Tiles.Cobble9},
                {0xff3232u, assets.Tiles.Cobble10},

                {0x54985fu, assets.Tiles.HouseS1L},
                {0x3c8348u, assets.Tiles.HouseS1R},
                {0x917575u, assets.Tiles.HouseS2L},
                {0x735555u, assets.Tiles.HouseS2R},
                {0x547359u, assets.Tiles.HouseS3L},
                {0x3d5e43u, assets.Tiles.HouseS3R},
            };

            Tile[] lHouses =
            {
                assets.Tiles.House1L,
                assets.Tiles.House2L,
                assets.Tiles.House3L,
                assets.Tiles.House4L,
            };

            Tile[] rHouses =
            {
                assets.Tiles.House1R,
                assets.Tiles.House2R,
                assets.Tiles.House3R,
                assets.Tiles.House4R,
            };

            int w = src.Width, h = src.Height;
            uint[] data = new uint[w * h];
            src.GetData(data);
            Random random = new Random();

            Indexer2D<Tile> indexer2D = new Indexer2D<Tile>(w, h);

            for (int col = 0; col < w; col++)
            {
                for (int row = 0; row < h; row++)
                {
                    uint pixel = data[col + row * w] & 0xffffff;
                    pixel = ((pixel & 0xff) << 16) | (pixel & 0xff00) | ((pixel & 0xff0000) >> 16);

                    // special case for left and right side of houses
                    if (pixel == 0xff8787u)
                        indexer2D[col, row] = lHouses[random.Next(0, lHouses.Length)];
                    else if (pixel == 0xff0000u)
                        indexer2D[col, row] = rHouses[random.Next(0, rHouses.Length)];
                    else
                    {
                        if (!map.ContainsKey(pixel))
                            indexer2D[col, row] = assets.Tiles.Empty;
                        else
                            indexer2D[col, row] = map[pixel];
                    }
                }
            }

            return indexer2D;
        }
    }
}
