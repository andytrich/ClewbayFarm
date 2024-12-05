namespace ClewbayFarmAPI.Dtos
{
    public class JobDto
    {
        public string Action { get; set; } = null!;
        public string Crop { get; set; } = null!;
        public string? BedOrTray { get; set; } // Null for propagation, "Bed X" for beds
        public DateTime Date { get; set; }
    }

}
