using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Menu
{
    [CreateAssetMenu(menuName = "StaticData/Menu Static Data", fileName = "MenuStaticData")]
    public class MenuStaticData: ScriptableObject
    {
        public List<MenuConfig> Configs;
    }
}