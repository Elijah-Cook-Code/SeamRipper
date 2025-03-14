using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeamRipperData.Models
{
    public class ClientMeasurements
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int ClientId { get; set; }  // Foreign Key

        [ForeignKey("ClientId")]
        public ClientInfo Client { get; set; } = null!; // Navigation Property

        public int? A_ChestMeasurement { get; set; }
        public int? B_SeatMeasurement { get; set; }
        public int? C_WaistMeasurement { get; set; }
        public int? D_TrouserMeasurement { get; set; }
        public int? E_F_HalfBackMeasurement { get; set; }
        public int? G_H_BackNeckToWaistMeasurement { get; set; }
        public int? G_I_SyceDepthMeasurement { get; set; }
        public int? I_L_SleeveLengthOnePieceMeasurement { get; set; }
        public int? E_I_SleeveLengthTwoPieceMeasurement { get; set; }
        public int? N_InsideLegMeasurement { get; set; }
        public int? P_Q_BodyRiseMeasurement { get; set; }
        public int? R_CloseWristMeasurement { get; set; }
    }
}
