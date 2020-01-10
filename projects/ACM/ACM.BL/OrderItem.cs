using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class OrderItem
    {
        public OrderItem()
        {

        }

        public OrderItem(int orderItemId)
        {
            OrderItemId = orderItemId;
        }
        public int OrderItemId { get; private set; }
        public int productId{ get; set; }
        
        public decimal? PurchasePrice { get; set; }

        public int quantity { get; set; }

        public OrderItem Retrieve(int OrderItemId)
        {

            return new OrderItem();
        }

        //Saves the current customer. 
        public bool save()
        {
            return true;
        }

        //Retrieves All Customer 
        public List<OrderItem> Retrieve()
        {
            return new List<OrderItem>();
        }

        public bool Validate()
        {

            var isValid = true;

           // if (string.IsNullOrWhiteSpace(ProductName)) isValid = false;
            if (PurchasePrice == null) isValid = false;
            if (productId <= 0) isValid = false;
            if (PurchasePrice <= 0) isValid = false;

            return isValid;
        }
    }
}
