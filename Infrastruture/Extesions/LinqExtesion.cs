using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure
{
    public static class LinqExtension
    {

        public static IQueryable<T> SortingAndPaging<T>(this IQueryable<T> source, string sortExpression, bool ascending, int pageNumber, int pageSize) =>
            source.DataSorting<T>(sortExpression, ascending).DataPaging(pageNumber, pageSize);


        public static IQueryable<T> DataSorting<T>(this IQueryable<T> source, string orderExpression, bool ascending)
        {
            //错误查询
            if (string.IsNullOrEmpty(orderExpression))
            {
                return source;
            }
            string sortingDir = string.Empty;
            if (ascending)
                sortingDir = "OrderBy";
            else
                sortingDir = "OrderByDescending";
            ParameterExpression param = Expression.Parameter(typeof(T), orderExpression);
            PropertyInfo pi = typeof(T).GetProperty(orderExpression);
            Type[] types = new Type[2];
            types[0] = typeof(T);
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, orderExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }

        public static IQueryable<T> DataPaging<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 1)
            {
                return source.Take(pageSize);
            }
            else
            {
                return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
        }

        private static IEnumerable<T> GetDynamicOrder<T, TResult>(IEnumerable<T> t, string orderExpression, bool ascending)
        {
            ParameterExpression express = Expression.Parameter(typeof(T), orderExpression);
            PropertyInfo property = typeof(T).GetProperty(orderExpression);
            MemberExpression member = Expression.MakeMemberAccess(express, property);
            Expression<Func<T, TResult>> lambda = Expression.Lambda<Func<T, TResult>>(member, express);
            if (ascending)
                return t.OrderBy(lambda.Compile());
            else
                return t.OrderByDescending(lambda.Compile());
        }


        public static IEnumerable<T> DynamicOrderBy<T>(this IEnumerable<T> t, string orderColumn, bool ascending)
        {
            PropertyInfo p = typeof(T).GetProperty(orderColumn);

            switch (Type.GetTypeCode(p.PropertyType))
            {
                case TypeCode.Boolean:
                    return GetDynamicOrder<T, bool>(t, orderColumn, ascending);
                case TypeCode.Byte:
                    return GetDynamicOrder<T, byte>(t, orderColumn, ascending);
                case TypeCode.Char:
                    return GetDynamicOrder<T, char>(t, orderColumn, ascending);
                case TypeCode.DateTime:
                    return GetDynamicOrder<T, DateTime>(t, orderColumn, ascending);
                case TypeCode.Decimal:
                    return GetDynamicOrder<T, decimal>(t, orderColumn, ascending);
                case TypeCode.Double:
                    return GetDynamicOrder<T, double>(t, orderColumn, ascending);
                case TypeCode.Int16:
                    return GetDynamicOrder<T, Int16>(t, orderColumn, ascending);
                case TypeCode.Int32:
                    return GetDynamicOrder<T, Int32>(t, orderColumn, ascending);
                case TypeCode.Int64:
                    return GetDynamicOrder<T, Int64>(t, orderColumn, ascending);
                case TypeCode.Single:
                    return GetDynamicOrder<T, Single>(t, orderColumn, ascending);
                case TypeCode.String:
                    return GetDynamicOrder<T, string>(t, orderColumn, ascending);
                default:
                    throw new Exception("不能排序" + orderColumn + "类型");
            }
        }

        /// <summary>
        /// 多where子句查询
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <param name="t">实体集合</param>
        /// <param name="source">实体实例</param>
        /// <returns>实体集合</returns>
        public static IEnumerable<TSource> Wheres<TSource>(this IEnumerable<TSource> t, TSource source)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                object value = p.GetValue(source, null);
                object propertyDefaultValue = p.PropertyType.IsValueType ? Activator.CreateInstance(p.PropertyType) : null;
                if (!object.Equals(value, propertyDefaultValue))
                {
                    ParameterExpression express = Expression.Parameter(typeof(TSource), "o");
                    BinaryExpression binary = Expression.Equal(Expression.MakeMemberAccess(express, p), Expression.Constant(value));
                    Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(binary, express);
                    t = t.Where(lambda.Compile());
                }
            }
            return t;
        }

        /// <summary>
        /// 多where子句查询
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <typeparam name="TResult">Expression的返回类型</typeparam>
        /// <param name="t">实体集合</param>
        /// <param name="expression">表达式</param>
        /// <returns>实体集合</returns>
        public static IEnumerable<TSource> Wheres<TSource>(this IEnumerable<TSource> t, Expression<Func<TSource, bool>> expression)
        {
            foreach (Expression e in DivideBinaryExpression(expression.Body))
            {
                object expressionValue = null;
                if ((e as BinaryExpression) != null)
                {
                    BinaryExpression be = e as BinaryExpression;
                    expressionValue = LambdaExpression.Lambda(be.Right).Compile().DynamicInvoke();
                }
                else
                {
                    MethodCallExpression mce = e as MethodCallExpression;
                    expressionValue = LambdaExpression.Lambda(mce.Arguments[0]).Compile().DynamicInvoke();
                }
                if (expressionValue != null)
                {
                    if (!string.IsNullOrEmpty(expressionValue.ToString()))
                        t = t.Where(Expression.Lambda<Func<TSource, bool>>(e, expression.Parameters).Compile());
                }
            }
            return t;
        }

        /// <summary>
        /// 分解表达式树
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private static Stack<Expression> DivideBinaryExpression(Expression expression)
        {
            Stack<Expression> stack = new Stack<Expression>();

            if (expression.NodeType != ExpressionType.AndAlso)
            {
                stack.Push(expression);
            }
            else
            {
                BinaryExpression tree = expression as BinaryExpression;
                while (tree != null && tree.NodeType == ExpressionType.AndAlso)
                {
                    stack.Push(tree.Right);
                    if (tree.Left.NodeType != ExpressionType.AndAlso)
                    {
                        stack.Push(tree.Left);
                    }
                    tree = tree.Left as BinaryExpression;
                }
            }
            return stack;
        }

        /// <summary>
        /// 获取表达式的值(如o.Name==name则得到name的具体值)
        /// </summary>
        /// <param name="member">表达式</param>
        /// <returns>具体值</returns>
        private static object GetExpressionValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
}
