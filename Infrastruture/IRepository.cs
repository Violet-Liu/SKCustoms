using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        IUnitOfWork UnitOfWork { get; }

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById<T>(T id);

        void Delete<T>(IEnumerable<T> keyValues);

        bool IsExist(object id);

        IQueryable<TEntity> GetAll();

        TEntity GetById<T>(T id);

        IQueryable<TEntity> GetAll<TProperty>(int pg_index, int pg_size, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending);

        //IQueryable<TEntity> GetAll(int pageIndex, int pageCount, string orderText, bool ascending);

        IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> GetAll<TProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending);

        void BulkInsert(IEnumerable<TEntity> entities);

        void MySqlBulkInsert(IEnumerable<TEntity> entities);



    }
}
