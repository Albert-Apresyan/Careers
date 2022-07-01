using Careers.DataAccess.Models;
using Careers.DataAccess.Repositories.Abstract;
using Careers.DataAccess.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Careers.DataAccess.Repositories.Implemented
{
    public class JobRepository : Repository, IJobRepository
    {
        public JobRepository(CareersDbContext dbContext) : base(dbContext)
        {
        }
        public async Task AddPost(JobPostsModel jobPostsModels)
        {

            JobPosts jobPost = new JobPosts();
            jobPost.Title = jobPostsModels.Title;
            jobPost.Description = jobPostsModels.Description;
            jobPost.ApplicationProcedure = jobPostsModels.ApplicationProcedure;
            jobPost.Requirements = jobPostsModels.Requirements;
            jobPost.Responsibilities = jobPostsModels.Responsibilities;
            jobPost.IsActive = true;
            jobPost.IndustryTypeId = jobPostsModels.IndustryTypeId;
            jobPost.PostedDate = DateTime.Now;
            jobPost.Deadline = jobPost.PostedDate.AddMonths(1);
            jobPost.UserId = jobPostsModels.UserId;
            await DbContext.Jobs.AddAsync(jobPost);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<IndustriesViewModel>> GetAllIndustries()
        {
            var allIndustries = await DbContext.Industries
                                 .Select(a => new IndustriesViewModel
                                 {
                                     Id = a.Id,
                                     Name = a.Name,
                                 })
                                 .ToListAsync();
            return allIndustries;
        }

        public async Task<StaticPagedList<JobPostsModel>> GetAllPosts(string jobSearch, int? industryType, string sortFields = "Title",   int page = 1,int pageSize = 10)
        {
            var allJobPosts = await DbContext.Jobs
                                    .Where(x => x.IsActive
                                           && (string.IsNullOrEmpty(jobSearch) || x.Title.ToLower().Contains(jobSearch.ToLower()) || x.Description.ToLower().Contains(jobSearch.ToLower()))
                                           && (!industryType.HasValue || industryType == x.IndustryTypeId))
                                    .Select(a => new JobPostsModel
                                    {
                                        Id = a.Id,
                                        ApplicationProcedure = a.ApplicationProcedure,
                                        UserId = a.UserId,
                                        PostedDate = a.PostedDate,
                                        Deadline = a.Deadline,
                                        Title = a.Title,
                                        Description = a.Description,
                                        Responsibilities = a.Responsibilities,
                                        Requirements = a.Description,
                                        IndustryTypeId = a.IndustryTypeId,
                                    }).ToListAsync();

            var totalCount = allJobPosts.Count;

            var sort = String.IsNullOrEmpty(sortFields) ? "Title" : sortFields;
            switch (sortFields)
            {
                case "Title_desc":
                    allJobPosts = allJobPosts.OrderByDescending(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Title":
                    allJobPosts = allJobPosts.OrderBy(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Description_desc":
                    allJobPosts = allJobPosts.OrderByDescending(x => x.Description).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Description":
                    allJobPosts = allJobPosts.OrderBy(x => x.Description).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "PostedDate_desc":
                    allJobPosts = allJobPosts.OrderByDescending(x => x.PostedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "PostedDate":
                    allJobPosts = allJobPosts.OrderBy(x => x.PostedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Deadline_desc":
                    allJobPosts = allJobPosts.OrderByDescending(x => x.Deadline).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case "Deadline":
                    allJobPosts = allJobPosts.OrderBy(x => x.Deadline).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }

            
            var allJobPostsPaged = new StaticPagedList<JobPostsModel>(allJobPosts, page, pageSize, totalCount);
            return allJobPostsPaged;
        }

        public async Task<List<JobPostsModel>> GetUserPosts(int userId)
        {
            var posts = await DbContext.Jobs
                        .Where(x => x.UserId == userId)
                        .Select(a => new JobPostsModel
                        {
                            Id = a.Id,
                            ApplicationProcedure = a.ApplicationProcedure,
                            UserId = a.UserId,
                            PostedDate = a.PostedDate,
                            Deadline = a.Deadline,
                            IsActive = a.IsActive,
                            Title = a.Title,
                            Requirements = a.Requirements,
                            Responsibilities= a.Responsibilities,
                            IndustryTypeId = a.IndustryTypeId,
                            Description = a.Description,
                        }).ToListAsync();
            return posts;
        }
        public async Task<AppliedJobs> AddUserAppliedJobs(int userId, int jobId)
        {
            AppliedJobs appliedJob = new AppliedJobs();
            appliedJob.UserId = userId;
            appliedJob.JobId = jobId;
            await DbContext.AppliedJobs.AddAsync(appliedJob);
            await DbContext.SaveChangesAsync();

            return appliedJob;
        }

        public async Task DeactivateJob(int jobId)
        {
            var jobPost = await DbContext.Jobs
                            .Where(x => x.Id == jobId)
                            .SingleOrDefaultAsync();
            jobPost.IsActive = false;
            await DbContext.SaveChangesAsync();
        }

        public async Task ActivateJob(int jobId)
        {
            var jobPost = await DbContext.Jobs
                            .Where(x => x.Id == jobId)
                            .SingleOrDefaultAsync();
            jobPost.IsActive = true;
            await DbContext.SaveChangesAsync();
        }

        public async Task EditJob(int Id,JobPostsModel jobPosts)
        {
            var jobPost = await DbContext.Jobs
                        .Where(x => x.Id == jobPosts.Id && x.UserId == Id)
                        .SingleOrDefaultAsync();

            jobPost.ApplicationProcedure = jobPosts.ApplicationProcedure;
            jobPost.Id = jobPosts.Id;
            jobPost.UserId = Id;
            jobPost.PostedDate = jobPosts.PostedDate;
            jobPost.Deadline = jobPosts.Deadline;
            jobPost.Responsibilities = jobPosts.Responsibilities;
            jobPost.IsActive = jobPosts.IsActive;
            jobPost.Title = jobPosts.Title;
            jobPost.Requirements = jobPosts.Requirements;
            jobPost.IndustryTypeId = jobPosts.IndustryTypeId;
            jobPost.Description = jobPosts.Description;

            await DbContext.SaveChangesAsync();
        }
        public async Task<JobPostsModel> GetUserPostedJob(int userId , int jobId )
        {
            var post = await (from x in DbContext.Jobs
                              where x.UserId == userId && x.Id == jobId
                              select new JobPostsModel
                              {
                                  Id = x.Id,
                                  Requirements = x.Requirements,
                                  Title = x.Title,
                                  Description = x.Description,
                                  ApplicationProcedure = x.ApplicationProcedure,
                                  Responsibilities = x.Responsibilities,
                                  Deadline = x.Deadline,
                                  PostedDate = x.PostedDate,
                              })
                              .FirstOrDefaultAsync();
            return post;
        }

        public async Task<JobPostsModel> GetJobView(int jobId, int userId )
        {
            var jobPostsModel = new JobPostsModel();
            jobPostsModel.AppliedUsersCount = await DbContext.AppliedJobs
                                .Where(x => x.JobId == jobId)
                                .CountAsync();
             
            var post = await DbContext.Jobs
                             .Where(x => x.Id == jobId )
                             .Select(a => new JobPostsModel
                             {
                                 Id = a.Id,
                                 UserId = a.UserId,
                                 Requirements = a.Requirements,
                                 Title = a.Title,
                                 IndustryTypeId = a.IndustryTypeId,
                                 Description = a.Description,
                                 Responsibilities = a.Responsibilities,
                                 ApplicationProcedure = a.ApplicationProcedure,
                                 Deadline = a.Deadline,
                                 PostedDate = a.PostedDate,
                                 AppliedUsersCount = jobPostsModel.AppliedUsersCount,
                             }).SingleOrDefaultAsync();
            post.IsApplied = await DbContext.AppliedJobs
                            .AnyAsync(x=>x.UserId == userId && x.JobId == jobId);
            
            return post;
        }
    }
}
