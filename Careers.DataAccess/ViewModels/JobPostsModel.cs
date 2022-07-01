using Careers.DataAccess.Models;
using System;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace Careers.DataAccess.ViewModels
{
    public class JobPostsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Title { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Description { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Requirements { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Responsibilities { get; set; }
        public bool IsActive { get; set; }
        public bool IsApplied { get; set; }
        public int AppliedUsersCount { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string ApplicationProcedure { get; set; }
        public int IndustryTypeId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}