using System.Collections.Generic;

namespace yolo {
    public class BucketList {
        public List<BucketListItem> Items;

        public string Header = "Bucket list:";

        public Dictionary<AchievementType, string> ItemText = new Dictionary<AchievementType, string>()
        {
            {AchievementType.PutUpBin, "Put up the bin"},
            {AchievementType.EatIceCream, "Eat an icecream"},
            {AchievementType.HugPerson, "Hug people"},
            {AchievementType.BuyFood, "Buy groceries"},
            {AchievementType.DeliverFood, "Bring groceries to grandma's house"},
            {AchievementType.CleanGraffitti, "Clean up graffitti"},
            {AchievementType.FeedDucks, "Feed the ducks"},

            {AchievementType.ToppleBin, "Kick the bin"},
            {AchievementType.BadIceCream, "Throw away icecream"},
            {AchievementType.CursePerson, "Curse people"},
            {AchievementType.DoGraffitti, "Make some graffitti"},
            {AchievementType.PeeInPond, "Pee in the pond"},
            {AchievementType.PeeInFountain, "Pee in the fountain"},
            {AchievementType.YellOnTree, "Yell at a tree"}
        };

        public AchievementType[] GoodActionsMultiple = new[] {AchievementType.PutUpBin, AchievementType.HugPerson};
        public AchievementType[] GoodActions = new[]
        {
             AchievementType.EatIceCream, AchievementType.BuyFood, AchievementType.DeliverFood,
            AchievementType.CleanGraffitti, AchievementType.FeedDucks
        };
        public AchievementType[] BadActionsMultiple = new[] {AchievementType.ToppleBin, AchievementType.CursePerson};
        public AchievementType[] BadActions = new[]
        {
            AchievementType.BadIceCream, AchievementType.DoGraffitti, AchievementType.PeeInPond, 
            AchievementType.PeeInFountain, AchievementType.YellOnTree
        };

        public void FillBucketList(bool isGood)
        {
            if (isGood)
            {
                var multipleG = Utils.RandChoice(GoodActionsMultiple);
                Items.Add(new BucketListItem(multipleG, 3)); // ToDo: Constants?
                var otherG = Utils.RandChooseN(GoodActionsMultiple, 2);
                foreach (var itemType in otherG)
                {
                    Items.Add(new BucketListItem(itemType, 1));
                }
                return;
            }
            // is bad
            var multipleB = Utils.RandChoice(GoodActionsMultiple);
            Items.Add(new BucketListItem(multipleB, 3)); // ToDo: Constants?
            var otherB = Utils.RandChooseN(GoodActionsMultiple, 2);
            foreach (var itemType in otherB)
            {
                Items.Add(new BucketListItem(itemType, 1));
            }
        }

        public BucketList(List<BucketListItem> items) {
            Items = items;
        }

        public void ProcessAchievement(AchievementType achievement)
        {
            TryCrossingOut(achievement);
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
        CleanGraffitti
    }
}