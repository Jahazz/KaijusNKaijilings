using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolygonWithCutout : MaskableGraphic
{
    [field: SerializeField]
    private List<RectTransform> OuterNodes { get; set; }
    [field: SerializeField]
    private List<RectTransform> InnerNodes { get; set; }

    public void Update ()
    {
        SetAllDirty();
    }

    protected UIVertex[] SetVbo (Vector2[] vertices, Vector2[] uvs)
    {
        UIVertex[] vbo = new UIVertex[4];

        for (int i = 0; i < vertices.Length; i++)
        {
            UIVertex vert = UIVertex.simpleVert;
            vert.color = color;
            vert.position = vertices[i];
            vert.uv0 = uvs[i];
            vbo[i] = vert;
        }

        return vbo;
    }

    protected override void OnPopulateMesh (VertexHelper vh)
    {
        vh.Clear();
        Vector2 prevX = Vector2.zero;
        Vector2 prevY = Vector2.zero;
        Vector2[] uv = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

        Vector2[] pos = new Vector2[4];

        for (int i = 0; i < OuterNodes.Count; i++)
        {
            AddVertexQuad(OuterNodes[i].anchoredPosition, InnerNodes[i].anchoredPosition, ref prevX, ref prevY, uv, pos, vh);
        }
        AddVertexQuad(OuterNodes[0].anchoredPosition, InnerNodes[0].anchoredPosition, ref prevX, ref prevY, uv, pos, vh);
    }

    private void AddVertexQuad (Vector2 outer, Vector2 inner, ref Vector2 prevX, ref Vector2 prevY, Vector2[] uv, Vector2[] pos, VertexHelper vh)
    {
        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(1, 0);
        uv[3] = new Vector2(0, 0);
        pos[0] = prevX;
        pos[1] = outer;
        pos[2] = inner;
        pos[3] = prevY;
        prevX = pos[1];
        prevY = pos[2];
        vh.AddUIVertexQuad(SetVbo(pos, uv));
    }
}
