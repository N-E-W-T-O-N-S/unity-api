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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UnityEngine.Vector3[] ToUnityVectorArray(this NEWTONS.Core.Vector3[] vectors)
    {
        UnityEngine.Vector3[] unityVectors = new UnityEngine.Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            unityVectors[i] = vectors[i].ToUnityVector();
        }
        return unityVectors;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NEWTONS.Core.Vector3[] ToNewtonsVectorArray(this UnityEngine.Vector3[] vectors)
    {
        NEWTONS.Core.Vector3[] newtonsVectors = new NEWTONS.Core.Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            newtonsVectors[i] = vectors[i].ToNewtonsVector();
        }
        return newtonsVectors;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UnityEngine.Vector2 ToUnityVector(this NEWTONS.Core.Vector2 vector)
    {
        return new UnityEngine.Vector2(vector.x, vector.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NEWTONS.Core.Vector2 ToNewtonsVector(this UnityEngine.Vector2 vector)
    {
        return new NEWTONS.Core.Vector2(vector.x, vector.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UnityEngine.Vector2[] ToUnityVectorArray(this NEWTONS.Core.Vector2[] vectors)
    {
        UnityEngine.Vector2[] unityVectors = new UnityEngine.Vector2[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            unityVectors[i] = vectors[i].ToUnityVector();
        }
        return unityVectors;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NEWTONS.Core.Vector2[] ToNewtonsVectorArray(this UnityEngine.Vector2[] vectors)
    {
        NEWTONS.Core.Vector2[] newtonsVectors = new NEWTONS.Core.Vector2[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            newtonsVectors[i] = vectors[i].ToNewtonsVector();
        }
        return newtonsVectors;
    }
}
