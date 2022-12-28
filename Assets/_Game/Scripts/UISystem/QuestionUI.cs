using UnityEngine;
using TMPro;

namespace Trivia
{
    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI questionTMP;

        [SerializeField] GameObject correctBanner;
        [SerializeField] GameObject incorrectBanner;

        [SerializeField] AnswerUI[] answerUIComponents = new AnswerUI[4];

        private string currentCorrectChoise;

        private void OnEnable()
        {
            EventManager.AnswerClickedEvent += OnAnswerClicked;
        }

        private void OnDisable()
        {
            EventManager.AnswerClickedEvent -= OnAnswerClicked;
        }

        public void UpdataUI(QuestionSO questionSO)
        {
            currentCorrectChoise = questionSO.correctAnswer;

            questionTMP.text = questionSO.questionText;

            for (int i = 0; i < answerUIComponents.Length; i++)
            {
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
            }
            else
            {
                EventManager.ScoreLost(5);
            }

            ShowBanner(answerCorrect);
        }

        private void ShowBanner(bool correct)
        {
            if (correct)
            {
                correctBanner.SetActive(true);
            }
            else
            {
                incorrectBanner.SetActive(true);
            }
        }
    }
}