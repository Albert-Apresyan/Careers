using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Careers.Common.Enums
{
    public enum JobIndustries
    {
        [Display(Name = "UI/UX/Graphic Design ")]
        [Description("UI/UX/Graphic Design ")]
        UIUXOrGraphicDesign,
        [Display(Name = "Product/Project Management ")]
        [Description("Product/Project Management ")]
        ProductOrProjectManagement,
        [Display(Name = "Sales/service management")]
        [Description("Sales/service management")]
        SalesOrServiceManagement,
        [Display(Name = "Marketing/Advertising")]
        [Description("Marketing/Advertising")]
        MarketingOrAdvertising,
        [Display(Name = "Human Resources")]
        [Description("Human Resources")]
        HumanResources,
        [Display(Name = "DevOps")]
        [Description("DevOps")]
        DevOps,
        [Display(Name = "Tourism")]
        [Description("Tourism")]
        Tourism,
        [Display(Name = "Foreign Language")]
        [Description("Foreign Language")]
        ForeignLanguage
    }
}