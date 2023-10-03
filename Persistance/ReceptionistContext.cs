using Dapper;
using Domain;
using Domain.Repositories;
using Domain.Exceptions;
using Npgsql;

namespace Persistance
{
    public class ReceptionistContext : IReceptionistRepository
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=QwErTy135790;");

        public async Task<Receptionist> CreateAsync(Receptionist _receptionist, CancellationToken token)
        {
            connection.Open();

            var receptionist = (Receptionist) await connection.QueryAsync<Receptionist>($"INSERT INTO Receptionist (FirstName, MiddleName, LastName, Email, OfficeId, Photo) " +
                                                                                        $"VALUES ({_receptionist.FirstName},{_receptionist.MiddleName},{_receptionist.LastName},{_receptionist.Email},{_receptionist.OfficeId},{_receptionist.Photo})");

            connection.Close();

            if (receptionist == null)
            {
                throw new BadRequestException("Receptionist does not created");
            }

            return receptionist;
        }

        public async Task DeleteAsync(Guid receptionistId, CancellationToken token)
        {
            connection.Open();

            await connection.QueryAsync<Receptionist>($"DELETE FROM Receptionist WHERE Receptionist.Id = {receptionistId}");

            connection.Close();
        }

        public async Task<List<Receptionist>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            connection.Open();

            var receptionistsList = (List<Receptionist>) await connection.QueryAsync<Receptionist>("SELECT * FROM Receptionist");

            connection.Close();

            return receptionistsList;
        }

        public async Task<Receptionist> GetByIdAsync(Guid receptionistId, CancellationToken cancellationToken = default)
        {
            connection.Open();

            var receptionist = (Receptionist)await connection.QueryAsync<Receptionist>($"SELECT * FROM Receptionist WHERE Receptionist.Id = {receptionistId}");

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

            var receptionist = (Receptionist) await connection.QueryAsync<Receptionist>($"UPDATE Receptionist SET Receptionist.FistName = {_receptionist.FirstName}" +
                                                                                        $"AND Receptionist.MiddleName = {_receptionist.MiddleName}" +
                                                                                        $"AND Receptionist.LastName = {_receptionist.LastName}" +
                                                                                        $"AND Receptionist.Email = {_receptionist.Email}" +
                                                                                        $"AND Receptionist.OfficeId = {_receptionist.OfficeId}" +
                                                                                        $"AND Receptionist.Photo = {_receptionist.Photo}");

            connection.Close();

            if (receptionist == null)
            {
                throw new BadRequestException("Receptionist does not updated");
            }

            return receptionist;
        }
    }
}
