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

    System.Diagnostics.Stopwatch sw = new();

    float time = 0f;
    int frames = 0;

    private void FixedUpdate()
    {
        if (frames >= 50 * 20)
        {
            Debug.Log(time / frames);
            return;
        }

        sw.Start();
        NEWTONS.Core._2D.Physics2D.Update(Time.fixedDeltaTime);
        sw.Stop();
        time += sw.ElapsedMilliseconds;
        frames++;
        sw.Reset();
    
    }

    #region Serialization
    [System.Serializable]
    private struct SerializerPhysicsWorld2D
    {
        public int steps;
        public Vector2 gravity;
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
