using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class LeaderboardMenu : Menu
    {
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());
        }
    }
}