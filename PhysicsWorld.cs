using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

public class PhysicsWorld : MonoBehaviour
{
    public static List<KinematicBody> tests = new List<KinematicBody>();

    public UnityEngine.Vector3 Gravity
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
