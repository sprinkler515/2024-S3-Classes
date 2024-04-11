namespace _0411_S3_Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character hero = new("Shrek");
            Character villain = new("Dragon");

            hero.NewTraits();
            villain.NewTraits();

            hero.StatusWindow();
            Console.WriteLine();
            villain.StatusWindow();

            Console.WriteLine("\n-----------------------------------\n");

            BattlePhase(hero, villain);
        }

        public class Character(string name)
        {
            public string name = name;
            public int lifePoints = 20;
            public int traitStr = 5;
            public int traitDef = 0;

            public bool IsAlive() => lifePoints > 0;

            public void Attack(Character defender) =>
                defender.lifePoints -= traitStr - defender.traitDef;

            public void NewTraits()
            {
                var rand = new Random();

                lifePoints = 20 + rand.Next() % 10;
                traitStr = 5 + rand.Next() % 5;
                traitDef = rand.Next() % 5;
            }

            public void StatusWindow()
            {
                Console.WriteLine("===================================");
                Console.WriteLine($"Status windows: {name}");
                Console.WriteLine("===================================");
                Console.WriteLine("LP: " + lifePoints);
                Console.WriteLine("Strength: " + traitStr);
                Console.WriteLine("Defense: " + traitDef);
            }
        }

        public static void BattlePhase(Character hero, Character villain)
        {
            var rand = new Random();
            int turn = rand.Next() % 2;
            int phase = 1;
            Character attacker, defender;

            while (hero.IsAlive() && villain.IsAlive())
            {
                int damage;

                attacker = turn % 2 == 0 ? hero : villain;
                defender = turn % 2 != 0 ? hero : villain;

                attacker.Attack(defender);
                damage = attacker.traitStr - defender.traitDef;

                Console.Write($"Phase {phase++}: ");
                Console.WriteLine($"{attacker.name}'s turn to attack");
                if (CriticalHit())
                {
                    damage *= 2;
                    attacker.Attack(defender);
                    Console.Write("CRITICAL HIT! ");
                }
                Console.WriteLine($"{attacker.name} inflict {damage} damage points to {defender.name}");

                if (!defender.IsAlive())
                    Console.WriteLine($"DOWN! {defender.name} has been slain\n");
                else
                    Console.WriteLine($"{defender.name} has {defender.lifePoints}LP left\n");

                turn++;
            }
        }

        public static bool CriticalHit()
        {
            var rand = new Random();
            int criticalValue = rand.Next() % 5;
            int hitValue = rand.Next() % 5;

            return criticalValue == hitValue;
        }
    }
}
