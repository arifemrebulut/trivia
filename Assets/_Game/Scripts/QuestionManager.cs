using System.Collections.Generic;
using System.Linq;
using System;
using Zenject;

namespace Trivia
{
    public class QuestionManager : IInitializable, IDisposable
    {
        private Settings settings;

        private QuestionSO currentQuestion;

        public List<string> Categories { get; private set; }

        public QuestionManager(Settings _settings)
        {
            settings = _settings;

            Categories = new List<string>();
            settings.configurationSO.categories.ForEach(x => Categories.Add(x.categoryName));
        }

        public void Initialize()
        {
            EventManager.LoadNewQuestionEvent += LoadNextQuestion;
        }

        public void Dispose()
        {
            EventManager.LoadNewQuestionEvent -= LoadNextQuestion;
        }

        public void LoadNextQuestion(string _categoryName)
        {
            QuestionSO question = GetQuestionForCategory(_categoryName);

            if (question != null)
            {
                settings.questionUI.UpdataUI(question);

                currentQuestion = question;
            }
        }

        public QuestionSO GetQuestionForCategory(string _categoryName)
        {
            List<QuestionSO> questionsInCategory = settings.configurationSO.categories
                .FirstOrDefault(category => category.categoryName == _categoryName).questions;

            if (questionsInCategory != null)
            {
                QuestionSO randomQuestionSO = questionsInCategory[UnityEngine.Random.Range(0, questionsInCategory.Count)];

                return randomQuestionSO;
            }

            return null;
        }

        [Serializable]
        public class Settings
        {
            public QuestionUI questionUI;
            public QuestionsConfigurationSO configurationSO;
        }

    }
}