using UnityEngine;

namespace UnityTween
{
    public static class VectorExtension
    {

        public static float InverseLerp(this Vector3 v, Vector3 a, Vector3 b)
        {
            Vector3 ab = b - a;
            Vector3 av = v - a;
            return Vector3.Dot(av, ab) / Vector3.Dot(ab, ab);
        }
        
        public static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 v) => (v - a) / (b - a);
    }
}
