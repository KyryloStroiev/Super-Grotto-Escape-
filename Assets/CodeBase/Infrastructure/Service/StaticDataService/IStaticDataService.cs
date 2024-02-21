using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Menu;
using CodeBase.StaticData.Player;
using CodeBase.UI.Service.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.Service.StaticDataService
{
    public interface IStaticDataService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        LevelStaticData ForLevel(string sceneKey);
        PlayerData ForPlayer(string player);
        MenuConfig ForMenu(MenuId menuId);
    }
}