using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public class World {
        public static readonly Vector2 Forward = new Vector2(0, 1);
        public static readonly Vector2 Backward = new Vector2(0, -1);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);

        public List<Scene> Scenes { get; }
        public Scene CurrentScene { get; private set; }
        // In seconds.
        public float TimeToLive { get; private set; }
        private Context context;

        public World(List<Scene> scenes, Scene startingScene, float timeToLive, Context context) {
            this.context = context;
            Scenes = scenes;
            CurrentScene = startingScene;
            TimeToLive = timeToLive;
        }

        public void SwitchToScene(Scene nextScene, Vector2 playerPosition) {
            CurrentScene = nextScene;
            context.Player.Entity.Position = playerPosition;
            context.Player.Entity.Scene = nextScene;
        }

        public void Update() {
            TimeToLive -= (float) context.GameTime.ElapsedGameTime.TotalSeconds;
            CurrentScene.Update();
        }
    }
}