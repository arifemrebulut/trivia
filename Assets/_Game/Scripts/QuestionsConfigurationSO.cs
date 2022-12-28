using System.Collections.Generic;
using UnityEngine;
using System;

namespace Trivia
{
    [CreateAssetMenu(fileName = "NewQuestionsConfiguration", menuName = "ScriptableObjects/QuestionsConfiguration")]
    public class QuestionsConfigurationSO : ScriptableObject
    {
        public List<Category> categories = new List<Category>();

        [Serializable]
        public class Category
        {
            public string categoryName;
            public List<QuestionSO> questions;
        }
    }
}