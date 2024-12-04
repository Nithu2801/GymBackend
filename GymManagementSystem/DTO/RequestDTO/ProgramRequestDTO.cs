using GymManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.RequestDTO
{
    public class ProgramRequestDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProgramImageRequestDTO>? Images { get; set; }

    }
}
