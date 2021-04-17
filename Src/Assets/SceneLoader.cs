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

    public class DumSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            return new Scene("dum", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.DumScene, context.Assets), context);
        }
    }

    public class NemocniceSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            return new Scene("nemocnice", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.NemocniceScene, context.Assets), context);
        }
    }

    public class ObchodSceneLoader : ISceneLoader
    {
        public Scene LoadScene(Context context)
        {
            // TODO entities
            List<Entity> entities = new List<Entity>();

            return new Scene("obchod", entities,
                TileMapLoader.LoadIndexer(context.Assets.Textures.ObchodScene, context.Assets), context);
        }
    }

    static class TileMapLoader
    {
        public static Indexer2D<Tile> LoadIndexer(Texture2D src, AssetBank assets)
        {
            Dictionary<uint, Tile> map = new Dictionary<uint, Tile>
            {
                {0x0c0f81u, assets.Tiles.HospitalFloor},

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