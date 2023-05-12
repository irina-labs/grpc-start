using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ITalkRepository
    {
        Task<Talk> AddAsync(Talk talk);
        Task<int> DeleteAsync(int speakerId);
        Task<List<Talk>> GetAsync();
        Task<Talk> GetByIdAsync(int talkId);
        Task<int> UpdateAsync(Talk talk);
    }

}
