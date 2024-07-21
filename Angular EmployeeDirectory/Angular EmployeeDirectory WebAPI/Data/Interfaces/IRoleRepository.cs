using Repository.DataConcerns;

namespace Repository.Interfaces
{
    public interface IRoleRepository
    {
        public void AddingRole(Role r);
        public List<Role> DisplayRoles(int locationId);
        public List<Role> FilteringRoles(string[] locations, string[] departments);


        //public void UpdatingARole(int id, Role r);
        //public void DeletingARole(int inputId);
    }
}
