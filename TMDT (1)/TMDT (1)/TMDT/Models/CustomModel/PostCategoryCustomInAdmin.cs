using System.ComponentModel;

namespace TMDT.Models.CustomModel
{
    public partial class PostCategoryCustomInAdmin
    {
        public int ID { get; set; }

        [DisplayName("Tên menu")]
        public string Name { get; set; }

        public string Alias { get; set; }

        [DisplayName("Là menu con")]
        public string ParentName { get; set; }

        [DisplayName("Thứ tự sắp xếp")]
        public int DisplayOrder { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        public bool? Homeflag { get; internal set; }
    }
}
