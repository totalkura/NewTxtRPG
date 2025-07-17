namespace NewTxtRPG.Structs
{
    internal class Quest
    {
        public enum ConditionType
        {
            Level,
            Attack,
            DungeonClear
        }

        public string Name { get; }
        public string Description { get; }
        public string ConditionDescription { get; }
        public int ConditionTarget { get; }
        public int ConditionProgress { get; set; }
        public int RewardGold { get; }
        public string RewardItemName { get; }
        public ConditionType Type { get; }

        public bool IsAccepted { get; set; }
        public bool IsComplete { get; set; }

        public Quest(string name, string description, string conditionDescription, int target,
                     int rewardGold, string rewardItemName, ConditionType type)
        {
            Name = name;
            Description = description;
            ConditionDescription = conditionDescription;
            ConditionTarget = target;
            RewardGold = rewardGold;
            RewardItemName = rewardItemName;
            Type = type;
            ConditionProgress = 0;
            IsAccepted = false;
            IsComplete = false;
        }
    }
}
