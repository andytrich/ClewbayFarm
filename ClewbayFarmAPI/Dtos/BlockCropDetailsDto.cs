namespace ClewbayFarmAPI.Dtos
{
    public class BlockCropDetailsDto
    {
        public int BedId { get; set; }
        public string CropType { get; set; } = null!;
        public string CropVariety { get; set; } = null!;
        public DateTime PlantingDate { get; set; }
        public DateTime? RemovalDate { get; set; }
        public int PlantingWeek { get; set; }
        public int? RemovalWeek { get; set; }
    }

}
