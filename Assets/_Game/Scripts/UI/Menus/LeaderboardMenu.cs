using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SimpleJSON;

namespace Trivia
{
    public class LeaderboardMenu : MenuBase
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Transform listParent;
        [SerializeField] private GameObject listItemPrefab;

        [Header("Page Navigator")]
        [SerializeField] private GameObject[] navigationDots;
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;

        private int currentPage = 0;

        private List<LeaderboardListItemUi> listItemUis;

        private LeaderboardApiClient leaderboardApi;

        [Inject]
        private void Constract(LeaderboardApiClient _leaderboardApi)
        {
            leaderboardApi = _leaderboardApi;
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(() => uiManager.GoToPreviousMenu());
            leftButton.onClick.AddListener(() => GoPreviousPage());
            rightButton.onClick.AddListener(() => GoNextPage());

            PopulateList();
        }

        private async void PopulateList()
        {
            LeaderboardPageData leaderboardPageData = await leaderboardApi.GetLeaderboardPageDataAsync(currentPage);

            List<LeaderboardListItemData> listItemDatas = leaderboardPageData.data;

            listItemUis = new List<LeaderboardListItemUi>();

            foreach (var item in listItemDatas)
            {
                LeaderboardListItemUi listItemUi =
                    Instantiate(listItemPrefab, listParent).GetComponent<LeaderboardListItemUi>();

                listItemUi.UpdateUI(item.rank.ToString(), item.nickname, item.score.ToString());

                listItemUis.Add(listItemUi);
            }

            HighlightDot();
        }

        private async void UpdateList()
        {
            LeaderboardPageData leaderboardPageData = await leaderboardApi.GetLeaderboardPageDataAsync(currentPage);

            List<LeaderboardListItemData> listItemDatas = leaderboardPageData.data;

            for (int i = 0; i < listItemDatas.Count; i++)
            {
                LeaderboardListItemData data = listItemDatas[i];

                listItemUis[i].UpdateUI(data.rank.ToString(), data.nickname, data.score.ToString());
            }

            HighlightDot();
        }

        private void GoPreviousPage()
        {
            if (currentPage - 1 >= 0)
            {
                currentPage--;

                UpdateList();
            }

        }

        private void GoNextPage()
        {
            if (currentPage + 1 <= 1)
            {
                currentPage++;

                UpdateList();
            }
        }

        private void HighlightDot()
        {

            for (int i = 0; i < navigationDots.Length; i++)
            {
                GameObject highlight = navigationDots[i].transform.Find("Highlight").gameObject;

                if (i == currentPage)
                {
                    highlight.SetActive(true);
                }
                else
                {
                    highlight.SetActive(false);
                }
            }
        }
    }
}