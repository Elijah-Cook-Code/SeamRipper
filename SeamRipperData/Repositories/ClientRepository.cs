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
        public async Task AddClientAsync(ClientInfo client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateClientAsync(ClientInfo client)
        {
            _context.Clients.Update(client);
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

        public async Task<List<ClientMeasurements>> GetClientMeasurementsAsync()
        {
            return await _context.Measurements.Include(m => m.Client).ToListAsync();
        }

        // Fetch measurements for a specific client
        public async Task<List<ClientMeasurements>> GetMeasurementsByClientIdAsync(int clientId)
        {
            return await _context.Measurements.Where(m => m.ClientId == clientId).ToListAsync();
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

    }
}
