using Microsoft.Data.SqlClient;
using System.Data;
using userForm.Models;
using userForm.ViewModel;

namespace userForm.Repository
{
    public class UserRepo : IUserRepo
    {
        //AddEdit
        public ResponseMsg AddEditUsers(AddUpdateModel info)
        {
            ResponseMsg ObjResponse = new ResponseMsg();

            var builder = WebApplication.CreateBuilder();
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            if (info.Id == 0)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UserDetailsInsert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = info.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = info.LastName;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = info.Gender;
                    cmd.Parameters.Add("@Dob", SqlDbType.Date).Value = info.Dob;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = info.PhoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = info.Email;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = info.Address;
                    cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = info.CityId;
                    cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = info.StateId;
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = info.CountryId;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = info.PostalCode;
                    cmd.Parameters.Add("@ImageFileName", SqlDbType.VarChar).Value = info.ImageFilename;
                    


                    int iReturn = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (iReturn > 0)
                    {
                        ObjResponse.Code = 200;
                        ObjResponse.Message = "User details submitted successfully";

                        return ObjResponse;
                    }

                    ObjResponse.Code = 500;
                    ObjResponse.Message = "Error! Plese check the validations.";
                    return ObjResponse;

                }
                catch (Exception ex)
                {
                    ObjResponse.Message = ex.Message;
                    return ObjResponse;
                }

            }
            else if (info.Id > 0)
            {
                try
                {
                    var conString = builder.Configuration.GetConnectionString("DefaultConnection");
                    SqlConnection con = new SqlConnection(conString);
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlCommand cmd = new SqlCommand("UpddateBankApi", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = info.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = info.LastName;
                    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = info.Gender;
                    cmd.Parameters.Add("@Dob", SqlDbType.Date).Value = info.Dob;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = info.PhoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = info.Email;
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar).Value = info.Address;
                    cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = info.CityId;
                    cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = info.StateId;
                    cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = info.CountryId;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.VarChar).Value = info.PostalCode;
                    cmd.Parameters.Add("@ImageFileName", SqlDbType.VarChar).Value = info.ImageFilename;


                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = info.Id;

                    int iReturn = cmd.ExecuteNonQuery();
                    con.Close();
                    if (iReturn > 0)
                    {
                        ObjResponse.Code = 200;
                        ObjResponse.Message = "Record Update Successfully.";
                        return ObjResponse;
                    }
                    ObjResponse.Code = 500;
                    ObjResponse.Message = "Record not updated.";
                    return ObjResponse;
                }
                catch (Exception ex)
                {
                    ObjResponse.Message += ex.Message;
                    return ObjResponse;
                }
            }
            else
            {
                ObjResponse.Message = "Data not found";
                ObjResponse.Code = 404;
                return ObjResponse;
            }

        }

        // Delete User
        public ResponseMsg DeleteUser(int id)
        {
            ResponseMsg responseMessage = new ResponseMsg();
            try
            {
                priyanshuContext dbcontext = new priyanshuContext();
                var entity = dbcontext.UserDetails.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
                if (entity != null)
                {
                    entity.IsActive = false;
                    entity.IsDeleted = true;
                    entity.DeletedBy = "Admin";
                    entity.DeletedAt = DateTime.Now;
                    dbcontext.SaveChanges();
                    responseMessage.Message = "Successfully Deleted";
                    responseMessage.Code = 200;
                    return responseMessage;
                }
                responseMessage.Message = "Data not found";
                responseMessage.Code = 404;
                return responseMessage;
            }
            catch (Exception ex)
            {
                responseMessage.Message = ex.Message;
                return responseMessage;
            }
        }


        // GetCity
        public List<ViewCountryModel> GetCity()
        {
            priyanshuContext dbcontex = new priyanshuContext();

            var Obj = (from city in dbcontex.CityTables

                       join state in dbcontex.StateTables on city.StateId equals state.Id
                       join country in dbcontex.CountryTables on state.CountryId equals country.Id
                       select new ViewCountryModel
                       {
                           Id = city.Id,
                           CityName = city.Name,
                           StateName = state.Name,
                           CountryName = country.Name,
                       }
                        ).ToList();
            return Obj;
        }

        //public List<CountryTable> GetCountry()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<StateTable> GetState()
        //{
        //    throw new NotImplementedException();
        //}

        //GetAll
        public List<ViewUserModel> GetUsers()
        {
            priyanshuContext dbcontex = new priyanshuContext();

            var Obj = (from Usr in dbcontex.UserDetails
                       where Usr.IsDeleted == false
                       join cnt in dbcontex.CountryTables on Usr.CountryId equals cnt.Id
                       join state in dbcontex.StateTables on Usr.StateId equals state.Id
                       join city in dbcontex.CityTables on Usr.CityId equals city.Id
                       orderby Usr.Id descending
                       select new ViewUserModel
                       {
                           Id = Usr.Id,
                           FirstName = Usr.FirstName,
                           LastName = Usr.LastName,
                           Gender = Usr.Gender,
                           Dob = Usr.Dob,
                           PhoneNumber = Usr.PhoneNumber,
                           Email = Usr.Email,
                           Address = Usr.Address,
                           CityId = Usr.CityId,
                           CityName = city.Name,
                           StateId = Usr.StateId,
                           StateName = state.Name,
                           CountryId = Usr.CountryId,
                           CountryName = cnt.Name,
                           PostalCode = Usr.PostalCode,
                           ImageFilename = Usr.ImageFilename,
                           CreatedBy = Usr.CreatedBy,
                           CreatedAt = Usr.CreatedAt,
                           UpdatedAt = Usr.UpdatedAt,
                           UpdatedBy = Usr.UpdatedBy,
                       }
                        ).ToList();
            return Obj;
        }

        //GetByID
        public ViewUserModel GetUsersByID(int id)
        {
            priyanshuContext dbcontex = new priyanshuContext();

            var Obj = (from Usr in dbcontex.UserDetails
                       where Usr.IsDeleted == false
                       join cnt in dbcontex.CountryTables on Usr.CountryId equals cnt.Id
                       join state in dbcontex.StateTables on Usr.StateId equals state.Id
                       join city in dbcontex.CityTables on Usr.CityId equals city.Id
                       orderby Usr.Id descending
                       select new ViewUserModel

                       {
                           Id = Usr.Id,
                           FirstName = Usr.FirstName,
                           LastName = Usr.LastName,
                           Gender = Usr.Gender,
                           Dob = Usr.Dob,
                           PhoneNumber = Usr.PhoneNumber,
                           Email = Usr.Email,
                           Address = Usr.Address,
                           CityId = Usr.CityId,
                           CityName = city.Name,
                           StateId = Usr.StateId,
                           StateName = state.Name,
                           CountryId = Usr.CountryId,
                           CountryName = cnt.Name,
                           PostalCode = Usr.PostalCode,
                           ImageFilename = Usr.ImageFilename,
                           CreatedBy = Usr.CreatedBy,
                           CreatedAt = Usr.CreatedAt,
                           UpdatedAt = Usr.UpdatedAt,
                           UpdatedBy = Usr.UpdatedBy,
                       }
                        ).FirstOrDefault();
            return Obj;
        }
    }
}
