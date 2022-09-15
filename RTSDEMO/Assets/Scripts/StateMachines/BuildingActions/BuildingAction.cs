using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;

namespace KC
{
    public abstract class BuildingAction : ScriptableObject
    {
        
        
        public abstract void OnClicked(BuildingBehaviour building);

        public abstract void OnUpdate(BuildingBehaviour building);

        public abstract void OnRightClicked(BuildingBehaviour building);

    }
}
