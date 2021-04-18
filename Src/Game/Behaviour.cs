using System;
using Microsoft.Xna.Framework;

namespace yolo {
    public abstract class Behaviour {
        public Behaviour(Entity entity)
        {
            Entity = entity;
        }
        
        public Entity Entity { get; }
        public Vector3 Position {
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

        protected Interactable(Entity entity) : base(entity)
        {
        }
    }

    public class Bin : Interactable
    {
        public bool IsOverturned { get; private set; }

        public Bin(bool isOverturned, Entity entity) : base(entity)
        {
            IsOverturned = isOverturned;
            Entity.Animation = IsOverturned
                ? new Animation(Entity.Context.Assets.Sprites.TrashcanDown)
                : new Animation(Entity.Context.Assets.Sprites.TrashcanUp);
        }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            if (IsOverturned)
            {
                IsOverturned = false;
                Entity.Animation.Reset(Entity.Context.Assets.Sprites.TrashcanUp);
                return AchievementType.PutUpBin;
            }
            IsOverturned = true;
            Entity.Animation.Reset(Entity.Context.Assets.Sprites.TrashcanDown);
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
        public IceCreamStand(Entity entity) : base(entity)
        {
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

        public Pond(Entity entity) : base(entity)
        {
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

        public GraffittiHouse(Entity entity) : base(entity)
        {
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

        public Grandma(Entity entity) : base(entity)
        {
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

        public Tree(Entity entity) : base(entity)
        {
        }
    }

    public class Fountain : Interactable
    {
        public Fountain(Entity entity) : base(entity)
        {
        }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            return AchievementType.PeeInFountain;
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == false;
        }
    }

    public class CashRegister : Interactable
    {
        public CashRegister(Entity entity) : base(entity)
        {
        }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            Entity.Context.Player.PickGroceries();
            return AchievementType.BuyFood;
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood && !Entity.Context.Player.HasGroceries;
        }
    }
    
    
}