using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;

namespace KC
{
    public class BuildingBehaviour : MonoBehaviour, ISelectable
    {
        

        public Building building;

        public BuildingBehaviourState state;

        public SpriteRenderer spriteRenderer;

        public Vector3Int rallyPoint;

        private bool isSelected = false;

        [SerializeField]
        private ISelectable.Group Group;

        [SerializeField]
        private bool multiSelectable = false;

        public void OnMouseEnter()
        {
            if(!SelectManager.Instance.MouseOverObjects.Contains(this))
                SelectManager.Instance.MouseOverObjects.Add(this);
        }
        
        public void OnMouseExit()
        {
            SelectManager.Instance.MouseOverObjects.Remove(this);
        }

        public void OnMouseDown()
        {
            state.LeftClickedOnMe(this);
        }

        public void Update()
        {
            state.OnUpdate(this);

            if(Input.GetMouseButtonDown(0))
                state.LeftClicked(this);

            if(Input.GetMouseButtonDown(1))
                state.RightClicked(this);
            
        }

        public void Select(bool multi = false)
        {
            if(!SelectManager.Instance.selecteds.Contains(this))
                OnSelect(multi);
        }

        public void Deselect()
        {
            OnDeselect();
        }

        public void OnSelect(bool multi = false)
        {
            isSelected = true;
            // highlightObject.SetActive(isSelected);
            // currentState = ScriptableObject.CreateInstance<SelectedUnit>();

            FindObjectOfType<InformationManager>().PrepareUI(this);

            SelectManager.Instance.Select(this, multi);
            // PrepareUI();
            // Debug.Log("Se�ildi");
        }

        public void OnDeselect()
        {
            isSelected = false;
            // highlightObject.SetActive(isSelected);
            // currentState = ScriptableObject.CreateInstance<UnselectedUnit>();

            SelectManager.Instance.Deselect(this);
        }

        public bool IsSelected()
        {
            return isSelected;
        }

        public ISelectable.Group GetGroup()
        {
            return Group;
        }

        public bool MultiSelectable()
        {
            return multiSelectable;
        }

        public void Draw()
        {
            spriteRenderer.sprite = building.view;
        }

    }
}
