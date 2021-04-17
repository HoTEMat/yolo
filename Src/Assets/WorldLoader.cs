using System.Collections.Generic;

namespace yolo {
    public interface IWorldLoader {
        World LoadWorld(Context context);
    }

    public class FirstLevelLoader : IWorldLoader {
        public World LoadWorld(Context context) {
            Scene mainScene = new MainSceneLoader().LoadScene(context);

            List<Scene> scenes = new List<Scene>
            {
                mainScene,
                new ObchodSceneLoader().LoadScene(context),
                new NemocniceSceneLoader().LoadScene(context),
                new DumSceneLoader().LoadScene(context),
            };

            return new World(scenes, 60, context);
        }
    }
}