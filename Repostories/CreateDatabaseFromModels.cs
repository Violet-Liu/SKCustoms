using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Diagnostics;

namespace Repostories
{
    public class Initializer : CreateDatabaseIfNotExists<SKContext>
    {
        protected override void Seed(SKContext context)
        {
            base.Seed(context);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            #region sysuser
                var _sysUser = new SysUser
                {
                    Name = "superAdmin",
                    TrueName = "系统管理员",
                    Pwd = "12345678".MD5(),
                    Sex = "未知",
                    State = 1,
                    Position = "系统管理员",
                    Creater = 1,
                    CreateTime = DateTime.Now,
                    Remark = "系统初始化管理员，不允许删除"
                };

            var _sysRole = new SysRole
            {
                Name = "超级管理员",
                Description = "管理系统",
                Creater = 1,
                CreateTime = DateTime.Now
            };

            _sysUser.SysRoles.Add(_sysRole);
            
            context.SysUsers.Add(_sysUser);
            #endregion

            #region sysModule

            var _sysModule1 = new SysModule
            {
                Name = "系统管理",
                EnglishName="System Management",
                Url="sys",
                Sort=1,
                Remark="",
                Enable=1,
                CreateTime=DateTime.Now,
                Creater=1
            };   //系统管理菜单


            var user_manager = new SysModule
            {
                Name="用户管理",
                EnglishName="User Manage",
                Url="SysUser",
                Sort=1,
                Enable=1,
                Creater=1,
                CreateTime=DateTime.Now,
                IsLast=1
            };  //用户管理

            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "分配角色", KeyCode = "Allot", IsValid = 1 });
            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "创建", KeyCode = "Create", IsValid = 1 });
            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });
            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            user_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "保存", KeyCode = "Save", IsValid = 1 });

            _sysModule1.SubSysModules.Add(user_manager);

            var role_manage = new SysModule
            {
                Name = "角色组管理",
                EnglishName = "Rike Manage",
                Url = "SysRole",
                Sort = 2,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            }; //角色管理

            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "分配用户", KeyCode = "Allot", IsValid = 1 });
            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "创建", KeyCode = "Create", IsValid = 1 });
            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });
            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            role_manage.SysModuleOperates.Add(new SysModuleOperate { Name = "保存", KeyCode = "Save", IsValid = 1 });

            _sysModule1.SubSysModules.Add(role_manage);

            var module_setting = new SysModule
            {
                Name = "模块维护",
                EnglishName = "Module Setting",
                Url = "SysModule",
                Sort = 3,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            }; //模块管理

            module_setting.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });

            _sysModule1.SubSysModules.Add(module_setting);

            var role_auth = new SysModule
            {
                Name = "角色权限设置",
                EnglishName = "Role Authorize",
                Url = "SysRight",
                Sort = 6,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            role_auth.SysModuleOperates.Add(new SysModuleOperate { Name = "保存", KeyCode = "Save", IsValid = 1 });
            _sysModule1.SubSysModules.Add(role_auth);

            var query_user_auth = new SysModule
            {
                Name = "用户权限查询",
                EnglishName = "Query User Authority",
                Url = "SysGetRightByUser",
                Sort = 7,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };
            query_user_auth.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            _sysModule1.SubSysModules.Add(query_user_auth);


            var query_role_auth = new SysModule
            {
                Name = "角色权限查询",
                EnglishName = "Query Role Authority",
                Url = "SysGetRightByRole",
                Sort = 8,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };
            query_role_auth.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            _sysModule1.SubSysModules.Add(query_role_auth);

            var sys_error_log = new SysModule
            {
                Name = "系统异常日志",
                EnglishName = "Query SysErrorLog",
                Url = "SysError",
                Sort = 9,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };
            sys_error_log.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            _sysModule1.SubSysModules.Add(sys_error_log);

            var sysModule2 = new SysModule
            {
                Name = "备案管理",
                EnglishName = "Record Manage",
                Url = "RecordManage",
                Sort = 2,
                Remark = "",
                Enable = 1,
                CreateTime = DateTime.Now,
                Creater = 1
            };

            var record_manager = new SysModule
            {
                Name = "备案管理",
                EnglishName = "Record Manage",
                Url = "RecordManage",
                Sort = 10,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };
            record_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            record_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "添加", KeyCode = "Add", IsValid = 1 });
            record_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "批量导入", KeyCode = "export", IsValid = 1 });
            record_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            record_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });

            sysModule2.SubSysModules.Add(record_manager);


            var recordM_Grade = new SysModule
            {
                Name = "备案分级",
                EnglishName = "Record Manage Grade",
                Url = "RecordManageGrade",
                Sort = 1,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            recordM_Grade.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            recordM_Grade.SysModuleOperates.Add(new SysModuleOperate { Name = "添加", KeyCode = "Add", IsValid = 1 });
            recordM_Grade.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            recordM_Grade.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });

            sysModule2.SubSysModules.Add(recordM_Grade);

            var carColor = new SysModule
            {
                Name = "车身颜色设置",
                EnglishName = "CarColor",
                Url = "CarColor",
                Sort = 2,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            carColor.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            carColor.SysModuleOperates.Add(new SysModuleOperate { Name = "添加", KeyCode = "Add", IsValid = 1 });
            carColor.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            carColor.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });

            var carType = new SysModule
            {
                Name = "车型设置",
                EnglishName = "carType set",
                Url = "CarType",
                Sort = 3,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            carType.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            carType.SysModuleOperates.Add(new SysModuleOperate { Name = "添加", KeyCode = "Add", IsValid = 1 });
            carType.SysModuleOperates.Add(new SysModuleOperate { Name = "删除", KeyCode = "Delete", IsValid = 1 });
            carType.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Edit", IsValid = 1 });

            sysModule2.SubSysModules.Add(recordM_Grade);
            sysModule2.SubSysModules.Add(carColor);
            sysModule2.SubSysModules.Add(carType);


            var sysModule3 = new SysModule
            {
                Name = "布控管理",
                EnglishName = "Layout Manage",
                Url = "Layout",
                Sort = 3,
                Remark = "",
                Enable = 1,
                CreateTime = DateTime.Now,
                Creater = 1
            };


            var layout_manager = new SysModule
            {
                Name = "布控管理",
                EnglishName = "Layout Manage",
                Url = "Layout",
                Sort = 1,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            layout_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            layout_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "编辑", KeyCode = "Update", IsValid = 1 });
            layout_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "添加", KeyCode = "Add", IsValid = 1 });
            layout_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "批量导入", KeyCode = "Export", IsValid = 1 });
            layout_manager.SysModuleOperates.Add(new SysModuleOperate { Name = "有效设置", KeyCode = "Valid_Set", IsValid = 1 });

            sysModule3.SubSysModules.Add(layout_manager);

            //var layout_manager_query = new SysModule
            //{
            //    Name = "布控查询",
            //    EnglishName = "Layout Manage Query",
            //    Url = "Layout",
            //    Sort = 2,
            //    Enable = 1,
            //    Creater = 1,
            //    CreateTime = DateTime.Now,
            //    IsLast = 1
            //};
            //layout_manager_query.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });

            //sysModule3.SubSysModules.Add(layout_manager_query);


            var sysModule4 = new SysModule
            {
                Name = "信息采集管理",
                EnglishName = "Capture Manage",
                Url = "Capture",
                Sort = 4,
                Remark = "",
                Enable = 1,
                CreateTime = DateTime.Now,
                Creater = 1
            };

            var capture_query = new SysModule
            {
                Name = "信息采集",
                EnglishName = "Capture Query",
                Url = "Capture",
                Sort = 2,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            capture_query.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            capture_query.SysModuleOperates.Add(new SysModuleOperate { Name = "备案", KeyCode = "Record", IsValid = 1 });
            capture_query.SysModuleOperates.Add(new SysModuleOperate { Name = "手动报警", KeyCode = "Alarm", IsValid = 1 });

            sysModule4.SubSysModules.Add(capture_query);


            var sysModule5 = new SysModule
            {
                Name = "报警检查管理",
                EnglishName = "Alarm Manage",
                Url = "Alarm",
                Sort = 5,
                Remark = "",
                Enable = 1,
                CreateTime = DateTime.Now,
                Creater = 1
            };


            var alarm = new SysModule
            {
                Name = "报警检查",
                EnglishName = "Alarm",
                Url = "Alarm",
                Sort = 1,
                Enable = 1,
                Creater = 1,
                CreateTime = DateTime.Now,
                IsLast = 1
            };

            alarm.SysModuleOperates.Add(new SysModuleOperate { Name = "查询", KeyCode = "Query", IsValid = 1 });
            alarm.SysModuleOperates.Add(new SysModuleOperate { Name = "修改", KeyCode = "Modify", IsValid = 1 });

            sysModule5.SubSysModules.Add(alarm);
            #endregion

            context.SysModules.Add(_sysModule1);
            context.SysModules.Add(sysModule2);
            context.SysModules.Add(sysModule3);
            context.SysModules.Add(sysModule4);
            context.SysModules.Add(sysModule5);


            context.CarTypes.Add(new CarType { Name = "小型车" });
            context.CarTypes.Add(new CarType { Name = "大型车" });
            context.CarTypes.Add(new CarType { Name = "超大型车" });


            context.CarColors.Add(new CarColor { Name = "白" });
            context.CarColors.Add(new CarColor { Name = "黑" });
            context.CarColors.Add(new CarColor { Name = "灰" });
            context.CarColors.Add(new CarColor { Name = "银" });
            context.CarColors.Add(new CarColor { Name = "蓝" });
            context.CarColors.Add(new CarColor { Name = "红" });
            context.CarColors.Add(new CarColor { Name = "黄" });
            context.CarColors.Add(new CarColor { Name = "粉" });
            context.CarColors.Add(new CarColor { Name = "绿" });
            context.CarColors.Add(new CarColor { Name = "棕" });

            context.Commite();
            sw.Stop();

            var s1 = sw.Elapsed.TotalMilliseconds;
            sw.Reset();

            sw.Start();
            context.Initialize_Rights();
            context.Initialize_RightOperates();
            sw.Stop();
            var s2 = sw.Elapsed.TotalMilliseconds;



            

        }
    }
}
