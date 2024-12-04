using GymManagementSystem.Entities;

namespace GymManagementSystem.IRepositories
{
    public interface IProgramRepository
    {
        WorkoutProgram CreateProgram(WorkoutProgram workoutProgram);
        ProgramImages CreateImage(ProgramImages programImages);
        WorkoutProgram GetWorkoutProgram(Guid id);
        Subscription GetSubscription(Guid id);
        ProgramPayment GetProgramPayment(Guid ProgramId, Guid SubscriptionPaymentId);
        ProgramPayment GetProgramPayment(Guid Id);
        List<WorkoutProgram> GetAllWorkoutProgram();
        WorkoutProgram UpdateProgram(WorkoutProgram workoutProgram);
        ProgramPayment UpdateProgramPayment(ProgramPayment programPayment);
        ProgramPayment CreateProgramPayment(ProgramPayment programPayment);
        void DeleteProgram(WorkoutProgram workoutProgram);


        Subscription CreateSubscription(Subscription subscription);
        SubscriptionPayment CreateSubscriptionPayment(SubscriptionPayment subscriptionPayment);
        SubscribedProgram AddSubscribedProgram(SubscribedProgram subscribedProgram);
        List<Subscription> GetAllSubscriptions();
        Subscription UpdateSubscription(Subscription subscription);
        void DeleteSubscription(Subscription subscription);
        SubscriptionPayment GetSubscriptionPayment(Guid id,Guid subId);
        void DeleteSubscriptionPayment(SubscriptionPayment subscriptionPayment);
        List<SubscribedProgram> GetAllSubscribedPrograms();
        SubscribedProgram GetSingleSubscribedProgram(Guid subscribeId, Guid programId);
        List<SubscribedProgram> GetProgramsOfSubscription(Guid subscriptionId);
        List<SubscribedProgram> GetSubscriptionsOfSingleProgram(Guid programId);
        SubscribedProgram UpdateSubscribedProgram(SubscribedProgram subscribedProgram);
        SubscriptionPayment UpdateSubscritionPayment(SubscriptionPayment subscriptionPayment);
        List<SubscriptionPayment> GetSubscriptionAllPayments(Guid id);
        SubscriptionPayment GetSubscriptionPayment(Guid id);
    }
}
