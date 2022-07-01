using Careers.DataAccess.ViewModels;
using Careers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Careers.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository
    {
        public Task<int> GetUserAppliedPostsCount(int userId);
        public Task<int> GetUserPostsCount(int userId);
    }
}
