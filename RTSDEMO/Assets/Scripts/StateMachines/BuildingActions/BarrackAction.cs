using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;
using KC.RTS.Units;

namespace KC
{
    [CreateAssetMenu(fileName = "BarrackAction", menuName = "Buildings/Actions/BarrackAction")]
    public class BarrackAction : BuildingAction
    {

        public List<Unit> unitList;

        public override void OnClicked(BuildingBehaviour building)
        {



        }

        public override void OnRightClicked(BuildingBehaviour building)
        {
            Grid grid = FindObjectOfType<Grid>();

            Vector3Int gridPos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            building.rallyPoint = gridPos;

            // Update visually
        }

        public override void OnUpdate(BuildingBehaviour building)
        {
            return;
        }

        public void ProductUnit(BuildingBehaviour building, Unit unit)
        {
            
            GameObject unitObject = FindObjectOfType<UnitSpawner>().SpawnUnit(unit);

            unitObject.transform.position = building.transform.position;

            unitObject.GetComponent<UnitBehaviour>().StartMove(building.rallyPoint);


        }
    }
}
