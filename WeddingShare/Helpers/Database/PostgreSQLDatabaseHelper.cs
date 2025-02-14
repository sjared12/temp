using System.Data;
using Npgsql;
using WeddingShare.Enums;
using WeddingShare.Models.Database;

namespace WeddingShare.Helpers.Database
{
    public class PostgreSQLDatabaseHelper : IDatabaseHelper
    {
        private readonly string _connString;
        private readonly ILogger _logger;

        public PostgreSQLDatabaseHelper(IConfigHelper config, ILogger<PostgreSQLDatabaseHelper> logger)
        {
            _connString = config.GetOrDefault("ConnectionStrings:DefaultConnection", "Host=db-postgresql-nyc1-15224-do-user-18934870-0.m.db.ondigitalocean.com;Port=25060;Database=WeddingShare;Username=doadmin;Password=AVNS_Ga1Wpw8OTF8fE4H75-4");
            _logger = logger;

            _logger.LogInformation($"Using PostgreSQL connection string: '{_connString}'");
        }

        #region Gallery
        public async Task<List<GalleryModel>> GetAllGalleries()
        {
            var result = new List<GalleryModel>();
            
            using (var conn = new NpgsqlConnection(_connString))
            {
                var cmd = new NpgsqlCommand(@"SELECT g.*, COUNT(gi.id) AS total,
                        SUM(CASE WHEN gi.state=@ApprovedState THEN 1 ELSE 0 END) AS approved,
                        SUM(CASE WHEN gi.state=@PendingState THEN 1 ELSE 0 END) AS pending
                        FROM galleries g
                        LEFT JOIN gallery_items gi ON g.id = gi.gallery_id
                        GROUP BY g.id ORDER BY name ASC;", conn);
                
                cmd.Parameters.AddWithValue("ApprovedState", (int)GalleryItemState.Approved);
                cmd.Parameters.AddWithValue("PendingState", (int)GalleryItemState.Pending);
                
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new GalleryModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            SecretKey = reader.IsDBNull(2) ? null : reader.GetString(2),
                            TotalItems = reader.GetInt32(3),
                            ApprovedItems = reader.GetInt32(4),
                            PendingItems = reader.GetInt32(5)
                        });
                    }
                }
            }
            return result;
        }

        public async Task<GalleryModel?> AddGallery(GalleryModel model)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                var cmd = new NpgsqlCommand(@"INSERT INTO galleries (name, secret_key) VALUES (@Name, @SecretKey) RETURNING id;", conn);
                cmd.Parameters.AddWithValue("Name", model.Name.ToLower());
                cmd.Parameters.AddWithValue("SecretKey", string.IsNullOrWhiteSpace(model.SecretKey) ? (object)DBNull.Value : model.SecretKey);

                await conn.OpenAsync();
                var galleryId = (int)await cmd.ExecuteScalarAsync();
                
                return await GetGallery(galleryId);
            }
        }

        #endregion

        #region Users
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                var cmd = new NpgsqlCommand(@"SELECT COUNT(id) FROM users WHERE username=@Username AND password=@Password;", conn);
                cmd.Parameters.AddWithValue("Username", username.ToLower());
                cmd.Parameters.AddWithValue("Password", password);

                await conn.OpenAsync();
                var result = (long)await cmd.ExecuteScalarAsync() > 0;
                return result;
            }
        }

        public Task<GalleryModel?> GetGallery(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryModel?> GetGallery(string name)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryModel?> EditGallery(GalleryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WipeGallery(GalleryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WipeAllGalleries()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGallery(GalleryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<GalleryItemModel>> GetAllGalleryItems(int? galleryId, GalleryItemState state = GalleryItemState.All, MediaType type = MediaType.All, GalleryOrder order = GalleryOrder.UploadedDesc, int limit = int.MaxValue, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetPendingGalleryItemCount(int? galleryId = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<GalleryItemModel>> GetPendingGalleryItems(int? galleryId = null)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryItemModel?> GetPendingGalleryItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryItemModel?> GetGalleryItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryItemModel?> GetGalleryItemByChecksum(int galleryId, string checksum)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryItemModel?> AddGalleryItem(GalleryItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<GalleryItemModel?> EditGalleryItem(GalleryItemModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteGalleryItem(GalleryItemModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InitAdminAccount(UserModel model)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                var cmd = new NpgsqlCommand(@"
                    INSERT INTO users (username, password) 
                    VALUES (@Username, @Password)
                    ON CONFLICT (username) DO NOTHING;", conn);
        
                cmd.Parameters.AddWithValue("Username", model.Username.ToLower());
                cmd.Parameters.AddWithValue("Password", model.Password);

                await conn.OpenAsync();
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                await conn.CloseAsync();
        
                return rowsAffected > 0;
            }
        }


        public Task<UserModel?> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserModel?> GetUser(string name)
        {
            using (var conn = new NpgsqlConnection(_connString))
            {
                var cmd = new NpgsqlCommand(@"
                    SELECT id, username, password, role
                    FROM users
                    WHERE username = @Username;", conn);

                cmd.Parameters.AddWithValue("Username", name.ToLower());

                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new UserModel
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Password = reader.GetString(2),
                            Role = reader.GetString(3)
                        };
                    }
                }
                await conn.CloseAsync();
            }
            return null;
        }

        public Task<UserModel?> AddUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel?> EditUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementLockoutCount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetLockout(int id, DateTime? datetime)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetLockoutCount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetMultiFactorToken(int id, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetMultiFactorToDefault()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Import(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Export(string path)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
