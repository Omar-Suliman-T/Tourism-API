using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Trip;
using Tourist.DOMAIN.model;


namespace Tourist.APPLICATION.Mapping.Trip
{
    public class TripProfile: Profile
    {
        public TripProfile()
        {
            // CreateMap<From, To>();

            CreateMap<AddTripDTO, Tourist.DOMAIN.model.Trip>();

            CreateMap<Tourist.DOMAIN.model.Trip, GetTripsByIdDTO>();
        }
    }
}
