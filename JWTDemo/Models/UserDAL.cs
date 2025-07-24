using Microsoft.Data.SqlClient;

namespace JWTDemo.Models
{
    public class UserDAL
    {
        private readonly string _connectionString;

        public UserDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public UserModel validateUser(string username, string password)
        {
            UserModel model = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UserLogin", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue ("@Password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    model = new UserModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = Convert.ToString(reader["Username"]),
                        Role = Convert.ToString(reader["Role"])
                    };
                }
                reader.Close();
            }

            return model;
        }
    }
}
