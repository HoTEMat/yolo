using System.Collections.Generic;

namespace yolo {
    public interface IWorldLoader {
        World LoadWorld(Context context);
    }

    public class FirstLevelLoader : IWorldLoader {
        public World LoadWorld(Context context) {
            List<Scene> scenes = new();
            Scene mainScene = new MainSceneLoader().LoadScene(context);
            scenes.Add(mainScene);
            // TODO: other scenes

            return World(scenes, currentScene: mainScene, timeToLive: 300);
        }
    }
}