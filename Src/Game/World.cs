using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public class World {
        public List<Scene> Scenes { get; }
        public Scene CurrentScene { get; }
        private Context context;
        
        public World(Context context) =>
            this.context = context;

        public void SwitchToScene(Scene nextScene, Vector2 playerPosition) {
            
        }
    }
}