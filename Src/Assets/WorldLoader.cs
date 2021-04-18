using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public interface IWorldLoader {
        public Vector3 PlayerStartVector { get; }
        
        World LoadWorld(Context context);
    }

    public class FirstLevelLoader : IWorldLoader {
        public Vector3 PlayerStartVector => new Vector3(23.8f, 6.5f, 0);
        
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
        public Vector3 PlayerStartVector => new Vector3(14.8f, 17.0f, 0);

        public World LoadWorld(Context context) {
            Scene nemocniceScene = new NemocniceSceneLoader2().LoadScene(context);
            List<Scene> scenes = new List<Scene>
            {
                nemocniceScene,
            };

            return new World(scenes, nemocniceScene, 60, context, false);
        }
    }
}