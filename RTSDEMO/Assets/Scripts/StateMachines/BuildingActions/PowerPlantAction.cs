using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;

namespace KC
{
    [CreateAssetMenu(fileName = "PowerPlantAction", menuName = "Buildings/Actions/PowerPlantAction")]
    public class PowerPlantAction : BuildingAction
    {

        public int amount;

        public override void OnClicked(BuildingBehaviour building)
        {
            

            return;
        }

        public override void OnUpdate(BuildingBehaviour building)
        {
            // Debug.Log($"Producingx{amount}...");
            // TODO PRODUCTION CODES HERE
        }

        public override void OnRightClicked(BuildingBehaviour building)
        {
            return;
        }

    }
}
