using System;
using System.Collections.Generic;
using ACM.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void Retrieve()
        {

            //-- Arrange
            var customerRepository = new CustomerRepository();

            var expected = new Customer(1)
            {
                //CustomerId = 1,
                EmailAddress = "fala@gmail.com",
                Firstname = "Frodo",
                LastName = "Baggins"
            };
            //-- Act
            var actual = customerRepository.Retrieve(1);

            //-- Assert

            Assert.AreEqual(expected.Firstname, actual.Firstname);
            //Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void RetrieveExistingWithAddress()
        {

            //-- Arrange
            var customerRepository = new CustomerRepository();

            var expected = new Customer(1)
            {
                //CustomerId = 1,
            EmailAddress = "fala@gmail.com",
            Firstname = "Frodo",
            LastName = "Baggins",
            AddressList = new List<Address>()
                {
            new Address() {
            AddressType = 1,
            StreetLine1 = "Bag End",
            StreetLine2 = "Bagshot row",
            City = "Bobby",
            State = "Shire",
            Country = "Middle Earth",
            PostalCode = "144",

                    },
             new Address()
               {
               AddressType = 2,
                StreetLine1 = "Green Dragon",
                StreetLine2 = "Bywater",
                City = "Bobby",
                State = "Shire",
                Country = "Middle Earth",
                PostalCode = "146",

                    }
                }
            };
            //-- Act
            var actual = customerRepository.Retrieve(1);

            //-- Assert

            Assert.AreEqual(expected.CustomerId, actual.CustomerId);
            Assert.AreEqual(expected.EmailAddress, actual.EmailAddress);
            Assert.AreEqual(expected.Firstname, actual.Firstname);
            Assert.AreEqual(expected.LastName, actual.LastName);
            //Assert.AreEqual(expected, actual);

            for(int i = 0; i < 1; i++)
            {
                Assert.AreEqual(expected.AddressList[i].AddressType, actual.AddressList[i].AddressType);
                Assert.AreEqual(expected.AddressList[i].StreetLine1, actual.AddressList[i].StreetLine1);
                Assert.AreEqual(expected.AddressList[i].City, actual.AddressList[i].City);
                Assert.AreEqual(expected.AddressList[i].State, actual.AddressList[i].State);
                Assert.AreEqual(expected.AddressList[i].Country, actual.AddressList[i].Country);
                Assert.AreEqual(expected.AddressList[i].PostalCode, actual.AddressList[i].PostalCode);
            }
                    
        }
    }
}
