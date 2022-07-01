using Careers.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Careers.DataAccess.Wrappers
{
    public interface IUnitOfWork
    {
        #region Repositories
        public IUserRepository UserRepository { get; }
        public IJobRepository JobRepository { get; }
        #endregion
    }
}
