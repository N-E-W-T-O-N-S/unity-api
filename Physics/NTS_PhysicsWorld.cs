using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_PhysicsWorld : MonoBehaviour, ISerializationCallbackReceiver
{
    public static NTS_PhysicsWorld Instance;

    public int Steps
    {
        get => NEWTONS.Core._3D.Physics.Steps;
        set => NEWTONS.Core._3D.Physics.Steps = value;
    }

    public bool UseCustomDrag
    {
        get => NEWTONS.Core._3D.Physics.UseCustomDrag;
        set => NEWTONS.Core._3D.Physics.UseCustomDrag = value;
    }

    public float Temperature
    {
        get => NEWTONS.Core._3D.Physics.Temperature;
        set => NEWTONS.Core._3D.Physics.Temperature = value;
    }

    public float Density
    {
        get => NEWTONS.Core._2D.Physics2D.Density;
        set => NEWTONS.Core._2D.Physics2D.Density = value;
    }

    public UnityEngine.Vector2 Gravity
    {
        get => NEWTONS.Core._2D.Physics2D.Gravity.ToUnityVector();
        set => NEWTONS.Core._2D.Physics2D.Gravity = value.ToNewtonsVector();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(this);
            return;
        }
    }

    private void OnValidate()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(this);
            return;
        }
    }

    private void FixedUpdate()
    {
        NEWTONS.Core._3D.Physics.Update(Time.fixedDeltaTime);
    }

    //private void OnDrawGizmos()
    //{
    //    foreach (var node in NEWTONS.Core._3D.Physics._bvh.nodes)
    //    {
    //        if (!node.isLeaf)
    //            continue;

    //        NEWTONS.Core.Vector3 size = (node.bounds.Max - node.bounds.Min);
    //        Vector3 center = (node.bounds.Min + size * 0.5f).ToUnityVector();

    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(center, 0.05f);
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawWireCube(center, size.ToUnityVector());
    //        Gizmos.color = Color.white;
    //    }
    //}

    #region Serialization
    [System.Serializable]
    private struct SerializerPhysicsWorld2D
    {
        public int steps;
        public Vector3 gravity;
        public bool useCustomDrag;
        public float temperature;
        public float density;
    }

    [SerializeField]
    private SerializerPhysicsWorld2D _serializerWorld;

    public void OnBeforeSerialize()
    {
        _serializerWorld = new()
        {
            steps = Steps,
            gravity = Gravity,
            useCustomDrag = UseCustomDrag,
            temperature = Temperature,
            density = Density,
        };
    }

    public void OnAfterDeserialize()
    {
        Steps = _serializerWorld.steps;
        Gravity = _serializerWorld.gravity;
        UseCustomDrag = _serializerWorld.useCustomDrag;
        Temperature = _serializerWorld.temperature;
        Density = _serializerWorld.density;
    }
    #endregion
}
