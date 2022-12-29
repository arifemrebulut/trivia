using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trivia
{
    public class UIManager
    {
        private Settings settings;

        private List<MenuBase> menuList;
        private MenuBase lastActiveMenu;

        private readonly Stack<MenuBase> menuHistory = new Stack<MenuBase>();

        public UIManager(Settings _settings)
        {
            settings = _settings;

            menuList = settings.canvas.GetComponentsInChildren<MenuBase>(true).ToList();
            menuList.ForEach(x => x.Hide());

            lastActiveMenu = settings.startingMenu;
            SwitchMenu<MainMenu>(false);
        }

        public void SwitchMenu<T>(bool rememberPrevious = true) where T : MenuBase
        {
            if (lastActiveMenu != null)
            {
                lastActiveMenu.Hide();
            }

            MenuBase desiredMenu = menuList.Find(x => x is T);

            if (desiredMenu != null)
            {
                if (rememberPrevious)
                {
                    menuHistory.Push(lastActiveMenu);
                }

                desiredMenu.Show();
                lastActiveMenu = desiredMenu;
            }
            else { Debug.LogWarning("The desired menu was not found!"); }
        }

        public void SwitchMenu(MenuBase menu, bool rememberPrevious = true)
        {
            if (lastActiveMenu != null)
            {
                if (rememberPrevious)
                {
                    menuHistory.Push(lastActiveMenu);
                }

                lastActiveMenu.Hide();
            }

            menu.Show();

            lastActiveMenu = menu;
        }

        public void GoToPreviousMenu()
        {
            if (menuHistory.Count > 0)
            {
                SwitchMenu(menuHistory.Pop(), false);
            }
        }

        [Serializable]
        public class Settings
        {
            public Transform canvas;
            public MenuBase startingMenu;
        }
    }
}