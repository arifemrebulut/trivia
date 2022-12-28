using UnityEngine;
using UnityEditor;
using SimpleJSON;

namespace Trivia
{
    public class JsonToQuestion : MonoBehaviour
    {
        [MenuItem("Tools/JsonToQuestionSO")]
        static void JsonToQuestionSO()
        {
            Debug.Log("Generation Started...");

            string json = Resources.Load<TextAsset>("questions").text;
            
            JSONNode node = JSON.Parse(json);

            JSONArray questions = node["questions"].AsArray;

            for (int i = 0; i < questions.Count; i++)
            {
                QuestionSO questionSO = ScriptableObject.CreateInstance<QuestionSO>();
                string fileName = "Question_" + i + ".asset";

                JSONObject questionObject = questions[i].AsObject;

                questionSO.questionText = questionObject["question"];

                JSONArray answersArray = questionObject["choices"].AsArray;

                questionSO.answer0 = answersArray[0];
                questionSO.answer1 = answersArray[1];
                questionSO.answer2 = answersArray[2];
                questionSO.answer3 = answersArray[3];

                questionSO.correctAnswer = questionObject["answer"].Value;

                string path = "Assets/_Game/ScriptableObjects/" + fileName;

                AssetDatabase.CreateAsset(questionSO, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = questionSO;

                Debug.Log(fileName + " generated.");
            }

            Debug.Log("Generation Finished");
            Debug.Log("File's path : " + "Assets/_Game/ScriptableObjects");
        }
    }
}
