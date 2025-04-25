using System.Collections.Generic;
using System.Drawing;

namespace MagesLastStand
{
    public class Enemy
    {
        public float Health { get; set; }
        public int Speed { get; }
        public int CurrentPathIndex { get; set; }
        public PointF Position { get; set; }
        public int GoldReward { get; }

        public Enemy(int health, int speed, Point startPos, int goldReward)
        {
            Health = health;
            Speed = speed;
            Position = startPos;
            GoldReward = goldReward;
            CurrentPathIndex = 0;
        }

        public void Move(List<Point> path)
        {
            if (CurrentPathIndex >= path.Count - 1) return;

            Point target = path[CurrentPathIndex + 1];
            float dx = target.X - Position.X;
            float dy = target.Y - Position.Y;
            float distance = (float)System.Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;
            }

            Position = new PointF(
                Position.X + dx * Speed,
                Position.Y + dy * Speed);

            if (System.Math.Abs(Position.X - target.X) < Speed &&
                System.Math.Abs(Position.Y - target.Y) < Speed)
            {
                CurrentPathIndex++;
            }
        }
    }
}