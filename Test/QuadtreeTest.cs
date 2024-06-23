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

    private Quadtree<MeshRenderer> _tree;
    MeshRenderer[] renderers;


    void Start()
    {
        renderers = new MeshRenderer[numberOfPoints];
        _tree = new Quadtree<MeshRenderer>(new Rectangle(transform.position.ToNewtonsVector(), size.x, size.y), 4);
        transform.localScale = size;

        for (int i = 0; i < numberOfPoints; i++)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
            obj.transform.localScale = UnityEngine.Vector3.one * 0.5f;
            obj.transform.position = new UnityEngine.Vector2(Random.value * size.x, Random.value * size.y) - size * 0.5f;
            var rend = obj.GetComponent<MeshRenderer>();
            renderers[i] = rend;
            _tree.Insert(new QuadtreeData<MeshRenderer>(new Rectangle(obj.transform.position.ToNewtonsVector(), obj.transform.localScale.x, obj.transform.localScale.y), rend));
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rect = new Rectangle(rectAnchor.transform.position.ToNewtonsVector(), rectAnchor.transform.localScale.x, rectAnchor.transform.localScale.y);
        List<QuadtreeData<MeshRenderer>> data = new();
        _tree.Receive(rect, data);

        foreach (var rend in renderers)
        {
            rend.material.color = Color.white;
        }

        foreach (var renderer in data)
        {
            renderer.Data.material.color = Color.red;
        }

    }
}
