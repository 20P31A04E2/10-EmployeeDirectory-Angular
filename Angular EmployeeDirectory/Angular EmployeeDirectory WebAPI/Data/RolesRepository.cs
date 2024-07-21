using Repository.DataConcerns;
using Repository.Interfaces;

namespace Repository
{
    public class RolesRepository : IRoleRepository
    {
        private readonly EmployeeDBContext _context;
        public RolesRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        // Adding a role starts here

        public List<Role> JoinTables()
        {
            var query = from role in _context.Roles
                        join location in _context.Locations on role.LocationId equals location.LocationId
                        join department in _context.Departments on role.DepartmentId equals department.DepartmentId
                        select new Role
                        {
                            RolesId = role.RolesId,
                            RoleName = role.RoleName,
                            RoleDescription = role.RoleDescription,
                            LocationId = role.LocationId,
                            Location = role.Location,
                            DepartmentId = role.DepartmentId,
                            Department = role.Department,
                        };
            return query.ToList();
        }
        public void AddingRole(Role r)
        {
                _context.Roles.Add(r);
                _context.SaveChanges();
        }
        // Adding a role ends here

        // Displaying all/a single role starts here
        public List<Role> DisplayRoles(int locationId)
        {
            if(locationId == 0)
            return JoinTables();
            else
            return JoinTables().Where(r => r.LocationId == locationId).ToList();
        }
        // Displaying all/a single role ends here

        //Filtering roles
        public List<Role> FilteringRoles(string[] locations, string[] departments)
        {
            var roles = JoinTables().AsQueryable();
            if (locations != null && locations.Length > 0)
            {
                roles = roles.Where(e => locations.Contains(e.Location.LocationName));
            }

            if (departments != null && departments.Length > 0)
            {
                roles = roles.Where(e => departments.Contains(e.Department.DepartmentName));
            }
            return roles.ToList();
        }

        // Updating an role starts here
        public void UpdatingARole(int id, Role role)
        {
                var roleToUpdate = _context.Roles.SingleOrDefault(r => r.RolesId == id);

                if (roleToUpdate is not null)
                {
                    roleToUpdate.RoleName = role.RoleName;
                    roleToUpdate.RoleDescription = role.RoleDescription;
                    roleToUpdate.LocationId = role.LocationId;
                    _context.Roles.Update(roleToUpdate);
                    _context.SaveChanges();
                }
        }

        // Deleting an Employee starts here
        public void DeletingARole(int inputId)
        {

                var role = _context.Roles.SingleOrDefault(r => r.RolesId == inputId);
                if (role is not null)
                {
                    _context.Roles.Remove(role);
                    _context.SaveChanges();
                }
        }
        // Deleting an Employee ends here
    }
}
