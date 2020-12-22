using UnityEngine;

// https://easings.net/#
namespace UnityTween
{
    public static class MathfEx // moze nazwa klase ease
    {
        private const float PI = Mathf.PI;

        public static float Linear(float x)
        {
            return x;
        }

        public static float EaseInSine(float x)
        {
            return 1 - Mathf.Cos((x * PI) * 0.5f);
        }

        public static float EaseOutSine(float x)
        {
            return Mathf.Sin((x * PI) * 0.5f);
        }

        public static float EaseInOutSine(float x)
        {
            return -(Mathf.Cos(PI * x) - 1.0f) * 0.5f;
        }

        public static float EaseInQuad(float x)
        {
            return x * x;
        }

        public static float EaseOutQuad(float x)
        {
            return 1.0f - (1.0f - x) * (1.0f - x);
        }

        public static float EaseInOutQuad(float x)
        {
            return x < 0.5f ? 2.0f * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2) * 0.5f;
        }

        public static float EaseInCubic(float x)
        {
            return x * x * x;
        }

        public static float EaseOutCubic(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 3);
        }

        public static float EaseInOutCubic(float x)
        {
            return x < 0.5f ? 4.0f * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 3) * 0.5f;
        }

        public static float EaseInQuart(float x)
        {
            return x * x * x * x;
        }

        public static float EaseOutQuart(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 4);
        }

        public static float EaseInOutQuart(float x)
        {
            return x < 0.5f ? 8.0f * x * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 4) * 0.5f;
        }

        public static float EaseInQuint(float x)
        {
            return x * x * x * x * x;
        }

        public static float EaseOutQuint(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 5);
        }

        public static float EaseInOutQuint(float x)
        {
            return x < 0.5f ? 16.0f * x * x * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 5) * 0.5f;
        }

        public static float EaseInExpo(float x)
        {
            return x == 0.0f ? 0 : Mathf.Pow(2.0f, 10 * x - 10);
        }

        public static float EaseOutExpo(float x)
        {
            return x == 1.0f ? 1.0f : 1.0f - Mathf.Pow(2.0f, -10 * x);
        }

        public static float EaseInOutExpo(float x)
        {
            return x == 0.0f ? 0 : x == 1.0f ? 1 : x < 0.5f ? Mathf.Pow(2.0f, 20 * x - 10) * 0.5f : (2.0f - Mathf.Pow(2.0f, -20 * x + 10)) * 0.5f;
        }

        public static float EaseInCirc(float x)
        {
            return 1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(x, 2));
        }

        public static float EaseOutCirc(float x)
        {
            return Mathf.Sqrt(1.0f - Mathf.Pow(x - 1.0f, 2));
        }

        public static float EaseInOutCirc(float x)
        {
            return x < 0.5f ? (1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * x, 2))) * 0.5f : (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2)) + 1.0f) * 0.5f;
        }

        public static float EaseInBack(float x)
        {
            var c1 = 1.70158f;
            var c3 = c1 + 1.0f;
            return c3 * x * x * x - c1 * x * x;
        }

        public static float EaseOutBack(float x)
        {
            var c1 = 1.70158f;
            var c3 = c1 + 1.0f;
            return 1.0f + c3 * Mathf.Pow(x - 1.0f, 3) + c1 * Mathf.Pow(x - 1.0f, 2);
        }

        public static float EaseInOutBack(float x)
        {
            var c1 = 1.70158f;
            var c2 = c1 * 1.525f;
            return x < 0.5f ? (Mathf.Pow(2.0f * x, 2) * ((c2 + 1.0f) * 2.0f * x - c2)) * 0.5f : (Mathf.Pow(2.0f * x - 2.0f, 2) * ((c2 + 1.0f) * (x * 2.0f - 2.0f) + c2) + 2.0f) * 0.5f;
        }

        public static float EaseInElastic(float x)
        {
            var c = (2.0f * PI) / 3.0f;
            return x == 0.0f ? 0 : x == 1.0f ? 1 : -Mathf.Pow(2.0f, 10 * x - 10) * Mathf.Sin((x * 10.0f - 10.75f) * c);
        }

        public static float EaseOutElastic(float x)
        {
            var c = (2.0f * PI) / 3.0f;
            return x == 0.0f ? 0 : x == 1.0f ? 1 : Mathf.Pow(2.0f, -10 * x) * Mathf.Sin((x * 10.0f - 0.75f) * c) + 1.0f;
        }

        public static float EaseInOutElastic(float x)
        {
            var c = (2.0f * PI) / 4.5f;
            return x == 0.0f ? 0 : x == 1.0f ? 1 : x < 0.5f ? -(Mathf.Pow(2.0f, 20 * x - 10) * Mathf.Sin((20.0f * x - 11.125f) * c)) * 0.5f : (Mathf.Pow(2.0f, -20 * x + 10) * Mathf.Sin((20.0f * x - 11.125f) * c)) * 0.5f + 1.0f;
        }

        public static float EaseInBounce(float x)
        {
            return 1.0f - EaseOutBounce(1.0f - x);
        }

        public static float EaseOutBounce(float x)
        {
            var n1 = 7.5625f;
            var d1 = 2.75f;

            if (x < 1.0f / d1)
                return n1 * x * x;
            else if (x < 2.0f / d1)
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            else if (x < 2.5f / d1)
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            else
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }

        public static float EaseInOutBounce(float x)
        {
            return x < 0.5f ? (1.0f - EaseOutBounce(1.0f - 2.0f * x)) * 0.5f : (1.0f + EaseOutBounce(2.0f * x - 1.0f)) * 0.5f;
        }
    }
}
