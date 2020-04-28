using System;
using System.Threading.Tasks;
using ProjectStructureDemo.Entities;
using ProjectStructureDemo.IRepository;
using ProjectStructureDemo.IServices;
namespace ProjectStructureDemo.Services
{
    public class UserService : BaseServices<User>,IUserService
    {
        public UserService(IUserRepository repository):base(repository)
        {
            repo = repository;
        }
        public async Task<string> GetUserNameAsync(string id)
        {
            var user = await repo.GetSingleAsync(id);
            if(null != user) {
                return user.UserName;
            }
            return string.Empty;
        }
    }
}
