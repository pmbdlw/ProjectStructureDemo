using System;
using ProjectStructureDemo.Entities;
using ProjectStructureDemo.IRepository;
using SqlSugar;
namespace ProjectStructureDemo.Repository
{
    public class UserRepository : RepositoryBase<User>,IUserRepository
    {
        public UserRepository(string connection):base(connection)
        {
        }
    }
}
