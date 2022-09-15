using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KC
{
    public class BuiltState : BuildingBehaviourState
    {
        public override void LeftClicked(MonoBehaviour behaviour, bool multi = false)
        {
            return;
        }

        public override void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false)
        {
            BuildingBehaviour buildingBehaviour = behaviour as BuildingBehaviour;
            
            buildingBehaviour.Select(multi);

            buildingBehaviour.building.buildingAction.OnClicked(buildingBehaviour);


        }

        public override void OnUpdate(MonoBehaviour behaviour)
        {
            BuildingBehaviour buildingBehaviour = behaviour as BuildingBehaviour;

            buildingBehaviour.building.buildingAction.OnUpdate(buildingBehaviour);
        }

        public override void RightClicked(MonoBehaviour behaviour, bool multi = false)
        {
            BuildingBehaviour buildingBehaviour = behaviour as BuildingBehaviour;

            buildingBehaviour.building.buildingAction.OnRightClicked(buildingBehaviour);
        }
    }
}
