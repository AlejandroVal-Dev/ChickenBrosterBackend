namespace Security.Domain.Entities
{
    public class RolePermission
    {
        public int RoleId { get; private set; }
        public int PermissionId { get; private set; }
        public DateTime AssignedAt { get; private set; }

        // Constructor
        public RolePermission(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
            AssignedAt = DateTime.UtcNow;
        }
    
    }
}
