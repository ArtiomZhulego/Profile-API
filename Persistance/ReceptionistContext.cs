using Dapper;
using Domain;
using Domain.Repositories;
using Domain.Exceptions;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Persistance
{
    public class ReceptionistContext : IReceptionistRepository
    {
        private NpgsqlConnection connection;

        public ReceptionistContext(IConfiguration configuration) 
        {
            connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<Receptionist> CreateAsync(Receptionist _receptionist, CancellationToken token)
        {
            connection.Open();

            var receptionist = await connection.QueryAsync<Receptionist>($"INSERT INTO public.\"Receptionist\" (\"Id\",\"FirstName\", \"MiddleName\", \"LastName\", \"Email\", \"OfficeId\", \"Photo\") " +
                                                                                        $"VALUES (@FirstName,@MiddleName,@LastName,@Email,@OfficeId,@Photo)", _receptionist);

            connection.Close();

            if (receptionist == null)
            {
                throw new BadRequestException("Receptionist does not created");
            }

            return receptionist.FirstOrDefault();
        }

        public async Task DeleteAsync(Guid receptionistId, CancellationToken token)
        {
            connection.Open();

            await connection.QueryAsync<Receptionist>($"DELETE FROM public.\"Receptionist\" WHERE \"Id\" = @Id",new { Id = receptionistId });

            connection.Close();
        }

        public async Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            connection.Open();

            var receptionistsList = (List<Receptionist>) await connection.QueryAsync<Receptionist>("SELECT * FROM public.\"Receptionist\" ORDER BY \"Id\" ASC\r\n");

            connection.Close();

            return receptionistsList;
        }

        public async Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            connection.Open();

            var receptionist = (Receptionist)await connection.QueryAsync<Receptionist>($"SELECT * FROM public.\"Receptionist\" WHERE \"Id\" = @Id", new { Id = receptionistId});

            if (receptionist == null) 
            { 
                throw new ReceptionistNotFoundException(receptionistId);
            }

            connection.Close();

            return receptionist;
        }

        public async Task<Receptionist> UpdateAsync(Guid receptionistId, Receptionist _receptionist, CancellationToken token)
        {
            connection.Open();

            var receptionist = (Receptionist) await connection.QueryAsync<Receptionist>($"UPDATE Receptionist SET \"FistName\" = @FistName" +
                                                                                        $"AND \"MiddleName\" = @MiddleName" +
                                                                                        $"AND \"LastName\" = @LastName" +
                                                                                        $"AND \"Email\" = @Email" +
                                                                                        $"AND \"OfficeId\" = @OfficeId" +
                                                                                        $"AND \"Photo\" = @Photo" +
                                                                                        $"WHERE \"Id\" = @Id",_receptionist);

            connection.Close();

            if (receptionist == null)
            {
                throw new BadRequestException("Receptionist does not updated");
            }

            return receptionist;
        }
    }
}
