using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using GymManagementSystem.IServices;

namespace GymManagementSystem.Services
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMemberRepository _memberRepository;

        public MessageService(IMessageRepository messageRepository, IMemberRepository memberRepository)
        {
            _messageRepository = messageRepository;
            _memberRepository = memberRepository;
        }

        public async Task<string> AddAdminMessage(MessageRequestDTO message)
        {
            var member=_memberRepository.GetMember(message.MemberId);
            if (member == null) { return "Member not found"; }
            AdminMessage newmessage = new AdminMessage()
            {
                Date = DateTime.Now,
                Message = message.Message,
                MemberId = message.MemberId,
                Status = true
            };
            var data =  _messageRepository.AddAdminMessage(newmessage);
            if (data == null) { return "Message could not be Sent"; }
            return "Message is Sent Successfully";
        }
        public async Task<string> AddMemberMessage(MessageRequestDTO message)
        {
            var member = _memberRepository.GetMember(message.MemberId);
            if (member == null) { throw new Exception("Member not found"); }
            MemberMessage newmessage = new MemberMessage()
            {
                Date = DateTime.Now,
                Message = message.Message,
                MemberId = message.MemberId,
                Member = member,
                Status = true
            };
            var data = _messageRepository.AddMemberMessage(newmessage);
            if (data == null) { throw new Exception("Message could not be Sent"); }
            return "Message is Sent Successfully";
        }
        public async Task<List<MessageResponseDTO>> GetAdminMessageToSingleMember(Guid MemberId)
        {
            List<MessageResponseDTO> responseList = new List<MessageResponseDTO>();
            var data = await _messageRepository.GetAdminMessage(MemberId);
            if (data == null) { throw new Exception("Member not found"); }
            foreach (var message in data)
            {
                MessageResponseDTO response = new MessageResponseDTO()
                {
                    Id = message.Id,
                    Date = message.Date,
                    Message = message.Message,
                    MemberId = message.MemberId
                };
                responseList.Add(response);
            }
            return responseList;
        }
        public async Task<List<MessageResponseDTO>> GetAllAdminMessage()
        {
            List<MessageResponseDTO> responseList = new List<MessageResponseDTO>();
            var data = await _messageRepository.GetAllAdminMessages();
            if (data == null) { throw new Exception("Member not found"); }
            foreach (var message in data)
            {
                MessageResponseDTO response = new MessageResponseDTO()
                {
                    Id = message.Id,
                    Date = message.Date,
                    Message = message.Message,
                    MemberId = message.MemberId
                };
                responseList.Add(response);
            }
            return responseList;
        }
        public async Task<List<MessageResponseDTO>> GetSingleMemberMessage(Guid MemberId)
        {
            List<MessageResponseDTO> responseList = new List<MessageResponseDTO>();
            var data = await _messageRepository.GetMemberMessage(MemberId);
            if (data == null) { throw new Exception("Member not found"); }
            foreach (var message in data)
            {
                MessageResponseDTO response = new MessageResponseDTO()
                {
                    Id = message.Id,
                    Date = message.Date,
                    Message = message.Message,
                    MemberId = message.MemberId
                };
                responseList.Add(response);
            }
            return responseList;
        }
        public async Task<List<MessageResponseDTO>> GetAllMemberMessage()
        {
            List<MessageResponseDTO> responseList = new List<MessageResponseDTO>();
            var data = await _messageRepository.GetAllMessages();
            if (data == null) { throw new Exception("Member not found"); }
            foreach (var message in data)
            {
                MessageResponseDTO response = new MessageResponseDTO()
                {
                    Id = message.Id,
                    Date = message.Date,
                    Message = message.Message,
                    MemberId = message.MemberId
                };
                responseList.Add(response);
            }
            return responseList;
        }
        public async Task<string> DeleteAdminMessage(Guid id)
        {
            var data = await _messageRepository.GetSinglrAdminMessage(id);
            if (data == null) { throw new Exception("Message not found"); }
            data.Status = false;
            var deletedMessage = await _messageRepository.UpdateAdminMessage(data);
            if (deletedMessage == null) { throw new Exception("Message couldn't be deleted"); }
            return "Message removed successfully";
        }
        public async Task<string> DeleteMemberMessage(Guid id)
        {
            var data = await _messageRepository.GetSingleMessage(id);
            if (data == null) { throw new Exception("Message not found"); }
            data.Status = false;
            var deletedMessage = await _messageRepository.UpdateMemberMessage(data);
            if (deletedMessage == null) { throw new Exception("Message couldn't be deleted"); }
            return "Message removed successfully";
        }
    }
}
