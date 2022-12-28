using UnityEngine;
using TMPro;

namespace Trivia
{
    public class QuestionUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI questionTMP;

        [SerializeField] TextMeshProUGUI[] answerTMPComponents = new TextMeshProUGUI[4];

        public void UpdataUI(QuestionSO questionSO)
        {
            questionTMP.text = questionSO.questionText;

            for (int i = 0; i < answerTMPComponents.Length; i++)
            {
                answerTMPComponents[i].text = questionSO.answers[i];
            }
        }
    }
}