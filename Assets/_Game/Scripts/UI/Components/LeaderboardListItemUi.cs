using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Trivia
{
    public class LeaderboardListItemUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rankTMP;
        [SerializeField] private TextMeshProUGUI nicknameTMP;
        [SerializeField] private TextMeshProUGUI scoreTMP;

        public void UpdateUI(string rank, string nickname, string score)
        {
            rankTMP.text = rank;
            nicknameTMP.text = nickname;
            scoreTMP.text = score;
        }
    }
}