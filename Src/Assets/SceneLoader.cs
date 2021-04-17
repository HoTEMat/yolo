using Microsoft.Xna.Framework.Graphics;

namespace yolo {
    public interface ISceneLoader {
        Scene LoadScene(AssetBank assets);
    }

    

    public class ParkSceneLoader : ISceneLoader {
        public Scene LoadScene(AssetBank assets) {
            throw new System.NotImplementedException();
        }
        
    }
    
    static class TileMapLoader {
        public const uint Wall = 0x00000000; // TODO
        public const uint Road = 0x00000000; // TODO
        
        public static TileIndexer2D LoadIndexer(Texture2D src, AssetBank assets) {
            int w = src.Width, h = src.Height;
            uint[] data = new uint[w * h];
            src.GetData(data);
            for (int col = 0; col < w; col++) {
                for (int row = 0; row < h; row++) {
                    uint pixel = data[col + row * w];
                }
            }
        }
        
        private static Tile CreateTile(uint code, AssetBank assets) {
            if (code == Wall) {
                return new Tile {
                    Sprite = 
            } 
        }
    }
}