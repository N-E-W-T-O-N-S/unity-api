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
    public float initialTemperature = NEWTONS.Core.Physics.Temperature;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Density"/> to alter the Density.
    /// </remarks>
    public float initialDensity = NEWTONS.Core.Physics.Density;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="UseCustomDrag"/> to alter the UseCustomDrag.
    /// </remarks>
    public bool initialUseCustomDrag = NEWTONS.Core.Physics.UseCustomDrag;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Gravity"/> to alter the Gravity.
    /// </remarks>
    public UnityEngine.Vector3 initialGravity = NEWTONS.Core.Physics.Gravity.ToUnityVector();
    #endregion

    public static bool UseCustomDrag
    {
        get => NEWTONS.Core.Physics.UseCustomDrag;
        set => NEWTONS.Core.Physics.UseCustomDrag = value;
    }

    public static float Temperature
    {
        get => NEWTONS.Core.Physics.Temperature;
        set => NEWTONS.Core.Physics.Temperature = value;
    }

    public static float Density
    {
        get => NEWTONS.Core.Physics.Density;
        set => NEWTONS.Core.Physics.Density = value;
    }

    public static UnityEngine.Vector3 Gravity
    {
        get => NEWTONS.Core.Physics.Gravity.ToUnityVector();
        set => NEWTONS.Core.Physics.Gravity = value.ToNewtonsVector();
    }

    private void Awake()
    {
        NEWTONS.Core.Physics.Temperature = initialTemperature;
        NEWTONS.Core.Physics.Density = initialDensity;
        NEWTONS.Core.Physics.UseCustomDrag = initialUseCustomDrag;
        NEWTONS.Core.Physics.Gravity = initialGravity.ToNewtonsVector();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        NEWTONS.Core.Physics.Update(Time.fixedDeltaTime);
    //}

    private void FixedUpdate()
    {
        NEWTONS.Core.Physics.Update(Time.fixedDeltaTime);
    }

    public static void DestroyBody(NTS_Rigidbody body)
    {
        //tests.Remove(body);
        NEWTONS.Core.Rigidbody b = body.Body;
        if (b != null)
            NEWTONS.Core.Physics.RemoveBody(b);
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
