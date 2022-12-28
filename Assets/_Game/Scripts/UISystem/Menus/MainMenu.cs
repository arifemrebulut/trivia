using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class MainMenu : Menu
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button leaderBoardButton;

        private void Start()
        {
            playButton.onClick.AddListener(() => uiManager.SwitchMenu<WheelMenu>());
            leaderBoardButton.onClick.AddListener(() => uiManager.SwitchMenu<LeaderboardMenu>()); 
        }
    }
}