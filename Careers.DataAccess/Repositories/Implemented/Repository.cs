using Careers.DataAccess.Repositories.Abstract;
using Careers.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Careers.DataAccess.Repositories.Implemented
{
    public class Repository : IRepository
    {
        protected readonly CareersDbContext DbContext;
        public Repository(CareersDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
