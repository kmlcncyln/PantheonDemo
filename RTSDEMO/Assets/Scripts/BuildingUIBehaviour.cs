using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using KC.RTS.Buildings;

namespace KC
{
    public class BuildingUIBehaviour : MonoBehaviour, IPointerDownHandler
    {

        public Building building;
        
        public Image image;

        public BuildingManager manager;

        public BuildingClicked OnBuildingClicked;
        public delegate void BuildingClicked();

        public void Draw()
        {
            image.sprite = building.view;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnBuildingClicked?.Invoke();
        }

        [ContextMenu("Build")]
        public void Build()
        {
            manager.Build(building);
        }

    }
}
