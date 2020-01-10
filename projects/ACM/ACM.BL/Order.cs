using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Order : EntityBase, ILoggable
    {

        public Order() : this(0)
        {

        }

        public Order(int orderId)
        {
            OrderId = orderId;
            OrderItems = new List<OrderItem>();
        }

        public int CustomerId { get; set; }

        public int CustomerType { get; set; }
        public DateTimeOffset? OrderDate { get; set; } //Compare Date and Time in different time Zone
        public int OrderId { get; set; }

        public int ShippingAddressId { get; set; }

        public override string ToString() => $"{OrderDate.Value.Date} ({OrderId})";
 
        public List<OrderItem> OrderItems { get; set; }
        public Order Retrieve(int OrderId)
        {

            return new Order();
        }

        //Saves the current customer. 
        public bool save()
        {
            return true;
        }

        //Retrieves All Customer 
        public List<Order> Retrieve()
        {
            return new List<Order>();
        }

        public string Log() => $"{OrderId}: {OrderDate} Detail: {OrderDate} Status: {EntityState.ToString()}";
        public override bool Validate()
        {

            var isValid = true;

            
            if (OrderDate == null) isValid = false;

            return isValid;
        }
    }
}
