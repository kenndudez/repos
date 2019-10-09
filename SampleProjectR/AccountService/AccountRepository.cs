
using SampleProjectR.Context;
using SampleProjectR.Models;
using SampleProjectR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleProjectR.AccountService
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
        {
        }

        public void Create(Owners entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Owners entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Owners> FindByCondition(Expression<Func<Owners, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Update(Owners entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<Owners> IRepositoryBase<Owners>.FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
