using SeamRipperData.Repositories;
using SeamRipperData.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Numerics;
using SeamRipperData.Dtos;

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
            return await _clientRepository.GetClientsWithMeasurementsAsync(); // Ensure it fetches measurements
        }


        public async Task<ClientInfo?> GetClientWithMeasurementsAsync(int clientId)
        {
            return await _clientRepository.GetClientByIdAsync(clientId);
        }

        public async Task AddClientAsync(string name, string phone, string notes, List<ClientMeasurements> measurements)
        {
            var newClient = new ClientInfo
            {
                Name = name,
                PhoneNumber = phone, // ✅ make sure this is here
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


                if (client.Measurements == null || client.Measurements.Count == 0)
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

        public async Task GenerateRandomClientsAsync(int numberOfClients = 5)
        {
            var existingClients = await _clientRepository.GetClientsAsync();

            var newClients = new List<ClientInfo>();

            var firstNames = new[] { "Alex", "Jordan", "Taylor", "Sam", "Morgan", "Charlie", "Casey" };
            var lastNames = new[] { "Smith", "Johnson", "Lee", "Garcia", "Martinez", "Brown", "Williams" };

            for (int i = 0; i < numberOfClients; i++)
            {
                string randomName;
                int attempts = 0;
                do
                {
                    randomName = $"{firstNames[Random.Shared.Next(firstNames.Length)]} {lastNames[Random.Shared.Next(lastNames.Length)]}";
                    attempts++;
                }
                while (existingClients.Any(c => c.Name.Equals(randomName, StringComparison.OrdinalIgnoreCase)) && attempts < 5);

                if (attempts >= 5)
                    continue; // clearly avoid infinite loop

                var client = new ClientInfo
                {
                    Name = randomName,
                    Notes = "Auto-generated client",
                    Date = DateTime.UtcNow,
                    Measurements =
                    [
                        new ClientMeasurements
                {
                    A_ChestMeasurement = Random.Shared.Next(32, 50),
                    C_WaistMeasurement = Random.Shared.Next(28, 40),
                    D_TrouserMeasurement = Random.Shared.Next(30, 45),
                    B_SeatMeasurement = Random.Shared.Next(30, 45),
                    E_F_HalfBackMeasurement = Random.Shared.Next(12, 18),
                    G_H_BackNeckToWaistMeasurement = Random.Shared.Next(15, 25),
                    G_I_SyceDepthMeasurement = Random.Shared.Next(6, 10),
                    I_L_SleeveLengthOnePieceMeasurement = Random.Shared.Next(20, 30),
                    E_I_SleeveLengthTwoPieceMeasurement = Random.Shared.Next(20, 30),
                    N_InsideLegMeasurement = Random.Shared.Next(28, 36),
                    P_Q_BodyRiseMeasurement = Random.Shared.Next(8, 14),
                    R_CloseWristMeasurement = Random.Shared.Next(6, 10)
                }
                    ]
                };

                newClients.Add(client);
            }

            foreach (var client in newClients)
                await _clientRepository.AddClientAsync(client);
        }

        public async Task ClearClientMeasurementAsync(int measurementId)
        {
            var measurement = await _clientRepository.GetMeasurementByIdAsync(measurementId);
            if (measurement is not null)
            {
                measurement.A_ChestMeasurement = 0;
                measurement.B_SeatMeasurement = 0;
                measurement.C_WaistMeasurement = 0;
                measurement.D_TrouserMeasurement = 0;
                measurement.E_F_HalfBackMeasurement = 0;
                measurement.G_H_BackNeckToWaistMeasurement = 0;
                measurement.G_I_SyceDepthMeasurement = 0;
                measurement.I_L_SleeveLengthOnePieceMeasurement = 0;
                measurement.E_I_SleeveLengthTwoPieceMeasurement = 0;
                measurement.N_InsideLegMeasurement = 0;
                measurement.P_Q_BodyRiseMeasurement = 0;
                measurement.R_CloseWristMeasurement = 0;

        await _clientRepository.UpdateClientMeasurementAsync(measurement);
            }
        }


        public List<ClientInfo> MapDtosToClients(List<ClientInfoDto> dtos)
        {
            return dtos.Select(dto =>
            {
                var client = new ClientInfo
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    PhoneNumber = dto.PhoneNumber,
                    Date = dto.Date,
                    Notes = dto.Notes,
                    Measurements = new List<ClientMeasurements>()
                };

                if (dto.Measurements != null)
                {
                    foreach (var m in dto.Measurements)
                    {
                        var measurement = new ClientMeasurements
                        {
                            Id = m.Id,
                            ClientId = dto.Id,
                            A_ChestMeasurement = (int?)m.A_ChestMeasurement,
                            B_SeatMeasurement = (int?)m.B_SeatMeasurement,
                            C_WaistMeasurement = (int?)m.C_WaistMeasurement,
                            D_TrouserMeasurement = (int?)m.D_TrouserMeasurement,
                            E_F_HalfBackMeasurement = (int?)m.E_F_HalfBackMeasurement,
                            G_H_BackNeckToWaistMeasurement = (int?)m.G_H_BackNeckToWaistMeasurement,
                            G_I_SyceDepthMeasurement = (int?)m.G_I_SyceDepthMeasurement,
                            I_L_SleeveLengthOnePieceMeasurement = (int?)m.I_L_SleeveLengthOnePieceMeasurement,
                            E_I_SleeveLengthTwoPieceMeasurement = (int?)m.E_I_SleeveLengthTwoPieceMeasurement,
                            N_InsideLegMeasurement = (int?)m.N_InsideLegMeasurement,
                            P_Q_BodyRiseMeasurement = (int?)m.P_Q_BodyRiseMeasurement,
                            R_CloseWristMeasurement = (int?)m.R_CloseWristMeasurement,
                            Client = client // ✅ link back to parent
                        };

                        client.Measurements.Add(measurement);
                    }
                }

                return client;
            }).ToList();
        }




        public ClientInfoDto MapClientToDto(ClientInfo client)
        {
            return new ClientInfoDto
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Notes = client.Notes,
                Date = client.Date,
                Measurements = client.Measurements?.Select(m => new MeasurementsDto
                {
                    Id = m.Id,
                    ClientId = client.Id,
                    A_ChestMeasurement = m.A_ChestMeasurement,
                    B_SeatMeasurement = m.B_SeatMeasurement,
                    C_WaistMeasurement = m.C_WaistMeasurement,
                    D_TrouserMeasurement = m.D_TrouserMeasurement,
                    E_F_HalfBackMeasurement = m.E_F_HalfBackMeasurement,
                    G_H_BackNeckToWaistMeasurement = m.G_H_BackNeckToWaistMeasurement,
                    G_I_SyceDepthMeasurement = m.G_I_SyceDepthMeasurement,
                    I_L_SleeveLengthOnePieceMeasurement = m.I_L_SleeveLengthOnePieceMeasurement,
                    E_I_SleeveLengthTwoPieceMeasurement = m.E_I_SleeveLengthTwoPieceMeasurement,
                    N_InsideLegMeasurement = m.N_InsideLegMeasurement,
                    P_Q_BodyRiseMeasurement = m.P_Q_BodyRiseMeasurement,
                    R_CloseWristMeasurement = m.R_CloseWristMeasurement
                }).ToList() ?? new List<MeasurementsDto>()
            };
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
