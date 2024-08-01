using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class NTS_PhysicsWorld2D : MonoBehaviour, ISerializationCallbackReceiver
{
    public static NTS_PhysicsWorld2D Instance;

    public List<NTS_Rigidbody2D> tests = new();
    public List<NTS_KonvexCollider2D> colltest = new();

    public int Steps
    {
        get => NEWTONS.Core._2D.Physics2D.Steps;
        set => NEWTONS.Core._2D.Physics2D.Steps = value;
    }

    public bool UseCustomDrag
    {
        get => NEWTONS.Core._2D.Physics2D.UseCustomDrag;
        set => NEWTONS.Core._2D.Physics2D.UseCustomDrag = value;
    }

    public float Temperature
    {
        get => NEWTONS.Core._2D.Physics2D.Temperature;
        set => NEWTONS.Core._2D.Physics2D.Temperature = value;
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

    public Broadphase BroadphaseAlgorithm
    {
        get => (Broadphase)(int)NEWTONS.Core._2D.Physics2D.broadphaseAlgorithm;
        set => NEWTONS.Core._2D.Physics2D.broadphaseAlgorithm = (NEWTONS.Core._2D.Physics2D.Broadphase)(int)value;
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
        //if (Input.GetKeyDown(KeyCode.Space))
            NEWTONS.Core._2D.Physics2D.Update(Time.fixedDeltaTime);
    }

    public enum Broadphase
    {
        Quadtree = 0,
        BVH = 1,
    }

    //private void OnDrawGizmos()
    //{
    //    foreach (var node in NEWTONS.Core._2D.Physics2D._bvh.nodes)
    //    {
    //        if (!node.isLeaf)
    //            continue;

    //        NEWTONS.Core.Vector2 size = (node.bounds.Max - node.bounds.Min);
    //        Vector2 center = (node.bounds.Min + size * 0.5f).ToUnityVector();

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
        public Vector2 gravity;
        public bool useCustomDrag;
        public float temperature;
        public float density;
        public Broadphase broadphaseAlgorithm;
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
            broadphaseAlgorithm = BroadphaseAlgorithm,
        };
    }

    public void OnAfterDeserialize()
    {
        Steps = _serializerWorld.steps;
        Gravity = _serializerWorld.gravity;
        UseCustomDrag = _serializerWorld.useCustomDrag;
        Temperature = _serializerWorld.temperature;
        Density = _serializerWorld.density;
        BroadphaseAlgorithm = _serializerWorld.broadphaseAlgorithm;
    }
    #endregion
}
