using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class PlayerBehaviour : Behaviour {
        public const float WalkSpeed = 4f; // TODO

        public bool IsGood { get; }
        public bool HasGroceries { get; set; }
        public BucketList TodoList { get; }
        private SpriteOrientationManager orientationManager;
        public float FreezeFor { get; set; }

        public PlayerBehaviour(bool isGood, BucketList todoList, int spriteNum, Entity entity) : base(entity) {
            IsGood = isGood;
            TodoList = todoList;
            HasGroceries = false;
            orientationManager = new SpriteOrientationManager(spriteNum, Entity);
        }

        public void HandInGroceries() {
            HasGroceries = false;
            // ToDo : stop displaying groceries symbol
        }
        public void PickGroceries() {
            HasGroceries = true;
            // ToDo: start displaying groceries
        }

        public override void Update() {

            if (FreezeFor > 0) {
                FreezeFor -= Entity.Context.dSec;
                return;
            } else {
                FreezeFor = 0;
            }

            HandleWalking();
            HandleInteraction();
        }

        private void HandleWalking() {
            KeyboardManager kb = Context.Keyboard;
            Vector3 changeDir = Vector3.Zero;

            if (Context.World.TimeToLive <= 0)
            {
                orientationManager.Dead();
                return;
            }
            
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

            Vector3 posChange = changeDir * WalkSpeed * (float)Context.GameTime.ElapsedGameTime.TotalSeconds;
            Position += posChange;
            orientationManager.UpdateOrientation(posChange);
        }

        private void HandleInteraction() {
            // TODO: remove this (just for testing)
            //if (Context.Keyboard.IsKeyPressed(Keys.P) && Entity.Scene.SelectedInteractable != null) {
            //    float d = Entity.GetDistanceFrom(Entity.Scene.SelectedInteractable.Entity);
            //    Console.WriteLine(d);
            //}

            // TODO: remove this (just for testing)
            //if (Context.Keyboard.IsKeyPressed(Keys.R)) {
            //    Context.Game.Restart(new FirstLevelLoader());
            //}


            KeyboardManager kb = Context.Keyboard;
            var scene = Entity.Scene;
            if (kb.IsKeyPressed(Keys.F) && (scene.SelectedInteractable?.CanInteract() ?? false)) {
                var achievement = Entity.Scene.SelectedInteractable?.Interact();
                if (achievement != null) {
                    ShowHearts();
                    bool crossed = TodoList.TryCrossingOut((AchievementType)achievement);
                    if (crossed) {
                        Context.Score.addScoreForAchievement(Context.TSec);
                    }
                }
            }
        }

        private void ShowHearts() {
            Vector3 pos = Entity.Position + World.Up * 0.5f + World.Backward * 0.001f;
            ISpriteSet hearth = IsGood ? Context.Assets.Sprites.HeartGood : Context.Assets.Sprites.HeartBad;
            ParticleSystemGenerator.HearthsGenerator(Entity.Scene, Context)
                .Generate(hearth, pos);
        }
    }
}