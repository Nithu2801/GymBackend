using GymManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DTO.Response_DTO
{
    public class MessageResponseDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Guid MemberId { get; set; }
    }
}
