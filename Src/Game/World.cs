using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public class World {

        public static readonly Vector2 Forward = new Vector2(0, 1);
        public static readonly Vector2 Backward = new Vector2(0, -1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);



        public List<Scene> Scenes { get; }
        public Scene CurrentScene { get; }
        private Context context;

        public World(Context context) =>
            this.context = context;

        public void SwitchToScene(Scene nextScene, Vector2 playerPosition) {
        }
    }
}
