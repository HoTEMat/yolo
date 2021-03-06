using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class World {
        public static readonly Vector3 Forward = new Vector3(0, 1, 0);
        public static readonly Vector3 Backward = new Vector3(0, -1, 0);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);
        public static readonly Vector3 Up = new Vector3(0, 0, -1);

        public Dictionary<string, Scene> Scenes { get; private set; }
        public Scene CurrentScene { get; private set; }
        // In seconds.
        public float TimeToLive { get; private set; }
        private Context context;
        private SceneSwitchInfo? plannedSceneSwitch;
        public bool hud;

        public World(List<Scene> scenes, Scene currentScene, float timeToLive, Context context, bool hud = true) {
            this.context = context;
            Scenes = scenes.ToDictionary(scene => scene.Name);
            CurrentScene = currentScene;
            TimeToLive = timeToLive;
            this.hud = hud;
        }
        
        public void SwitchToScene(string newSceneName, Vector3 playerPosition) {
            plannedSceneSwitch = new SceneSwitchInfo {
                SceneName = newSceneName,
                NewPlayerPos = playerPosition
            };
        }
        
        // Dont call this if you dont know what you are doing
        public void TriggerSceneSwitchCheck() {
            if (plannedSceneSwitch == null)
                return;
                
            Scene newScene = Scenes[plannedSceneSwitch.Value.SceneName];
            Entity player = context.Player.Entity;
            
            CurrentScene.RemoveTemporalEntitiesNow();
            CurrentScene.RemoveEntityNow(player);
            newScene.AddEntityNow(player);
            player.Position = plannedSceneSwitch.Value.NewPlayerPos;

            CurrentScene = newScene;
            context.Renderer.RebuildTerrainMesh();
            plannedSceneSwitch = null;
        }

        public void Update() {
            TimeToLive -= (float) context.GameTime.ElapsedGameTime.TotalSeconds;
            CurrentScene.Update();
            
            if (TimeToLive <= -1) {
                context.Game.GameOver();
            }
            
            
            // TODO: remove this
            //if (context.Keyboard.IsKeyPressed(Keys.I)) {
            //    context.Game.StartIntro();
           // }
        }
        
        public void Destroy() {
            foreach (var (_, scene) in Scenes) {
                scene.Destroy();
            }
            Scenes = null;
            CurrentScene = null;
            context = null;
            plannedSceneSwitch = null;
        }
        
        struct SceneSwitchInfo
        {
            public string SceneName;
            public Vector3 NewPlayerPos;
        }
    }
}