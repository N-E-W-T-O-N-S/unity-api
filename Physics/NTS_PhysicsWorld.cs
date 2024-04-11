using NEWTONS.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTS_PhysicsWorld : MonoBehaviour
{
    public static List<NTS_Rigidbody> tests = new List<NTS_Rigidbody>();
    public static List<NTS_CuboidCollider> colltest = new List<NTS_CuboidCollider>();

    //TODO: Look into removing these:
    #region Internal NEWTONS fields
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Temperature"/> to alter the Temperature.
    /// </remarks>
    public float initialTemperature = NEWTONS.Core._3D.Physics.Temperature;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Density"/> to alter the Density.
    /// </remarks>
    public float initialDensity = NEWTONS.Core._3D.Physics.Density;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="UseCustomDrag"/> to alter the UseCustomDrag.
    /// </remarks>
    public bool initialUseCustomDrag = NEWTONS.Core._3D.Physics.UseCustomDrag;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Gravity"/> to alter the Gravity.
    /// </remarks>
    public UnityEngine.Vector3 initialGravity = NEWTONS.Core._3D.Physics.Gravity.ToUnityVector();
    #endregion

    public static bool UseCustomDrag
    {
        get => NEWTONS.Core._3D.Physics.UseCustomDrag;
        set => NEWTONS.Core._3D.Physics.UseCustomDrag = value;
    }

    public static float Temperature
    {
        get => NEWTONS.Core._3D.Physics.Temperature;
        set => NEWTONS.Core._3D.Physics.Temperature = value;
    }

    public static float Density
    {
        get => NEWTONS.Core._3D.Physics.Density;
        set => NEWTONS.Core._3D.Physics.Density = value;
    }

    public static UnityEngine.Vector3 Gravity
    {
        get => NEWTONS.Core._3D.Physics.Gravity.ToUnityVector();
        set => NEWTONS.Core._3D.Physics.Gravity = value.ToNewtonsVector();
    }

    private void Awake()
    {
        NEWTONS.Core._3D.Physics.DeltaTime = Time.fixedDeltaTime;
        NEWTONS.Core._3D.Physics.Temperature = initialTemperature;
        NEWTONS.Core._3D.Physics.Density = initialDensity;
        NEWTONS.Core._3D.Physics.UseCustomDrag = initialUseCustomDrag;
        NEWTONS.Core._3D.Physics.Gravity = initialGravity.ToNewtonsVector();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        NEWTONS.Core.Physics.Update(Time.fixedDeltaTime);
    //}

    private void FixedUpdate()
    {
        NEWTONS.Core._3D.Physics.Update();
    }

    public static void DestroyBody(NTS_Rigidbody body)
    {
        //tests.Remove(body);
        NEWTONS.Core._3D.Rigidbody b = body.Body;
        if (b != null)
            NEWTONS.Core._3D.Physics.RemoveBody(b);
    }

    private void Test2()
    {
        CollisionInfo coll = colltest[0].CuboidColl.IsColliding(colltest[1].CuboidColl);
        if (coll.didCollide)
        {
            colltest[0].GetComponent<MeshRenderer>().material.color = Color.red;
            colltest[1].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            colltest[0].GetComponent<MeshRenderer>().material.color = Color.white;
            colltest[1].GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
