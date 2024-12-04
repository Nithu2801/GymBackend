using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using GymManagementSystem.IServices;

namespace GymManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProgramRepository _programRepository;
        private readonly IMemberRepository _memberRepository;

        public PaymentService(IPaymentRepository paymentRepository,IProgramRepository programRepository,IMemberRepository memberRepository)
        {
            _paymentRepository = paymentRepository;
            _programRepository = programRepository;
            _memberRepository = memberRepository;
        }
        public async Task<string> AddPayment(PaymentRequestDTO payment)
        {
            var programPayment=_programRepository.GetProgramPayment(payment.ProgramPaymentId);
            var member=_memberRepository.GetMember(payment.MemberId);
            if (member != null && programPayment != null)
            {
            Payment newPayment = new Payment()
            {
                PaymentDate = DateTime.Now,
                MemberId = payment.MemberId,
                ProgramPaymentId = payment.ProgramPaymentId,
            };
                var data=_paymentRepository.AddPayment(newPayment);
                var programPay = _programRepository.GetProgramPayment(payment.ProgramPaymentId);
                var enrollment = _memberRepository.GetEnrollments(payment.MemberId, programPay.ProgramId);
                if(enrollment != null)
                {
                    var subscription=_programRepository.GetSubscription(enrollment.SubscriptionId);
                    enrollment.NextDueDate = (enrollment.NextDueDate).AddMonths(subscription.Duration);
                    var updatedEnrollment=_memberRepository.UpdateEnrollment(enrollment);
                    if (updatedEnrollment == null)
                    {
                        throw new Exception("Enrollment was not updated");
                    }
                }
                if (data != null){return "Payment added successfully";}else { return "Payment cannot be added. Please Try Again!"; }
            }
            else { return "Member or Program Payment is invalid.Please Try again"; }
        }
        public async Task<List<PaymentResponseDTO>> GetMemberPayments(Guid memberId)
        {
            List<PaymentResponseDTO> responseList= new List<PaymentResponseDTO>();
            var data=await _paymentRepository.GetMemberPayments(memberId);
            if(data == null)
            {
                throw new Exception("MemberId is incorrect");
            }
            foreach (var payment in data)
            {
                var programPayment=_programRepository.GetProgramPayment(payment.ProgramPaymentId) ;
                var program=_programRepository.GetWorkoutProgram(programPayment.ProgramId);
                var subscription=_programRepository.GetSubscriptionPayment(programPayment.SubscriptionPaymentId);
                PaymentResponseDTO response = new PaymentResponseDTO()
                {
                    MemberId = payment.MemberId,
                    Date = payment.PaymentDate,
                    Amount = programPayment.Amount,
                    ProgramName = program.Name,
                    PaymentType = subscription.PaymentType
                };
                responseList.Add(response); 
            }
            return responseList;
        }
        public async Task<List<PaymentResponseDTO>> GetAllPayments()
        {
            List<PaymentResponseDTO> responseList = new List<PaymentResponseDTO>();
            var data = await _paymentRepository.GetAllPayments();
            if (data == null)
            {
                throw new Exception("Error");
            }
            foreach (var payment in data)
            {
                var programPayment = _programRepository.GetProgramPayment(payment.ProgramPaymentId);
                var program = _programRepository.GetWorkoutProgram(programPayment.ProgramId);
                var subscription = _programRepository.GetSubscriptionPayment(programPayment.SubscriptionPaymentId);
                PaymentResponseDTO response = new PaymentResponseDTO()
                {
                    MemberId = payment.MemberId,
                    Date = payment.PaymentDate,
                    Amount = programPayment.Amount,
                    ProgramName = program.Name,
                    PaymentType = subscription.PaymentType
                };
                responseList.Add(response);
            }
            return responseList;
        }
       
        public async Task<List<MemberProgramResponseDTO>> GetOverDueMembers()
        {
            var responseList = new List<MemberProgramResponseDTO>();
            var data=await _memberRepository.GetOverDueEnrollments();
            if (data == null) { throw new Exception("Data is null"); }
            foreach(var member in data)
            {
                MemberProgramResponseDTO response = new MemberProgramResponseDTO()
                {
                    MemberId = member.Member.Id,
                    FirstName = member.Member.FirstName,
                    LastName = member.Member.LastName,
                    Email = member.Member.Email,
                    DOB = member.Member.DOB,
                    ContactNo = member.Member.ContactNo,
                    Address = member.Member.Address,
                    Age = member.Member.Age,
                    Height = member.Member.Height,
                    Weight = member.Member.Weight,
                    Gender = member.Member.Gender,
                    NicNo = member.Member.NicNo,
                    UserId = member.Member.UserId,
                    ProgramId=member.ProgramId,
                    Name=member.Program.Name,
                    SubscriptionTitle=member.Subscription.Title,
                };
                responseList.Add(response);
            }
            return responseList;
        }
    }
}
