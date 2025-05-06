// The line to execute for database first in the package Manager Console :
// Scaffold-DbContext "server=DELL-JASON\MSSQLSERVER2019;database=EonixWebApi;Integrated Security=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Project Infrastructure
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Data
{
    public class EonixDbContext
    {
        private readonly IConfiguration _configuration;
        public EonixDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("sqlConnection"));

        //public virtual DbSet<Address> Addresses { get; set; }
        //public virtual DbSet<Company> Companies { get; set; }
        //public virtual DbSet<Contact> Contacts { get; set; }
        //public virtual DbSet<ContactRole> ContactRoles { get; set; }
        //public virtual DbSet<Country> Countries { get; set; }

    }
}
