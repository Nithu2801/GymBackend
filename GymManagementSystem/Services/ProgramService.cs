using GymManagementSystem.DTO.RequestDTO;
using GymManagementSystem.DTO.Response_DTO;
using GymManagementSystem.Entities;
using GymManagementSystem.IRepositories;
using GymManagementSystem.IServices;

namespace GymManagementSystem.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IProgramRepository _programRepository;

        public ProgramService(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }
        public async Task<string> AddProgram(ProgramRequestDTO programRequestDTO)
        {
            WorkoutProgram program = new WorkoutProgram()
            {
                Name = programRequestDTO.Name,
                Description = programRequestDTO.Description,
                CreatedDate = DateTime.Now,
                Status = true
            };
            var data =  _programRepository.CreateProgram(program);
            foreach (var image in programRequestDTO.Images)
            {
                ProgramImages newimage = new ProgramImages()
                {
                    ImagePath = image.ImagePath,
                    alternative = image.alternative,
                    ProgramId = data.Id,
                    Program = data
                };
                var programImage =  _programRepository.CreateImage(newimage);
            }
            if (data != null)
            {
                return "Program Added Successfully";
            }
            else
            {
                return "Try again";
            }
        }
        public async Task<List<ProgramResponseDTO>> GetAllPrograms()
        {
            List<ProgramResponseDTO> programResponses = new List<ProgramResponseDTO>();
            var data =  _programRepository.GetAllWorkoutProgram();
            foreach (var program in data)
            {
                var imageList = new List<ProgramImageResponseDTO>();
                List<SubscriptionResponseDTO> programSubscriptions = new List<SubscriptionResponseDTO>();
                var subProgramList=_programRepository.GetSubscriptionsOfSingleProgram(program.Id);
                if (subProgramList != null)
                {
                    foreach (var programSubscription in subProgramList)
                    {
                        var subscription = _programRepository.GetSubscription(programSubscription.SubscribeId);
                        if (subscription != null)
                        {

                            var programPaymentList = await GetProgramPayments(subscription.Id, program.Id);
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
                foreach (var image in program.Images)
                {
                    var imageResponseDTO = new ProgramImageResponseDTO(image.ImagePath, image.alternative);
                    imageList.Add(imageResponseDTO);
                }
                ProgramResponseDTO response = new ProgramResponseDTO()
                {

                    Id = program.Id,
                    Name = program.Name,
                    Description = program.Description,
                    IsProgramNew = (program.CreatedDate.Year == DateTime.Now.Year &&program.CreatedDate.Month == DateTime.Now.Month)?true :false,
                    Images = imageList,
                    Subscriptions = programSubscriptions
                };
                programResponses.Add(response);
            }
            return programResponses;
        }
        public async Task<ProgramResponseDTO> GetSingleProgram(Guid Id)
        {
            var data =  _programRepository.GetWorkoutProgram(Id);

            var imageList = new List<ProgramImageResponseDTO>();
            List<SubscriptionResponseDTO> programSubscriptions = new List<SubscriptionResponseDTO>();
            if (data.Subscriptions != null)
            {
                foreach (var programSubscription in data.Subscriptions)
                {
                    var subscription = _programRepository.GetSubscription(programSubscription.SubscribeId);
                    if(subscription != null)
                    {

                    var programPaymentList = await GetProgramPayments(subscription.Id, data.Id);
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
            foreach (var image in data.Images)
            {
                var imageResponseDTO = new ProgramImageResponseDTO(image.ImagePath, image.alternative);
                imageList.Add(imageResponseDTO);
            }
            ProgramResponseDTO response = new ProgramResponseDTO()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Images = imageList,
                Subscriptions = programSubscriptions
            };
            return response;
        }
        public async Task<string> DeleteProgram(Guid id)
        {
            var data = _programRepository.GetWorkoutProgram(id);
            data.Status=false;
            var updatedProgram= _programRepository.UpdateProgram(data);
            if (updatedProgram != null)
            {
                return "Program is deleted successfully";
            }
            else
            {
                return "Try Again";
            }
        }
        public async Task<string> UpdateProgram(Guid id,ProgramRequestDTO programRequestDTO)
        {
            var data =  _programRepository.GetWorkoutProgram(id);
            data.Name = programRequestDTO.Name;
            data.Description = programRequestDTO.Description;
            var updatedProgram =  _programRepository.UpdateProgram(data);
            if (updatedProgram != null)
            {
                return "Program is Updated successfully";
            }
            else
            {
                return "Try Again";
            }
        }
        public async Task<string> UpdateProgramPayment(Guid id,decimal Amount)
        {
            var data= _programRepository.GetProgramPayment(id);
            data.Status = false;
            var deletePayment= _programRepository.UpdateProgramPayment(data);
            var newPayment = new ProgramPayment()
            {
                Amount = Amount,
                ProgramId = data.ProgramId,
                SubscriptionPaymentId = data.SubscriptionPaymentId,
                Program = data.Program,
                SubscriptionPayment = data.SubscriptionPayment
            };
            var programPayment = _programRepository.CreateProgramPayment(newPayment);
            if (programPayment != null)
            {
                return "Program Payment is Updated successfully";
            }
            else
            {
                return "Try Again";
            }
        }
        public async Task<List<ProgramPaymentResponseDTO>> GetProgramPayments(Guid SubscriptionId, Guid ProgramId)
        {
            var data =  _programRepository.GetSubscription(SubscriptionId);
            var programPaymentList = new List<ProgramPaymentResponseDTO>();
            foreach (var paymentType in data.SubscriptionPayments)
            {
                var programPayment =  _programRepository.GetProgramPayment(ProgramId, paymentType.Id);
                if (programPayment != null)
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


        public async Task<string> AddSubscription(SubscriptionRequestDTO subscription)
        {
            Subscription newSubscription = new Subscription()
            {
                Title = subscription.Title,
                Description = subscription.Description,
                Duration = subscription.Duration,
                Date = DateTime.Now,
                IsSpecialOffer = subscription.IsSpecialOffer,
                Status = true
            };
            var data = _programRepository.CreateSubscription(newSubscription);
            if (data != null && subscription.PaymentRequests != null)
            {
                foreach (var subPayment in subscription.PaymentRequests)
                {
                    SubscriptionPayment newSubPayment = new SubscriptionPayment()
                    {
                        PaymentType = subPayment.PaymentType,
                        PaymentDate = subPayment.PaymentDate,
                        UserCanPay = subPayment.UserCanPay,
                        Status=true,
                        SubscriptionId = data.Id,
                        Subscription = data
                    };
                    var newPayment= _programRepository.CreateSubscriptionPayment(newSubPayment);
                }
            }
            if (data != null)
            {
                return "New subscription created Successfully";
            }
            else
            {
                return "Please try again";
            }
        }
        public async Task<string> AddSubscriptionPayment(Guid subscriptionId,SubscriptionPaymentRequestDTO subPayment)
        {
            SubscriptionPayment newSubPayment = new SubscriptionPayment()
            {
                PaymentType = subPayment.PaymentType,
                PaymentDate = subPayment.PaymentDate,
                Status = true,
                UserCanPay = subPayment.UserCanPay,
                SubscriptionId = subscriptionId
            };
            var data = _programRepository.CreateSubscriptionPayment(newSubPayment);
            if (data != null)
            {
                return "New payment type added successfully";
            }
            else
            {
                return "Try again";
            }
        }
        public async Task<string> AddProgramPayment(ProgramPaymentRequestDTO programPay)
        {
            var subscriptionPayment = _programRepository.GetSubscriptionPayment(programPay.SubPaymentId);
            var program = _programRepository.GetWorkoutProgram(programPay.ProgramId);
            if(subscriptionPayment != null && program != null)
            {
                ProgramPayment payment = new ProgramPayment()
                {
                    Amount = programPay.Amount,
                    Status = true,
                    ProgramId = programPay.ProgramId,
                    SubscriptionPaymentId = programPay.SubPaymentId,
                    SubscriptionPayment = subscriptionPayment,
                    Program = program

                };
                var programPayment = _programRepository.CreateProgramPayment(payment);
                if (programPayment == null)
                {
                    return "Cannot create program payment";
                }
            }
            else { return "Program Or Subscribtion Payment not found"; }
            return string.Empty;
  
        }
        public async Task<string> AddSubscribedProgram(SubscribedProgramRequestDTO subProgram)
        {
            var program=_programRepository.GetWorkoutProgram(subProgram.ProgramId);
            if (program == null)
            {
                return "Program not found "+ subProgram.ProgramId;
            }
            var subscription = _programRepository.GetSubscription(subProgram.SubscriptionId);
            if (subscription == null)
            {
                return "Subscription not found";
            }
            SubscribedProgram subscribedProgram = new SubscribedProgram()
            {
                ProgramId = subProgram.ProgramId,
                SubscribeId = subProgram.SubscriptionId,
                CreatedDate = DateTime.Now,
                Status = true,
                WorkoutProgram=program,
                Subscription=subscription
            };
            var data = _programRepository.AddSubscribedProgram(subscribedProgram);
            if (data != null && subProgram.PaymentRequests != null)
            {
                foreach (var subPayment in subProgram.PaymentRequests)
                {
                    var subscriptionPayment = _programRepository.GetSubscriptionPayment(subPayment.SubPaymentId,subProgram.SubscriptionId);
                    if (subscriptionPayment != null)
                    {
                        ProgramPayment payment = new ProgramPayment()
                        {
                            Amount = subPayment.Amount,
                            Status = true,
                            ProgramId = subProgram.ProgramId,
                            SubscriptionPaymentId = subPayment.SubPaymentId,
                            SubscriptionPayment=subscriptionPayment,
                            Program=program

                        };
                        var programPayment=_programRepository.CreateProgramPayment(payment);
                        if (programPayment == null)
                        {
                            return "Cannot create program payment";
                        }
                    }else { return "Cannot find subscribtion payment"; }
                }
            }
            if (data != null){return "Program is available for subscribtion";} else{return "Try again";}
        }
        public async Task<string> AddSubscribedProgramList(Guid subscriptionId, List<Guid> programIds)
        {
            foreach( Guid id in programIds)
            {
                SubscribedProgram subscribedProgram = new SubscribedProgram()
                {
                    ProgramId = id,
                    SubscribeId = subscriptionId,
                    CreatedDate = DateTime.Now,
                    Status = true
                };
                var data = _programRepository.AddSubscribedProgram(subscribedProgram);
                if(data == null)
                {
                    return "Error.Please try again";
                }
            }
                return "Program is available for subscribtion";
        }

        public async Task<AdminSubscribtionResponseDTO> GetSingleSubscription(Guid subscriptionId)
        {
            var data=_programRepository.GetSubscription(subscriptionId);
            if(data != null)
            {
                List<SubPaymentResponseDTO> paymentTypes = new List<SubPaymentResponseDTO>();
                List<string> Programs= new List<string>();
                if (data.SubscribedPrograms != null)
                {
                foreach(var singleProgram in data.SubscribedPrograms)
                {
                    var workoutProgram= _programRepository.GetWorkoutProgram(singleProgram.ProgramId);
                    if(workoutProgram != null)
                    {
                        Programs.Add(workoutProgram.Name);

                    }
                }
                }
                if(data.SubscriptionPayments != null)
                {
                foreach(var payType in data.SubscriptionPayments)
                {
                        if(payType.Status == true)
                        {
                    SubPaymentResponseDTO paymentResponseDTO = new SubPaymentResponseDTO()
                    {
                        PaymentType = payType.PaymentType,
                        PaymentDate = payType.PaymentDate,
                        UserCanPay = payType.UserCanPay
                    };
                    paymentTypes.Add(paymentResponseDTO);
                        }
                }

                }
                var response = new AdminSubscribtionResponseDTO()
                {
                    Id = subscriptionId,
                    Title = data.Title,
                    Description = data.Description,
                    Duration = data.Duration,
                    IsNewSubscription = (data.Date.Year == DateTime.Now.Year && data.Date.Month == DateTime.Now.Month) ? true : false,
                    IsSpecialOffer = data.IsSpecialOffer,
                    SubscriptionPayments = paymentTypes,
                    ProgramNames=Programs
                };
                return response;
            }
            else
            {
                throw new Exception("Subscription not found");
            }

        }
        public async Task<List<AdminSubscribtionResponseDTO>> GetAllSubscriptions()
        {
            List<AdminSubscribtionResponseDTO> SubscritonResponseList= new List<AdminSubscribtionResponseDTO>();
            var allData=_programRepository.GetAllSubscriptions();
            if (allData != null)
            {
                foreach (var data in allData)
                {
                    List<SubPaymentResponseDTO> paymentTypes = new List<SubPaymentResponseDTO>();
                    List<string> Programs = new List<string>();
                    var programList = _programRepository.GetProgramsOfSubscription(data.Id);
                    if(programList != null)
                    {
                    foreach (var singleProgram in programList)
                    {
                        var workoutProgram = _programRepository.GetWorkoutProgram(singleProgram.ProgramId);
                        if (workoutProgram != null)
                        {
                            Programs.Add(workoutProgram.Name);

                        }
                    }
                    }
                    if (data.SubscriptionPayments != null)
                    {
                    foreach (var payType in data.SubscriptionPayments)
                    {
                        SubPaymentResponseDTO paymentResponseDTO = new SubPaymentResponseDTO()
                        {
                            PaymentType = payType.PaymentType,
                            PaymentDate = payType.PaymentDate,
                            UserCanPay = payType.UserCanPay
                        };
                        paymentTypes.Add(paymentResponseDTO);
                    }

                    }
                    var response = new AdminSubscribtionResponseDTO()
                    {
                        Id = data.Id,
                        Title = data.Title,
                        Description = data.Description,
                        Duration = data.Duration,
                        IsNewSubscription = (data.Date.Year == DateTime.Now.Year && data.Date.Month == DateTime.Now.Month) ? true : false,
                        IsSpecialOffer = data.IsSpecialOffer,
                        SubscriptionPayments = paymentTypes,
                        ProgramNames = Programs
                    };
                    SubscritonResponseList.Add(response);

                }
                return SubscritonResponseList;
            }
            else
            {
                throw new Exception("No data available");
            }
        }

        public async Task<SubscriptionResponseDTO> UpdateSubscription (Guid id,SubscriptionRequestDTO subscription)
        {
            var data=_programRepository.GetSubscription(id);
            if (data != null)
            {
                data.Title = subscription.Title;
                data.Description = subscription.Description;
                data.Duration = subscription.Duration;
                data.IsSpecialOffer = subscription.IsSpecialOffer;
               var updatedData= _programRepository.UpdateSubscription(data);
                var response = new SubscriptionResponseDTO()
                {
                    Title = updatedData.Title,
                    Description = updatedData.Description,
                    Duration = updatedData.Duration,
                    IsSpecialOffer = updatedData.IsSpecialOffer,
                    IsNewSubscription = (updatedData.Date.Year == DateTime.Now.Year && updatedData.Date.Month == DateTime.Now.Month) ? true : false
                };
                return response;
            }
            else
            {
                throw new Exception("Subscription not found");
            }
        }
        public async Task<string> UpdateSubscriptionPayment(Guid id,SubscriptionPaymentRequestDTO subPayment)
        {
            var data=_programRepository.GetSubscriptionPayment(id);
            if (data != null)
            {
                data.PaymentType = subPayment.PaymentType;
                data.PaymentDate = subPayment.PaymentDate;
                data.UserCanPay = subPayment.UserCanPay;
                var UpdatedData=_programRepository.UpdateSubscritionPayment(data);
                if (UpdatedData != null)
                {
                    return "Updated Successfully";
                }
                else
                {
                    throw new Exception("Error in updating");
                }
            }
            else
            {
                throw new Exception("Subscription Payment not found");
            }
        }


        public async Task<string> DeleteSubscription(Guid id)
        {
            var deleteData= _programRepository.GetSubscription(id);
            if (deleteData != null)
            {
                deleteData.Status = false;
                var data=_programRepository.UpdateSubscription(deleteData);
                if (data != null)
                {
                return "Subscription is not available anymore";
                }
            }
                return "Subscription not found";
        }
        public async Task<string> DeleteSubscriptionPayment(Guid id)
        {
            var deleteData = _programRepository.GetSubscriptionPayment(id);
            if (deleteData != null)
            {
                deleteData.Status = false;
                var data = _programRepository.UpdateSubscritionPayment(deleteData);
                if (data != null)
                {
                    return "Subscription Payment is not available anymore";
                }
            }
            return "Payment is not found try again";

        }
        public async Task<string> DeleteSubscribedProgram(Guid subscribeId,Guid programId)
        {
            var deleteData = _programRepository.GetSingleSubscribedProgram(subscribeId,programId);
            if (deleteData != null)
            {
                deleteData.Status = false;
                var data = _programRepository.UpdateSubscribedProgram(deleteData);
                if (data != null)
                {
                    return "Program is not available to subscribe anymore";
                }
            }
            return "Cannot find the Program or subcribtion.Please Try Again!";
        }

    }
}
