using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public KillData KillData;
        public PlayerState PlayerState;
        public Stats PlayerStats;
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            KillData = new KillData();
            PlayerState = new PlayerState();
            PlayerStats = new Stats();
        }
    }
}