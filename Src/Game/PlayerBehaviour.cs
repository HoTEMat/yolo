using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class PlayerBehaviour : Behaviour {
        public const float WalkSpeed = 1f; // TODO
        
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
            HandleWalking();
            HandleInteraction();
        }

        private void HandleWalking() {
            KeyboardManager kb = Context.Keyboard;
            Vector3 changeDir = Vector3.Zero;
            if (kb.IsKeyDown(Keys.Right)) {
                changeDir += new Vector3(1, 0, 0);
            }

            if (kb.IsKeyDown(Keys.Left)) {
                changeDir += new Vector3(-1, 0, 0);
            }

            if (kb.IsKeyDown(Keys.Up)) {
                changeDir += new Vector3(0, -1, 0);
            }

            if (kb.IsKeyDown(Keys.Down)) {
                changeDir += new Vector3(0, 1, 0);
            }

            if (changeDir != Vector3.Zero) {
                changeDir.Normalize();
            }

            Vector3 posChange = changeDir * WalkSpeed * (float) Context.GameTime.ElapsedGameTime.TotalSeconds;
            Position += posChange;
            orientationManager.UpdateOrientation(posChange);
        }

        private void HandleInteraction() {
            KeyboardManager kb = Context.Keyboard;
            var scene = Entity.Scene;
            if (kb.IsKeyPressed(Keys.F) && (scene.SelectedInteractable?.CanInteract() ?? false)) {
                var achievement = Entity.Scene.SelectedInteractable?.Interact();
                if (achievement != null)
                {
                    TodoList.ProcessAchievement((AchievementType)achievement);
                }
            }
        }
    }
}