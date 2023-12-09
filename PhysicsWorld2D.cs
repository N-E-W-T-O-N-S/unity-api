using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsWorld2D : MonoBehaviour
{
    public static List<KinematicBody2D> tests = new List<KinematicBody2D>();
    public static List<KonvexCollider2D> colltest = new List<KonvexCollider2D>();

    //TODO: Look into removing these:
    #region Internal NEWTONS fields
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Temperature"/> to alter the Temperature.
    /// </remarks>
    public float initialTemperature = NEWTONS.Core.Physics2D.Temperature;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Density"/> to alter the Density.
    /// </remarks>
    public float initialDensity = NEWTONS.Core.Physics2D.Density;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="UseCustomDrag"/> to alter the UseCustomDrag.
    /// </remarks>
    public bool initialUseCustomDrag = NEWTONS.Core.Physics2D.UseCustomDrag;
    /// <summary>
    /// <b><u>WARNING:</u></b> Internal NEWTONS field. Do not use.
    /// </summary>
    /// <remarks>
    /// Do not use this field directly. Insteade use <see cref="Gravity"/> to alter the Gravity.
    /// </remarks>
    public Vector2 initialGravity = NEWTONS.Core.Physics2D.Gravity.ToUnityVector();
    #endregion

    public static bool UseCustomDrag
    {
        get => NEWTONS.Core.Physics2D.UseCustomDrag;
        set => NEWTONS.Core.Physics2D.UseCustomDrag = value;
    }

    public static float Temperature
    {
        get => NEWTONS.Core.Physics2D.Temperature;
        set { NEWTONS.Core.Physics2D.Temperature = value; }
    }

    public static float Density
    {
        get => NEWTONS.Core.Physics2D.Density;
        set { NEWTONS.Core.Physics2D.Density = value; }
    }


    public static UnityEngine.Vector2 Gravity
    {
        get => NEWTONS.Core.Physics2D.Gravity.ToUnityVector();
        set { NEWTONS.Core.Physics2D.Gravity = value.ToNewtonsVector(); }
    }

    private void Awake()
    {
        NEWTONS.Core.Physics2D.Temperature = initialTemperature;
        NEWTONS.Core.Physics2D.Density = initialDensity;
        NEWTONS.Core.Physics2D.UseCustomDrag = initialUseCustomDrag;
        NEWTONS.Core.Physics2D.Gravity = initialGravity.ToNewtonsVector();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        NEWTONS.Core.Physics.Update(Time.fixedDeltaTime);
    //}

    private void FixedUpdate()
    {
        NEWTONS.Core.Physics2D.Update(Time.fixedDeltaTime);
        //Test2();
    }

    public static void DestroyBody(KinematicBody2D body)
    {
        //tests.Remove(body);
        NEWTONS.Core.KinematicBody2D b = body.Body;
        if (b != null)
            NEWTONS.Core.Physics2D.RemoveBody(b);
    }

    private void Test2()
    {
        bool b = colltest[0].KonvexCollider.Collision(colltest[1].KonvexCollider);
        if (b)
        {
            colltest[0].GetComponent<SpriteRenderer>().material.color = Color.red;
            colltest[1].GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        else
        {
            colltest[0].GetComponent<SpriteRenderer>().material.color = Color.white;
            colltest[1].GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }
}
