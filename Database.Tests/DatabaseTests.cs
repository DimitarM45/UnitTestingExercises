namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            database = new Database();
        }

        [TestCase()]
        [TestCase(1)]
        [TestCase(1, 2, 3, 4, 5)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        public void Test_ConstructorShouldSetGivenValuesProperly(params int[] values)
        {
            database = new Database(values);

            int[] expectedCollection = values;
            int[] actualCollection = database.Fetch();

            CollectionAssert.AreEqual(expectedCollection, actualCollection,
                "Constructor should set given values properly!");
        }

        [TestCase()]
        [TestCase(1)]
        [TestCase(1, 2, 3, 4, 5)]
        [TestCase(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16)]
        public void Test_PropertyCountShouldReturnCorrectValue(params int[] values)
        {
            database = new Database(values);

            int expectedCount = values.Length;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount,
                "Property \"Count\" should return correct value!");
        }

        [Test]
        public void Test_MethodAddShouldIncreaseCountWhenAddingElement()
        {
            database.Add(1);

            int expectedCount = 1;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount,
                "Method \"Add\" should increase the value of \"Count\" when adding an element!");
        }

        [Test]
        public void Test_MethodAddShouldThrowExceptionWhenTryingToAdd17thElement()
        {
            database = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.Throws<InvalidOperationException>( () => database.Add(1),
                "Method \"Add\" should throw InvalidOperationException when trying to add a 17th element!");
        }

        [Test]
        public void Test_MethodRemoveShouldDecreaseCountWhenRemovingElement()
        {
            database = new Database(1);

            database.Remove();

            int expectedCount = 0;
            int actualCount = database.Count;

            Assert.AreEqual(expectedCount, actualCount,
                "Method \"Remove\" should decrease the value of \"Count\" when removing an element!");
        }

        [Test]
        public void Test_MethodRemoveShouldThrowExceptionWhenTryingToRemoveElementFromEmptyDatabase()
        {
            Assert.Throws<InvalidOperationException>(() => database.Remove(),
                "Method \"Remove\" should throw InvalidOperationException hen trying to remove an element from an empty database!");
        }

        [Test]
        public void Test_MethodFetchShouldReturnArrayWithGivenValues()
        {
            int[] expectedCollection = new int[] { 1, 2, 3, 4, 5 };

            database = new Database(expectedCollection);

            int[] actualCollection = database.Fetch();

            CollectionAssert.AreEqual(expectedCollection, actualCollection,
                "Method \"Fetch\" should return an array with the given values!");
        }
    }
}
