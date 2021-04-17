using System.Collections.Generic;

namespace yolo {
    public interface IWorldLoader {
        World LoadWorld(AssetBank assets);
    }

    public class FirstLevelLoader : IWorldLoader {
        public World LoadWorld(AssetBank assets) {
            List<Scene> scenes = new();
            Scene park = new ParkSceneLoader().LoadScene(assets);
            scenes.Add(park);
            // TODO: other scenes

            return World(scenes, currentScene: park, timeToLive: 300);
        }
    }
}