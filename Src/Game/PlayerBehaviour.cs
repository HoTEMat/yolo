using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class PlayerBehaviour : Behaviour {
        public const float WalkSpeed = 1; // TODO
        
        public bool IsGood { get; }

        public PlayerBehaviour(bool isGood)
        {
            IsGood = isGood;
        }

        public override void Update() {
            var kbs = Keyboard.GetState();
            HandleWalking(kbs);
            HandleInteraction(kbs);
        }

        private void HandleWalking(KeyboardState kbs) {
            Vector2 posChange = Vector2.Zero;
            if (kbs.IsKeyDown(Keys.Right)) {
                posChange += new Vector2(WalkSpeed, 0);
            }

            if (kbs.IsKeyDown(Keys.Left)) {
                posChange += new Vector2(-WalkSpeed, 0);
            }

            if (kbs.IsKeyDown(Keys.Up)) {
                posChange += new Vector2(0, -WalkSpeed);
            }

            if (kbs.IsKeyDown(Keys.Down)) {
                posChange += new Vector2(0, WalkSpeed);
            }

            posChange.Normalize();
            Position += posChange;
        }

        private void HandleInteraction(KeyboardState kbs) {
            var scene = Entity.Scene;
            if (kbs.IsKeyDown(Keys.F) && (scene.SelectedInteractable?.CanInteract() ?? false)) {
                Entity.Scene.SelectedInteractable?.Interact();
            }
        }
    }
}