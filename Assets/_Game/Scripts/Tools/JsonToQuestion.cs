using UnityEngine;
using UnityEditor;
using SimpleJSON;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Trivia
{
    public static class JsonToQuestion
    {
        [MenuItem("Tools/JsonToQuestionSO")]
        static void JsonToQuestionSO()
        {
            Debug.Log("Generation Started...");

            HashSet<string> categoryNamesSet = new HashSet<string>();

            #region QuestionSO Generation
            // Read json and parse it.
            //string json = Resources.Load<TextAsset>("questions").text;
            var json = EditorUtility.OpenFilePanel("Select Json File", "", "json");

            using (StreamReader reader = new StreamReader(json))
            {
                json = reader.ReadToEnd();
            }

            JSONNode node = JSON.Parse(json);

            JSONArray questions = node["questions"].AsArray;

            List<QuestionSO> generatedQuestions = new List<QuestionSO>();

            for (int i = 0; i < questions.Count; i++)
            {
                // Create new ScriptableObject instance for every question in json.
                QuestionSO questionSO = ScriptableObject.CreateInstance<QuestionSO>();
                string fileName = "Question_" + i + ".asset";

                JSONObject questionObject = questions[i].AsObject;

                // Assign values from question to ScriptableObject's fields.
                questionSO.questionText = questionObject["question"];

                JSONArray answersArray = questionObject["choices"].AsArray;

                for (int j = 0; j < answersArray.Count; j++)
                {
                    questionSO.answers[j] = answersArray[j];
                }

                questionSO.correctAnswer = questionObject["answer"];
                questionSO.category = questionObject["category"];

                categoryNamesSet.Add(questionObject["category"]);
                generatedQuestions.Add(questionSO);

                string path = "Assets/_Game/ScriptableObjects/Questions/" + fileName;

                // Create ScriptableObject asset.
                AssetDatabase.CreateAsset(questionSO, path);
                #endregion

                Debug.Log(fileName + " generated.");
            }

            #region Populate Configuration File
            string configurationFileName = "QuestionsConfiguration";

            QuestionsConfigurationSO configurationFile = Resources.Load<QuestionsConfigurationSO>(configurationFileName);

            if (configurationFile == null)
            {
                configurationFile = ScriptableObject.CreateInstance<QuestionsConfigurationSO>();

                string fileName = configurationFileName + ".asset";

                string path = "Assets/Resources/" + fileName;

                AssetDatabase.CreateAsset(configurationFile, path);

                Debug.Log("QuestionsConfiguraiton file created at : " + "Assets/Resources");
            }

            List<QuestionsConfigurationSO.Category> categories = new List<QuestionsConfigurationSO.Category>();

            foreach (var categoryName in categoryNamesSet)
            {
                QuestionsConfigurationSO.Category category = new QuestionsConfigurationSO.Category();
                category.categoryName = categoryName;
                category.questions = generatedQuestions.FindAll(question => question.category == categoryName);

                categories.Add(category);
            }

            Debug.Log(configurationFile);

            configurationFile.categories.Clear();
            configurationFile.categories = categories;

            EditorUtility.SetDirty(configurationFile);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = configurationFile;
            #endregion

            Debug.Log("Generation Finished");
            Debug.Log("File's path : " + "Assets/_Game/ScriptableObjects/Questions");
        }
    }
}