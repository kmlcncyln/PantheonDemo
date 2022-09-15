using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    
    public static SelectManager Instance;

    [SerializeField]
    public List<Object> MouseOverObjects;

    public List<Object> selecteds = new List<Object>();

    public List<ISelectable> SelectedObjects
    {
        get
        {
            return selecteds.OfType<ISelectable>().ToList();
        }
    }



    public void Awake() 
    {

        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && MouseOverObjects.Count <= 0)
        {
            DeselectAll();
        }
    }
    
    public void Select(ISelectable selectable, bool multi = false)
    {
        ISelectable.Group selectedGroup = ISelectable.Group.None;
        if(selecteds.Count >= 1)
            selectedGroup = SelectedObjects[0].GetGroup();

        if(multi)
        {

            if(selectedGroup == selectable.GetGroup())
            {
                selecteds.Add(selectable as Object);
            }
            else
            {
                DeselectAll();
                selecteds.Add(selectable as Object);
            }

        }
        else
        {
            DeselectAll();
            selecteds.Add(selectable as Object);
        }

    }

    public void Deselect(ISelectable selectable)
    {
        
        selecteds.Remove(selectable as Object);
        
    }

    public void DeselectAll()
    {
        List<ISelectable> tempList = new List<ISelectable>();
        tempList.AddRange(SelectedObjects);
        tempList.ForEach(x => x.Deselect());
        selecteds.Clear();
    }


}
