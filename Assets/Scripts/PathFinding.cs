using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//astar에서 만들어진 Node들을 통해서 Manager에서 길을 찾아달라고 할 때 찾아주는 곳 입니다.

public class PathFinding : MonoBehaviour
{
    [SerializeField] private Astar astar;

    private void Awake()
    {
        astar = GetComponent<Astar>();
    }

    public Stack<Vector3> PathFind(Vector3 npcPosition, Vector3 targetPosition, bool isDungeon, string dungeonName)
    {
        Node npcNode = astar.GetNodeByPosition(npcPosition, isDungeon, dungeonName);
        Node targetNode = astar.GetNodeByPosition(targetPosition, isDungeon, dungeonName);

        List<Node> openNode = new List<Node>();
        HashSet<Node> closeNode = new HashSet<Node>();

        openNode.Add(npcNode);

        while (openNode.Count != 0)
        {
            Node currentNode = openNode[0];

            for (int i = 1; i < openNode.Count; i++)
            {
                if (currentNode.FCost > openNode[i].FCost || (currentNode.FCost == openNode[i].FCost && currentNode.hCost > openNode[i].hCost))
                {
                    currentNode = openNode[i];
                }
            }

            openNode.Remove(currentNode);
            closeNode.Add(currentNode);

            if (currentNode == targetNode)
            {
                return GetPath(npcNode, targetNode, isDungeon);
            }

            List<Node> nodeList = astar.GetAroundNode(currentNode, isDungeon, dungeonName);

            foreach (Node aroundNode in nodeList)
            { 
                if (closeNode.Contains(aroundNode) || (!aroundNode.isWalkable && aroundNode.buildingType != BuildingType.Shop.ToString()))
                {
                    continue;
                }

                int newGCost = currentNode.gCost + GetDistance(currentNode, aroundNode);

                if (openNode.Contains(aroundNode))
                {
                    if (aroundNode.gCost > newGCost)
                    {
                        aroundNode.gCost = newGCost;
                        aroundNode.parentNode = currentNode;
                    }
                }
                else
                {
                    aroundNode.gCost = newGCost;
                    aroundNode.hCost = GetDistance(aroundNode, targetNode);
                    aroundNode.parentNode = currentNode;

                    openNode.Add(aroundNode);
                }
            }
        }

        return null;
    }

    List<Vector3> testPathList;

    private Stack<Vector3> GetPath(Node startNode, Node targetNode, bool isDungeon)
    {
        testPathList = new List<Vector3>();

        Stack<Vector3> returnNodePositionStack = new Stack<Vector3>();

        if (!isDungeon)
        {
            returnNodePositionStack.Push(targetNode.nodePosition); // 도착지점
        }

        testPathList.Add(targetNode.nodePosition);
        
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            returnNodePositionStack.Push(currentNode.nodePosition);
            testPathList.Add(currentNode.nodePosition);

            currentNode = currentNode.parentNode;
        }

        return returnNodePositionStack;
    }
    
    //대각선 이동 안함
    private int GetDistance(Node firstNode, Node secondNode)
    {
        int xDistance = Mathf.Abs(firstNode.xPosition - secondNode.xPosition);
        int yDistance = Mathf.Abs(firstNode.yPosition - secondNode.yPosition);

        //if (xDistance < yDistance)
        //{
        //    return xDistance * 14 + (yDistance - xDistance) * 10;
        //}
        //else
        //{
        //    return yDistance * 14 + (xDistance - yDistance) * 10;
        //}

        return xDistance * 10 + yDistance * 10;
    }
}
