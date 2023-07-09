using userForm.Models;
using userForm.ViewModel;

namespace userForm.Repository
{
    public interface IUserRepo
    {
        public List<ViewUserModel> GetUsers();
        public ResponseMsg AddEditUsers(AddUpdateModel info);
        public ViewUserModel GetUsersByID(int id);
        public ResponseMsg DeleteUser(int id);
        //public List<StateTable> GetState();
        public List<ViewCountryModel> GetCity();
        //public List<CountryTable> GetCountry();
    }
}
