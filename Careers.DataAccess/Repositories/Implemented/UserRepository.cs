using Careers.DataAccess.Models;
using Careers.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Careers.Models;
using Microsoft.EntityFrameworkCore;
using Careers.DataAccess.ViewModels;

namespace Careers.DataAccess.Repositories.Implemented
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(CareersDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetUserAppliedPostsCount(int userId)
        {
            var userAppliedJobsCount = await DbContext.AppliedJobs
                                .Where(x => x.UserId == userId)
                                .CountAsync();

            return userAppliedJobsCount;
        }
        public async Task<int> GetUserPostsCount(int userId)
        {
            var userJobsCount = await DbContext.Jobs
                                .Where(x => x.UserId == userId)
                                .CountAsync();

            return userJobsCount;
        }
    }
}
