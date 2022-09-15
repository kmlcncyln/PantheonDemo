using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedState", menuName = "States/SelectedState")]
public class SelectedUnit : BehaviourState
{
    public override void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false)
    {
        UnitBehaviour unit = behaviour as UnitBehaviour;

        if(unit == null)
            return;

        // unit.Deselect();
    }

    public override void RightClicked(MonoBehaviour behaviour, bool multi = false)
    {

        UnitBehaviour unit = behaviour as UnitBehaviour;

        if(unit == null)
            return;

        unit.StartMove(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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
