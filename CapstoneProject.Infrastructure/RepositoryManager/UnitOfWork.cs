using CapstoneProject.Domain.Entities;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Infrastructure.Repositories.Implementations;

namespace CapstoneProject.Infrastructure.RepositoryManager
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private IMentorRepository _mentorRepository;
        private IMenteeRepository _menteeRepository;
        private IAppointmentScheduleRepository _appointmentScheduleRepository;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dataContext);
                }
                return _userRepository;
            }
        }
        public IMentorRepository MentorRepository
        {
            get
            {
                if( _mentorRepository == null)
                {
                    _mentorRepository = new MentorRepository(_dataContext);
                }
                return _mentorRepository;
            }
        }
        public IMenteeRepository MenteeRepository
        {
            get
            {
                if ( _menteeRepository == null)
                {
                    _menteeRepository = new MenteeRepository(_dataContext);
                }
                return _menteeRepository;
            }
        }
        public IAppointmentScheduleRepository AppointmentScheduleRepository
        {
            get
            {
                if (_appointmentScheduleRepository == null)
                {
                    _appointmentScheduleRepository = new AppointmentScheduleRepository(_dataContext);
                }
                return _appointmentScheduleRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
