using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class PlayerBehaviour : Behaviour {
        public const float WalkSpeed = 1; // TODO
        
        public bool IsGood { get; }
        public bool HasGroceries { get; set; }
        public BucketList TodoList { get; }
        private SpriteOrientationManager orientationManager;
        
        public PlayerBehaviour(bool isGood, BucketList todoList, int spriteNum, Entity entity) : base(entity)
        {
            IsGood = isGood;
            TodoList = todoList;
            HasGroceries = false;
            orientationManager = new SpriteOrientationManager(spriteNum, Entity);
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
            Vector3 posChange = Vector3.Zero;
            if (kbs.IsKeyDown(Keys.Right)) {
                posChange += new Vector3(WalkSpeed, 0, 0);
            }

            if (kbs.IsKeyDown(Keys.Left)) {
                posChange += new Vector3(-WalkSpeed, 0, 0);
            }

            if (kbs.IsKeyDown(Keys.Up)) {
                posChange += new Vector3(0, -WalkSpeed, 0);
            }

            if (kbs.IsKeyDown(Keys.Down)) {
                posChange += new Vector3(0, WalkSpeed, 0);
            }

            posChange.Normalize();
            Position += posChange;
            orientationManager.UpdateOrientation(posChange);
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
    }
}