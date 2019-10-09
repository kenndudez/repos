using SampleProjectR.Models;
using SampleProjectR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProjectR.AccountService
{
    interface IAccountRepository : IRepositoryBase<Owners>
    {
        IEnumerable<Account> Create(int count);
        IEnumerable<Account> GetById(int pageIndex, int pageSize);

        IEnumerable<Account> Update(int pageIndex, int pageSize);

      
    }
}

