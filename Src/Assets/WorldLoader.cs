using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface IWorldLoader {
        public Vector3 PlayerStartVector { get; }
        
        World LoadWorld(Context context);
    }

    public class FirstLevelLoader : IWorldLoader {
        public Vector3 PlayerStartVector => new Vector3(25, 16, 0);
        
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

    public class IntroLevelLoader : IWorldLoader {
        public Vector3 PlayerStartVector => new Vector3(18.6f, 17.7f, 0);

        public World LoadWorld(Context context) {
            Scene nemocniceScene = new NemocniceSceneLoader().LoadScene(context);
            List<Scene> scenes = new List<Scene>
            {
                nemocniceScene,
            };
            return new World(scenes, nemocniceScene, 60, context);
        }
    }
}