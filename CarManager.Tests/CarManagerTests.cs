namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [SetUp]
        public void Setup()
        {
            car = new Car("make", "model", 1, 1);
        }

        [TestCase("m")]
        [TestCase("carMake")]
        public void Test_ConstructorShouldSetMakeProperly(string make)
        {
            car = new Car(make, "model", 1, 1);

            string expectedMake = make;
            string actualMake = car.Make;

            Assert.AreEqual(expectedMake, actualMake,
                "Constructor should set car make properly!");
        }

        [TestCase("m")]
        [TestCase("carModel")]
        public void Test_ConstructorShouldSetModelProperly(string model)
        {
            car = new Car("make", model, 1, 1);

            string expectedModel = model;
            string actualModel = car.Model;

            Assert.AreEqual(expectedModel, actualModel,
                "Constructor should set car model properly!");
        }

        [TestCase(1)]
        [TestCase(50)]
        public void Test_ConstructorShouldSetFuelConsumptionProperly(double fuelConsumption)
        {
            car = new Car("make", "model", fuelConsumption, 1);

            double expectedFuelConsumption = fuelConsumption;
            double actualFuelConsumption = car.FuelConsumption;

            Assert.AreEqual(expectedFuelConsumption, actualFuelConsumption,
                "Constructor should set car fuel consumption properly!");
        }

        [TestCase(1)]
        [TestCase(50)]
        public void Test_ConstructorShouldSetFuelCapacityProperly(double fuelCapacity)
        {
            car = new Car("make", "model", 1, fuelCapacity);

            double expectedFuelCapacity = fuelCapacity;
            double actualFuelCapacity = car.FuelCapacity;

            Assert.AreEqual(expectedFuelCapacity, actualFuelCapacity,
                "Constructor should set car fuel capacity properly!");
        }

        [Test]
        public void Test_ConstructorShouldSetFuelAmountProperly()
        {
            double expectedFuelAmount = 0;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount,
                "Constructor should set car fuel amount properly!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_MakeSetterShouldThrowExceptionWhenPassingNullOrEmptyMakeAsArgument(string make)
        {
            Assert.Throws<ArgumentException>(() => car = new Car(make, "model", 1, 1),
                "Car \"Make\" setter should throw ArgumentException when passing null or empty make as an argument!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_ModelSetterShouldThrowExceptionWhenPassingNullOrEmptyMakeAsArgument(string model)
        {
            Assert.Throws<ArgumentException>(() => car = new Car("make", model, 1, 1),
                "Car \"Model\" setter should throw ArgumentException when passing null or empty model as an argument!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_FuelConsumptionSetterShouldThrowExceptionWhenPassingZeroOrNegativeNumberAsArgument(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() => car = new Car("make", "model", fuelConsumption, 1),
                "Car \"FuelConsumption\" setter should throw ArgumentException when passing zero or a negative number fuel consumption as an argument!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_FuelAmountSetterShouldThrowExceptionWhenPassingZeroOrNegativeNumberAsArgument(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() => car = new Car("make", "model", 1, fuelCapacity),
                "Car \"FuelCapacity\" setter should throw ArgumentException when passing zero or a negative number fuel capacity as an argument!");
        }

        [TestCase(1)]
        [TestCase(100)]
        public void Test_MethodRefuelShouldAddGivenFuelToFuelAmount(double fuelToRefuel)
        {
            car = new Car("make", "model", 1, 100);

            double expectedFinalFuel = car.FuelAmount + fuelToRefuel;

            car.Refuel(fuelToRefuel);

            double actualFinalFuel = car.FuelAmount;

            Assert.AreEqual(expectedFinalFuel, actualFinalFuel,
                "Method \"Refuel\" should add given fuel to fuel amount!");
        }

        [TestCase(2)]
        [TestCase(100)]
        public void Test_MethodRefuelShouldSetFuelAmountToFuelCapacityWhenPassingBiggerFuelAmountThanFuelCapacityAsArgument(double fuelToRefuel)
        {
            car = new Car("make", "model", 1, fuelToRefuel - 1);

            double expectedFinalFuelAmount = car.FuelCapacity;

            car.Refuel(fuelToRefuel);

            double actualFinalFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFinalFuelAmount, actualFinalFuelAmount,
                "Method \"Refuel\" should set fuel amount to fuel capacity when passing a bigger fuel amount than the fuel capacity as argument!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-50)]
        public void Test_MethodRefuelShouldThrowExceptionWhenPassingZeroOrNegativeFuelAsArgument(double fuelToRefuel)
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(fuelToRefuel),
                "Method \"Refuel\" should throw ArgumentException when passing zero or a negative amount of fuel as argument!");
        }

        [TestCase(1)]
        [TestCase(100)]
        public void Test_MethodDriveShouldDecreaseFuelAmountWithNeededFuel(double distance)
        {
            car = new Car("make", "model", 1, 100);

            car.Refuel(100);

            double expectedFinalFuelAmount = car.FuelAmount - (distance / 100 * car.FuelConsumption);

            car.Drive(distance);

            double actualFinalFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFinalFuelAmount, actualFinalFuelAmount,
                "Method \"Drive\" should decrease fuel amaount with needed fuel!");
        }

        [TestCase(101)]
        [TestCase(500)]
        public void Test_MethodDriveShouldThrowExceptionWhenNeededFuelIsMoreThanFuelAmount(double distance)
        {
            car = new Car("make", "model", 100, 100);

            car.Refuel(100);

            Assert.Throws<InvalidOperationException>(() => car.Drive(distance),
                "Method \"Drive\" should throw exception when needed fuel is more than the fuel amount!");
        }
    }
}