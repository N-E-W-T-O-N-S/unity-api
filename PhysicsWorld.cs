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

    public static void DestroyBody(KinematicBody test)
    {
        tests.Remove(test);
        if (!test.body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
            throw new Exception("No KinematicBody");
        NEWTONS.Core.Physics.RemoveBody(b);
    }
}
