
namespace Polymorphism.Exercises
{
    public abstract class GameCharacter
    {
        protected string name;
        protected double health;
        protected double maxHealth;
        protected double attackPower;

        public GameCharacter(string name, double health, double power)
        {
            this.name = name;
            this.maxHealth = health;
            this.health = health;
            this.attackPower = power;
        }

        public virtual double CalculateDamage()
        {
            return attackPower;
        }

        public virtual void TakeDamage(double damage)
        {
            health -= damage;
            if (health < 0) health = 0;
            Console.WriteLine($"💔 {name} أصيب بضرر {damage:F1} | الصحة: {health:F1}");
        }

        public virtual double GetHealth() => health;
        public virtual bool IsAlive() => health > 0;
        public virtual string GetCharacterType() => "شخصية";

        public virtual void DisplayStats()
        {
            Console.WriteLine($"👤 {name} - {GetCharacterType()}");
            Console.WriteLine($"   الصحة: {health:F1}/{maxHealth}");
            Console.WriteLine($"   قوة الهجوم: {attackPower:F1}");
        }
    }

    public class Warrior : GameCharacter
    {
        public Warrior(string name)
            : base(name, 200, 50)
        {
        }

        public override double CalculateDamage()
        {
            return attackPower * 1.5;  // 50% أكثر
        }

        public override void TakeDamage(double damage)
        {
            double mitigated = damage * 0.7;  // يخفف 30% من الضرر
            base.TakeDamage(mitigated);
        }

        public override string GetCharacterType() => "محارب";
    }

    public class Mage : GameCharacter
    {
        public Mage(string name)
            : base(name, 100, 80)
        {
        }

        public override double CalculateDamage()
        {
            return attackPower * 1.2;  // 20% أكثر
        }

        public override string GetCharacterType() => "ساحر";
    }

    public class Archer : GameCharacter
    {
        public Archer(string name)
            : base(name, 150, 40)
        {
        }

        public override double CalculateDamage()
        {
            return attackPower * 1.3;  // 30% أكثر
        }

        public override string GetCharacterType() => "رامي";
    }

    public class BattleSystem
    {
        public void StartBattle(GameCharacter char1, GameCharacter char2)
        {
            Console.WriteLine($"\n⚔️  معركة بين {char1} و {char2}!");

            int turn = 1;
            while (char1.IsAlive() && char2.IsAlive())
            {
                Console.WriteLine($"\n🔄 الدور {turn}:");

                // الشخصية الأولى تهاجم
                double damage = char1.CalculateDamage();
                Console.WriteLine($"💥 {char1} يهاجم!");
                char2.TakeDamage(damage);

                if (!char2.IsAlive()) break;

                // الشخصية الثانية تهاجم
                damage = char2.CalculateDamage();
                Console.WriteLine($"💥 {char2} يهاجم!");
                char1.TakeDamage(damage);

                turn++;
            }

            Console.WriteLine("\n🏆 انتهت المعركة!");
            if (char1.IsAlive())
                Console.WriteLine($"✅ {char1} انتصر!");
            else
                Console.WriteLine($"✅ {char2} انتصر!");
        }
    }
}
