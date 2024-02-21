using System;
using CodeBase.UI.Service.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class OpenMenuButton: MonoBehaviour
    {
        public Button Button;

        public MenuId MenuId;
        private IMenuService _menuService;
        
        public void Construct(IMenuService menuService) => 
            _menuService = menuService;

        private void Awake()
        {
           Button.onClick.AddListener(Open);
        }

        private void Open()
        {
            _menuService.Open(MenuId);
        }
    }
}