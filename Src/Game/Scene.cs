using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public class Scene {
        private const float InteractableDistanceThreshold = 10; // TODO
        
        public List<Entity> Entities { get; }
        public TileIndexer2D Tiles { get; }
        public Interactable SelectedInteractable { get; private set; }
        private Context ctx;

        public Scene(List<Entity> entities, TileIndexer2D tiles, Context ctx) {
            this.ctx = ctx;
            Entities = entities;
            Tiles = tiles;
        }

        public void AddEntity(Entity e) {
            e.Scene = this;
            Entities.Add(e);
        }

        public void TriggerInteraction() {
            SelectedInteractable?.Interact();
        }

        public void Update() {
            ResolveInteractable();
            UpdateEntities();
            ResolveCollisions();
            UpdateCamera();
        }

        private void ResolveInteractable() {
            Vector2 playerPos = ctx.Player.Entity.Position;
            Interactable selected = null;
            float selectedDistance = 0; // dummy value
            foreach (Entity e in Entities) {
                if (e.Behavior is Interactable interactable) {
                    float distance = (e.Position - playerPos).Length();
                    if (distance <= InteractableDistanceThreshold &&
                        (selected == null || selectedDistance > distance)) {
                        selected = interactable;
                        selectedDistance = distance;
                    }
                }
            }

            if (SelectedInteractable != selected) {
                SelectedInteractable?.SetHighlighted(false);
                selected?.SetHighlighted(true);
                SelectedInteractable = selected;
            }
        }

        private void UpdateEntities() {
            foreach (Entity e in Entities) {
                e.Update();
            }
        }

        private void ResolveCollisions() {
            foreach (Entity e in Entities) {
                if (e.Collider != null && e.Collider.Movable) {
                    ResolveCollisionsFor(e);
                }
            }
        }

        private void ResolveCollisionsFor(Entity e) {
            Vector2 totalPush = Vector2.Zero;
            foreach (Entity other in Entities) {
                if (other.Collider != null && !other.Collider.Movable) {
                    totalPush += e.Collider.PushFrom(other);
                }
            }

            e.Position += totalPush;
        }

        private void UpdateCamera() {
            ctx.Camera.Update();
        }
    }
}