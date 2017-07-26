using AopSample.Entities;
using System.Collections.Generic;

namespace AopSample.ApplicationServices
{
    public interface IUserService : IService
    {
        IEnumerable<User> GetAll();

        void Add(User user);
    }
}
