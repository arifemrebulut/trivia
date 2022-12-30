using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Trivia
{
    public class AnswerUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI answerTMP;
        [SerializeField] Button answerButton;
        [SerializeField] GameObject correctBG;
        [SerializeField] GameObject incorrectBG;

        private string choise;
        private bool chosed;

        private void Start()
        {
            answerButton.onClick.AddListener(() => OnAnswerClicked());
        }

        public void PopulateAnswer(string answerText, string _choise)
        {
            answerTMP.text = answerText;
            choise = _choise;
        }

        private void EnableHighligt(bool correct)
        {
            if (correct)
            {
                correctBG.SetActive(true);
            }
            else
            {
                incorrectBG.SetActive(true);
            }
        }

        private void DisableHighlights()
        {
            correctBG.SetActive(false);
            incorrectBG.SetActive(false);
        }

        private void DisableInteractable()
        {
            answerButton.interactable = false;
        }

        private void OnAnswerClicked()
        {
            chosed = true;

            EventManager.AnswerClicked(choise);
        }

        public void CheckForAnswer(string correctChoise)
        {
            if (choise == correctChoise)
            {
                EnableHighligt(true);
            }

            if (choise != correctChoise && chosed)
            {

                EnableHighligt(false);

                IncorrectShake();
            }

            DisableInteractable();
        }

        public void IncorrectShake()
        {
            transform.DOShakeRotation(0.4f, 25f, 10, 30);
        }

        public void Reset()
        {
            answerButton.interactable = true;
            chosed = false;
            DisableHighlights();
        }
    }
}