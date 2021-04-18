using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface IWorldLoader {
        World LoadWorld(Context context);
    }

    public class FirstLevelLoader : IWorldLoader {
        public World LoadWorld(Context context) {
            Scene mainScene = new MainSceneLoader().LoadScene(context);
            Scene obchodScene = new ObchodSceneLoader().LoadScene(context);
            Scene nemocniceScene = new NemocniceSceneLoader().LoadScene(context);
            Scene dumScene = new DumSceneLoader().LoadScene(context, mainScene.Tomovo);

            List<Scene> scenes = new List<Scene>
            {
                mainScene,
                obchodScene,
                nemocniceScene,
                dumScene,
            };

            return new World(scenes, mainScene, 60, context);
        }
    }
}