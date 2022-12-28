using System;

namespace Trivia
{
    public static class EventManager
    {
        public static Action<string> AnswerClickedEvent;
        public static Action<string> CorrectAnswerCheckedEvent;
        public static Action<string> LoadNewQuestionEvent;
        public static Action<int> ScoreEarnedEvent;
        public static Action<int> ScoreLostEvent;

        public static void AnswerClicked(string choise) => AnswerClickedEvent?.Invoke(choise);
        public static void CorrectAnswerChecked(string correctAnswer) => CorrectAnswerCheckedEvent?.Invoke(correctAnswer);
        public static void LoadNewQuestion(string category) => LoadNewQuestionEvent?.Invoke(category);
        public static void ScoreEarned(int earnAmount) => ScoreEarnedEvent?.Invoke(earnAmount);
        public static void ScoreLost(int lostAmount) => ScoreLostEvent?.Invoke(lostAmount);
    }
}