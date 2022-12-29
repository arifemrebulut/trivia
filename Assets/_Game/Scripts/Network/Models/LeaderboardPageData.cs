using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia
{
    public class LeaderboardPageData
    {
        public int page { get; set; }
        public bool is_last { get; set; }
        public List<LeaderboardListItemData> data { get; set; }
    }
}