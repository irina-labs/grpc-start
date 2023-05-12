using Data.Interface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   
    public class TalkRepository : ITalkRepository
    {
        private readonly ConferenceDbContext dbContext;

        public TalkRepository(ConferenceDbContext context)
        {
            this.dbContext = context;
        }


        public async Task<List<Talk>> GetAsync()
        {
            return await dbContext.Talks.AsNoTracking().ToListAsync();
        }

        public async Task<Talk> GetByIdAsync(int talkId)
        {
            return await dbContext.Talks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == talkId);
        }

        public async Task<Talk> AddAsync(Talk talk)
        {
            dbContext.Add(talk);
            await dbContext.SaveChangesAsync();
            return talk;
        }

        public async Task<int> UpdateAsync(Talk talk)
        {
            var talkToUpdate = await dbContext.Talks
                                 .Where(x => x.Id == talk.Id)
                                 .FirstOrDefaultAsync();

            //update this?

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int speakerId)
        {
            var speakerToDelete = await dbContext.Talks
                                                    .Where(x => x.Id == speakerId)
                                                    .FirstOrDefaultAsync();

            dbContext.Remove(speakerToDelete);
            return await dbContext.SaveChangesAsync();
        }
    }
}
