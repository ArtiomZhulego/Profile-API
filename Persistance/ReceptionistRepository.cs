using Dapper;
using Domain;
using Domain.Repositories;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Persistance
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private NpgsqlConnection connection;

        public ReceptionistRepository(IConfiguration configuration) 
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Receptionist> CreateAsync(Receptionist _receptionist, CancellationToken token)
        {
            await connection.QueryAsync<Receptionist>($"INSERT INTO public.\"Receptionist\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Email\", \"OfficeId\", \"Photo\")" +
                                                                         $"VALUES (@FirstName,@MiddleName,@LastName,@Email,@OfficeId,@Photo)", _receptionist);

            return _receptionist;
        }

        public async Task DeleteAsync(Guid receptionistId, CancellationToken token)
        {
            await connection.QueryAsync<Receptionist>($"DELETE FROM public.\"Receptionist\" WHERE \"Id\" = @Id",new { Id = receptionistId });
        }

        public async Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var receptionistsList = await connection.QueryAsync<Receptionist>("SELECT * FROM public.\"Receptionist\" ORDER BY \"Id\" ASC\r\n");

            return receptionistsList.ToList();
        }

        public async Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            var receptionist = await connection.QueryAsync<Receptionist>($"SELECT * FROM public.\"Receptionist\" WHERE \"Id\" = @Id", new { Id = receptionistId});

            return receptionist.FirstOrDefault();
        }

        public async Task<Receptionist> UpdateAsync(Guid receptionistId, Receptionist _receptionist, CancellationToken token)
        {
            await connection.QueryAsync<Receptionist>($"UPDATE Receptionist SET \"FistName\" = @FistName" +
                                                                                        $"AND \"MiddleName\" = @MiddleName" +
                                                                                        $"AND \"LastName\" = @LastName" +
                                                                                        $"AND \"Email\" = @Email" +
                                                                                        $"AND \"OfficeId\" = @OfficeId" +
                                                                                        $"AND \"Photo\" = @Photo" +
                                                                                        $"WHERE \"Id\" = @Id",_receptionist);

            return _receptionist;
        }
    }
}
