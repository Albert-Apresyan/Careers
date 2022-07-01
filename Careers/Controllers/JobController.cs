using Careers.DataAccess.Models;
using Careers.DataAccess.ViewModels;
using Careers.DataAccess.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;


namespace Careers.Controllers
{
    public class JobController : BaseController
    {
        public JobController(SignInManager<User> signInManager,
                                UserManager<User> userManager,
                                IUnitOfWork unitOfWork)
                                : base(signInManager, userManager, unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> PostJob()
        {
            var a = await _unitOfWork.JobRepository.GetAllIndustries();
            ViewBag.Industries = new SelectList(a.ToArray(), "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostJob(JobPostsModel jobPostsModel)
        {
            var user = await _userManager.GetUserAsync(User);
            jobPostsModel.UserId = user.Id;
            await _unitOfWork.JobRepository.AddPost(jobPostsModel);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> GetMyJobs()
        {
            var user = await _userManager.GetUserAsync(User);
            var userjobs = await _unitOfWork.JobRepository.GetUserPosts(user.Id);

            return View(userjobs);
        }
        [Authorize]
        public async Task<IActionResult> GetAllJobs(string jobSearch, int? industryType, string sortFields = "Title",  int page = 1, int pageSize = 10)
        {
            ViewData["titleSort"] = sortFields == "Title" ? "Title_desc" : "Title";
            ViewData["descriptionSort"] = sortFields == "Description" ? "Description_desc" : "Description";
            ViewData["postedDateSort"] = sortFields == "PostedDate" ? "PostedDate_desc" : "PostedDate";
            ViewData["deadlineSort"] = sortFields == "Deadline" ? "Deadline_desc" : "Deadline";
            ViewBag.SortFields = sortFields;
            var industries = await _unitOfWork.JobRepository.GetAllIndustries();
            ViewBag.Industries = new SelectList(industries.ToArray(), "Id", "Name");
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = jobSearch;
            //ViewBag.Industry = industryType;
            var userjobs = await _unitOfWork.JobRepository.GetAllPosts(jobSearch, industryType, sortFields, page, pageSize);

            return Request.IsAjaxRequest()
                ? (IActionResult)PartialView("_JobsList", userjobs)
                : View(userjobs);
        }

        public async Task<IActionResult> Deactivate(int jobId)
        {
            await _unitOfWork.JobRepository.DeactivateJob(jobId);

            return RedirectToAction("GetMyJobs");
        }

        public async Task<IActionResult> Activate(int jobId)
        {
            await _unitOfWork.JobRepository.ActivateJob(jobId);

            return RedirectToAction("GetMyJobs");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int jobId)
        {
            var industries = await _unitOfWork.JobRepository.GetAllIndustries();
            ViewBag.Industries = new SelectList(industries.ToArray(), "Id", "Name");
            var user = await _userManager.GetUserAsync(User);
            var job = await _unitOfWork.JobRepository.GetUserPostedJob(user.Id, jobId);

            return View(job);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobPostsModel jobPosts)
        {
            var user = await _userManager.GetUserAsync(User);
            await _unitOfWork.JobRepository.EditJob(user.Id, jobPosts);

            return RedirectToAction("GetMyJobs");
        }

        public async Task<IActionResult> ViewJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var job = await _unitOfWork.JobRepository.GetJobView(jobId, user.Id);
            
            return View(job);
        }

        public async Task<IActionResult> ApplyForJob(int jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            var a = await _unitOfWork.JobRepository.AddUserAppliedJobs(user.Id, jobId);
            
            return RedirectToAction("ViewJob", "Job", new { jobId = jobId });
        }
    }
}