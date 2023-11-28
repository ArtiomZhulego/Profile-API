using Dapper;
using Domain;
using Domain.Repositories;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Persistance
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly NpgsqlConnection _connection;

        public ReceptionistRepository(IConfiguration configuration) 
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Receptionist> CreateAsync(Receptionist receptionist, CancellationToken token)
        {
            await _connection.QueryAsync<Receptionist>($"INSERT INTO public.\"Receptionist\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Email\", \"OfficeId\", \"Photo\", \"AccountId\")" +
                                                                         $"VALUES (@Id,@FirstName,@MiddleName,@LastName,@Email,@OfficeId,@Photo,@AccountId)", receptionist);

            return receptionist;
        }

        public async Task DeleteAsync(Guid receptionistId, CancellationToken token)
        {
            await _connection.QueryAsync<Receptionist>($"DELETE FROM public.\"Receptionist\" WHERE \"Id\" = @Id",new { Id = receptionistId });
        }

        public async Task<IEnumerable<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _connection.QueryAsync<Receptionist>("SELECT * FROM public.\"Receptionist\" ORDER BY \"Id\" ASC\r\n");

        public async Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            var receptionist = await _connection.QueryAsync<Receptionist>($"SELECT * FROM public.\"Receptionist\" WHERE \"Id\" = @Id", new { Id = receptionistId});

            return receptionist.FirstOrDefault();
        }

        public async Task<Receptionist> UpdateAsync(Guid receptionistId, Receptionist receptionist, CancellationToken token)
        {
            await _connection.QueryAsync<Receptionist>($"UPDATE public.\"Receptionist\" SET \"FirstName\" = @FirstName, " +
                                                                                        $"\"MiddleName\" = @MiddleName," +
                                                                                        $"\"LastName\" = @LastName," +
                                                                                        $"\"Email\" = @Email," +
                                                                                        $"\"OfficeId\" = @OfficeId," +
                                                                                        $"\"Photo\" = @Photo," +
                                                                                        $"\"AccountId\" = @AccountId " +
                                                                                        $"WHERE \"Id\" = @Id",receptionist);

            return receptionist;
        }
    }
}
