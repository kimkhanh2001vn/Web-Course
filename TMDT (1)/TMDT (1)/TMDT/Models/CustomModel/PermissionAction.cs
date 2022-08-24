namespace TMDT.Models.CustomModel
{
    public class PermissionAction
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string Description { get; set; }

        public bool IsGranted { get; set; }
    }
}