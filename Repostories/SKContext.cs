using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repostories.ModelConfigurations;
using Infrastruture;
using Repostories.BulkExtensions;

namespace Repostories
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SKContext: DbContext, IQueryableUnitOfWork
    {
        #region construction
        public SKContext():base("SKDatabase")
        {
            //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SKContext, Migrations.Configuration>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SKContext, Migrations.Configuration>());
            Database.SetInitializer(new Initializer());
        }
        #endregion

        #region property
        public virtual DbSet<SysUser> SysUsers { get; set; }

        public virtual DbSet<SysRole> SysRoles { get; set; }


        public virtual DbSet<SysModule> SysModules { get; set; }

        public virtual DbSet<SysRight> SysRights { get; set; }

        public virtual DbSet<SysModuleOperate> SysModuleOperates { get; set; }

        public virtual DbSet<SysRightOperate> SysRightOperates { get; set; }

        public virtual DbSet<RecordManager> RecordManagers { get; set; }

        public virtual DbSet<Capture> Captures { get; set; }

        public virtual DbSet<Layout> Layouts { get; set; }

        public virtual DbSet<Alarm> Alarms { get; set; }

        public virtual DbSet<SysErrorLog> SysErrorLogs { get; set; }

        public virtual DbSet<RecordMGrade> RecordMGrades { get; set; }

        public virtual DbSet<CarColor> CarColors { get; set; }

        public virtual DbSet<CarType> CarTypes { get; set; }

        #endregion


        #region Method

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //don't add 's' to mapping table name
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new SysUserConfiguration());
            modelBuilder.Configurations.Add(new SysRoleConfiguration());
            modelBuilder.Configurations.Add(new SysModuleConfiguration());
            modelBuilder.Configurations.Add(new SysRightConfiguration());
            modelBuilder.Configurations.Add(new SysModuleOperateConfiguration());
            modelBuilder.Configurations.Add(new SysRightOperateConfiguration());
            modelBuilder.Configurations.Add(new RecordManagerConfiguration());
            modelBuilder.Configurations.Add(new CaptureConfiguration());
            modelBuilder.Configurations.Add(new AlarmConfiguration());
            modelBuilder.Configurations.Add(new SysErrorLogConfiguration());
            modelBuilder.Configurations.Add(new LayoutConfiguration());
            modelBuilder.Configurations.Add(new RecordMGradeConfiguration());
            modelBuilder.Configurations.Add(new CarColorConfiguration());
            modelBuilder.Configurations.Add(new CarTypeConfiguration());
        }

        private static string GetConnectionString()
        {
            string conn;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                conn = ConfigPara.TestDBConnection;
            }
            else
            {
                conn = ConfigPara.DBConnection;
            }
            return conn;
        }

        public void Initialize_Rights()
        {
            ExecuteCommand(@"insert into SysRight(SysModuleId,SysRoleId,Rightflag)
	                            select a.Id, b.Id,1 from SysModule a, SysRole b");
        }

        public void Initialize_RightOperates()
        {
            ExecuteCommand(@"insert into SysRightOperate(RightId,SysModuleOperateId,IsValid)
	                            select a.ID,b.ID,1  from SysRight a,SysModuleOperate b");
        }

        public int Commite() => base.SaveChanges();
        public void RollBackChanges() => base.ChangeTracker.Entries().ToList().ForEach(o => o.State = EntityState.Unchanged);

        void IQueryableUnitOfWork.Attach<TEntity>(TEntity entity) => base.Entry<TEntity>(entity).State = EntityState.Unchanged;
        void IQueryableUnitOfWork.DelAttach<TEntity>(TEntity entity) => base.Entry<TEntity>(entity).State = EntityState.Deleted;

        public int ExecuteCommand(string sqlCommand, params object[] parameters) => Database.ExecuteSqlCommand(sqlCommand, parameters);

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)=> Database.SqlQuery<TEntity>(sqlQuery, parameters);

        IDbSet<TEntity> IQueryableUnitOfWork.GetSet<TEntity>() => base.Set<TEntity>();

        void IQueryableUnitOfWork.Modify<TEntity>(TEntity entity) => base.Entry<TEntity>(entity).State = EntityState.Modified;


        void IQueryableUnitOfWork.Modify<TEntity>(TEntity originalEntity, TEntity newEntity)
        {
            if (base.Entry<TEntity>(originalEntity).State != EntityState.Unchanged)
                base.Entry<TEntity>(originalEntity).State = EntityState.Unchanged;
            base.Entry<TEntity>(originalEntity).CurrentValues.SetValues(newEntity);
            
        }

        void IQueryableUnitOfWork.BulkInsert<TEntity>(IEnumerable<TEntity> entities) => this.BulkInsert(entities);

        void IQueryableUnitOfWork.MySqlBulkInsert<TEntity>(IEnumerable<TEntity> entities) => this.MySqlBulkInsert(entities);
        #endregion
    }
}
