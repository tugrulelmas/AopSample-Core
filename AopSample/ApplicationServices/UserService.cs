using AopSample.Entities;
using System.Collections.Generic;
using System;

namespace AopSample.ApplicationServices
{
    public class UserService : IUserService
    {
        public void Add(User user)
        {

        }

        public IEnumerable<User> GetAll()
        {
            return new List<User> { new User { Name = "John", Email = "test@sample.com" },
                                new User { Name = "John", Email = "test@sample.com" }};
        }
    }
}
