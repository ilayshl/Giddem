using UnityEngine;

public static class IsometricHelper
{
    static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    public static Vector3 ToIso(Vector3 input)
    {
        return _isoMatrix.MultiplyPoint3x4(input);
    }
}
