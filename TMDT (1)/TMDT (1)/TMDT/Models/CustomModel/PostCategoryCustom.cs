namespace TMDT.Models.CustomModel
{
    public class PostCategoryCustom
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public int ID { get; set; }

        public int ParentID { get; set; }

        public bool HasSub { get; set; }
    }
}