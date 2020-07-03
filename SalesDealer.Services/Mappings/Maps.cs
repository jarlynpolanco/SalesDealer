using AutoMapper;
using SalesDealer.Data.Models;
using SalesDealer.Shared;
using System;
using System.Globalization;

namespace SalesDealer.Services.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<SalesFH, SaleSummary>().ForMember(x => x.SaleDate, member =>
                member.MapFrom(source => DateTime.ParseExact(source.SaleDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }
    }
}
