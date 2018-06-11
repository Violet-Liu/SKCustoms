using Common;
using Domain;
using Infrastruture;
using Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Services
{
    /*
     * Insert采用的SqlBulkCopy
     * SqlBulkCopy  自增主键也需要插入对象包含这个自增主键字段（不赋值也行）
     * SqlBulkCopy插入datetable和数据库映射并不是按照ColumnName,而是ColumnIndex，特别注意啦
     * EF经过Migration迁移后新增的字段，即使你在model文件里写在中间，数据库也是附加到表最后，所以这里肯定会报错（第二行的规则），所以migration新增的字段请加在model的最后
     */
    public class BulkOperation<TEntity> where TEntity : class, IEntity
    {

        public static void MySqlBulkInsert(IEnumerable<TEntity> models, IRepository<TEntity> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentNullException(nameof(_repository));
            }

            _repository.MySqlBulkInsert(models);
        }

        public static async void MySqlBulkInsertAsync(IEnumerable<TEntity> models, IRepository<TEntity> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentNullException(nameof(_repository));
            }

            try
            {
                await Task.Run(() => _repository.MySqlBulkInsert(models));
            }
            catch (Exception e)   //异步吞掉异常，不然进程奔溃
            {
                var log = new SysErrorLog
                {
                    ErrReferrer = "",
                    ErrSource = e.Source,
                    ErrTime = DateTime.Now
                };
                log.ErrTimestr = log.ErrTime.ToString("yyyyMMdd");
                log.ErrStack = e.StackTrace;
                log.ErrType = SysErrorType.bulkInsert.ToString();
                log.ErrUrl = "/recordmanage/RecordManage/add_batch";
                log.ErrIp = "";
                log.ErrMessage = e.Message;

                using (SKContext context = new SKContext())
                {
                    context.SysErrorLogs.Add(log);
                    context.SaveChanges();
                }

            }
        }

        public static void BulkInsert(IEnumerable<TEntity> models, IRepository<TEntity> _repository) 
        {
            if (_repository == null)
            {
                throw new ArgumentNullException(nameof(_repository));
            }

            _repository.BulkInsert(models);
        }


        public static async void BulkInsertAsync(IEnumerable<TEntity> models, IRepository<TEntity> _repository)
        {
            if (_repository == null)
            {
                throw new ArgumentNullException(nameof(_repository));
            }

            try
            {
                await Task.Run(() => _repository.BulkInsert(models));
            }
            catch (Exception e)   //异步吞掉异常，不然进程奔溃
            {
                var log = new SysErrorLog
                {
                    ErrReferrer = "",
                    ErrSource = e.Source,
                    ErrTime = DateTime.Now
                };
                log.ErrTimestr = log.ErrTime.ToString("yyyyMMdd");
                log.ErrStack = e.StackTrace;
                log.ErrType = SysErrorType.bulkInsert.ToString();
                log.ErrUrl = "/recordmanage/RecordManage/add_batch";
                log.ErrIp = "";
                log.ErrMessage = e.Message;

                using (SKContext context = new SKContext())
                {
                    context.SysErrorLogs.Add(log);
                    context.SaveChanges();
                }
                    
            }
        }
    }
}
