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
        
        
       
        
    }
}
