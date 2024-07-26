using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class RoleService
    {
        private AppDBContext _context;
        public RoleService(AppDBContext context)
        {
            _context = context;
        }
        public List<Role> GetRoles()
        {
            return _context.Role.ToList();
        }
        public Role AddRole(Role Role)
        {
            _context.Add(Role);
            _context.SaveChanges();
            return Role;
        }
        public Role DeleteRole(int id)
        {
            var existingRole = _context.Role.Find(id);
            if (existingRole == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingRole);
                _context.SaveChanges();
                return existingRole;
            }
        }
        public Role GetIdRole(int id)
        {
            var role = _context.Role.Find(id);
            if (role == null)
            {
                return null;
            }
            return role;
        }
        public Role UpdateRole(int id, Role updateRole)
        {
            var existingRole = _context.Role.Find(id);
            if (existingRole == null)
            {
                return null;
            }
            existingRole.Name = updateRole.Name;
            _context.Update(existingRole);
            _context.SaveChanges();
            return existingRole;
        }
    }
}
