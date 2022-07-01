using Careers.DataAccess.Models;
using Careers.DataAccess.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace Careers.DataAccess.Repositories.Abstract
{
    public interface IJobRepository : IRepository
    {
        public Task AddPost(JobPostsModel jobPostsModels);
        public Task<List<IndustriesViewModel>> GetAllIndustries();
        public Task<AppliedJobs> AddUserAppliedJobs(int userId, int jobId);
        public Task<List<JobPostsModel>> GetUserPosts(int id);
        public Task<StaticPagedList<JobPostsModel>> GetAllPosts(string jobSearch , int? industryType, string sortFields,  int page,int  pageSize);
        public Task DeactivateJob(int jobId);
        public Task ActivateJob(int jobId);
        public Task EditJob(int Id, JobPostsModel jobPosts );
        public Task<JobPostsModel> GetUserPostedJob(int userId, int jobId);
        public Task<JobPostsModel> GetJobView(int jobId, int userId);
    }
}
