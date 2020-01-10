using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Customer  : EntityBase, ILoggable 
    {
        private string _lastName;
        public List<Address> AddressList { get; set; }
        public Customer(): this(0)
        {

        }

        public Customer(int customerId)
        {
            CustomerId = customerId;
            AddressList = new List<Address>();
        }
  
        public int CustomerId { get; set; }
        public  string EmailAddress { get; set; }
        public string Firstname { get; set; }

        public string Log() => $"{CustomerId}: {FullName} Email: {EmailAddress} Status: {EntityState.ToString()}";
        public string FullName {
            get
            {
               string FullName =  LastName;

                if (!string.IsNullOrWhiteSpace(Firstname))
                {
                    if(!string.IsNullOrWhiteSpace(FullName))
                    {
                        FullName += " , ";
                    }
                    FullName += Firstname;
                }
                return FullName;
            }
        }

        public static int InstanceCount { get; set; } // static is useful for sharing information that belong to the class n it shows it belongs to the class it self 
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        //Validates the customer Data
        public override bool Validate()
        {

            var isValid = true;

            if (string.IsNullOrWhiteSpace(LastName)) isValid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) isValid = false;

            return isValid;
        }

        //Retrieve One Customer 
        public Customer Retrieve(int CustomerId)
        {

            return new Customer();
        }

        //Saves the current customer. 
        public bool save()
        {
            return true;
        }

        //Retrieves All Customer 
        public List<Customer> Retrieve()
        {
            return new List<Customer>();
        }
    }
}
