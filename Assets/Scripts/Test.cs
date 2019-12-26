using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Quad q;

    void Start()
    {
        q = new Quad(new Vector2(0, 0), new Vector2(8, 8));

        Node a = new Node(new Vector2(1, 1), true);
        Node b = new Node(new Vector2(2, 5), false);
        Node c = new Node(new Vector2(7, 6), false);
        Node d = new Node(new Vector2(7, 6.5f), false);
        Node e = new Node(new Vector2(7.5f, 6.5f), false);
        Node f = new Node(new Vector2(4f, 4f), false);
        Node g = new Node(new Vector2(4.25f, 4.25f), false);

        q.Insert(a);
        q.Insert(b);
        q.Insert(c);
        q.Insert(d);
        q.Insert(e);
        q.Insert(f);
        q.Insert(g);

    }


    private void OnDrawGizmos()
    {
        DrawAll(q);
    }

    private void DrawAll(Quad q)
    {
        DrawQuad(q);

        if (q.TopLeftTree != null)
        {
            DrawAll(q.TopLeftTree);
        }
        if (q.TopRightTree != null)
        {
            DrawAll(q.TopRightTree);
        }
        if (q.BottomRightTree != null)
        {
            DrawAll(q.BottomRightTree);
        }
        if (q.BottomLeftTree != null)
        {
            DrawAll(q.BottomLeftTree);
        }

    }

    private void DrawQuad(Quad q)
    {

        Vector3 bottomLeft = new Vector3(q.TopLeft.x, q.TopLeft.y, 0);
        Vector3 topLeft = new Vector3(q.TopLeft.x, q.BottomRight.y, 0);
        Vector3 topRight = new Vector3(q.BottomRight.x, q.BottomRight.y, 0);
        Vector3 bottomRight = new Vector3(q.BottomRight.x, q.TopLeft.y, 0);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);

        if (q._Node != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(new Vector3(q._Node.Point.x, q._Node.Point.y, 0), new Vector3(0.1f, 0.1f, 0.1f));
        }
    }

}
