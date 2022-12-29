using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using Zenject;
using System;

namespace Trivia
{
    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI questionTMP;

        [Header("Banners")]
        [SerializeField] GameObject correctBanner;
        [SerializeField] GameObject incorrectBanner;
        [SerializeField] GameObject timeOutBanner;

        [SerializeField] AnswerUI[] answerUIComponents = new AnswerUI[4];

        private string currentCategory;
        private string currentCorrectChoise;

        private QuestionManager questionManager;
        private UIManager uiManager;

        private void OnEnable()
        {
            EventManager.NewQuestionLoadedEvent += UpdataUI;
            EventManager.AnswerClickedEvent += OnAnswerClicked;
            EventManager.TimeOutEvent += OnTimeOut;
        }

        private void OnDisable()
        {
            EventManager.NewQuestionLoadedEvent -= UpdataUI;
            EventManager.AnswerClickedEvent -= OnAnswerClicked;
            EventManager.TimeOutEvent -= OnTimeOut;
        }

        [Inject]
        private void Constract(QuestionManager _questionManager, UIManager _uiManager)
        {
            questionManager = _questionManager;
            uiManager = _uiManager;
        }

        private void Start()
        {
            UpdataUI(questionManager.CurrentQuestion);
        }

        public void UpdataUI(QuestionSO questionSO)
        {
            currentCorrectChoise = questionSO.correctAnswer;
            currentCategory = questionSO.category;

            questionTMP.text = questionSO.questionText;

            for (int i = 0; i < answerUIComponents.Length; i++)
            {
                answerUIComponents[i].Reset();

                string choise = i switch
                {
                    0 => "A",
                    1 => "B",
                    2 => "C",
                    3 => "D",
                    _ => "-1"
                };

                answerUIComponents[i].PopulateAnswer(questionSO.answers[i], choise);
            }
        }

        private void OnAnswerClicked(string choise)
        {
            foreach (var answer in answerUIComponents)
            {
                answer.CheckForAnswer(currentCorrectChoise);
            }

            bool answerCorrect = choise == currentCorrectChoise;

            if (answerCorrect)
            {
                EventManager.ScoreEarned(10);

                OnCorrect();
            }
            else
            {
                EventManager.ScoreLost(5);

                OnIncorrect();
            }
        }

        private void OnCorrect()
        {
            EventManager.ScoreEarned(10);

            TweenBanner(correctBanner.transform, () => EventManager.LoadNewQuestion(currentCategory));
        }

        private void OnIncorrect()
        {
            EventManager.ScoreLost(5);

            TweenBanner(incorrectBanner.transform, () => uiManager.SwitchMenu<WheelMenu>(false), 0.5f);
        }

        private void OnTimeOut()
        {
            EventManager.ScoreLost(3);

            TweenBanner(timeOutBanner.transform, () => uiManager.SwitchMenu<WheelMenu>(false), 0.5f);
        }

        private void TweenBanner(Transform banner, Action onComplete = null, float onCompleteDelay = 0f)
        {
            banner.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            // Scale tweens
            sequence.Append(banner.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
            sequence.AppendInterval(1f);
            sequence.Append(banner.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Linear));

            // Optinal OnComplete method and delay before method
            sequence.AppendInterval(onCompleteDelay);
            sequence.AppendCallback(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}