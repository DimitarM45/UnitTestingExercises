namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private static Warrior warrior;

        [SetUp]
        public void Setup()
        {
            warrior = new Warrior("name", 1, 1);
        }

        [TestCase("n")]
        [TestCase("name")]
        public void Test_ConstructorShouldSetNameProperly(string name)
        {
            string expectedName = name;

            warrior = new Warrior(name, 1, 1);

            string actualName = warrior.Name;

            Assert.AreEqual(expectedName, actualName,
                "Warrior constructor should set name properly!");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Test_ConstructorShouldThrowExceptionWhenPassingNullEmptyOrWhitespaceNameAsArgument(string name)
        {
            Assert.Throws<ArgumentException>(() => warrior = new Warrior(name, 1, 1),
                "Warrior constructor should throw ArgumentException when passing empty, null or whitespace as argument");
        }

        [TestCase(1)]
        [TestCase(50)]
        public void Test_ConstructorShouldSetDamageProperly(int damage)
        {
            int expectedDamage = damage;

            warrior = new Warrior("name", damage, 1);

            int actualDamage = warrior.Damage;

            Assert.AreEqual(expectedDamage, actualDamage,
                "Warrior constructor should set damage properly!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_ConstructorShouldThrowExceptionWhenPassingZeroOrNegativeDamageAsArgument(int damage)
        {
            Assert.Throws<ArgumentException>(() => warrior = new Warrior("name", damage, 1),
                "Warrior constructor should throw ArgumentException when passing zero or a negative number as argument!");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(50)]
        public void Test_ConstructorShouldSetHPProperly(int hp)
        {
            int expectedHp = hp;

            warrior = new Warrior("name", 1, hp);

            int actualHp = warrior.HP;

            Assert.AreEqual(expectedHp, actualHp,
                "Warrior constructor should set HP properly!");
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_ConstructorShouldThrowExceptionWhenPassingNegativeHPAsArgument(int hp)
        {
            Assert.Throws<ArgumentException>(() => warrior = new Warrior("name", 1, hp),
                "Warrior constructor should throw ArgumentException when passing negative HP as argument!");
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(99)]
        [TestCase(100)]
        public void Test_MethodAttackShouldLowerCurrentWarriorHPWithGivenWarriorDamage(int warriorDamage)
        {
            warrior = new Warrior("name", 10, 100);

            Warrior otherWarrior = new Warrior("name", warriorDamage, 100);

            int expectedHP = warrior.HP - warriorDamage;

            warrior.Attack(otherWarrior);

            int actualHP = warrior.HP;

            Assert.AreEqual(expectedHP, actualHP,
                "Method \"Attack\" should lower HP with the given warrior's damage!");
        }

        [TestCase(31)]
        [TestCase(50)]
        public void Test_MethodAttackShouldSetGivenWarriorHPToZeroWhenCurrentWarriorDamageIsBiggerThanGivenWarriorHP(int warriorHP)
        {
            warrior = new Warrior("name", 100, 100);

            Warrior otherWarrior = new Warrior("name", 1, warriorHP);

            warrior.Attack(otherWarrior);

            int expectedHP = 0;
            int actualdHP = otherWarrior.HP;

            Assert.AreEqual(expectedHP, actualdHP,
                "Method \"Attack\" should set given warrior HP to zero when the current warrior's damage is bigger than the given warrior's HP!");
        }

        [TestCase(50, 1)]
        [TestCase(50, 49)]
        [TestCase(50, 50)]
        public void Test_MethodAttackShouldLowerGivenWarriorHPByCurrentWarriorDamage(int warriorHP, int warriorDamage)
        {
            warrior = new Warrior("name", warriorDamage, 100);

            Warrior otherWarrior = new Warrior("name", 1, warriorHP);

            int expectedHP = warriorHP - warrior.Damage;

            warrior.Attack(otherWarrior);

            int actualHP = otherWarrior.HP;

            Assert.AreEqual(expectedHP, actualHP,
                "Method \"Attack\" should lower the given warrior's HP by the current warrior's damage!");
        }

        [TestCase(1)]
        [TestCase(15)]
        [TestCase(29)]
        [TestCase(30)]
        public void Test_MethodAttackShouldThrowExceptionWhenCurrentWarriorHPIsLowerOrEqualToMinimumAttackHP(int warriorHP)
        {
            warrior = new Warrior("name", 1, warriorHP);

            Warrior otherWarrior = new Warrior("name", 1, 1);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(otherWarrior),
                "Method \"Attack\" should throw InvalidOperationException when the current warrior's HP is lower or equal to the minimum attack HP (30)!");
        }

        [TestCase(1)]
        [TestCase(15)]
        [TestCase(29)]
        [TestCase(30)]
        public void Test_MethodAttackShouldThrowExceptionWhenGivenWarriorHPIsLowerOrEqualToMinimumAttackHP(int warriorHP)
        {
            warrior = new Warrior("name", 1, 31);

            Warrior otherWarrior = new Warrior("name", 1, warriorHP);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(otherWarrior),
                "Method \"Attack\" should throw InvalidOperationException when the given warrior's HP is lower or equal to the minimum attak HP (30)!");
        }

        [TestCase(31, 50)]
        [TestCase(40, 50)]
        [TestCase(49, 50)]
        public void Test_MethodAttackShouldThrowExceptionWhenCurrentWarriorHPIsLowerThanGivenWarriorDamage(int warriorHP, int warriorDamage)
        {
            warrior = new Warrior("name", 1, warriorHP);

            Warrior otherWarrior = new Warrior("name", warriorDamage, warriorHP + 1);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(otherWarrior),
                "Method \"Attack\" should throw InvalidOperationException when the current warrior's HP is lower than the given warrior's damage!");
        }
    }
}