using System.Collections.Generic;

namespace ANC_Authorize_CustomRequirement.AppCache
{
    public class RolePermissionCache
    {
        //实际在数据库获取与配置
        public static List<string> GetPermissions(string role){
            switch(role){
                case "Administrator":
                    return new List<string>(){ "Index","Privacy" };
                case "Custom":
                    return new List<string>(){ "Index" };
            }
            return new List<string>();
        }
    }
}