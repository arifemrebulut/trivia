using System;

namespace Trivia
{
    public static class EventManager
    {
        public static Action<string> AnswerClickedEvent;
        public static Action<string> LoadNewQuestionEvent;
        public static Action<QuestionSO> NewQuestionLoadedEvent;
        public static Action<int> ScoreEarnedEvent;
        public static Action<int> ScoreLostEvent;
        public static Action TimeOutEvent;

        public static void AnswerClicked(string choise) => AnswerClickedEvent?.Invoke(choise);
        public static void LoadNewQuestion(string category) => LoadNewQuestionEvent?.Invoke(category);
        public static void NewQuestionLoaded(QuestionSO questionSO) => NewQuestionLoadedEvent?.Invoke(questionSO);
        public static void ScoreEarned(int earnAmount) => ScoreEarnedEvent?.Invoke(earnAmount);
        public static void ScoreLost(int lostAmount) => ScoreLostEvent?.Invoke(lostAmount);
        public static void TimeOut() => TimeOutEvent?.Invoke();
    }
}