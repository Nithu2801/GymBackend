using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Entities
{
    public class ProgramImages
    {
        public Guid Id { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public string alternative {  get; set; }
        public Guid ProgramId { get; set; }
        public WorkoutProgram Program { get; set; }
    }
}
