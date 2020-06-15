namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_Name", ResourceType = typeof(StaticResoure.Resources))]
        [Required(ErrorMessageResourceName = "Category_RequireName", ErrorMessageResourceType = typeof(StaticResoure.Resources))]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_MetaTitle", ResourceType = typeof(StaticResoure.Resources))]
        public string MetaTitle { get; set; }

        [Display(Name = "Category_ParentID", ResourceType = typeof(StaticResoure.Resources))]
        public long? ParentID { get; set; }

        [Display(Name = "Category_DisplayOrder", ResourceType = typeof(StaticResoure.Resources))]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_SeoTitle", ResourceType = typeof(StaticResoure.Resources))]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_MetaKeywords", ResourceType = typeof(StaticResoure.Resources))]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Category_MetaDescriptions", ResourceType = typeof(StaticResoure.Resources))]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Category_Status", ResourceType = typeof(StaticResoure.Resources))]
        public bool? Status { get; set; }

        [Display(Name = "Category_ShowOnHome", ResourceType = typeof(StaticResoure.Resources))]
        public bool? ShowOnHome { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}
