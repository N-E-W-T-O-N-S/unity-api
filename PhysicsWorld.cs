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
        Debug.Log(NEWTONS.Core.Physics.Bodies.Count);
        for (int i = 0; i < tests.Count; i++)
        {
            if (!tests[i].body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            UnityEngine.Vector3 enginePos = b.Position.ToUnityVector();
            UnityEngine.Vector3 engineVelocity = b.Velocity.ToUnityVector();

            if (tests[i].transform.position != enginePos)
                tests[i].transform.position = enginePos;
            if (tests[i].Velocity != engineVelocity)
                tests[i].Velocity = engineVelocity;
        }
    }

    public static void DestroyBody(KinematicBody test)
    {
        tests.Remove(test);
        if (!test.body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
            throw new Exception("No KinematicBody");
        NEWTONS.Core.Physics.RemoveBody(b);
    }
}
