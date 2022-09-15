using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC.RTS.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName ="New Building/Basic")]
    public class BasicBuilding : ScriptableObject
    {
        public enum buildingType
        {
            Barracks,
            PowerPlants
        }

        [Space(15)]
        [Header("Building Settings")]

        public buildingType type;
        public new string name;
        public GameObject buildingPrefab;
        public BuildingActions.BuildingUnits Units;

        [Space(15)]
        [Header("Building Base Stats")]
        [Space(40)]
        public BuildingStatTypes.Base baseStats;
    }
}

