using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using KC.RTS.Units;

public class UnitBehaviour : MonoBehaviour, ISelectable
{
    
    
    public Unit unit;

    public BehaviourState currentState;

    public GameObject highlightObject;

    private bool isSelected = false;

    [SerializeField]
    private ISelectable.Group Group;

    [SerializeField]
    private bool multiSelectable = false;

    private bool moveStarted
    {
        get
        {
            return Vector2.Distance(transform.position, TargetPosition) > 0;
        }
    }

    [SerializeField]
    private List<Vector2> path = new List<Vector2>();

    private Vector2 TargetPosition 
    {
        get
        {
            if(path.Count > 0)
            {
                return path[0];
            }
            else
            {
                return transform.position;
            }
        }
    }

    public void OnMouseDown()
    {
        Debug.Log("deneme");
        currentState.LeftClickedOnMe(this, Input.GetKey(KeyCode.LeftShift));
    }

    public void OnMouseEnter()
    {
        if(!SelectManager.Instance.MouseOverObjects.Contains(this))
            SelectManager.Instance.MouseOverObjects.Add(this);
    }
    
    public void OnMouseExit()
    {
        if(SelectManager.Instance.MouseOverObjects.Contains(this))
            SelectManager.Instance.MouseOverObjects.Remove(this);
    }

    public void Update()
    {
        currentState.OnUpdate(this);

        if(Input.GetMouseButtonDown(1))
            currentState.RightClicked(this);
        
    }

    public void Select(bool multi = false)
    {
        OnSelect(multi);
    }

    public void Deselect()
    {
        OnDeselect();
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void OnDeselect()
    {
        isSelected = false;
        highlightObject.SetActive(isSelected);
        currentState = ScriptableObject.CreateInstance<UnselectedUnit>();

        SelectManager.Instance.Deselect(this);
    }

    public void OnSelect(bool multi = false)
    {
        isSelected = true;
        highlightObject.SetActive(isSelected);
        currentState = ScriptableObject.CreateInstance<SelectedUnit>();

        SelectManager.Instance.Select(this, multi);
    }

    public ISelectable.Group GetGroup()
    {
        return Group;
    }

    public bool MultiSelectable()
    {
        return multiSelectable;
    }

    public void StartMove(Vector3 position)
    {
        // TODO Pathfinding!
        if(SelectManager.Instance.selecteds.Count > 1)
        {
            List<ISelectable> selecteds = SelectManager.Instance.SelectedObjects;
            path = FindObjectOfType<MapManager>().GetPath(transform.position, position + (new Vector3(1, 0 , 0) * selecteds.FindIndex(0, selecteds.Count - 1, (x)=> x.Equals(this))));
            //targetPosition = position + (new Vector3(1, 0 , 0) * selecteds.FindIndex(0, selecteds.Count - 1, (x)=> x.Equals(this)));
        }
        else
            path = FindObjectOfType<MapManager>().GetPath(transform.position, position);

        if (path == null)
            path = new List<Vector2>();
        else if (Vector2.Distance(transform.position, TargetPosition) <= 0)
            path.RemoveAt(0);
    }

    public void Move()
    {
        if(moveStarted)
        {

            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, unit.baseStats.speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, TargetPosition) <= 0)
                path.RemoveAt(0);

        }
    }

}
