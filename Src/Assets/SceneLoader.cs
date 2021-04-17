using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace yolo
{
    public interface ISceneLoader
    {
        Scene LoadScene(Context context);
    }

    public class MainSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            return new Scene("main", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.MainScene, context.Assets), context);
        }
    }

    static class TileMapLoader
    {
        public static Indexer2D<Tile> LoadIndexer(Texture2D src, AssetBank assets)
        {
            Dictionary<uint, Tile> map = new Dictionary<uint, Tile>
            {
                {0x0c0f81, assets.Tiles.HospitalFloor},

                {0xae9a9a, assets.Tiles.HospitalL},
                {0xa19a9a, assets.Tiles.HospitalR},
                {0xe4e4e4, assets.Tiles.HospitalWindow1},
                {0xadadad, assets.Tiles.HospitalWindow2},
                {0x584848, assets.Tiles.HospitalWall},
                {0x817676, assets.Tiles.HospitalTableL},
                {0x6d6262, assets.Tiles.HospitalTableR},
                {0x423535, assets.Tiles.HospitalDoctor},

                {0x5a4a34, assets.Tiles.HouseFloor},
                {0x185122, assets.Tiles.HouseStairL},
                {0x257332, assets.Tiles.HouseStairR},
                {0x7bfc91, assets.Tiles.HouseElevatorL},
                {0x80ca8c, assets.Tiles.HouseElevatorR},
                {0x55835d, assets.Tiles.HouseGrandma},
                {0x1dff43, assets.Tiles.HouseWall},

                {0x197653, assets.Tiles.MarketL},
                {0x0d5b3d, assets.Tiles.MarketR},
                {0x534129, assets.Tiles.MarketIsleL},
                {0x352714, assets.Tiles.MarketIsleR},
                {0xdb8717, assets.Tiles.MarketCheckoutL},
                {0xbf740f, assets.Tiles.MarketCheckoutR},
                {0xd2a05d, assets.Tiles.MarketWall},
                {0x9d9d9d, assets.Tiles.MarketFloor},

                {0x428b1d, assets.Tiles.ParkGrass},
            };

            Tile[] lHouses =
            {
                assets.Tiles.House1L,
                assets.Tiles.House2L,
                assets.Tiles.House3L,
                assets.Tiles.House4L,
                assets.Tiles.House5L,
                assets.Tiles.House6L,
                assets.Tiles.House7L
            };

            Tile[] rHouses =
            {
                assets.Tiles.House1R,
                assets.Tiles.House2R,
                assets.Tiles.House3R,
                assets.Tiles.House4R,
                assets.Tiles.House5R,
                assets.Tiles.House6R,
                assets.Tiles.House7R
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
                    uint pixel = data[col + row * w] / 0xff;

                    // special case for left and right side of houses
                    if (pixel == 0xff8787)
                        indexer2D[col, row] = lHouses[random.Next(0, lHouses.Length)];
                    else if (pixel == 0xff0000)
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