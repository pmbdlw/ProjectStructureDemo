using System;
using System.Threading.Tasks;
using ProjectStructureDemo.Entities;
namespace ProjectStructureDemo.IServices
{
    public interface IUserService:IBaseServices<User>
    {
        public Task<string> GetUserNameAsync(string id);
    }
}
