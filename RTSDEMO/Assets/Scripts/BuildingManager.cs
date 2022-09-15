using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KC.RTS.Buildings;

namespace KC
{
    public class BuildingManager : MonoBehaviour
    {

        private void Start()
        {
            ShowBuildings();
        }

        public List<Building> buildables;

        public GameObject buildingUIPrefab;

        public GameObject buildingPrefab;

        public Transform buildingUIParent;

        public Transform buildingParent;


        [ContextMenu("Show Buildings")]
        public void ShowBuildings()
        {

            foreach(Transform tr in buildingUIParent.GetComponentsInChildren<Transform>())
                if(tr != buildingUIParent)
                    Destroy(tr.gameObject);

            int order = 0;
            foreach(Building building in buildables)
            {
                GameObject go = Instantiate(buildingUIPrefab, buildingUIParent);

                go.transform.localPosition = Vector3.zero + (new Vector3(1, 0, 0) * order);

                BuildingUIBehaviour buildingUIBehaviour = go.GetComponent<BuildingUIBehaviour>();
                buildingUIBehaviour.building = building;
                buildingUIBehaviour.manager = this;
                buildingUIBehaviour.Draw();
                buildingUIBehaviour.OnBuildingClicked = ()=> 
                {
                    buildingUIBehaviour.Build();
                    // Build(building);
                };


                order++;
            }

        }

        public void Build(Building building)
        {
            GameObject go = Instantiate(buildingPrefab, buildingParent);

            BuildingBehaviour buildingBehaviour = go.GetComponent<BuildingBehaviour>();
            buildingBehaviour.name = building.buildingname;
            buildingBehaviour.building = building;
            buildingBehaviour.state = ScriptableObject.CreateInstance<HoveringBuildingState>();

            buildingBehaviour.Draw();
        }


    }
}
