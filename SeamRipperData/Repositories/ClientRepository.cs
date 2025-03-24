using SeamRipperData.Models;
using Microsoft.EntityFrameworkCore;
using SeamRipperData.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamRipperData.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;
        public ClientRepository(ClientContext context)
        {
            _context = context;
        }
        public async Task<List<ClientInfo>> GetClientsAsync()
        {
            return await _context.Clients.Include(c => c.Measurements).ToListAsync();

        }
        public async Task<ClientInfo?> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients.Include(c => c.Measurements)
                                         .FirstOrDefaultAsync(c => c.Id == clientId);
        }
        public async Task<List<ClientInfo>> GetClientsByIdsAsync(List<int> ids)
        {
            return await _context.Clients
                .Include(c => c.Measurements)
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }

        public async Task AddClientAsync(ClientInfo client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateClientAsync(ClientInfo updatedClient)
        {
            var existing = await _context.Clients
                .Include(c => c.Measurements)
                .FirstOrDefaultAsync(c => c.Id == updatedClient.Id);

            if (existing == null) return;

            // Update base info
            existing.Name = updatedClient.Name;
            existing.PhoneNumber = updatedClient.PhoneNumber;
            existing.Notes = updatedClient.Notes;
            existing.Date = updatedClient.Date;

            // Remove existing measurements
            _context.Measurements.RemoveRange(existing.Measurements);

            // Add new measurements
            existing.Measurements = updatedClient.Measurements;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await _context.Clients.Include(c => c.Measurements)
                                               .FirstOrDefaultAsync(c => c.Id == clientId);
            if (client != null)
            {
                _context.Measurements.RemoveRange(client.Measurements);
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ClientInfo>> SearchClientsAsync(string query)
        {
            return await _context.Clients
                .Include(c => c.Measurements)
                .Where(c => EF.Functions.Like(c.Name, $"%{query}%"))
                .ToListAsync();
        }



        public async Task<List<ClientMeasurements>> GetClientMeasurementsAsync()
        {
            return await _context.Measurements.Include(m => m.Client).ToListAsync();
        }

        // Fetch measurements for a specific client
        public async Task<List<ClientMeasurements>> GetMeasurementsByClientIdAsync(int clientId)
        {
            return await _context.Measurements.Where(m => m.ClientId == clientId).ToListAsync();
        }

        public async Task<List<ClientInfo>> GetClientsWithMeasurementsAsync()
        {
            return await _context.Clients
                .Include(c => c.Measurements) // Ensures measurements are loaded
                .ToListAsync();
        }


        public async Task<ClientMeasurements?> GetMeasurementByIdAsync(int measurementId)
        {
            return await _context.Measurements
                .Include(m => m.Client) // clearly include Client to avoid null references
                .FirstOrDefaultAsync(m => m.Id == measurementId);
        }


        // Add a new measurement record
        public async Task AddClientMeasurementAsync(ClientMeasurements measurement)
        {
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();
        }

        // Update an existing measurement
        public async Task UpdateClientMeasurementAsync(ClientMeasurements measurement)
        {
            _context.Measurements.Update(measurement);
            await _context.SaveChangesAsync();
        }

        // Delete a measurement record
        public async Task DeleteClientMeasurementAsync(int measurementId)
        {
            var measurement = await _context.Measurements.FirstOrDefaultAsync(m => m.Id == measurementId);
            if (measurement != null)
            {
                _context.Measurements.Remove(measurement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ClientInfo>> GenerateRandomClientsAsync(int count)
        {
            var random = new Random();
            var clients = new List<ClientInfo>();

            var firstNames = new[] { "Alex", "Jordan", "Taylor", "Sam", "Morgan", "Charlie", "Casey", "Jamie", "Cameron", "Riley" };
            var lastNames = new[] { "Smith", "Johnson", "Lee", "Garcia", "Martinez", "Brown", "Williams", "Anderson", "Clark", "Hall" };

            for (int i = 0; i < count; i++)
            {
                var name = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";

                var client = new ClientInfo
                {
                    Name = name,
                    PhoneNumber = $"555-{random.Next(1000, 9999)}",
                    Notes = "Generated for testing",
                    Date = DateTime.Now,
                    Measurements = new List<ClientMeasurements>
            {
                new ClientMeasurements
                {
                    A_ChestMeasurement = random.Next(30, 50),
                    B_SeatMeasurement = random.Next(30, 50),
                    C_WaistMeasurement = random.Next(30, 50),
                    D_TrouserMeasurement = random.Next(30, 50),
                    E_F_HalfBackMeasurement = random.Next(10, 30),
                    G_H_BackNeckToWaistMeasurement = random.Next(10, 30),
                    G_I_SyceDepthMeasurement = random.Next(10, 30),
                    I_L_SleeveLengthOnePieceMeasurement = random.Next(10, 30),
                    E_I_SleeveLengthTwoPieceMeasurement = random.Next(10, 30),
                    N_InsideLegMeasurement = random.Next(10, 30),
                    P_Q_BodyRiseMeasurement = random.Next(10, 30),
                    R_CloseWristMeasurement = random.Next(5, 15)
                }
            }
                };

                await _context.Clients.AddAsync(client);
                clients.Add(client);
            }

            await _context.SaveChangesAsync();
            return clients;
        }



    }
}
