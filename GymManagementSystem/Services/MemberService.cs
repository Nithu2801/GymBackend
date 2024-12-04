using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using GymManagementSystem.IServices;
using GymManagementSystem.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace GymManagementSystem.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProgramRepository _programRepository;
        private readonly IConfiguration _configuration;


        public MemberService(IMemberRepository memberRepository, IProgramRepository programRepository , IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _programRepository = programRepository;
            _configuration = configuration;
        }
        public async Task<string> CreateAdmin(AdminRequestDTO adminRequestDTO)
        {
            User admin = new User()
            {
                Id = adminRequestDTO.Id,
                Email = adminRequestDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(adminRequestDTO.Password),
                ProfileImage = adminRequestDTO.ProfileImage,
                Roles = Roles.Admin
            };
            var data = _memberRepository.CreateUser(admin);
            if (data is User)
            {
                return "Admin Created Successfully";
            }
            else
            {
                return "Please try again";
            }
        }
        public async Task<string> CreateMember(MemberRequestDTO memberRequestDTO)
        {
            var password = memberRequestDTO.FirstName + memberRequestDTO.Age + memberRequestDTO.Height;
            var newUser = new User()
            {
                Id = memberRequestDTO.UserId,
                Password = BCrypt.Net.BCrypt.HashPassword(memberRequestDTO.Password),
                ProfileImage = memberRequestDTO.ProfileImage,
                Roles = Roles.Member,
                Email = memberRequestDTO.Email,
                Member = new Member()
                {
                    FirstName = memberRequestDTO.FirstName,
                    LastName = memberRequestDTO.LastName,
                    Email = memberRequestDTO.Email,
                    DOB = memberRequestDTO.DOB,
                    ContactNo = memberRequestDTO.ContactNo,
                    Address = memberRequestDTO.Address,
                    Age = memberRequestDTO.Age,
                    Height = memberRequestDTO.Height,
                    Weight = memberRequestDTO.Weight,
                    Gender = memberRequestDTO.Gender,
                    NicNo = memberRequestDTO.NicNo,
                    CreatedDate = DateTime.Now,
                    MemberStatus = true,
                    Password = BCrypt.Net.BCrypt.HashPassword(password)
                    //Password=memberRequestDTO.Password


                }
            };
            var data = _memberRepository.CreateUser(newUser); 
            if (data is User)
            {
                return "User Added Successfully"+password;
            }
            else
            {
                return "Please try again";
            }
        }
        public async Task<List<MemberResponseDTO>> GetAllMemberDetails()
        {
            var data =  _memberRepository.GetAllMembers();
            var memberResponseList = new List<MemberResponseDTO>();
            foreach (var member in data)
            {
                var memberResponseDTO = new MemberResponseDTO()
                {
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    DOB = member.DOB,
                    ContactNo = member.ContactNo,
                    Address = member.Address,
                    Age = member.Age,
                    Height = member.Height,
                    Weight = member.Weight,
                    Gender = member.Gender,
                    NicNo = member.NicNo,
                    UserId = member.UserId,
                    ProfileImage = member.User?.ProfileImage
                };
                memberResponseList.Add(memberResponseDTO);
            }
            return memberResponseList;
        }

        public async Task<MemberResponseDTO> GetSingleMember(Guid id)
        {
            var data = _memberRepository.GetMember(id);
            if (data == null)
            {
                throw new Exception("Member not found");
            }
            var memberResponseDTO = new MemberResponseDTO()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                DOB = data.DOB,
                ContactNo = data.ContactNo,
                Address = data.Address,
                Age = data.Age,
                Height = data.Height,
                Weight = data.Weight,
                Gender = data.Gender,
                NicNo = data.NicNo,
                UserId = data.UserId,
                ProfileImage = data.User.ProfileImage
            };
            return memberResponseDTO;
        }
        public async Task<MemberResponseDTO> GetMemberByUserId(string id)
        {
            var data =await _memberRepository.GetMemberByUserId(id);
            if (data == null)
            {
                throw new Exception("Member not found");
            }
            var memberResponseDTO = new MemberResponseDTO()
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email,
                DOB = data.DOB,
                ContactNo = data.ContactNo,
                Address = data.Address,
                Age = data.Age,
                Height = data.Height,
                Weight = data.Weight,
                Gender = data.Gender,
                NicNo = data.NicNo,
                UserId = data.UserId,
                ProfileImage = data.User.ProfileImage
            };
            return memberResponseDTO;
        }
        public async Task<string> UpdateMember(Guid Id, MemberRequestDTO memberRequestDTO)
        {
            var data =  _memberRepository.GetMember(Id);

            data.FirstName = memberRequestDTO.FirstName;
            data.LastName = memberRequestDTO.LastName;
            data.Email = memberRequestDTO.Email;
            data.DOB = memberRequestDTO.DOB;
            data.ContactNo = memberRequestDTO.ContactNo;
            data.Address = memberRequestDTO.Address;
            data.Age = memberRequestDTO.Age;
            data.Height = memberRequestDTO.Height;
            data.Weight = memberRequestDTO.Weight;
            data.Gender = memberRequestDTO.Gender;
            data.NicNo = memberRequestDTO.NicNo;
            data.User.ProfileImage = memberRequestDTO.ProfileImage;
            var updateMember =  _memberRepository.UpdateMember(data);
            if (updateMember is Member)
            {
                return "Member Updated Successfully";
            }
            else
            {
                return "Please try again";
            }
        }
        public async Task<string> DeleteMember(Guid Id)
        {
            var data =  _memberRepository.GetMember(Id);
            data.MemberStatus = false;
            var deleteMember =  _memberRepository.UpdateMember(data);
            if (deleteMember is Member)
            {
                return "Member is deleted Successfully";
            }
            else
            {
                return "Please try again";
            }
        }
        public async Task<string> AddEnrollment(EnrollmentRequestDTO enrollmentRequest)
        {
            var subscription =  _programRepository.GetSubscription(enrollmentRequest.SubscriptionId);
            Enrollment enrollment = new Enrollment()
            {
                MemberId = enrollmentRequest.MemberId,
                ProgramId = enrollmentRequest.ProgramId,
                SubscriptionId = enrollmentRequest.SubscriptionId,
                EnrolledDate = DateTime.Now,
                NextDueDate = DateTime.Now.AddMonths(subscription.Duration)
            };
            var data=  _memberRepository.CreateEnrollment(enrollment);
            if (data is Enrollment)
            {
                return "Enrollment Added Successfully";
            }
            else
            {
                return "Please Try Again";
            }
        }
        public async Task<List<EnrolledProgramResponseDTO>> GetMemberEnrolledPrograms(Guid id)
        {
            List<EnrolledProgramResponseDTO> enrolledProgramList= new List<EnrolledProgramResponseDTO>();
            var memberEnrollment =  _memberRepository.GetMemberEnrollments(id);
            foreach (var enrollment in memberEnrollment)
            {
                var programPaymentList =  GetProgramPayments(enrollment.SubscriptionId, enrollment.ProgramId);
                var subscription =  _programRepository.GetSubscription(enrollment.SubscriptionId);
                var subscriptionResponse = new ProgramSubscriptionResponseDTO()
                {
                    Id = subscription.Id,
                    Name = subscription.Title,
                    Description = subscription.Description,
                    IsNewSubscription = (subscription.Date.Year == DateTime.Now.Year && subscription.Date.Month == DateTime.Now.Month) ? true : false,
                    IsSpecialOffer = subscription.IsSpecialOffer,
                    Duration = subscription.Duration
                };
                var program =  _programRepository.GetWorkoutProgram(enrollment.ProgramId);
                var imageList = new List<ProgramImageResponseDTO>();

                foreach (var image in program.Images)
                {
                    var imageResponseDTO = new ProgramImageResponseDTO(image.ImagePath, image.alternative);
                    imageList.Add(imageResponseDTO);
                }
                EnrolledProgramResponseDTO programResponse = new EnrolledProgramResponseDTO()
                {
                    Id = program.Id,
                    Name = program.Name,
                    Description = program.Description,
                    Images = imageList,
                    ProgramPayments = programPaymentList,
                    ProgramSubscription = subscriptionResponse
                };
                enrolledProgramList.Add(programResponse);
                };
            return enrolledProgramList;
        }
        public async Task<string> DeleteEnrollment(Guid memberId,Guid programId)
        {
            var deleteEnrollment= _memberRepository.GetEnrollments(memberId, programId);
            if (deleteEnrollment != null)
            {
                _memberRepository.DeleteEnrollment(deleteEnrollment);
            }
            else
            {
                throw new Exception("Enrollment not found");
            }
            return "enrollment deleted successfully";
        }
        public async Task<List<ProgramResponseDTO>> GetEnrollablePrograms(Guid memberId)
        {
            List<ProgramResponseDTO> programResponses = new List<ProgramResponseDTO>();
            List<Guid> programIdList = new List<Guid>();
            var memberEnrollments =  _memberRepository.GetMemberEnrollments(memberId);
            foreach(var enrollment in memberEnrollments)
            {
                programIdList.Add(enrollment.ProgramId);
            }
            var data= _memberRepository.NotEnrolledPrograms(programIdList);
            foreach(var program in data)
            {
                var imageList = new List<ProgramImageResponseDTO>();
                List<SubscriptionResponseDTO> programSubscriptions = new List<SubscriptionResponseDTO>();
                if(program.Subscriptions != null)
                {
                foreach (var programSubscription in program.Subscriptions)
                {
                    var subscription = _programRepository.GetSubscription(programSubscription.SubscribeId);
                    if (subscription != null)
                    {

                        var programPaymentList =  GetProgramPayments(subscription.Id, program.Id);
                        var subscriptionResponse = new SubscriptionResponseDTO()
                        {
                            Id = subscription.Id,
                            Title = subscription.Title,
                            Description = subscription.Description,
                            IsNewSubscription = (subscription.Date.Year == DateTime.Now.Year && subscription.Date.Month == DateTime.Now.Month) ? true : false,
                            IsSpecialOffer = subscription.IsSpecialOffer,
                            Duration = subscription.Duration,
                            Payments = programPaymentList
                        };
                        programSubscriptions.Add(subscriptionResponse);
                    }
                }
                }
                if (program.Images != null)
                {
                foreach (var image in program.Images)
                {
                    var imageResponseDTO = new ProgramImageResponseDTO(image.ImagePath, image.alternative);
                    imageList.Add(imageResponseDTO);
                }

                }
                ProgramResponseDTO response = new ProgramResponseDTO()
                {

                    Id = program.Id,
                    Name = program.Name,
                    Description = program.Description,
                    Images = imageList,
                    Subscriptions = programSubscriptions
                };
                programResponses.Add(response);
            }
            return programResponses;
        }
        public List<ProgramPaymentResponseDTO> GetProgramPayments(Guid SubscriptionId, Guid ProgramId)
        {
            var data= _programRepository.GetSubscription(SubscriptionId);
            var programPaymentList=new List<ProgramPaymentResponseDTO>();
            foreach(var paymentType in data.SubscriptionPayments)
            {
                var programPayment =  _programRepository.GetProgramPayment(ProgramId, paymentType.Id);
                if(programPayment != null)
                {

                ProgramPaymentResponseDTO responseDTO = new ProgramPaymentResponseDTO()
                {
                    Id = programPayment.Id,
                    Amount = programPayment.Amount,
                    PaymentType = paymentType.PaymentType
                };
                programPaymentList.Add(responseDTO);
                }
            }
            return programPaymentList;
        }

    }
}
