namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [Display(Name = "Tài khoản")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(32)]
        public string Password { get; set; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20)]
        public string GroupID { get; set; }

        [Display(Name = "Họ tên")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Điện thoại")]
        [StringLength(50)]
        public string Phone { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
