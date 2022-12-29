using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia
{
    public class GameMenu : MenuBase
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;

        [Header("Pause PopUp Settings")]
        [SerializeField] private GameObject pausePopUp;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button resumeButton;

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
            pauseButton.onClick.AddListener(() =>
            {
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
            });

            mainMenuButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());

            resumeButton.onClick.AddListener(() =>
            {
                pausePopUp.SetActive(false);
                Time.timeScale = 1f;
            });
        }

        private void EarnScore(int earnAmount)
        {
            totalScore += earnAmount;

            scoreTMP.text = totalScore.ToString();
        }

        private void LostScore(int lostAmount)
        {
            if (totalScore - lostAmount < 0)
            {
                totalScore = 0;
            }
            else
            {
                totalScore -= lostAmount;
            }

            scoreTMP.text = totalScore.ToString();
        }
    }
}