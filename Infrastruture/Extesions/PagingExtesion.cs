using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public static class PagingExtesion
    {
        /// <summary>
        /// 对任意实体集合排序分页
        /// </summary>
        /// <typeparam name="TProperty">排序的属性</typeparam>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageCount">页记录数</param>
        /// <param name="entityList">实体集合</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        public static IEnumerable<TAnyEntity> Paging<TAnyEntity>(this  IEnumerable<TAnyEntity> entityList, int pageIndex, int pageCount, string orderText, bool ascending)
        {
            if (string.IsNullOrEmpty(orderText))
                return entityList.Skip(pageCount * pageIndex).Take(pageCount);
            else
                return entityList.DynamicOrderBy(orderText, ascending).Skip(pageCount * pageIndex).Take(pageCount);
        }

        /// <summary>
        ///  对任意实体集合排序分页
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageCount">页记录数</param>
        /// <param name="entityList">实体集合</param>
        /// <param name="orderText">排序的字段名称</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        public static IEnumerable<TAnyEntity> Paging<TAnyEntity, TProperty>(IEnumerable<TAnyEntity> entityList, int pageIndex, int pageCount, Func<TAnyEntity, TProperty> orderByExpression, bool ascending)
        {
            if (ascending)
                return entityList.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
            else
                return entityList.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
        }
    }
}
 