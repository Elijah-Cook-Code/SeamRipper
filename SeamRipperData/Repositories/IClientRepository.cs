using SeamRipperData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeamRipperData.Repositories
{
    public interface IClientRepository
    {
        Task<List<ClientInfo>> GetClientsAsync();
        Task<ClientInfo?> GetClientByIdAsync(int clientId);
        Task AddClientAsync(ClientInfo client);
        Task UpdateClientAsync(ClientInfo client);
        Task DeleteClientAsync(int clientId);

        // Client Measurements Management
        Task<List<ClientMeasurements>> GetClientMeasurementsAsync();
        Task<List<ClientMeasurements>> GetMeasurementsByClientIdAsync(int clientId);
        Task<ClientMeasurements?> GetMeasurementByIdAsync(int measurementId);

        Task AddClientMeasurementAsync(ClientMeasurements measurement);
        Task UpdateClientMeasurementAsync(ClientMeasurements measurement);
        Task DeleteClientMeasurementAsync(int measurementId);

        Task<List<ClientInfo>> GetClientsWithMeasurementsAsync();

    }
}