using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ISpeakerRepository
    {
        Task<Speaker> AddAsync(Speaker speaker);
        Task<int> DeleteAsync(int speakerId);
        Task<List<Speaker>> GetAllAsync();
        Task<Speaker> GetByIdAsync(int speakerId);
        Task<int> UpdateAsync(Speaker speaker);
    }
}
