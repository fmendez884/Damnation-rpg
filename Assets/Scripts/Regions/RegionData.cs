using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData : MonoBehaviour
{
        public string regionName;
        public int maxAmountEnemys = 4;
        public string BattleScene;
        public List<GameObject> possibleEnemys = new List<GameObject>();
}
