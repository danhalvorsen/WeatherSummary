using Microsoft.Extensions.Configuration;

namespace BasicWebAPI.DAL
{
    public class CreateCityCommand
    {
        private readonly IConfiguration config;

        public CreateCityCommand(IConfiguration config)
        {
            this.config = config;
        }

    }



}
