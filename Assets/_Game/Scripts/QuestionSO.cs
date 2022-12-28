using UnityEngine;

namespace Trivia
{
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "ScriptableObjects/Question")]
    public class QuestionSO : ScriptableObject
    {
        public string questionText;

        public string[] answers = new string[4];

        public string category;

        public string correctAnswer;
    }
}