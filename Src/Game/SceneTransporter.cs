using Microsoft.Xna.Framework;

namespace yolo {

    public class SceneTransporter : Interactable {
        private Scene newScene;
        private Vector2 newPlayerPosition;

        public SceneTransporter(Scene newScene, Vector2 newPlayerPosition) {
            this.newScene = newScene;
            this.newPlayerPosition = newPlayerPosition;
        }

        public override void Update() {
            // Do nothing.
        }

        public override AchievementType? Interact() {
            Context.World.SwitchToScene(newScene, newPlayerPosition);
            return null;
        }

        public override bool CanInteract() {
            return true;
        }

        protected override TimedSpriteSet DefaultSprite => AssetBank.SpriteBank. // TODO
        protected override TimedSpriteSet HighlightedSprite => AssetBank.SpriteBank. // TODO
    }
}