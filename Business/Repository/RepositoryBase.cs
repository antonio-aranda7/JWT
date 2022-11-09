using CompanyEmployees.Contracts;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyEmployees.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DutchContext _dutchContext;

        public RepositoryBase(DutchContext dutchContext)
        {
            _dutchContext = dutchContext;
        }

        public IQueryable<T> FindAll() => 
            _dutchContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _dutchContext.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => _dutchContext.Set<T>().Add(entity);

        public void Update(T entity) => _dutchContext.Set<T>().Update(entity);

        public void Delete(T entity) => _dutchContext.Set<T>().Remove(entity);
    }
}
