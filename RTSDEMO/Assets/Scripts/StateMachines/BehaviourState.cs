using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourState : ScriptableObject
{
    
    public abstract void LeftClicked(MonoBehaviour behaviour, bool multi = false);

    public abstract void LeftClickedOnMe(MonoBehaviour behaviour, bool multi = false);

    public abstract void RightClicked(MonoBehaviour behaviour, bool multi = false);

    public abstract void OnUpdate(MonoBehaviour behaviour);

}
