using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Conference.Domain
{
    public static class DataSeeder
    {

        public static void SeedData(ConferenceDbContext dbContext)
        {
            if (!dbContext.Speakers.Any())
            {
                dbContext.Speakers.AddRange(LoadSpeakers());
                dbContext.SaveChanges();
            }
        }

        private static List<Speaker> LoadSpeakers()
        {
            var jsonPath = @"D:\learning\MyGit\irina-labs\grpcworkshop\src\Domain\data.json";
            using (StreamReader file = File.OpenText(jsonPath))
            {

                JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                serializer.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                var speakers = (List<Speaker>)serializer.Deserialize(file, typeof(List<Speaker>));
                return speakers;
            }
        }
    }
}
