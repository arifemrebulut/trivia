using UnityEngine;

namespace Trivia
{
    [CreateAssetMenu(fileName = "NewQuestion", menuName = "ScriptableObjects/Question")]
    public class QuestionSO : ScriptableObject
    {
        public string questionText;

        public string answer0;
        public string answer1;
        public string answer2;
        public string answer3;

        public string correctAnswer;
    }
}