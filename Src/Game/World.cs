using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace yolo {
    public class World {
        public static readonly Vector3 Forward = new Vector3(0, 1, 0);
        public static readonly Vector3 Backward = new Vector3(0, -1, 0);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);
        public static readonly Vector3 Up = new Vector3(0, 0, -1);

        public Dictionary<string, Scene> Scenes { get; }
        public Scene CurrentScene { get; private set; }
        // In seconds.
        public float TimeToLive { get; private set; }
        private Context context;

        public World(List<Scene> scenes, Scene currentScene, float timeToLive, Context context) {
            this.context = context;
            Scenes = scenes.ToDictionary(scene => scene.Name);
            CurrentScene = currentScene;
            TimeToLive = timeToLive;
        }

        public void SwitchToScene(string newSceneName, Vector3 playerPosition) {
            CurrentScene = Scenes[newSceneName];
            context.Player.Entity.Position = playerPosition;
            context.Camera.Center =  playerPosition;
            context.Player.Entity.Scene = CurrentScene;

            context.Renderer.RebuildTerrainMesh();
        }

        public void Update() {
            TimeToLive -= (float) context.GameTime.ElapsedGameTime.TotalSeconds;
            CurrentScene.Update();
        }
    }
}