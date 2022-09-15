using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    
    public enum Group
    {
        None,
        Unit,
        Building
    }

    public void Select(bool multi = false);

    public void Deselect();
    
    public void OnSelect(bool multi = false);

    public void OnDeselect();

    public bool IsSelected();

    public Group GetGroup();

    public bool MultiSelectable();

}
