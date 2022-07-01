using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Careers.DataAccess.Models
{
    public class JobPosts
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
        public string Responsibilities { get; set; }
        public bool IsActive { get; set; }
        public int AppliedUsersCount { get; set; }
        [Required]
        public string ApplicationProcedure { get; set; }
        public int IndustryTypeId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
