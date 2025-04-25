using System.Drawing;

namespace MagesLastStand
{
    public class Tower
    {
        public Point Position { get; }
        public int Range { get; }
        public int Damage { get; }
        public int AttackCooldown { get; private set; }
        public int AttackSpeed { get; }
        public string Type { get; }

        public Tower(Point position, string type)
        {
            Position = position;
            Type = type;

            switch (type)
            {
                case "Arbalest":
                    Range = 150;
                    Damage = 25;
                    AttackSpeed = 30;
                    break;
                case "ChaosCrystal":
                    Range = 120;
                    Damage = 35;
                    AttackSpeed = 45;
                    break;
            }
        }

        public bool CanAttack()
        {
            if (AttackCooldown <= 0)
            {
                AttackCooldown = AttackSpeed;
                return true;
            }
            AttackCooldown--;
            return false;
        }

        public void Attack(Enemy enemy)
        {
            System.Diagnostics.Debug.WriteLine($"[{Type}] Атакует врага! Урон: {Damage}, Здоровье врага: {enemy.Health}->{enemy.Health - Damage}");
            enemy.Health -= Damage;
        }
    }
}
