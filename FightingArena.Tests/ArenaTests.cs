namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void Setup()
        {
            arena = new Arena();
        }

        [Test]
        public void Test_ConstructorShouldInitializeWarriorCollectionProperly()
        {
            int expectedCount = 0;
            int actualCount = arena.Count;

            Assert.AreEqual(expectedCount, actualCount,
                "Arena constructor should initialize warrior collection properly (initial Count sould be 0)!");
        }

        [Test]
        public void Test_PropertyWarriorsGetterShouldReturnCorrectValuse()
        {
            Warrior warrior = new Warrior("name", 1, 1);

            arena.Enroll(warrior);

            IReadOnlyCollection<Warrior> expectedCollectionByReferenceOfObjects = new List<Warrior>() { warrior };

            IReadOnlyCollection<Warrior> actualCollection = arena.Warriors;

            CollectionAssert.AreEqual(expectedCollectionByReferenceOfObjects, actualCollection,
                "Property \"Warriors\" getter should return the correct values of the inner warrior collection!");
        }

        [Test]
        public void Test_MethodEnrollShouldAddGivenWarriorToArenaWarriorCollection()
        {
            Warrior expectedWarrior = new Warrior("name", 1, 1);

            arena.Enroll(expectedWarrior);

            IReadOnlyCollection<Warrior> arenaCollection = arena.Warriors;

            Warrior actualWarrior = arenaCollection.FirstOrDefault(w => w.Name == "name");

            Assert.AreEqual(expectedWarrior, actualWarrior,
                "Method \"Enroll\" should add the given warrior to the arena warrior collection!");
        }

        [Test]
        public void Test_MethodEnrollShouldThrowExceptionWhenPassingWarriorWithNameOfExistingWarriorAsArgument()
        {
            arena.Enroll(new Warrior("name", 1, 1));

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("name", 1, 1)),
                "Method \"Enroll\" should throw InvalidOperationException when passing a warrior with the name of an existing warrior as argument!");

        }

        [Test]
        public void Test_MethodFightShouldMakeGivenAttackerAttackGivenDefender()
        {
            Warrior attacker = new Warrior("attacker", 25, 100);
            Warrior defender = new Warrior("defender", 20, 50);

            arena.Enroll(attacker);
            arena.Enroll(defender);

            arena.Fight(attacker.Name, defender.Name);

            int expectedAttackerHP = 80;
            int actualAttackerHP = attacker.HP;

            int expectedDefenderHP = 25;
            int actualDefenderHP = defender.HP;

            Assert.That(expectedAttackerHP == actualAttackerHP && expectedDefenderHP == actualDefenderHP,
                "Method \"Fight\" should make the given attacker attack the given defender (their hp values should be affected)!");
        }

        [TestCase("name")]
        [TestCase("name", "otherName")]
        public void Test_MethodFightShouldThrowExceptionWhenPassingNamesOfOneOrTwoNonExistantWarriors(params string[] warriorNames)
        {
            if (warriorNames.Length > 1)
            {
                arena.Enroll(new Warrior(warriorNames[0], 1, 1));
                arena.Enroll(new Warrior(warriorNames[1], 1, 1));
            }

            else
                arena.Enroll(new Warrior(warriorNames[0], 1, 1));

            Assert.Throws<InvalidOperationException>(() => arena.Fight("attacker", "defender"),
                "Method \"Fight\" should throw InvalidOperationException when passing names of one or tow non existant warriors!");
        }
    }
}
