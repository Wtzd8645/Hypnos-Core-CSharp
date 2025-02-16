﻿using System;

namespace Blanketmen.Hypnos.Mathematics
{
    public static partial class MathUtil
    {
        public static float EaseInQuad(float t)
        {
            return t * t;
        }

        public static float EaseOutQuad(float t)
        {
            return t * (2f - t);
        }

        public static float EaseInOutQuad(float t)
        {
            return t < 0.5f ? 2f * t * t : t * (4f - 2f * t) - 1f;
        }

        public static float EaseInCubic(float t)
        {
            return t * t * t;
        }

        public static float EaseOutCubic(float t)
        {
            return 1f + --t * t * t;
        }

        public static float EaseInOutCubic(float t)
        {
            return t < 0.5f ? 4f * t * t * t : 4f * --t * t * t + 1f;
        }

        public static float EaseInQuart(float t)
        {
            return t * t * t * t; // NOTE: Fast than (k *= k) * k
        }

        public static float EaseOutQuart(float t)
        {
            return 1f - (--t * t * t * t);
        }

        public static float EaseInOutQuart(float t)
        {
            return t < 0.5f ? 8f * t * t * t * t : -8f * --t * t * t * t + 1f;
        }

        public static float EaseInQuint(float t)
        {
            return t * t * t * t * t;
        }

        public static float EaseOutQuint(float t)
        {
            return 1f + --t * t * t * t * t;
        }

        public static float EaseInOutQuint(float t)
        {
            return t < 0.5f ? 16f * t * t * t * t * t : 16f * --t * t * t * t * t + 1f;
        }

        public static float EaseInSine(float t)
        {
            return 1f - (float)Math.Cos(t * Math.PI / 2f);
        }

        public static float EaseOutSine(float t)
        {
            return (float)Math.Sin(t * Math.PI / 2f);
        }

        public static float EaseInOutSine(float t)
        {
            return 0.5f * (1f - (float)Math.Cos(Math.PI * t));
        }

        public static float EaseInExpo(float t)
        {
            return t == 0f ? 0f : (float)Math.Pow(1024f, t - 1f);
        }

        public static float EaseOutExpo(float t)
        {
            return t == 1f ? 1f : 1f - (float)Math.Pow(2f, -10f * t);
        }

        public static float EaseInOutExpo(float t)
        {
            return t == 0f ? 0f : t == 1f ? 1f : t < 0.5f ? 0.5f * (float)Math.Pow(2f, 20f * t - 10f) : 0.5f * (2f - (float)Math.Pow(2f, -20f * t + 10f));
        }

        public static float EaseInCirc(float t)
        {
            return 1f - (float)Math.Sqrt(1f - t * t);
        }

        public static float EaseOutCirc(float t)
        {
            return (float)Math.Sqrt(1f - (--t) * t);
        }

        public static float EaseInOutCirc(float t)
        {
            return (t *= 2f) < 1f ? -0.5f * ((float)Math.Sqrt(1f - t * t) - 1f) : 0.5f * ((float)Math.Sqrt(1f - (t -= 2f) * t) + 1f);
        }

        // https://github.com/nicolausYes/easing-functions/blob/master/src/easing.cpp
        public static float EaseInElastic(float k)
        {
            return k == 0f ? 0f : k == 1f ? 1f : -(float)Math.Pow(2f, 10f * (k -= 1f)) * (float)Math.Sin((k - 0.1f) * (2f * Math.PI) / 0.4f);
        }

        public static float EaseOutElastic(float k)
        {
            return k == 0f ? 0f : k == 1f ? 1f : (float)Math.Pow(2f, -10f * k) * (float)Math.Sin((k - 0.1f) * (2f * Math.PI) / 0.4f) + 1f;
        }

        public static float EaseInOutElastic(float k)
        {
            return (k *= 2f) < 1f
                ? -0.5f * (float)Math.Pow(2f, 10f * (k -= 1f)) * (float)Math.Sin((k - 0.1f) * (2f * Math.PI) / 0.4f)
                : (float)Math.Pow(2f, -10f * (k -= 1f)) * (float)Math.Sin((k - 0.1f) * (2f * Math.PI) / 0.4f) * 0.5f + 1f;
        }

        public static float EaseInBack(float k)
        {
            const float s = 1.70158f;
            return k * k * ((s + 1f) * k - s);
        }

        public static float EaseOutBack(float k)
        {
            const float s = 1.70158f;
            return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
        }

        public static float EaseInOutBack(float k)
        {
            const float s2 = 2.5949095f;
            return (k *= 2f) < 1f ? 0.5f * (k * k * ((s2 + 1f) * k - s2)) : 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
        }

        public static float EaseInBounce(float k)
        {
            return 1f - EaseOutBounce(1f - k);
        }

        public static float EaseOutBounce(float k)
        {
            return k < (1f / 2.75f)
                ? 7.5625f * k * k
                : k < (2f / 2.75f)
                ? 7.5625f * (k -= 1.5f / 2.75f) * k + 0.75f
                : k < (2.5f / 2.75f) ? 7.5625f * (k -= 2.25f / 2.75f) * k + 0.9375f : 7.5625f * (k -= 2.625f / 2.75f) * k + 0.984375f;
        }

        public static float EaseInOutBounce(float k)
        {
            return k < 0.5f ? EaseInBounce(k * 2f) * 0.5f : EaseOutBounce(k * 2f - 1f) * 0.5f + 0.5f;
        }
    }
}