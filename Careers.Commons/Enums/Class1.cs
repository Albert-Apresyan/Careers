using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Careers.Commons.Enums
{
    public enum IndustryType
    {
        
        [Description("Banking")]
        [Display(Name = "Banking")]
        Banking = 1,
        [Display(Name = "Enterntainment")]
        [Description("Enterntainment")]
        Enterntainment,
        [Display(Name = "IT")]
        [Description("IT")]
        IT,
        [Display(Name = "Product/Project Management")]
        [Description("Product/Project Management")]
        ProductProjectManagement,
}
}
