using Infrastruture;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostories
{
    public interface IQueryableUnitOfWork :IUnitOfWork
    {
        /// <summary>
        /// 设置实体为Modified状态
        /// </summary>
        /// <param name="entity"></param>
        void Modify<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 设置实体为Modified状态，用于实体部分更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="originalEntity"></param>
        /// <param name="newEntity"></param>
        void Modify<TEntity>(TEntity originalEntity, TEntity newEntity) where TEntity : class;


        /// <summary>
        /// 设置实体为UnChanged状态
        /// </summary>
        /// <param name="entity"></param>
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        void DelAttach<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// 执行Sql查询，如存储过程
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sqlQuery"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        /// <summary>
        /// 执行Sql命令
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);

        /// <summary>
        /// 获取实体DbSet实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> GetSet<TEntity>() where TEntity : class;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        void BulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities"></param>
        void MySqlBulkInsert<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
