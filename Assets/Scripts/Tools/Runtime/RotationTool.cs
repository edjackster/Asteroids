using UnityEngine;

namespace Tools.Runtime
{
    public static class RotationTool
    {
        public static Vector3[] GetSplitDirections(
            Vector3 baseDirection,
            int count,
            float spreadAngle)
        {
            Vector3[] directions = new Vector3[count];

            if (count == 1)
            {
                directions[0] = baseDirection;
                return directions;
            }

            float startAngle = -spreadAngle * 0.5f;
            float angleStep = spreadAngle / count;

            for (int i = 0; i < count; i++)
            {
                float angle = startAngle + angleStep * i;

                directions[i] = Rotate(baseDirection, angle);
            }

            return directions;
        }

        private static Vector2 Rotate(Vector2 vector, float degrees)
        {
            float radians = degrees * Mathf.Deg2Rad;

            float sin = Mathf.Sin(radians);
            float cos = Mathf.Cos(radians);

            float x = vector.x * cos - vector.y * sin;
            float y = vector.x * sin + vector.y * cos;

            return new Vector2(x, y);
        }
    }
}
