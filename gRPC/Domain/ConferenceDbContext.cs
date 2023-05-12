using Conference.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class ConferenceDbContext : DbContext
    {
        public ConferenceDbContext()
        {
        }

        public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Speaker> Speakers { get; set; }

        public virtual DbSet<Talk> Talks { get; set; }

     
    }
}
