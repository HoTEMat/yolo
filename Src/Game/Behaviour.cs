using Microsoft.Xna.Framework;

namespace yolo {
    public abstract class Behaviour {
        public Entity Entity { get; }
        public Vector2 Position {
            get => Entity.Position;
            set => Entity.Position = value;
        }

        public Context Context => Entity.Context;
        public abstract void Update();
    }

    public abstract class Interactable : Behaviour {
        public abstract AchievementType? Interact();

        public abstract bool CanInteract();
        public bool Highlighted { get; private set; }
        public void SetHighlighted(bool highlighted) {
            if (highlighted == Highlighted)
                return;
            Entity.Animation.Highlighted = highlighted;
            Highlighted = highlighted;
        }
    }

    public class Bin : Interactable
    {
        public bool IsOverturned { get; private set; }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            if (IsOverturned)
            {
                IsOverturned = false;
                return AchievementType.PutUpBin;
            }
            IsOverturned = true;
            return AchievementType.ToppleBin;
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == IsOverturned;
        }

    }

    public class IceCreamStand : Interactable
    {
        public override void Update()
        {
            return;
        }

        public override AchievementType? Interact()
        {
            return Entity.Context.Player.IsGood ? AchievementType.EatIceCream : AchievementType.BadIceCream;
        }

        public override bool CanInteract()
        {
            return true; // you can always eat icecream
        }
    }
    public class Pond : Interactable
    {
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            return Entity.Context.Player.IsGood ? AchievementType.FeedDucks : AchievementType.PeeInPond;
        }
        public override bool CanInteract()
        {
            return true;
        }
    }

    public class GraffittiHouse : Interactable
    {
        public bool HasGraffitti { get; private set; }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            // ToDo: draw graffitti or clean it
            throw new System.NotImplementedException();
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == HasGraffitti;
        }
    }
    public class Grandma : Interactable
    {
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            Entity.Context.Player.HandInGroceries();
            return AchievementType.DeliverFood;
        }

        public override bool CanInteract()
        {
            return Entity.Context.Player.HasGroceries;
        }
    }
    public class Tree : Interactable
    {
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            return AchievementType.YellOnTree;
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == false;
        }
    }
}