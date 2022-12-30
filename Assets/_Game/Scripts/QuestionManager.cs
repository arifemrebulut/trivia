using System.Collections.Generic;
using System.Linq;
using System;
using Zenject;
using UnityEngine;

namespace Trivia
{
    public class QuestionManager : IInitializable, IDisposable
    {
        public Settings QuestionSettings { get; private set; }

        public QuestionSO CurrentQuestion { get; private set; }
        public string CurrentCategory { get; private set; }
        public List<string> Categories { get; private set; }

        public QuestionManager(Settings _settings)
        {
            QuestionSettings = _settings;

            Categories = new List<string>();
            QuestionSettings.configurationSO.categories.ForEach(x => Categories.Add(x.categoryName));
        }

        #region Subcsribe - Unsubscribe Events
        public void Initialize()
        {
            EventManager.LoadNewQuestionEvent += LoadNewQuestion;
        }

        public void Dispose()
        {
            EventManager.LoadNewQuestionEvent -= LoadNewQuestion;
        }
        #endregion

        private void LoadNewQuestion(string _categoryName)
        {
            QuestionSO question = GetQuestionForCategory(_categoryName);

            if (question != null)
            {
                CurrentQuestion = question;

                EventManager.NewQuestionLoaded(CurrentQuestion);
            }
        }

        private QuestionSO GetQuestionForCategory(string _categoryName)
        {
            if (Categories.Contains(_categoryName))
            {
                CurrentCategory = _categoryName;
            }

            List<QuestionSO> questionsInCategory = QuestionSettings.configurationSO.categories
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

            [Header("Points")]
            public int correctAnswerPoint;
            public int incorrectAnswerPoint;
            public int timeOutPoint;
        }

    }
}