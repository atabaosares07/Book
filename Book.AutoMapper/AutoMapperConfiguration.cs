using System;
using System.Collections.Generic;
using System.Text;

namespace Book.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            global::AutoMapper.Mapper.Initialize(config => config.AddProfile(new AutoMapperProfile()));
        }
    }
}
