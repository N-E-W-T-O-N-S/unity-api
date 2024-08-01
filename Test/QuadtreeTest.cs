using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core._2D;
using NEWTONS.Core;

public class QuadtreeTest : MonoBehaviour
{
    public UnityEngine.Vector2 size;
    public int numberOfPoints;

    public Transform rectAnchor;

    private BVH2D<MeshRenderer> _tree;
    MeshRenderer[] renderers;


    void Start()
    {
        renderers = new MeshRenderer[numberOfPoints];

        BVHData2D<MeshRenderer>[] data = new BVHData2D<MeshRenderer>[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            obj.transform.localScale = UnityEngine.Vector3.one * 0.5f;
            obj.transform.position = new UnityEngine.Vector2(Random.value * size.x, Random.value * size.y) - size * 0.5f;
            var rend = obj.GetComponent<MeshRenderer>();
            renderers[i] = rend;
            data[i] = new BVHData2D<MeshRenderer>(obj.transform.position.ToNewtonsVector(), new(0.25f, obj.transform.position.ToNewtonsVector()), rend);
        }

        _tree = new BVH2D<MeshRenderer>();
        _tree.Build(data);
        transform.localScale = size;

    }

    // Update is called once per frame
    void Update()
    {
        var pos = rectAnchor.transform.position.ToNewtonsVector();
        var rect = new Bounds2D(0.5f, pos);
        List<BVHData2D<MeshRenderer>> data = new();
        _tree.Receive(rect, data);

        foreach (var rend in renderers)
        {
            rend.material.color = Color.white;
        }

        foreach (var renderer in data)
        {
            renderer.data.material.color = Color.red;
        }

    }
}
