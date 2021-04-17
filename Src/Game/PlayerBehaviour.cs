using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class PlayerBehaviour : Behaviour {
        public const float WalkSpeed = 1; // TODO
        
        public bool IsGood { get; }
        public bool HasGroceries { get; set; }
        public BucketList TodoList { get; }
        private int spriteNum;
        public PlayerOrientation Orientation { get; private set; } = PlayerOrientation.Down;
        public bool Walking { get; private set; } = false;
        
        public PlayerBehaviour(bool isGood, BucketList todoList, int spriteNum)
        {
            IsGood = isGood;
            TodoList = todoList;
            this.spriteNum = spriteNum;
            HasGroceries = false;
        }

        public void HandInGroceries()
        {
            HasGroceries = false;
            // ToDo : stop displaying groceries symbol
        }
        public void PickGroceries()
        {
            HasGroceries = true;
            // ToDo: start displaying groceries
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
            UpdateOrientation(posChange);
        }

        private void UpdateOrientation(Vector2 posChange) {
            PlayerOrientation? newOrientation = GetOrientation(posChange);
            bool newWalking = newOrientation != null;
            if (newWalking && (!Walking || newOrientation.Value != Orientation)) {
                TimedSpriteSet newSprite = PersonSpriteSelector.GetWalkingSpriteSet(Context.Assets, spriteNum, newOrientation.Value);
                Orientation = (PlayerOrientation) newOrientation;
                Entity.ChangeSpriteTo(newSprite);
            } else if (!newWalking && Walking) {
                Sprite newSprite = PersonSpriteSelector.GetStaticSprite(Context.Assets, spriteNum, Orientation);
                Entity.ChangeSpriteTo(newSprite);
            }
            Walking = newWalking;
        }

        private void HandleInteraction(KeyboardState kbs) {
            var scene = Entity.Scene;
            if (kbs.IsKeyDown(Keys.F) && (scene.SelectedInteractable?.CanInteract() ?? false)) {
                var achievement = Entity.Scene.SelectedInteractable?.Interact();
                if (achievement != null)
                {
                    TodoList.ProcessAchievement((AchievementType)achievement);
                }
            }
        }

        private static PlayerOrientation? GetOrientation(Vector2 posChange) {
            float epsilon = 10e-3f;
            if (posChange.Y > epsilon)
                return PlayerOrientation.Down;
            if (posChange.Y < -epsilon)
                return PlayerOrientation.Up;
            if (posChange.X > epsilon)
                return PlayerOrientation.Right;
            if (posChange.X < -epsilon)
                return PlayerOrientation.Left;
            return null;
        }
    }

    public enum PlayerOrientation {
        Up,
        Right,
        Down,
        Left
    }
}