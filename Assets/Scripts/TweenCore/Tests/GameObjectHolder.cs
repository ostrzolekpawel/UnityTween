using System;
using UnityEngine;

namespace Assets.Scripts.TweenCore.Tests
{
    [Serializable]
    public enum AnimationType
    {
        Float,
        Vector2,
        Vector3,
        Quaternion
    }

    [Serializable]
    public class GameObjectHolder //: MonoBehaviour
    {
        public GameObject Target;
        public AnimationType Type;
    }
}
