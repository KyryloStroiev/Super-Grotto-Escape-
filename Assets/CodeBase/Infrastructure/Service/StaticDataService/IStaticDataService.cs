using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Service.StaticDataService
{
    public interface IStaticDataService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        PlayerData ForPlayer(string player);
    }
}