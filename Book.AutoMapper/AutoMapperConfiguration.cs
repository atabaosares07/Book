using System;
using System.Collections.Generic;
using System.Text;

namespace Book.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            global::AutoMapper.Mapper.Initialize(config => config.AddProfile(new AutoMapperProfile()));
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
