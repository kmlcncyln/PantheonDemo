using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC.RTS.Buildings
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Buildings/New Building")]
    public class Building : ScriptableObject
    {

        public Sprite view;

        public string buildingname;

        public int cost;

        public Vector2 size;

        public BuildingAction buildingAction;

    }
}
