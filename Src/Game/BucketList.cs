using System.Collections.Generic;

namespace yolo {
    public class BucketList {
        public List<BucketListItem> Items;

        public string Header = "Bucket list";

        public Dictionary<AchievementType, string> ItemText = new Dictionary<AchievementType, string>()
        {
            {AchievementType.PutUpBin, "Put up a bin"},
            {AchievementType.EatIceCream, "Eat an icecream"},
            {AchievementType.HugPerson, "Hug a person"},
            {AchievementType.BuyFood, "Buy groceries"},
            {AchievementType.DeliverFood, "Bring groceries to grandma's house"},
            {AchievementType.CleanGraffitti, "Clean up graffiti"},
            {AchievementType.FeedDucks, "Feed the ducks"},
            {AchievementType.ThankDoctor, "Thank the doctor"},
            {AchievementType.BuyFromStand, "Buy something from the market"},

            {AchievementType.ToppleBin, "Kick a bin"},
            {AchievementType.BadIceCream, "Throw away icecream"},
            {AchievementType.CursePerson, "Curse at a person"},
            {AchievementType.DoGraffitti, "Paint graffiti"},
            {AchievementType.PeeInPond, "Pee in the pond"},
            {AchievementType.PeeInFountain, "Pee in the fountain"},
            {AchievementType.YellOnTree, "Yell at a tree"},
            {AchievementType.CurseDoctor, "Curse at the doctor"},
            {AchievementType.BreakHospitalBed, "Break the hospital bed"},
            {AchievementType.StealFood, "Steal groceries"}
            
        };

        public AchievementType[] GoodActionsMultiple = new[] {AchievementType.PutUpBin, AchievementType.HugPerson};
        public AchievementType[] GoodActions = new[]
        {
             AchievementType.EatIceCream, AchievementType.BuyFood, AchievementType.DeliverFood,
            AchievementType.CleanGraffitti, AchievementType.FeedDucks, AchievementType.ThankDoctor, AchievementType.BuyFromStand
        };
        public AchievementType[] BadActionsMultiple = new[] {AchievementType.ToppleBin, AchievementType.CursePerson};
        public AchievementType[] BadActions = new[]
        {
            AchievementType.BadIceCream, AchievementType.DoGraffitti, AchievementType.PeeInPond, 
            AchievementType.PeeInFountain, AchievementType.YellOnTree, AchievementType.CurseDoctor, 
            AchievementType.BreakHospitalBed, AchievementType.StealFood
        };

        public void FillBucketList(bool isGood)
        {
            if (isGood)
            {
                var multipleG = Utils.RandChoice(GoodActionsMultiple);
                Items.Add(new BucketListItem(multipleG, 3)); // ToDo: Constants?
                var otherG = Utils.RandChooseN(GoodActions, 3);
                foreach (var itemType in otherG)
                {
                    Items.Add(new BucketListItem(itemType, 1));
                }
                return;
            }
            // is bad
            var multipleB = Utils.RandChoice(BadActionsMultiple);
            Items.Add(new BucketListItem(multipleB, 3)); // ToDo: Constants?
            var otherB = Utils.RandChooseN(BadActions, 3);
            foreach (var itemType in otherB)
            {
                Items.Add(new BucketListItem(itemType, 1));
            }
        }

        public BucketList(List<BucketListItem> items) {
            Items = items;
        }
        
        public bool TryCrossingOut(AchievementType achievement) {
            int idx = Items.FindIndex(i => i.Achievement == achievement);
            if (idx == -1)
                return false;
            var item = Items[idx];
            if (item.AllDone)
                return false;
            item.DoneCount++;
            return true;
        }
    }

    public class BucketListItem {
        public AchievementType Achievement { get; }
        public int TotalCount { get; }
        public int DoneCount { get; set; }
        public bool AllDone => TotalCount == DoneCount;
        
        public BucketListItem(AchievementType achievement, int totalCount) {
            Achievement = achievement;
            TotalCount = totalCount;
            DoneCount = 0;
        }
    }

    public enum AchievementType {
        ToppleBin,
        PutUpBin,
        EatIceCream,
        BadIceCream,
        HugPerson,
        CursePerson,
        FeedDucks,
        PeeInPond,
        PeeInFountain,
        YellOnTree,
        BuyFood,
        DeliverFood,
        DoGraffitti,
        CleanGraffitti,
        ThankDoctor,
        CurseDoctor,
        BreakHospitalBed,
        StealFood,
        BuyFromStand
    }
}