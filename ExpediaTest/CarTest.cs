using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;
namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
        public void TestThatCarGetsLocationFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String car1 = "ParkingLot14";
            String car2 = "ParkingLot72";

            Expect.Call(mockDB.getCarLocation(24)).Return(car1);
            Expect.Call(mockDB.getCarLocation(1025)).Return(car2);


            mocks.ReplayAll();

            //USING OBJECT MOTHER FOR TASK 9
            var target = ObjectMother.BMW();
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(24);
            Assert.AreEqual(car1, result);
            result = target.getCarLocation(1025);
            Assert.AreEqual(car2, result);
            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestThatMileageWorks()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            Expect.Call(mockDatabase.Miles).PropertyBehavior();
            int Miles = 1;
            mocks.ReplayAll();
            mockDatabase.Miles = Miles;
            var target = new Car(1);
            target.Database = mockDatabase;
            int miles = target.Mileage;

            Assert.AreEqual(miles, Miles);
            mocks.VerifyAll();
        }


	}
}
