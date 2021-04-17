using System.Collections.Generic;

namespace yolo {
    public class BucketList {
        public List<BucketListItem> Items;

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