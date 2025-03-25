using Microsoft.AspNetCore.Mvc;
using SeamRipperData.Repositories;
using SeamRipperData.Models;
using SeamRipperData.Dtos;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace SeamRipperAPI.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase

    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        //  Get Client by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientInfoDto>> GetClientById(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null) return NotFound();

            var clientDto = new ClientInfoDto
            {
                Id = client.Id,
                Name = client.Name,
                Date = client.Date,
                Notes = client.Notes,
                PhoneNumber = client.PhoneNumber,
                Measurements = client.Measurements.Select(m => new MeasurementsDto
                {
                    Id = m.Id,
                    ClientId = m.ClientId,
                    A_ChestMeasurement = (double?)m.A_ChestMeasurement,
                    B_SeatMeasurement = (double?)m.B_SeatMeasurement,
                    C_WaistMeasurement = (double?)m.C_WaistMeasurement,
                    D_TrouserMeasurement = (double?)m.D_TrouserMeasurement,
                    E_F_HalfBackMeasurement = (double?)m.E_F_HalfBackMeasurement,
                    G_H_BackNeckToWaistMeasurement = (double?)m.G_H_BackNeckToWaistMeasurement,
                    G_I_SyceDepthMeasurement = (double?)m.G_I_SyceDepthMeasurement,
                    I_L_SleeveLengthOnePieceMeasurement = (double?)m.I_L_SleeveLengthOnePieceMeasurement,
                    E_I_SleeveLengthTwoPieceMeasurement = (double?)m.E_I_SleeveLengthTwoPieceMeasurement,
                    N_InsideLegMeasurement = (double?)m.N_InsideLegMeasurement,
                    P_Q_BodyRiseMeasurement = (double?)m.P_Q_BodyRiseMeasurement,
                    R_CloseWristMeasurement = (double?)m.R_CloseWristMeasurement
                }).ToList()
            };

            return Ok(clientDto);
        }

        // ✅ Get all Clients
        [HttpGet]
        public async Task<ActionResult<List<ClientInfoDto>>> GetClients()
        {
            var clients = await _clientRepository.GetClientsAsync();

            var clientDtos = clients.Select(client => new ClientInfoDto
            {
                Id = client.Id,
                Name = client.Name,
                Date = client.Date,
                Notes = client.Notes,
                PhoneNumber = client.PhoneNumber,
                Measurements = client.Measurements.Select(m => new MeasurementsDto
                {
                    Id = m.Id,
                    ClientId = m.ClientId,
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
                }).ToList()
            }).ToList();

            return Ok(clientDtos);
        }


        // ✅ Add Client
        [HttpPost]
        public async Task<ActionResult<ClientInfoDto>> AddClient(ClientInfoDto clientDto)
        {
            var client = new ClientInfo
            {
                Name = clientDto.Name,
                Date = clientDto.Date == default ? DateTime.Now : clientDto.Date,
                Notes = clientDto.Notes,
                PhoneNumber = clientDto.PhoneNumber,
                Measurements = clientDto.Measurements.Select(m => new ClientMeasurements
                {
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
                    R_CloseWristMeasurement = (int?)m.R_CloseWristMeasurement
                }).ToList()
            };

            await _clientRepository.AddClientAsync(client);

            var resultDto = new ClientInfoDto
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Notes = client.Notes,
                Date = client.Date,
                Measurements = client.Measurements.Select(m => new MeasurementsDto
                {
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
                }).ToList()
            };

            return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, resultDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientInfoDto clientDto)
        {
            var updatedClient = new ClientInfo
            {
                Id = id,
                Name = clientDto.Name,
                PhoneNumber = clientDto.PhoneNumber,
                Notes = clientDto.Notes,
                Date = clientDto.Date,
                Measurements = clientDto.Measurements?.Select(m => new ClientMeasurements
                {
                    Id = m.Id,
                    ClientId = id,
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
                    R_CloseWristMeasurement = (int?)m.R_CloseWristMeasurement
                }).ToList()
            };

            await _clientRepository.UpdateClientAsync(updatedClient);
            return NoContent();
        }

        // ✅ Delete Client
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null) return NotFound();

            await _clientRepository.DeleteClientAsync(id); // ✅ Automatically saves changes
            return NoContent();
        }

        [HttpPost("generate/{count}")]
        public async Task<ActionResult<List<ClientInfoDto>>> GenerateRandomClients(int count)
        {
            var clients = await _clientRepository.GenerateRandomClientsAsync(count);

            var clientDtos = clients.Select(client => new ClientInfoDto
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
            }).ToList();

            return Ok(clientDtos);
            
        }

        [HttpPost("batch")]
        public async Task<ActionResult<List<ClientInfoDto>>> GetClientsByIds([FromBody] List<int> ids)
        {
            var clients = await _clientRepository.GetClientsByIdsAsync(ids);

            var clientDtos = clients.Select(client => new ClientInfoDto
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Notes = client.Notes,
                Date = client.Date,
                Measurements = client.Measurements.Select(m => new MeasurementsDto
                {
                    Id = m.Id,
                    ClientId = m.ClientId,
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
                }).ToList()
            }).ToList();

            return Ok(clientDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ClientInfoDto>>> SearchClients([FromQuery] string query)
        {
            var clients = await _clientRepository.SearchClientsAsync(query);

            var clientDtos = clients.Select(client => new ClientInfoDto
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
            }).ToList();

            return Ok(clientDtos);
        }

        [HttpPut("measurements/{id}")]
        public async Task<IActionResult> UpdateMeasurement(int id, [FromBody] MeasurementsDto dto)
        {
            var measurement = await _clientRepository.GetMeasurementByIdAsync(id);
            if (measurement == null) return NotFound();

            measurement.A_ChestMeasurement = (int?)dto.A_ChestMeasurement;
            measurement.B_SeatMeasurement = (int?)dto.B_SeatMeasurement;
            measurement.C_WaistMeasurement = (int?)dto.C_WaistMeasurement;
            measurement.D_TrouserMeasurement = (int?)dto.D_TrouserMeasurement;
            measurement.E_F_HalfBackMeasurement = (int?)dto.E_F_HalfBackMeasurement;
            measurement.G_H_BackNeckToWaistMeasurement = (int?)dto.G_H_BackNeckToWaistMeasurement;
            measurement.G_I_SyceDepthMeasurement = (int?)dto.G_I_SyceDepthMeasurement;
            measurement.I_L_SleeveLengthOnePieceMeasurement = (int?)dto.I_L_SleeveLengthOnePieceMeasurement;
            measurement.E_I_SleeveLengthTwoPieceMeasurement = (int?)dto.E_I_SleeveLengthTwoPieceMeasurement;
            measurement.N_InsideLegMeasurement = (int?)dto.N_InsideLegMeasurement;
            measurement.P_Q_BodyRiseMeasurement = (int?)dto.P_Q_BodyRiseMeasurement;
            measurement.R_CloseWristMeasurement = (int?)dto.R_CloseWristMeasurement;

            await _clientRepository.UpdateClientMeasurementAsync(measurement);
            return Ok();
        }

        [HttpPut("measurements/clear/{id}")]
        public async Task<IActionResult> ClearMeasurement(int id)
        {
            Console.WriteLine($"🔍 Attempting to clear measurement ID: {id}");

            var measurement = await _clientRepository.GetMeasurementByIdAsync(id);
            if (measurement == null)
            {
                Console.WriteLine("❌ Measurement not found in DB.");
                return NotFound();
            }

            // Reset all values
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
            return Ok();
        }





    }
}
