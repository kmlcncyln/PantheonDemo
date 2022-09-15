using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC.RTS.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "New Unit")]
    public class Unit : ScriptableObject
    {
        public enum unitType
        {
            Soldier
        };

        [Space(15)]
        [Header("Unit Settings")]

        public unitType type;
        public string unitName;

        public Sprite view;

        public GameObject unitPrefab;

        [Space(15)]
        [Header("Unit Base Stats")]
        [Space(40)]

        public UnitStatTypes.Base baseStats;

    }
}

