using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace yolo {
    public class Scene {
        private const float InteractableDistanceThreshold = 0.7f; // TODO

        public string Name { get; }
        public List<Entity> Entities { get; }
        private List<Entity> toBeAddedEntities = new();
        public Indexer2D<Tile> Tiles { get; }
        public Interactable SelectedInteractable { get; private set; }
        public Vector3 Tomovo { get; set; }

        private Context ctx;

        public Scene(string name, List<Entity> entities, Indexer2D<Tile> tiles, Context ctx) {
            Name = name;
            this.ctx = ctx;
            Entities = entities;
            Tiles = tiles;
        }

        public void AddEntity(Entity e) {
            e.Scene = this;
            toBeAddedEntities.Add(e);
        }
        
        // Dont call this if you dont know what you are doing. 
        // Maybe you want to use Destroy() - that is safe.
        public void RemoveEntityNow(Entity e) {
            e.Scene = null;
            Entities.Remove(e);
        }
        
        // Dont call this if you dont know what you are doing. 
        // Maybe you want to use AddEntity() - that is safe.
        public void AddEntityNow(Entity e) {
            e.Scene = this;
            Entities.Add(e);
        }

        public void Update() {
            UpdateKeyboard();
            RemoveDestroyed();
            ResolveInteractable();
            UpdateEntities();
            ResolveCollisions();
            InsertAddedEntities();
            UpdateCamera();
            ctx.World.TriggerSceneSwitchCheck();
        }

        private void UpdateKeyboard() {
            ctx.Keyboard.RegisterState(Keyboard.GetState());
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
                    float distance = e.GetDistanceFrom(ctx.Player.Entity);
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

        private void InsertAddedEntities() {
            Entities.AddRange(toBeAddedEntities);
            toBeAddedEntities = new List<Entity>();
        }

        // Don't call this from the update loop.
        public void RemoveTemporalEntitiesNow() {
            Entities.RemoveAll(e => e.IsTemporal);
        }
    }
}