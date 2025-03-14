using SeamRipperData.Repositories;
using SeamRipperData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeamRipperData.Services
{
    public class ClientServices
    {
        private readonly IClientRepository _clientRepository;

        public ClientServices(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClientInfo>> GetClientsAsync()
        {
            return await _clientRepository.GetClientsAsync();
        }

        public async Task<ClientInfo?> GetClientWithMeasurementsAsync(int clientId)
        {
            return await _clientRepository.GetClientByIdAsync(clientId);
        }

        public async Task AddClientAsync(string name, string notes, List<ClientMeasurements> measurements)
        {
            var newClient = new ClientInfo
            {
                Name = name,
                Notes = notes,
                Date = DateTime.UtcNow,
                Measurements = measurements
            };

            await _clientRepository.AddClientAsync(newClient);
        }

        public async Task UpdateClientAsync(int clientId, string newName, string newNotes, ClientMeasurements newMeasurements)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientId);
            if (client != null)
            {
                client.Name = newName;
                client.Notes = newNotes;

                if (client.Measurements == null || !client.Measurements.Any())
                {
                    client.Measurements = new List<ClientMeasurements> { newMeasurements };
                }
                else
                {
                    var existingMeasurements = client.Measurements.First();
                    existingMeasurements.A_ChestMeasurement = newMeasurements.A_ChestMeasurement;
                    existingMeasurements.B_SeatMeasurement = newMeasurements.B_SeatMeasurement;
                    existingMeasurements.C_WaistMeasurement = newMeasurements.C_WaistMeasurement;
                    existingMeasurements.D_TrouserMeasurement = newMeasurements.D_TrouserMeasurement;
                    existingMeasurements.E_F_HalfBackMeasurement = newMeasurements.E_F_HalfBackMeasurement;
                    existingMeasurements.E_I_SleeveLengthTwoPieceMeasurement = newMeasurements.E_I_SleeveLengthTwoPieceMeasurement;
                    existingMeasurements.G_H_BackNeckToWaistMeasurement = newMeasurements.G_H_BackNeckToWaistMeasurement;
                    existingMeasurements.G_I_SyceDepthMeasurement = newMeasurements.G_I_SyceDepthMeasurement;
                    existingMeasurements.I_L_SleeveLengthOnePieceMeasurement = newMeasurements.I_L_SleeveLengthOnePieceMeasurement;
                    existingMeasurements.N_InsideLegMeasurement = newMeasurements.N_InsideLegMeasurement;
                    existingMeasurements.P_Q_BodyRiseMeasurement = newMeasurements.P_Q_BodyRiseMeasurement;
                    existingMeasurements.R_CloseWristMeasurement = newMeasurements.R_CloseWristMeasurement;
                }

                await _clientRepository.UpdateClientAsync(client);
            }
        }

        public async Task DeleteClientAsync(int clientId)
        {
            await _clientRepository.DeleteClientAsync(clientId);
        }
        public async Task<List<ClientMeasurements>> GetClientMeasurementsAsync()
        {
            return await _clientRepository.GetClientMeasurementsAsync();
        }

        // Fetch measurements by Client ID
        public async Task<List<ClientMeasurements>> GetMeasurementsByClientIdAsync(int clientId)
        {
            return await _clientRepository.GetMeasurementsByClientIdAsync(clientId);
        }

        // Add a new client measurement
        public async Task AddClientMeasurementAsync(ClientMeasurements measurement)
        {
            await _clientRepository.AddClientMeasurementAsync(measurement);
        }

        // Update an existing client measurement
        public async Task UpdateClientMeasurementAsync(ClientMeasurements measurement)
        {
            await _clientRepository.UpdateClientMeasurementAsync(measurement);
        }

        // Delete a client measurement
        public async Task DeleteClientMeasurementAsync(int measurementId)
        {
            await _clientRepository.DeleteClientMeasurementAsync(measurementId);
        }
    }
}
