using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Trivia
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeTMP;

        private int initialTime = 20;
        private int remainingTime;

        private bool canWork;

        private void Start()
        {
            remainingTime = initialTime;
            canWork = true;

            UpdateUI();

            StartCoroutine(TimerCoroutine());
        }

        #region Subscribe - Unsubscribe Events
        private void OnEnable()
        {
            EventManager.AnswerClickedEvent += StopTimer;
            EventManager.NewQuestionLoadedEvent += ResetTimer;

            ResetTimer(null);
        }

        private void OnDisable()
        {
            EventManager.AnswerClickedEvent -= StopTimer;
            EventManager.NewQuestionLoadedEvent -= ResetTimer;
        }
        #endregion

        private IEnumerator TimerCoroutine()
        {
            while (remainingTime > 0 && canWork)
            {
                yield return new WaitForSeconds(1f);

                remainingTime--;

                UpdateUI();
            }

            if (remainingTime == 0)
            {
                EventManager.TimeOut();
            }
        }

        private void UpdateUI()
        {
            timeTMP.text = remainingTime.ToString();
        }

        private void StopTimer(string answer)
        {
            StopAllCoroutines();

            canWork = false;
        }

        private void ResetTimer(QuestionSO question)
        {
            StopAllCoroutines();

            remainingTime = initialTime;
            canWork = true;

            UpdateUI();
            StartCoroutine(TimerCoroutine());
        }
    }
}