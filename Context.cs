using Microsoft.Xna.Framework;

namespace yolo {
    public class Context {
        public GameTime GameTime { get; }
        public AssetBank Assets { get; } 
        public World World { get; }
        public PlayerBehaviour Player { get; }
        public Camera Camera;
        public Renderer Renderer;
    }
}