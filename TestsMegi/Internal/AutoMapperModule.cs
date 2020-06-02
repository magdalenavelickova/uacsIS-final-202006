using System.Reflection;

using AutoMapper;


namespace UniversityApplication.Tests.Internal
{
    public static class AutoMapperModule
    {
        private static MapperConfiguration configuration;
        private static IMapper mapper;

        public static IMapper CreateMapper()
        {
            if (mapper == null)
            {
                mapper = new Mapper(CreateMapperConfiguration());
            }

            return mapper;
        }

        public static MapperConfiguration CreateMapperConfiguration()
        {
            if (configuration == null)
            {
                configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(Assembly.Load("BankApplication.Data"));

                    //cfg.AddProfile(new AddressProfile());
                    //cfg.AddProfile(new ExamProfile());
                    //cfg.AddProfile(new StudentProfile());
                    //cfg.AddProfile(new TranscriptProfile());
                });
            }

            return configuration;
        }
    }
}
