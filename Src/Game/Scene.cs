using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace yolo {
    public class Scene {
        private const float InteractableDistanceThreshold = 10; // TODO

        public string Name { get; }
        public List<Entity> Entities { get; }
        public Indexer2D<Tile> Tiles { get; }
        public Interactable SelectedInteractable { get; private set; }
        private Context ctx;

        public Scene(string name, List<Entity> entities, Indexer2D<Tile> tiles, Context ctx) {
            Name = name;
            this.ctx = ctx;
            Entities = entities;
            Tiles = tiles;
        }

        public void AddEntity(Entity e) {
            e.Scene = this;
            Entities.Add(e);
        }

        public void Update() {
            RemoveDestroyed();
            ResolveInteractable();
            UpdateEntities();
            ResolveCollisions();
            UpdateCamera();
        }

        private void RemoveDestroyed() {
            Entities.RemoveAll(e => e.Destroyed);
        }

        private void ResolveInteractable() {
            Vector3 playerPos = ctx.Player.Entity.Position;
            Interactable selected = null;
            float selectedDistance = 0; // dummy value
            foreach (Entity e in Entities) {
                if (e.Behavior is Interactable interactable && interactable.CanInteract()) {
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
            Vector3 totalPush = Vector3.Zero;
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