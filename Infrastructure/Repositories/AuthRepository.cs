using Domain.Repositories;
using Infrastructure.Common;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EntrepriseDatabaseSettings _dbSettings;
        private readonly MySqlConnection _db;

        public AuthRepository(IOptions<EntrepriseDatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            _db = new MySqlConnection(_dbSettings.ConnectionString);
        }

        public Task<int> Login(string username, string password)
        {
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "SELECT emp_id FROM auth WHERE username = @username AND password = @password";
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var id = reader.GetInt32("emp_id");
                _db.Close();
                return Task.FromResult(id);
            }
            else
            {
                _db.Close();
                return Task.FromResult(-1);
            }
        }
    }
}
