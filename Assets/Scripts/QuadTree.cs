using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Quad
{
    public Vector2 TopLeft;
    public Vector2 BottomRight;

    public Node _Node;

    public Quad TopLeftTree;
    public Quad TopRightTree;
    public Quad BottomRightTree;
    public Quad BottomLeftTree;

    public Quad(Vector2 topL, Vector2 botR)
    {
        TopLeft = topL;
        BottomRight = botR;
    }

    public void Insert(Node node)
    {
        if (!InBoundary(node.Point)) return;

        // We are at a quad of unit area 
        // We cannot subdivide this quad further 
        if (Math.Abs(TopLeft.x - BottomRight.x) <= 0.25 &&
            Math.Abs(TopLeft.y - BottomRight.y) <= 0.25)
        {
            if (_Node == null)
                _Node = node;
            return;
        }


        if ((TopLeft.x + BottomRight.x) / 2 >= node.Point.x)
        {
            // Node is inside of the Top Left Tree
            if ((TopLeft.y + BottomRight.y) / 2 >= node.Point.y)
            {

                if (TopLeftTree == null)
                {
                    TopLeftTree = new Quad(
                        new Vector2(TopLeft.x, TopLeft.y),
                        new Vector2(
                            (TopLeft.x + BottomRight.x) / 2,
                            (TopLeft.y + BottomRight.y) / 2)
                        );
                }

                TopLeftTree.Insert(node);
            }
            // Node is inside of the Bottom Left Tree
            else
            {
                if (BottomLeftTree == null)
                {
                    BottomLeftTree = new Quad(
                        new Vector2(TopLeft.x, (TopLeft.y + BottomRight.y) / 2),
                        new Vector2((TopLeft.x + BottomRight.y) / 2, BottomRight.y)
                        );
                }
                BottomLeftTree.Insert(node);
            }
        }
        else
        {
            // Node is inside of Top Right Tree
            if ((TopLeft.y + BottomRight.y) / 2 >= node.Point.y)
            {
                if (TopRightTree == null)
                {
                    TopRightTree = new Quad(
                        new Vector2((TopLeft.x + BottomRight.x) / 2, TopLeft.y),
                        new Vector2(BottomRight.x, (TopLeft.y + BottomRight.y) / 2)
                        );
                }
                TopRightTree.Insert(node);
            }
            // Node is inside of Bottom Right Tree
            else
            {
                if (BottomRightTree == null)
                {
                    BottomRightTree = new Quad(
                        new Vector2((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2),
                        new Vector2(BottomRight.x, BottomRight.y)
                        );
                }
                BottomRightTree.Insert(node);
            }
        }

    }

    public Node Search(Vector2 point)
    {
        if (!InBoundary(point)) return null;

        if (_Node != null) return _Node;

        if ((TopLeft.x + BottomRight.x) / 2 >= point.x)
        {
            // Top Left
            if ((TopLeft.y + BottomRight.y) / 2 >= point.y)
            {
                if (TopLeftTree == null) return null;
                return TopLeftTree.Search(point);
            }

            // Bottom Left
            else
            {
                if (BottomLeftTree == null) return null;
                return BottomLeftTree.Search(point);
            }
        }
        else
        {
            // Top Right
            if ((TopLeft.y + BottomRight.y) / 2 >= point.y)
            {
                if (TopRightTree == null) return null;
                return TopRightTree.Search(point);
            }

            // Bottom Right
            else
            {
                if (BottomRightTree == null) return null;
                return BottomRightTree.Search(point);
            }
        }
    }

    public bool InBoundary(Vector2 point)
    {
        return (
            point.x >= TopLeft.x &&
            point.x <= BottomRight.x &&
            point.y >= TopLeft.y &&
            point.y <= BottomRight.y);
    }
}

public class Node
{
    public Vector2 Point;
    public bool Active;

    public Node(Vector2 point, bool active)
    {
        Point = point;
        Active = active;
    }
}
