using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class GameMenu : Menu
    {
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());
        }
    }
}