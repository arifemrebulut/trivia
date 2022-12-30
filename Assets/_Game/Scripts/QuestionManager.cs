using System.Collections.Generic;
using System.Linq;
using System;
using Zenject;
using UnityEngine;

namespace Trivia
{
    public class QuestionManager : IInitializable, IDisposable
    {
        private Settings settings;

        public QuestionSO CurrentQuestion { get; private set; }
        public string CurrentCategory { get; private set; }
        public List<string> Categories { get; private set; }

        public QuestionManager(Settings _settings)
        {
            settings = _settings;

            Categories = new List<string>();
            settings.configurationSO.categories.ForEach(x => Categories.Add(x.categoryName));
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