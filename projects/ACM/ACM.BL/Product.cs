using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
   public class Product : EntityBase, ILoggable
    {

        public Product()
        {

        }

        public Product(int productId)
        {
            ProductId = productId;
        }

        private string _productName;
        public decimal? CurrentPrice { get; set; }
        public string ProductDescription { get; set; }

        public string log() => $"{ProductId}: {ProductName} Detail: {ProductDescription} Status: {EntityState.ToString()}";
        public int ProductId { get; set; }

        public string ProductName {
            get
            {
              
                return _productName.InsertSpaces();
            }

            set
            {
                _productName = value;
            }
                }

        public override string ToString() => ProductName;
        public Order Retrieve(int ProductId)
        {

            return new Order();
        }

        //Saves the current customer. 
        public bool save()
        {
            return true;
        }
        public string Log() => $"{ProductId}: {ProductName} Detail: {CurrentPrice} Status: {EntityState.ToString()}";
        //Retrieves All Customer 
        public List<Order> Retrieve()
        {
            return new List<Order>();
        }

        public override bool Validate()
        {

            var isValid = true;

            if (string.IsNullOrWhiteSpace(ProductName)) isValid = false;
            if (CurrentPrice == null) isValid = false;

            return isValid;
        }
    }
}
