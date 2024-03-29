﻿using System;
using ACM.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void RetrieveTest()
        {
            //-- Arrange
            var productRepository = new ProductRepository();

            var expected = new Product(2)
            {
                ProductName = "Sunflower",
                ProductDescription = "Assorted Size Set of 4 Bright Yellow",
                CurrentPrice = 15.96M

            };
            //-- Act 
            var actual = productRepository.Retrieve(2);

            //-- Assert
            Assert.AreEqual(expected.CurrentPrice, actual.CurrentPrice);
            Assert.AreEqual(expected.ProductDescription, actual.ProductDescription);
            Assert.AreEqual(expected.ProductName, actual.ProductName);
        }

        [TestMethod]
        public void SaveTestValid()


        {
            //-- Arrange
            var productRepository = new ProductRepository();

            var updatedProduct = new Product(2)
            {
                ProductName = "Sunflower",
                ProductDescription = "Assorted Size Set of 4 Bright Yellow",
                CurrentPrice = 18M,
                HasChanges = true

            };
            //-- Act 
            var actual = productRepository.save(updatedProduct);

            //-- Assert
            Assert.AreEqual(true, actual
);
            
        }

        public void SaveTestMissingValid()


        {
            //-- Arrange
            var productRepository = new ProductRepository();

            var updatedProduct = new Product(2)
            {
                ProductName = "Sunflower",
                ProductDescription = "Assorted Size Set of 4 Bright Yellow",
                CurrentPrice = null,
                HasChanges = true

            };
            //-- Act 
            var actual = productRepository.save(updatedProduct);

            //-- Assert
            Assert.AreEqual(false, actual);

        }
    }
}
