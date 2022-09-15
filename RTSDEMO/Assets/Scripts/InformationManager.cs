using KC.RTS.Buildings;
using KC.RTS.Units;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KC
{
    public class InformationManager : MonoBehaviour
    {

        public Image image;

        public TextMeshProUGUI text;

        public TextMeshProUGUI unitText;

        public Transform unitParent;

        public GameObject unitPrefab;


        public void PrepareUI(BuildingBehaviour building)
        {

            foreach (Transform tr in unitParent.GetComponentsInChildren<Transform>())
                if (tr != unitParent)
                    Destroy(tr.gameObject);

            image.sprite = building.building.view;

            text.text = building.building.buildingname;

            BarrackAction barrackAction = building.building.buildingAction as BarrackAction;

            unitText.gameObject.SetActive(barrackAction != null);

            if (barrackAction == null)
                return;

            foreach(Unit unit in barrackAction.unitList)
            {
                GameObject unitObject = Instantiate(unitPrefab, unitParent);

                unitObject.GetComponentInChildren<Image>().sprite = unit.view;

                // unitObject.GetComponent<TextMeshProUGUI>().text = unit.unitName;

                unitObject.GetComponentInChildren<Button>().onClick.AddListener(()=>
                {
                    barrackAction.ProductUnit(building, unit);
                });
            }


        }

    }
}
