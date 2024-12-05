namespace ClewbayFarmAPI.Dtos
{
    public class JobListDto
    {
        public int Year { get; set; }
        public int Week { get; set; }
        public List<JobDto> Jobs { get; set; } = new List<JobDto>();
    }

}
