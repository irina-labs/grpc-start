using Data.Interface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{


    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ConferenceDbContext dbContext;

        public SpeakerRepository(ConferenceDbContext context)
        {
            this.dbContext = context;
        }


        public async Task<List<Speaker>> GetAllAsync()
        {
            return await dbContext.Speakers.AsNoTracking().ToListAsync();
        }

        public async Task<Speaker> GetByIdAsync(int speakerId)
        {
            return await dbContext.Speakers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == speakerId);
        }

        public async Task<Speaker> AddAsync(Speaker speaker)
        {
            dbContext.Add(speaker);
            await dbContext.SaveChangesAsync();
            return speaker;
        }

        public async Task<int> UpdateAsync(Speaker speaker)
        {
            dbContext.Update(speaker);
           
            return await dbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteAsync(int speakerId)
        {
            var speakerToDelete = await dbContext.Speakers
                                                    .Where(x => x.Id == speakerId)
                                                    .FirstOrDefaultAsync();

            dbContext.Remove(speakerToDelete);
            return await dbContext.SaveChangesAsync();
        }
    }
}
