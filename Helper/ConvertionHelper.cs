using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NEWTONS.Core;
using System.Runtime.CompilerServices;

public static class ConvertionHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UnityEngine.Vector3 ToUnityVector(this NEWTONS.Core.Vector3 vector)
    {
        return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NEWTONS.Core.Vector3 ToNewtonsVector(this UnityEngine.Vector3 vector)
    {
        return new NEWTONS.Core.Vector3(vector.x, vector.y, vector.z);
    }
}
