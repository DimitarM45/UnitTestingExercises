using ExtendedDatabase;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseExtended.Tests
{
    [TestFixture]
    public class PersonTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(200)]
        public void Test_ConstructorShouldSetIdProperly(long id)
        {
            Person person = new Person(id, "username");

            long expectedId = id;
            long actualId = person.Id;

            Assert.AreEqual(expectedId, actualId,
                "Constructor should set the given Id properly!");
        }

        [TestCase("n")]
        [TestCase("username")]
        public void Test_ConstructorShouldSetNameProperly(string username)
        {
            Person person = new Person(1, username);

            string expectedUsername = username;
            string actualUsername = person.UserName;

            Assert.AreEqual(expectedUsername, actualUsername,
                "Constructor should set the given name properly!");
        }
    }
}
