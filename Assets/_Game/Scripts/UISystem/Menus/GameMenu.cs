using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class GameMenu : Menu
    {
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI scoreTMP;

        private int totalScore;

        private void OnEnable()
        {
            EventManager.ScoreEarnedEvent += EarnScore;
            EventManager.ScoreLostEvent += LostScore;

        }

        private void OnDisable()
        {
            EventManager.ScoreEarnedEvent -= EarnScore;
            EventManager.ScoreLostEvent -= LostScore;

        }

        private void Start()
        {
            backButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());
        }

        private void EarnScore(int earnAmount)
        {
            totalScore += earnAmount;

            scoreTMP.text = totalScore.ToString();
        }

        private void LostScore(int lostAmount)
        {
            totalScore -= lostAmount;

            scoreTMP.text = totalScore.ToString();
        }
    }
}