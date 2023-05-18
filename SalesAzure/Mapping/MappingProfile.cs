using AutoMapper;
using SalesAzure.DTO;
using SalesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAzure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SalesModel, SalesDTO>();
            CreateMap<SalesDTO, SalesModel>();
        }
    }
}
