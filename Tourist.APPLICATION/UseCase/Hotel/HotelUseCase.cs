using FluentEmail.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Tourist.APPLICATION.DTO.Hotel;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.UseCase.Hotel
{
    public class HotelUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public HotelUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(HttpStatusCode, string)> AddHotelAsync(AddHotelDTO hotelDto)
        {
            try
            {
                var hotel = new DOMAIN.model.Hotel()
                {
                    Name = hotelDto.Name,
                    Description = hotelDto.Description,
                    Stars = hotelDto.Stars,
                    PricePerNight = hotelDto.PricePerNight,
                    Address = hotelDto.Address,
                    Phone = hotelDto.Phone,
                    ImageUrl = hotelDto.ImageUrl,
                    CityId = hotelDto.CityId
                };

                await _unitOfWork.Hotel.AddAsync(hotel);
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Added Successfully");
            }
            catch (Exception ex)
            {

                return (HttpStatusCode.BadRequest, ex.Message);

            }
        }
        public async Task<(HttpStatusCode, string)> UpdateHotelAsync(UpdateHotelDTO hotelDto)
        {
            try
            {

                var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == hotelDto.HotelId&&h.IsDeleted==false);
                if (hotel == null)
                {
                    return (HttpStatusCode.NotFound, "Hotel Not Found");
                }
                hotel.Name = hotelDto.Name;
                hotel.Description = hotelDto.Description;
                hotel.Stars = hotelDto.Stars;
                hotel.PricePerNight = hotelDto.PricePerNight;
                hotel.Address = hotelDto.Address;
                hotel.Phone = hotelDto.Phone;
                hotel.ImageUrl = hotelDto.ImageUrl;
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Updated Successfully");
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public async Task<(HttpStatusCode, string)> DeleteHotelAsync(int  HotelId)
        {
            try
            { 
                var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == HotelId && h.IsDeleted==false);
                if (hotel == null)
                {
                    return (HttpStatusCode.NotFound, "Hotel Not Found");
                }
                hotel.IsDeleted=true;
                await _unitOfWork.SaveChangesAsync();
                return (HttpStatusCode.OK, "Hotel Deleted Successfully");
            }
            catch (Exception ex)
            {
                return (HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public async Task<(HttpStatusCode, DOMAIN.model.Hotel)> GetHotelByIdAsync(int  HotelId)
        {
            
                var hotel = await _unitOfWork.Hotel.GetAsync(h => h.HotelId == HotelId && h.IsDeleted==false);
                if (hotel == null)
                {
                    return (HttpStatusCode.NotFound, new DOMAIN.model.Hotel());
                }
                return (HttpStatusCode.OK, hotel);
        }
        public async Task<(HttpStatusCode, List<DOMAIN.model.Hotel>)> GetAllHoteIdAsync()
        {
            
                var hotels = await _unitOfWork.Hotel.GetAllAsync(h=>h.IsDeleted==false);

                return (HttpStatusCode.OK, hotels.ToList());
        }
    }
}
