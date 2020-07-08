using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cepres.Services.API.Test
{
    public class AutoMapperConfig
    {
        public static readonly AutoMapperConfig Instance = new AutoMapperConfig();
        public IMapper Mapper { get; private set; }
        private AutoMapperConfig()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Infrastructure.AutoMapper());
            });
            Mapper = mockMapper.CreateMapper();
        }
    }
}
