using System;
using System.Collections.Generic;
using System.Drawing;

namespace MagesLastStand
{
    public class GameEngine
    {
        public List<Point> Path { get; } = new List<Point>();
        public List<Enemy> Enemies { get; } = new List<Enemy>();
        public List<Tower> Towers { get; } = new List<Tower>();
        public bool WaveInProgress { get; private set; }

        public GameEngine()
        {
            InitializePath();
        }

        private void InitializePath()
        {
            Path.Add(new Point(0, 200));
            Path.Add(new Point(200, 200));
            Path.Add(new Point(200, 400));
            Path.Add(new Point(600, 400));
        }

        public void StartWave()
        {
            WaveInProgress = true;
            GameSettings.Wave++;
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            int enemyCount = 5 + GameSettings.Wave * 2;

            for (int i = 0; i < enemyCount; i++)
            {
                int health = 50 + GameSettings.Wave * 10;
                int speed = 1 + GameSettings.Wave / 5;
                int goldReward = 10 + GameSettings.Wave * 2;

                if (GameSettings.Wave % 5 == 0 && i == enemyCount - 1)
                {
                    health *= 3;
                    goldReward *= 5;
                }

                Enemies.Add(new Enemy(health, speed, Path[0], goldReward));
            }
        }

        public void Update()
        {
            MoveEnemies();
            ProcessTowerAttacks();
            CleanDeadEnemies();
            CheckWaveCompletion();
        }

        private void MoveEnemies()
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = Enemies[i];
                enemy.Move(Path);

                if (enemy.CurrentPathIndex >= Path.Count - 1)
                {
                    Enemies.RemoveAt(i);
                }
            }
        }

        private void ProcessTowerAttacks()
        {
            foreach (var tower in Towers)
            {
                if (!tower.CanAttack()) continue;

                var target = FindTarget(tower);
                if (target != null)
                {
                    AttackEnemy(tower, target);
                }
            }
        }

        private Enemy FindTarget(Tower tower)
        {
            Enemy closestEnemy = null;
            float minDist = tower.Range;

            foreach (var enemy in Enemies)
            {
                if (enemy.Health <= 0) continue;

                float dist = Distance(tower.Position, enemy.Position);
                if (dist <= tower.Range && dist < minDist)
                {
                    minDist = dist;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        private void AttackEnemy(Tower tower, Enemy enemy)
        {
            tower.Attack(enemy);
            if (enemy.Health <= 0)
            {
                GameSettings.Gold += enemy.GoldReward;
            }
        }

        private void CleanDeadEnemies()
        {
            Enemies.RemoveAll(e => e.Health <= 0);
        }

        private void CheckWaveCompletion()
        {
            if (WaveInProgress && Enemies.Count == 0)
            {
                WaveInProgress = false;
            }
        }

        public void AddTower(Point position, string type)
        {
            int cost = GetTowerCost(type);
            if (GameSettings.Gold >= cost)
            {
                GameSettings.Gold -= cost;
                Towers.Add(new Tower(position, type));
            }
        }

        private int GetTowerCost(string type)
        {
            return type switch
            {
                "Arbalest" => 50,
                "ChaosCrystal" => 80,
                _ => 0
            };
        }

        private float Distance(Point a, PointF b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}