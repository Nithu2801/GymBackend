namespace GymManagementSystem.DTO.Response_DTO
{
    public class ProgramImageResponseDTO
    {
        public string ImagePath { get; set; }
        public string alternative { get; set; }

        public ProgramImageResponseDTO(string imagePath, string alternative)
        {
            ImagePath = imagePath;
            this.alternative = alternative;
        }

        public ProgramImageResponseDTO()
        {
        }
    }
}
