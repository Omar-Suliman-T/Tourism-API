using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Hotel;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IHotelRepository:IRepository<Hotel>
    {
        public Task UpdateAsync(Hotel hotel);
    }
}
