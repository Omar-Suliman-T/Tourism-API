using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.DTO.Review;

namespace Tourist.APPLICATION.Mapping.Review
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile()
        {
            CreateMap<AddreviewDTO, Tourist.DOMAIN.model.Review>();
            CreateMap<Tourist.DOMAIN.model.Review, GetReviewByIdDTO>();
        }
    }
}
