using Concerns;
using Services.Interfaces;
using Repository.Interfaces;
using Repository;

namespace Services
{
    public class RoleServices : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        //Add role starts here
        public void AddRole(Role role)
        {
            _roleRepository.AddingRole(ConvertToDataRole(role));
        }
        //Add role ends here

        //Display all start here
        public List<Role> DisplayAll()
        {
            return ConvertToConcernsRole(_roleRepository.DisplayRoles(0));
        }
        // Display all ends here

        // Display a single role starts here
        public List<Role> DisplayRole(int locationId)
        {
            return ConvertToConcernsRole(_roleRepository.DisplayRoles(locationId));
        }
        // Display a single role ends here

        //Roles filtering
        public List<Role> FilteredRoles(string[] locations, string[] departments)
        {
            return ConvertToConcernsRole(_roleRepository.FilteringRoles(locations, departments));
        }

        // Editing a role starts here
        //public void EditingARole(int id, Role role)
        //{
        //    _roleRepository.UpdatingARole(id, ConvertToDataRole(role));
        //}
        //// Editing a role ends here

        // //Delete a role starts here
        //public void DeleteRole(int inputId)
        //{
        //    _roleRepository.DeletingARole(inputId);
        //}
        // Delete a role ends here

        // Converting from Concerns.Role to Data.DataConcerns.Role
        private static Repository.DataConcerns.Role ConvertToDataRole(Role concernsRole)
        {
            Repository.DataConcerns.Role dataRole = new Repository.DataConcerns.Role();
            dataRole.RolesId = concernsRole.RolesId;
            dataRole.RoleName = concernsRole.RoleName;
            dataRole.RoleDescription = concernsRole.RoleDescription;
            dataRole.LocationId = concernsRole.LocationId;
            dataRole.DepartmentId = concernsRole.DepartmentId;
            return dataRole;
        }
        // Ends here

        // Converting from Data.DataConcerns.Role to Concerns.Role
        private static List<Role> ConvertToConcernsRole(List<Repository.DataConcerns.Role> dataRoles)
        {
            List<Role> concernsRoles = new List<Role>();

            foreach (var dataRole in dataRoles)
            {
                Role concernsRole = new Role();
                concernsRole.RolesId = dataRole.RolesId;
                concernsRole.RoleName = dataRole.RoleName;
                concernsRole.RoleDescription = dataRole.RoleDescription;
                concernsRole.LocationId = dataRole.LocationId;
                concernsRole.LocationName = dataRole.Location.LocationName;
                concernsRole.DepartmentId = dataRole.DepartmentId;
                concernsRole.DepartmentName = dataRole.Department.DepartmentName;
                concernsRoles.Add(concernsRole);
            }
            return concernsRoles;
        }
        // Ends here
    }
}