using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector4Int
{
    #region Variables
    // Variables
    public int x;
    public int y;
    public int z;
    public int w;
    #endregion

    #region Constructors
    public Vector4Int(Vector2Int l, Vector2Int r)
    {
        x = l.x; y = l.y; z = r.x; w = r.y;
    }
    public Vector4Int(int x, int y, int z, int w)
    {
        this.x = x; this.y = y; this.z = z; this.w = w;
    }
    public Vector4Int(Vector4Int v)
    {
        x = v.x; y= v.y; z = v.z; w = v.w;
    }
    #endregion

    #region Operator Overloads
    public static Vector4Int operator +(Vector4Int a) => a;
    public static Vector4Int operator -(Vector4Int a) => new(-a.x, -a.y, -a.z, -a.w);

    public static Vector4Int operator +(Vector4Int a, Vector4Int b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Vector4Int operator -(Vector4Int a, Vector4Int b) => a + (-b);

    public static bool operator ==(Vector4Int a, Vector4Int b) => (a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w);
    public static bool operator !=(Vector4Int a, Vector4Int b) => !(a == b);

    #endregion

    public override string ToString() => "(" + x + " " + y + " " + z + " " + w + ")";
    public override bool Equals(object obj) => this == (obj as Vector4Int);
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
