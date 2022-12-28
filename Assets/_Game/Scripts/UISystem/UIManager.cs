using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Trivia
{
    public class UIManager
    {
        private Settings settings;

        private List<Menu> menuList;
        private Menu lastActiveMenu;

        private readonly Stack<Menu> menuHistory = new Stack<Menu>();

        public UIManager(Settings _settings)
        {
            settings = _settings;

            menuList = settings.canvas.GetComponentsInChildren<Menu>(true).ToList();
            menuList.ForEach(x => x.Hide());

            lastActiveMenu = settings.startingMenu;
            SwitchMenu<MainMenu>(false);
        }

        public void SwitchMenu<T>(bool rememberPrevious = true) where T : Menu
        {
            if (lastActiveMenu != null)
            {
                lastActiveMenu.Hide();
            }

            Menu desiredMenu = menuList.Find(x => x is T);

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

        public void SwitchMenu(Menu menu, bool rememberPrevious = true)
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
            public Menu startingMenu;
        }
    }
}