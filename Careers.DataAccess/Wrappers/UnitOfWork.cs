using Careers.DataAccess.Models;
using Microsoft.Extensions.Configuration;
//using Careers.DataAccess.Repositories.Abstract;
//using Careers.DataAccess.Repositories.Implemented;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Careers.DataAccess.Repositories.Abstract;
using Careers.DataAccess.Repositories.Implemented;

namespace Careers.DataAccess.Wrappers
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CareersDbContext dbContext)
        {
            DbContext = dbContext;
        }
        private readonly CareersDbContext DbContext;

        #region Repositories
        private readonly IUserRepository userRepository;
        public IUserRepository UserRepository { get { return userRepository ?? new UserRepository(DbContext);}}
        private readonly IJobRepository jobRepository;
        public IJobRepository JobRepository { get { return jobRepository ?? new JobRepository(DbContext);}}
        #endregion
    }
}
