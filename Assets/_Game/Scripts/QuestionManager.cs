using System.Collections.Generic;
using System.Linq;
using System;

namespace Trivia
{
    public class QuestionManager
    {
        private Settings settings;

        public QuestionManager(Settings _settings)
        {
            settings = _settings;

            string randomCategoryName = settings.configurationSO.categories[UnityEngine.Random.Range(0, 4)].categoryName;

            LoadNextQuestion(randomCategoryName);
        }

        public void LoadNextQuestion(string _categoryName)
        {
            QuestionSO question = GetQuestionForCategory(_categoryName);

            if (question != null)
            {
                settings.questionUI.UpdataUI(question);
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