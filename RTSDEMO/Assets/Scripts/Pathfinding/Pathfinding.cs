using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Pathfinding
{


    public class Pathfinding
    {

        private const int MOVE_STRAIGHT_COST = 10;
        private Grid<PathNode> grid;

        private List<PathNode> openList;
        private List<PathNode> closedList;


        public Pathfinding(int width, int height, float cellSize, Vector2Int lowerLimits, Vector3 originPosition, Tilemap roadTileMap)
        {

            this.grid = new Grid<PathNode>(width, height, cellSize, lowerLimits, originPosition, roadTileMap, (Grid<PathNode> g, Vector2Int pos, Tilemap rtm) => new PathNode(g, pos, rtm));

        }


        public List<PathNode> FindPath(Vector2Int startPos, Vector2Int endPos)
        {

            PathNode startNode = grid.GetValue(startPos);
            //Debug.Log(startNode.ToString());
            PathNode endNode = grid.GetValue(endPos);
            //Debug.Log(endPos.ToString());

            if (!endNode.Walkable())
                return null;

            // if (!startNode.Walkable())
            //     return null;

            openList = new List<PathNode> { startNode };
            closedList = new List<PathNode>();

            for(int x = grid.GetLowerLimits().x; x - grid.GetLowerLimits().x < grid.GetWidth(); ++x)
            {
                for(int y = grid.GetLowerLimits().y; y - grid.GetLowerLimits().y < grid.GetHeight(); ++y)
                {

                    PathNode pathNode = grid.GetValue(new Vector2Int(x, y));

                    //Debug.Log(pathNode.ToString());

                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.cameFromNode = null;

                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();


            while(openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostNode(openList);

                if(currentNode == endNode)
                {
                    // Reached final node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach(PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode)) continue;

                    if (neighbourNode == null) continue;

                    if (!neighbourNode.Walkable()) continue;

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if(tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }

                    }

                }


            }

            // Out of nodes on the OpenList

            return null;

        }

        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {

            return grid.GetNeighbourList(currentNode.position);

        }

        private List<PathNode> CalculatePath(PathNode endNode)
        {
            List<PathNode> path = new List<PathNode>();

            path.Add(endNode);

            PathNode currentNode = endNode;

            while(currentNode.cameFromNode != null)
            {

                path.Add(currentNode.cameFromNode);
                currentNode = currentNode.cameFromNode;

            }

            path.Reverse();

            return path;
        }

        private int CalculateDistanceCost(PathNode a, PathNode b)
        {

            int xDist = Mathf.Abs(a.position.x - b.position.x);
            int yDist = Mathf.Abs(a.position.y - b.position.y);
            int remaining = Mathf.Abs(xDist - yDist);

            return b.GetCost() * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostNode = pathNodeList[0];
            for(int i = 1; i < pathNodeList.Count; ++i)
            {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost)
                    lowestFCostNode = pathNodeList[i];
            }
            return lowestFCostNode;
        }

        public Grid<PathNode> GetGrid()
        {
            return grid;
        }

        public void SetGrid(Grid<PathNode> grid)
        {
            this.grid = grid;
        }

    }




}