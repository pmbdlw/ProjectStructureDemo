using System;
using System.Threading.Tasks;
using ProjectStructureDemo.Entities;
namespace ProjectStructureDemo.IRepository
{
    public interface IUserRepository:IRepositoryBase<User>
    {
    }
}
