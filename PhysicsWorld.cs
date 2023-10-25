using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld : MonoBehaviour
{
    public static List<KinematicBody> tests = new List<KinematicBody>();

    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Temperature"/> to alter the Temperature.
    /// </remarks>
    public float initialTemperature = NEWTONS.Core.Physics.Temperature;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Density"/> to alter the Density.
    /// </remarks>
    public float initialDensity = NEWTONS.Core.Physics.Density;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="UseCustomDrag"/> to alter the UseCustomDrag.
    /// </remarks>
    public bool initialUseCustomDrag = NEWTONS.Core.Physics.UseCustomDrag;
    /// <summary>
    /// Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this method directly. Insteade use <see cref="Gravity"/> to alter the Gravity.
    /// </remarks>
    public Vector3 initialGravity = NEWTONS.Core.Physics.Gravity.ToUnityVector();

    public static bool UseCustomDrag 
    {
        get => NEWTONS.Core.Physics.UseCustomDrag;
        set => NEWTONS.Core.Physics.UseCustomDrag = value;
    }

    public static float Temperature
    {
        get => NEWTONS.Core.Physics.Temperature;
        set { NEWTONS.Core.Physics.Temperature = value; }
    }

    public static float Density
    {
        get => NEWTONS.Core.Physics.Density;
        set { NEWTONS.Core.Physics.Density = value; }
    }


    public static UnityEngine.Vector3 Gravity
    {
        get => NEWTONS.Core.Physics.Gravity.ToUnityVector();
        set { NEWTONS.Core.Physics.Gravity = value.ToNewtonsVector(); }
    }

    private void FixedUpdate()
    {
        NEWTONS.Core.Physics.Update(Time.fixedDeltaTime);
    }

    public static void DestroyBody(KinematicBody body)
    {
        tests.Remove(body);
        NEWTONS.Core.KinematicBody b = body.GetPhysicsBody();
        if (b != null)
            NEWTONS.Core.Physics.RemoveBody(b);
    }
}
