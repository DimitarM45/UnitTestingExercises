namespace DatabaseExtended.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ExtendedDatabase;
    using NUnit.Framework;
    using NUnit.Framework.Internal;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        private Person[] testPeople;

        [SetUp]
        public void Setup()
        {
            database = new Database();

            testPeople = new Person[35];

            for (int i = 0; i < testPeople.Length; i++)
                testPeople[i] = new Person(i, ((char)i).ToString());
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(16)]
        public void Test_ConstructorShouldSetExactValuesProperly(int peopleCount)
        {
            Person[] peopleParams = new Person[peopleCount];

            for (int i = 0; i < peopleCount; i++)
                peopleParams[i] = testPeople[i];

            if (peopleCount != 0)
                database = new Database(peopleParams);

            int databaseExpectedCount = peopleCount;
            int databaseActualCount = database.Count;

            Assert.AreEqual(databaseExpectedCount, databaseActualCount,
                "Database constructor should set values properly!");
        }

        [TestCase(17)]
        [TestCase(35)]
        public void Test_ConstructorShouldThrowExceptionWhenPassingMoreThan16ParametersAsArgument(int peopleCount)
        {
            Person[] peopleParams = new Person[peopleCount];

            for (int i = 0; i < peopleCount; i++)
                peopleParams[i] = testPeople[i];

            Assert.Throws<ArgumentException>(() => database = new Database(peopleParams),
                "Database constructor should throw an ArgumentException when passing more than 16 parameters (or an array with a length over 16)!");
        }

        [Test]
        public void Test_PropertyCountShouldIncreaseWhenAddingANewPerson()
        {
            database.Add(new Person(1, "username"));

            int databaseExpectedCount = 1;
            int databaseActualCount = database.Count;

            Assert.AreEqual(databaseExpectedCount, databaseActualCount,
                "Property \"Count\" should increase its value when adding a new person!");    
        }

        [Test]
        public void Test_PropertyCountShouldDecreaseWhenRemovingAPerson()
        {
            database = new Database(new Person(1, "username"));

            database.Remove();

            int databaseExpectedCount = 0;
            int databaseActualCount = database.Count;

            Assert.AreEqual(databaseExpectedCount, databaseActualCount,
                "Property \"Count\" should decrease its value when removing a person!");
        }

        [Test]
        public void Test_MethodAddShouldAddPersonToDatabase()
        {
            Person expectedPerson = new Person(1, "username");

            database = new Database(expectedPerson);

            Person actualPerson = database.FindById(1);

            Assert.AreEqual(expectedPerson, actualPerson,
                "Method \"Add\" should add the given person to the database!");
        }

        [Test]
        public void Test_MethodAddShouldThrowExceptionWhenTryingToAddA17thPerson()
        {
            Person[] peopleParams = new Person[16];

            for (int i = 0; i < 16; i++)
                peopleParams[i] = testPeople[i];

            database = new Database(peopleParams);

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(1, "username")),
                "Method \"Add\" should throw InvalidOperationException when trying to add a 17th person!");
        }

        [Test]
        public void Test_MethodAddShouldThrowExceptionWhenTryingToAddAPersonWithTheSameNameAsAnExistingPerson()
        {
            database = new Database(new Person(1, "username"));

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(2, "username")),
                "Method \"Add\" should throw InvalidOperationException exception when trying to add a person with the same username as another person already in the database!");
        }

        [Test]
        public void Test_MethodAddShouldThrowExceptionWhenTryingToAddAPersonWithTheSameIDAsAnExistingPerson()
        {
            database = new Database(new Person(1, "username"));

            Assert.Throws<InvalidOperationException>(() => database.Add(new Person(1, "different username")),
                "Method \"Add\" should throw InvalidOperationException exception when trying to add a person with the same Id as another person already in the database!");
        }

        [Test]
        public void Test_MethodRemoveShouldRemoveLastPersonFromDatabase()
        {
            database = new Database(new Person(1, "username"));

            database.Remove();

            Assert.Throws<InvalidOperationException>(() => database.FindById(1));
        }

        [Test]
        public void Test_MethodRemoveShouldThrowExceptionWhenTryingToRemoveAPersonFromEmptyDatabase()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove(),
                "Method \"Remove\" should throw InvalidOperationException when trying to remove a person from an empty Database!");
        }

        [Test]
        public void Test_MethodFindByUsernameShouldReturnPersonWithGivenUsername()
        {
            database = new Database(new Person(1, "username"));

            string expectedPersonUsername = "username";

            Person retrievedPerson = database.FindByUsername("username");

            string retrievedPersonUsername = retrievedPerson.UserName;

            Assert.AreEqual(expectedPersonUsername, retrievedPerson.UserName,
                "Method \"FindByUsername\" should return the person with the given username!");
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_MethodFindByUsernameShouldThrowExceptionWhenPassingAnEmptyOrNullUsernameAsArgument(string username)
        {
            database = new Database(new Person(1, "username"));

            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(username),
                "Method \"FindByUsername\" should throw ArgumentNullException when passing empty or null username as argument!");
        }

        [Test]
        public void MethodFindByUsernameShouldThrowExceptionWhenPassingUsernameOfNonExistantPersonAsArgument()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("username"),
                "Method \"FindByUsername\" should throw InvalidOperationException when passing username of non-existant person as argument!");
        }

        [Test]
        public void MethodFindByIdShouldReturnPersonWithGivenId()
        {
            database = new Database(new Person(1, "username"));

            long expectedPersonId = 1;

            Person retrievedPerson = database.FindById(1);

            long actualPersonId = 1;

            Assert.AreEqual(expectedPersonId, actualPersonId,
                "Method \"FindById\" should return the person with the given id!");
        }

        [TestCase(-1)]
        [TestCase(-50)]
        public void MethodFindByIdShouldThrowExceptionWhenPassingANegativeIdAsArgument(long id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(id),
                "Method \"FindById\" should throw ArgumentOutOfRangeException when passing a negative Id as argument!");
        }

        [Test]
        public void MethodFindByIdShouldThrowExceptionWhenPassingIdOfNonExistantPerson()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(1),
                "Method \"FindById\" should throw InvalidOperationException when passing Id of a non-existant person!");
        }
    }
}