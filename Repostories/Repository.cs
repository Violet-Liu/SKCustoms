using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastruture;
using System.Data.Entity;
using Infrastructure;

namespace Repostories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private IQueryableUnitOfWork _unitOfWork;

       //private SKContext _context;

        public virtual IUnitOfWork UnitOfWork { get => this._unitOfWork; }

        //public virtual SKContext Context { get => this._context; }

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

            //this._context = (SKContext)unitOfWork;
        }

        private IDbSet<TEntity> GetSet() => _unitOfWork.GetSet<TEntity>();

        

        public void Insert(TEntity entity) => GetSet().Add(entity);

        public void Update(TEntity entity) => _unitOfWork.Modify<TEntity>(entity);

        public void Delete(TEntity entity)
        {
            _unitOfWork.Attach<IEntity>(entity);
            GetSet().Remove(entity);
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        public IQueryable<TEntity> GetAll() => GetSet();

        public IQueryable<TEntity> GetAll<TProperty>(int pg_index, int pg_size, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
                return GetSet().OrderBy(orderByExpression).Skip(pg_size * (pg_index - 1)).Take(pg_size);
            else
                return GetSet().OrderByDescending(orderByExpression).Skip(pg_size * (pg_index - 1)).Take(pg_size);
        }

        public IQueryable<TEntity> GetAll<TProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> orderByExpression, bool ascending)
        {
            if (ascending)
                return GetSet().Where(expression).OrderBy(orderByExpression).Take(pageCount * (pageIndex-1)).Skip(pageIndex);
            else
                return GetSet().Where(expression).OrderByDescending(orderByExpression).Take(pageCount * (pageIndex-1)).Skip(pageIndex);
        }


        public TEntity GetById<T>(T id) => GetSet().Find(id);

        public IQueryable<TEntity> GetByWhere(Expression<Func<TEntity, bool>> expression) => GetSet().Where(expression);

        public void Delete<T>(IEnumerable<T> keyValues)
        {
            foreach (var item in keyValues)
            {
                TEntity model = GetById(item);

                if (null != model)
                {
                    GetSet().Remove(model);
                }

            }
        }

        public bool IsExist(object id) => null != GetById(id);

        public void BulkInsert(IEnumerable<TEntity> entities)=> _unitOfWork.BulkInsert(entities);

        public void MySqlBulkInsert(IEnumerable<TEntity> entities) => _unitOfWork.MySqlBulkInsert(entities);

        public void DeleteById<T>(T id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _unitOfWork.Attach<IEntity>(entity);

                GetSet().Remove(entity);
            }
        }

         

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _unitOfWork.Attach<IEntity>(entity);
                GetSet().Remove(entity);
            }
        }
    }
}
