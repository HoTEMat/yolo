using System;
using Microsoft.Xna.Framework;

namespace yolo {
    public abstract class Behaviour {
        public Behaviour(Entity entity)
        {
            Entity = entity;
            context = entity.Context;
        }

        protected Context context { get; private set; }
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
            if (IsOverturned) {
                Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.TrashcanDown);
            } else {
                Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.TrashcanUp);
            }
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
        private bool interacted;
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            interacted = false;
            return Entity.Context.Player.IsGood ? AchievementType.EatIceCream : AchievementType.BadIceCream;
        }

        public override bool CanInteract()
        {
            return !interacted;
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
            if (Entity.Context.Player.IsGood)
            {
                Fed = true;
                return AchievementType.FeedDucks;
            }
            Peed = true;
            Entity.Animation.Reset(Entity.Context.Assets.Sprites.ParkPondPee);
            return AchievementType.PeeInPond;
        }

        public bool Peed { get; set; }
        public bool Fed { get; set; }

        public override bool CanInteract()
        {
            return (Entity.Context.Player.IsGood && !Fed)|| (!Entity.Context.Player.IsGood && !Peed);
        }

        public Pond(Entity entity) : base(entity)
        {
        }
    }

    public class Graffitti : Interactable
    {
        public bool Visible { get; private set; }
        public Graffitti(Entity entity, bool visible) : base(entity)
        {
            Visible = visible;
            if (Visible)
            {
                Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.Grafitti);
                return;
            }
            Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.FadedGrafitti);
        }

        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            if (Visible)
            {
                Visible = false;
                Entity.Animation.Reset(Entity.Context.Assets.Sprites.FadedGrafitti);
                return AchievementType.CleanGraffitti;
            }
            else
            {
                Visible = true;
                Entity.Animation.Reset(Entity.Context.Assets.Sprites.Grafitti);
                return AchievementType.DoGraffitti;
            }
        }
        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == Visible;
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

    public class GroceryStand : Interactable
    {
        public bool Robed { get; set; }
        public GroceryStand(Entity entity) : base(entity)
        {
            Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.MarketIsleEntity);
        }

        public override void Update()
        {
            return;
        }

        public override AchievementType? Interact()
        {
            Entity.Context.Player.PickGroceries();
            Entity.Animation.Reset(Entity.Context.Assets.Sprites.MarketIsleStolenEntity);
            return AchievementType.StealFood;
        }

        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == false && Robed == false && Entity.Context.Player.HasGroceries == false;
        }
    }

    public class Doctor : Interactable
    {
        private bool interacted;
        public Doctor(Entity entity) : base(entity)
        {
            Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.HospitalDoctorEntity);
        }
        public override void Update()
        {
            return;
        }
        public override AchievementType? Interact()
        {
            interacted = true;

            if (Entity.Context.Player.IsGood && !interacted)
            {
                return AchievementType.ThankDoctor;
            }

            return AchievementType.CurseDoctor;
        }

        public override bool CanInteract()
        {
            return true;
        }
    }

    public class HospitalBed : Interactable
    {
        private bool Broken { get; set; }
        public HospitalBed(Entity entity) : base(entity)
        {
            Broken = false;
            Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.HospitalBedEntity);
        }

        public override void Update()
        {
            return;
        }

        public override AchievementType? Interact()
        {
            Broken = true;
            Entity.Animation.Reset(Entity.Context.Assets.Sprites.HospitalBedBrokenEntity);
            return AchievementType.BreakHospitalBed;
        }

        public override bool CanInteract()
        {
            return Entity.Context.Player.IsGood == false && Broken == false;
        }
    }

    public class MarketStand : Interactable
    {
        private bool _bought = false;

        public MarketStand(Entity entity) : base(entity)
        {
        }

        public override void Update()
        {
            return;
        }

        public override AchievementType? Interact()
        {
            _bought = true;
            return AchievementType.BuyFromStand;
        }

        public override bool CanInteract()
        {
            if (_bought) return false;

            return Entity.Context.Player.IsGood;
        }
    }

    public class IntroDoctorBehavior : Interactable {
        private bool _bought = false;

        public IntroDoctorBehavior(Entity entity) : base(entity) {
            Entity.ChangeSpriteTo(Entity.Context.Assets.Sprites.HospitalDoctorEntity);
        }

        public override void Update() {
            return;
        }

        public override AchievementType? Interact() {
            Entity.Context.Assets.Dialogs.DoctorDialog.OpenNewDialog(Entity.Context);
            return null;
        }

        public override bool CanInteract() {
            if (_bought) return false;

            return Entity.Context.Player.IsGood;
        }
    }
}