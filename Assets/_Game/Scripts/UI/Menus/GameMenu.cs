using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Trivia
{
    public class GameMenu : MenuBase
    {
        [SerializeField] private TextMeshProUGUI scoreTMP;
        [SerializeField] private Transform scoreIcon;

        [Header("Pause PopUp Settings")]
        [SerializeField] private GameObject pausePopUp;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button resumeButton;

        private int previousScore;
        private int totalScore;

        #region Subscribe - Unsubscrive Events
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
        #endregion

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
            previousScore = totalScore;

            totalScore += earnAmount;

            StartCoroutine(AnimateScoreText());
        }

        private void LostScore(int lostAmount)
        {
            previousScore = totalScore;

            if (totalScore - lostAmount < 0)
            {
                totalScore = 0;
            }
            else
            {
                totalScore -= lostAmount;
            }

            StartCoroutine(AnimateScoreText());
        }

        private IEnumerator AnimateScoreText()
        {
            scoreIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f);
            scoreIcon.DOShakeRotation(0.4f, 25f, 10, 30);

            if (totalScore > previousScore)
            {
                while (previousScore + 1 < totalScore)
                {
                    yield return new WaitForSeconds(1f / 10f);

                    previousScore++;

                    scoreTMP.text = previousScore.ToString();
                }
            }
            else
            {
                while (previousScore - 1 > totalScore)
                {
                    yield return new WaitForSeconds(1f / 10f);

                    previousScore--;

                    scoreTMP.text = previousScore.ToString();
                }
            }
        }
    }
}