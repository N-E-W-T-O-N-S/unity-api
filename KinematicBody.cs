using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System;

public class KinematicBody : MonoBehaviour
{
    public WeakReference<NEWTONS.Core.KinematicBody> body;
    [SerializeField]
    private bool useGravity = true;
    public bool UseGravity 
    {  
        get => useGravity; 
        set 
        {
            if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            if (b.UseGravity != value)
                b.UseGravity = value;
            useGravity = value; 
        } 
    }

    [SerializeField]
    private UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 Velocity 
    { 
        get => velocity; 
        set 
        {
            if (!body.TryGetTarget(out NEWTONS.Core.KinematicBody b))
                throw new Exception("No KinematicBody");
            if (b.Velocity != value.ToNewtonsVector())
                b.Velocity = value.ToNewtonsVector();
            velocity = value; 
        } 
    }

    private void Awake()
    {

        body = new WeakReference<NEWTONS.Core.KinematicBody>(
        new NEWTONS.Core.KinematicBody()
            {
                Position = transform.position.ToNewtonsVector(),
                UseGravity = useGravity,
                Velocity = velocity.ToNewtonsVector()
            });
        PhysicsWorld.tests.Add(this);
    }

    private void OnDestroy()
    {
        PhysicsWorld.DestroyBody(this);
    }
}