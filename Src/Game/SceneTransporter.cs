using Microsoft.Xna.Framework;

namespace yolo {
    
    public class SceneTransporter : Interactable {
        private string newSceneName;
        private Vector3 newPlayerPosition;

        public SceneTransporter(string newSceneName, Vector3 newPlayerPosition, Entity entity) : base(entity) {
            this.newSceneName = newSceneName;
            this.newPlayerPosition = newPlayerPosition;
        }

        public override void Update() {
            // Do nothing.
        }

        public override AchievementType? Interact() {
            Context.World.SwitchToScene(newSceneName, newPlayerPosition);
            return null;
        }

        public override bool CanInteract() {
            return true;
        }
    }
        
}
