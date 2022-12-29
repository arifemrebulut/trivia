using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using Zenject;
using TMPro;

namespace Trivia
{
    public class WheelMenu : MenuBase
    {
        [Header("Wheel Spin Settings")]
        [SerializeField] private Transform wheel;
        [SerializeField] private float spinDuration;
        [SerializeField] private Ease spinEase;
        [SerializeField] private int spinAmount;

        [Header("Buttons")]
        [SerializeField] private Button spinButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button backButton;

        [SerializeField] private TextMeshProUGUI categoryTMP;

        private QuestionManager questionManager;
        private List<string> categories;

        [Inject]
        private void Constract(QuestionManager _questionManager)
        {
            questionManager = _questionManager;
        }

        private void Start()
        {
            spinButton.onClick.AddListener(() => Spin());
            playButton.onClick.AddListener(() => uiManager.SwitchMenu<GameMenu>(false));
            backButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());

            categories = questionManager.Categories;

            categoryTMP.gameObject.transform.localScale = Vector3.one;
            categoryTMP.text = "Tap on the Spin!";
        }

        private void Spin()
        {

            categoryTMP.gameObject.transform.localScale = Vector3.zero;
            categoryTMP.text = string.Empty;

            spinButton.interactable = false;

            float randomAngle = Random.Range(0, 360);

            string category = DetectLandedCategory(randomAngle);

            float rotateAngle = (360 * spinAmount) + randomAngle;

            wheel.DOLocalRotate(new Vector3(0f, 0f, rotateAngle), spinDuration, RotateMode.FastBeyond360)
                .SetEase(spinEase)
                .OnComplete(() => OnWheelFinishedSpinning(category));
        }

        private void OnWheelFinishedSpinning(string category)
        {
            EventManager.LoadNewQuestion(category);

            Sequence sequence = DOTween.Sequence();

            sequence.Append(playButton.gameObject.transform.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack));

            sequence.Join(categoryTMP.gameObject.transform.DOScale(Vector3.one, 0.5f)
                .OnStart(() =>
                {
                    categoryTMP.text = Utils.GenerateCategoryDisplayName(category);
                })
                .SetEase(Ease.OutBack));
        }

        private string DetectLandedCategory(float angle)
        {
            float anglePerCagetory = 360f / categories.Count;

            return categories[(int)(angle / anglePerCagetory)];
        }

        private void OnDisable()
        {
            Reset();
        }

        private void Reset()
        {
            spinButton.interactable = true;

            playButton.gameObject.transform.localScale = Vector3.zero;
            categoryTMP.gameObject.transform.localScale = Vector3.zero;
            categoryTMP.text = "Tap on the Spin!";

            wheel.localEulerAngles = Vector3.zero;
        }
    }
}