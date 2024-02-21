using System;
using CodeBase.UI.Menu;
using CodeBase.UI.Service.Menu;
using UnityEngine;

namespace CodeBase.StaticData.Menu
{
    [Serializable]
    public class MenuConfig
    {
        public MenuId menuId;
        public GameObject Prefab;
    }
}