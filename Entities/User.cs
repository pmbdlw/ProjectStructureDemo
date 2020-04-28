using System;
using SqlSugar;
namespace ProjectStructureDemo.Entities
{
    public class User
    {
        public User()
        {
        }
        [SugarColumn(IsPrimaryKey =true)]
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string CountryCode { get; set; }
        public string CellPhone { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
