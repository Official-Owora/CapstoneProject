using CapstoneProject.Infrastructure.Repositories.Abstractions;

namespace CapstoneProject.Infrastructure.RepositoryManager
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMentorRepository MentorRepository { get; }
        IMenteeRepository MenteeRepository { get; }
        IAppointmentScheduleRepository AppointmentScheduleRepository { get; }
        Task SaveAsync();

    }
}
