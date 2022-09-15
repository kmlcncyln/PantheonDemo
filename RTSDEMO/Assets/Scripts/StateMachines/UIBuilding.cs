using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
    
    [CreateAssetMenu(fileName = "UIBuilding", menuName = "States/Building/UIBuilding")]
    public class UIBuilding : BuildingBehaviourState
    {
        public override void LeftClicked(MonoBehaviour behaviour, bool multi = false)
        {
            return;
        }

        public override void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false)
        {
            BuildingBehaviour building = behaviour as BuildingBehaviour;
            
            building.state = CreateInstance<HoveringBuildingState>();
        }

        public override void OnUpdate(MonoBehaviour behaviour)
        {
            return;
        }

        public override void RightClicked(MonoBehaviour behaviour, bool multi = false)
        {
            return;
        }
    }
}
