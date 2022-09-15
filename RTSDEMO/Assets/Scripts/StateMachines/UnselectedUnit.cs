using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnselectedState", menuName = "States/UnselectedState")]
public class UnselectedUnit : BehaviourState
{
    public override void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false)
    {

        UnitBehaviour unit = behaviour as UnitBehaviour;

        if(unit == null)
            return;

        unit.Select(multi);

    }

    public override void RightClicked(MonoBehaviour behaviour, bool multi = false)
    {
        return;
    }

    public override void OnUpdate(MonoBehaviour behaviour)
    {

        UnitBehaviour unit = behaviour as UnitBehaviour;

        if(unit == null)
            return;

        unit.Move();
    }

    public override void LeftClicked(MonoBehaviour behaviour, bool multi = false)
    {
        return;
    }
}
