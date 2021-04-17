using System.Collections.Generic;

namespace yolo {
    public class BucketList {
        public List<BucketListItem> Items;

        public string Header = "Bucket list:";

        public Dictionary<AchievementType, string> ItemText = new Dictionary<AchievementType, string>()
        {
            {AchievementType.PutUpBin, "Put up the bin - 3"},
            {AchievementType.EatIceCream, "Eat an icecream"},
            {AchievementType.HugPerson, "Hug people - 3"},
            {AchievementType.BuyFood, "Buy groceries"},
            {AchievementType.DeliverFood, "Bring groceries to grandma's house"},
            {AchievementType.CleanGraffitti, "Clean up graffitti"},
            {AchievementType.FeedDucks, "Feed the ducks"},

            {AchievementType.ToppleBin, "Kick the bin - 3"},
            {AchievementType.BadIceCream, "Throw away icecream"},
            {AchievementType.CursePerson, "Curse people - 3"},
            {AchievementType.DoGraffitti, "Make some graffitti"},
            {AchievementType.PeeInPond, "Pee in the pond"},
            {AchievementType.PeeInFountain, "Pee in the fountain"},
            {AchievementType.YellOnTree, "Yell at a tree"}
        };

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