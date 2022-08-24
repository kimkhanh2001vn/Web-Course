using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TMDT.Models.CustomModel
{
    public class AdvertisementCustomAdmin
    {
        //Similar to Advertisment with SectionName different , feels stupid using this but couldnt map it to Advertisement since its entity
        public int ID { get; set; }
        [DisplayName("Tên khách hàng")]
        public string Customer { get; set; }
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [DisplayName("Hình ảnh")]
        public string ImageLink { get; set; }
        [DisplayName("Đường dẫn")]
        public string RefererLink { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public Nullable<int> ClickCount { get; set; }
        [DisplayName("Ngày bắt đầu")]
        public System.DateTime CreatedDate { get; set; }
        [DisplayName("Ngày kết thúc")]
        public System.DateTime EndDate { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        [DisplayName("Ghi chú")]
        public string Description { get; set; }
        [DisplayName("Khu vực")]
        public string SectionName { get; set; }
        public bool IsDefault { get; set; }
    }
}