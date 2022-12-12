// https://www.gizma.com/easing/

using System;
using Mathf = System.Math;

namespace Gamecodeur
{
    class Tween
    {
        public float start { get; set; }
        public float end { get; set; }
        public float time { get; set; }
        public float duration { get; set; }
        public bool ended { get; private set; }
        private easeType ease;

        public enum easeType
        {
            Linear,
            Sin,
            SinOut,
            Quad,
            Cube,
            Exp,
            Circ,
        }

        public Tween()
        {
            start = 0;
            end = 0;
            time = 0;
            duration = 0;
            ended = false;
            ease = easeType.Linear;
        }

        public Tween(float pStart, float pEnd, float pDuration, easeType pEase = easeType.Linear)
        {
            Start(pStart, pEnd, pDuration, pEase);
        }

        public void Start(float pStart, float pEnd, float pDuration, easeType pEase = easeType.Linear)
        {
            start = pStart;
            end = pEnd;
            time = 0;
            duration = pDuration;
            ended = false;
            ease = pEase;
        }

        public void Update(float dt, ref float pValue)
        {
            float t = time / duration;

            if (!ended)
            {
                time += dt;

                switch (ease)
                {
                    case easeType.Linear:
                        pValue = LinearTween(start, end, t);
                        break;
                    case easeType.Sin:
                        pValue = SinusoidalTween(start, end, t);
                        break;
                    case easeType.SinOut:
                        pValue = SinusoidalOutTween(start, end, t);
                        break;
                    case easeType.Quad:
                        pValue = QuadraticTween(start, end, t);
                        break;
                    case easeType.Cube:
                        pValue = CubicTween(start, end, t);
                        break;
                    case easeType.Exp:
                        pValue = ExponentialTween(start, end, t);
                        break;
                    case easeType.Circ:
                        pValue = CircularTween(start, end, t);
                        break;
                    default:
                        break;
                }
            }
            if (time >= duration && !ended)
            {
                ended = true;
            }
        }

        // Linear tween function
        float LinearTween(float start, float end, float time)
        {
            return start + (end - start) * time;
        }

        float CircularTween(float start, float end, float time)
        {
            //t /= d;
            //return -c * (Math.sqrt(1 - t * t) - 1) + b;
            float c = (end - start);
            float b = start;
            return (float)(-c * (Math.Sqrt(1 - time * time) - 1) + b);

        }

        float QuadraticTween(float start, float end, float time)
        {
            float c = (end - start);
            float b = start;
            float t = time;
            return c * t * t + b;
        }


        float CubicTween(float start, float end, float time)
        {
            float c = (end - start);
            float b = start;
            float t = time;
            return c * t * t * t + b;

        }

        float ExponentialTween(float start, float end, float time)
        {
            float c = (end - start);
            float b = start;
            float t = time;

            return (float)(c * Math.Pow(2, 10 * (t - 1)) + b);

        }

        float SinusoidalTween(float start, float end, float time)
        {
            // t = current time
            // b = start
            // c = change in value
            // d = duration
            // return -c * Math.cos(t/d * (Math.PI/2)) + c + b;
            float c = (end - start);
            float b = start;
            return (float)(-c * Mathf.Cos((time * Mathf.PI) / 2)) + c + b;
        }

        float SinusoidalOutTween(float start, float end, float time)
        {
            float c = (end - start);
            float b = start;
            float t = time;

            return (float)(c * Math.Sin(t * (Math.PI / 2)) + b);
        }

    }
}
