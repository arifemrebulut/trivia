using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
            if (choise == correctChoise && chosed)
            {
                EnableHighligt(true);
            }

            if(choise != correctChoise && chosed)
            {
                EnableHighligt(false);
            }

            if(choise == correctChoise && !chosed)
            {
                EnableHighligt(true);
            }

            DisableInteractable();
        }

        public void Reset()
        {
            answerButton.interactable = true;
            chosed = false;
            DisableHighlights();
        }
    }
}