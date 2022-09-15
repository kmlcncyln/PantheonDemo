using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Units;

namespace KC
{
    public class UnitSpawner : MonoBehaviour
    {
        
        public Transform unitSpawnArea;
        
        public GameObject SpawnUnit(Unit unit)
        {
            return Instantiate(unit.unitPrefab, unitSpawnArea);
        }




    }
}
