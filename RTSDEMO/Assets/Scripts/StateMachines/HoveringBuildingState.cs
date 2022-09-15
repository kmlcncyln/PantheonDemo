using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;
using Pathfinding;

namespace KC
{
    public class HoveringBuildingState : BuildingBehaviourState
    {

        
        public override void LeftClicked(MonoBehaviour behaviour, bool multi = false)
        {
            LeftClickedOnMe(behaviour, multi);
        }

        public override void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false)
        {
            
            BuildingBehaviour building = behaviour as BuildingBehaviour;

            Build(behaviour);

        }

        public override void OnUpdate(MonoBehaviour behaviour)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition.z = 0;

            Grid grid = FindObjectOfType<Grid>();

            Vector3Int gridPos = grid.WorldToCell(targetPosition);

            Building building = (behaviour as BuildingBehaviour).building;

            Vector3 gridedPosition = grid.CellToWorld(gridPos) + grid.cellSize / 2 + new Vector3(grid.cellSize.x * building.size.x - 1, grid.cellSize.y * building.size.y - 1) / 2;

            behaviour.transform.position = gridedPosition + new Vector3(0, 0, -.05f);
        }

        public override void RightClicked(MonoBehaviour behaviour, bool multi = false)
        {          
            Cancel(behaviour);        
        }

        public void Build(MonoBehaviour behaviour)
        {


            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            BuildingBehaviour building = behaviour as BuildingBehaviour;

            MapManager mapmanager = FindObjectOfType<MapManager>();
            List<PathNode> buildingArea = mapmanager.GetBuildingArea(targetPosition, building.building);

            foreach(PathNode node in buildingArea)
            {
                if (!node.buildable)
                {

                    Debug.Log("Cant build here");

                    // Cancel(behaviour);

                    return; 
                }
            }


            mapmanager.SetBuildingAreaUnable(targetPosition, building.building);

            // buildingArea.ForEach((node)=>
            // {
            //     node.buildable = false;
            //     node.speed = 0;
            // });

            building.state = CreateInstance<BuiltState>();

        }

        public void Cancel(MonoBehaviour behaviour)
        {
            Destroy(behaviour.gameObject);
        }

    }
}
