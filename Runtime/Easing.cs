using UnityEngine;

namespace Napadol.Tools
{
    public class Easing
    {
        #region EaseIn
        public float EaseInSine(float t)
        {
            return 1f - Mathf.Cos((t * Mathf.PI) / 2f);
        }
        public float EaseInCubic(float t)
        {
            return t * t * t;
        }
        public float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }
        public float EaseInCirc(float t)
        {
            return 1f - Mathf.Sqrt(1f - Mathf.Pow(t, 2));
        }
        public float EaseInElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            
            return t == 0f 
                ? 0f
                : t >= 0.99f
                    ? 1f
                    : -Mathf.Pow(2f, 10f * t - 10f) 
                      * Mathf.Sin((t * 10f - 10.75f) * c4);
        }
        public float EaseInQuad(float t)
        {
            return t * t;
        }
        public float EaseInQuart(float t)
        {
            return t * t * t * t;
        }
        public float EaseInExpo(float t)
        {
            return t == 0 ? 0 : Mathf.Pow(2f, 10f * t - 10f);
        }
        public float EaseInBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            
            return c3 * t * t * t - c1 * t * t;
        }
        public float EaseInBounce(float t)
        {
            return 1f - EaseOutBounce(1f - t);
        }
        #endregion
        #region EaseOut
        public float EaseOutSine(float t)
        {
            return Mathf.Sin((t * Mathf.PI) / 2f);
        }
        public float EaseOutCubic(float t)
        {
            return 1f - Mathf.Pow(1 - t, 3);
        }
        public float EaseOutQuint(float t)
        {
            return 1f - Mathf.Pow(1 - t, 5);
        }
        public float EaseOutCirc(float t)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
        }
        public float EaseOutElastic(float t)
        {
            const float c4 = (2f * Mathf.PI) / 3f;
            
            return t == 0f
                ? 0f
                : t >= 0.99f
                    ? 1f
                    : Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
        }
        public float EaseOutQuad(float t)
        {
            return 1f - (1f - t) * (1f - t);
        }
        public float EaseOutQuart(float t)
        {
            return 1f - Mathf.Pow(1f - t, 4);
        }
        public float EaseOutExpo(float t)
        {
            return t >= 0.99f ? 1f : 1f - Mathf.Pow(2, -10f * t);
        }
        public float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1f;
            return 1f + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1f, 2);
        }
        public float EaseOutBounce(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (t < 1f / d1)
            {
                return n1 * t * t;
            } 
            else if (t < 2f / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t <  2.5f / d1)
            {
                return  n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else
            {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }
        #endregion
        #region EaseInOut
        public float EaseInOutSine(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1f)/2f;
        }
        public float EaseInOutCubic(float t)
        {
            return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3) / 2f;
        }
        public float EaseInOutQuint(float t)
        {
            return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5) / 2f;
        }
        public float EaseInOutCirc(float t)
        {
            return t < 0.5f 
                ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2))) / 2f 
                : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2)) + 1f) / 2f;
        }
        public float EaseInOutElastic(float t)
        {
            const float c5 = (2f * Mathf.PI) / 4.5f;
            
            return t == 0f
                ? 0f
                : t >= 0.99f
                    ? 1f
                    : t < 0.5f
                    ? -(Mathf.Pow(2f, 20 * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f
                    : (Mathf.Pow(2f, -20 * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f + 1f;
        }
        public float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : 1 - Mathf.Pow(-2f * t + 2f, 2) / 2f;
        }
        public float EaseInOutQuart(float t)
        {
            return t < 0.5f ? 8 * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 4) / 2f;
        }
        public float EaseInOutExpo(float t)
        {
            return t == 0f
                ? 0f
                : t >= 0.99f
                    ? 1f
                    : t < 0.5f ? Mathf.Pow(2f, -20f * t - 10f) / 2f
                        : (2f - Mathf.Pow(2f, -20f * t + 10f) / 2f);
        }
        public float EaseInOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            return t < 0.5f
                ? (Mathf.Pow(2f * t, 2) * ((c2 + 1f) * 2f * t - c2)) / 2f
                : (Mathf.Pow(2f * t - 2f, 2) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
        }
        public float EaseInOutBounce(float t)
        {
            return t < 0.5f
                ? (1 - EaseOutBounce(1f - 2f * t)) / 2f
                : (1 + EaseOutBounce(2f * t - 1f)) / 2f;
        }
        #endregion
    }
}
