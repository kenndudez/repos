using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTester.Models;

namespace WebApiTester.Service
{
    interface IShoppingService
    {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem GetById(Guid id);
        void Remove(Guid id);
    }
}

