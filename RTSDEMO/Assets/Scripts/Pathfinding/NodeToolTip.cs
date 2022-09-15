using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeToolTip : MonoBehaviour
{

    public Pathfinding.PathNode node;

    public TextMeshProUGUI title;
    public TextMeshProUGUI X,Y;
    public TextMeshProUGUI key;
    public TextMeshProUGUI speed;
    public Toggle movable;

    public Sprite tick, cross;

    public Animator animator;

    public void Init()
    {

        X.text = "X: " + node.position.x.ToString();
        Y.text = "Y: " + node.position.y.ToString();
        key.text = node.key.ToString(); ;

        if (node.roadRuleTile != null)
        {

            //title.text = node.roadRuleTile.theme.ToString();

            //if (node.roadRuleTile.settlementTile) 
            //{

            //    if(node.settlementObject != null)
            //    {
            //        title.text = node.settlementObject.settlement.name;
            //    }

            //}

            speed.text = node.roadRuleTile.speed.ToString();

            movable.isOn = true;
            Image image = movable.graphic as Image;
            image.color = node.roadRuleTile.movable ? Color.green : Color.red;
            image.sprite = node.roadRuleTile.movable ? tick : cross;

        }

        animator.SetBool("Open", true);

    }

    public void CloseToolTip()
    {

        animator.SetBool("Open", false);

    }

    public void DestroyToolTip()
    {

        Destroy(gameObject);

    }


}
