namespace ClewbayFarmAPI.Dtos
{
    public class CropDto
    {
        public string Type { get; set; } = null!;
        public string Variety { get; set; } = null!;
        public bool IsDirectSow { get; set; }
    }

}
