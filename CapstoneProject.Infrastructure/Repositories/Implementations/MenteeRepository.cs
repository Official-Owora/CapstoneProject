﻿using CapstoneProject.Domain.Entities;
using CapstoneProject.Domain.Enums;
using CapstoneProject.Infrastructure.Persistence;
using CapstoneProject.Infrastructure.Repositories.Abstractions;
using CapstoneProject.Shared.RequestParameter.Common;
using CapstoneProject.Shared.RequestParameter.ModelParameters;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Infrastructure.Repositories.Implementations
{
    internal sealed class MenteeRepository : RepositoryBase<Mentee>, IMenteeRepository
    {
        private readonly DbSet<Mentee> _mentees;

        public MenteeRepository(DataContext dataContext) : base(dataContext)
        {
            _mentees = dataContext.Set<Mentee>();
        }
       
        public async Task<IEnumerable<Mentee>> GetAllMenteesAsync()
        {
            var mentee = await _mentees.OrderBy(x => x.UserId).ToListAsync();
            return mentee;
        }


        public async Task<Mentee> GetMenteeByIdAsync(string id)
        {
            return await _mentees.Where(m => m.UserId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<PagedList<Mentee>> GetMenteeByIsMatched(MenteeRequestInputParameter parameter, bool IsMatched, ProgrammingLanguage programmingLanguage, TechTrack techTrack)
        {
            IQueryable<Mentee> mentees = _mentees;
            if (!IsMatched)
            {
                mentees = mentees.Where(m => m.IsMatched && m.ProgrammingLanguage == programmingLanguage && m.TechTrack == techTrack);
            }
            var menteesPage = await mentees
                .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                .Take(parameter.PageSize).ToListAsync();
            var totalCount = await _mentees.CountAsync();
            return new PagedList<Mentee>(menteesPage, totalCount, parameter.PageNumber, parameter.PageSize);
            
        }
    }
}
