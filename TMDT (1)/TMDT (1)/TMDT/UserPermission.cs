//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TMDT
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPermission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserPermission()
        {
            this.UserGrantPermissions = new HashSet<UserGrantPermission>();
        }
    
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public string BusinessId { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual UserBusiness UserBusiness { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserGrantPermission> UserGrantPermissions { get; set; }
    }
}