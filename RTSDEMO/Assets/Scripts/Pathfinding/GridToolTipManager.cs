using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridToolTipManager : MonoBehaviour
{

    public float toolTipTime = 5;
    private Vector2Int previousNodePosition;
    public float timer = 5;
    private bool toolTipOpened = false;

    public MapManager mapManager;

    public Pathfinding.PathNode node;
    private List<Pathfinding.PathNode> neighbourNodes;

    public Transform nodeToolTipArea;
    public Tilemap nodeTileMap;
    public GameObject nodeToolTipPrefab;
    public RuleTile nodeTile;

    private NodeToolTip openedToolTip;

    public Texture2D settlementCursor;
    public Texture2D defaultCursor;

    public void Start()
    {
        timer = toolTipTime;

        neighbourNodes = new List<Pathfinding.PathNode>();

        Vector3 mousePosition = Vector3.zero;
        node = mapManager.pathfinding.GetGrid().GetValue(mousePosition);
        previousNodePosition = node.position;

    }

    public void Update()
    {
        

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        node = mapManager.pathfinding.GetGrid().GetValue(mousePosition);

        if (node == null)
            return;

        //if (!GameSystems.mouseFree)
        //    return;

        if (node.position != previousNodePosition && !neighbourNodes.Contains(node))
        {

            nodeTileMap.ClearAllTiles();

            timer = toolTipTime;
            previousNodePosition = node.position;

            //HighlightNode(node);

            if (toolTipOpened)
            {
                CloseToolTip();
            }



        }
        else
        {
            if(timer > 0)
                timer -= Time.deltaTime;

            if (timer <= 0)
            {

                if (!toolTipOpened)
                    ToolTip();

            }


        }

    }


    //public void HighlightNode(Pathfinding.PathNode node)
    //{

    //    neighbourNodes.Clear();

    //    if (node.settlementObject != null)
    //    {

    //        List<Pathfinding.PathNode> checkNodes = new List<Pathfinding.PathNode>();
    //        List<Pathfinding.PathNode> nodes = node.GetAllSettlementNodes(ref checkNodes);

    //        neighbourNodes.AddRange(nodes);

    //        foreach (Pathfinding.PathNode currentNode in nodes)
    //            nodeTileMap.SetTile((Vector3Int)currentNode.position, nodeTile);

    //        SettlementObject targetSettlement = nodes[0].settlementObject;
    //        targetSettlement.Select();

    //    }
    //    else
    //    {
    //        if(SettlementObject.selectedObject != null)
    //            SettlementObject.selectedObject.Select(false);

    //        nodeTileMap.SetTile((Vector3Int)node.position, nodeTile);
    //    }


    //}

    public void ToolTip()
    {

        if (openedToolTip != null)
            openedToolTip.CloseToolTip();


        NodeToolTip nodeToolTip = Instantiate(nodeToolTipPrefab, nodeToolTipArea).GetComponent<NodeToolTip>();

        nodeToolTip.transform.position = FindObjectOfType<Grid>().CellToWorld((Vector3Int)node.position) + new Vector3(.5f, .5f, 0);

        nodeToolTip.node = node;
        nodeToolTip.Init();

        nodeTileMap.SetTile((Vector3Int)node.position, nodeTile);

        openedToolTip = nodeToolTip;
        toolTipOpened = true;

    }

    public void CloseToolTip()
    {

        openedToolTip.CloseToolTip();

        toolTipOpened = false;
    }


}
