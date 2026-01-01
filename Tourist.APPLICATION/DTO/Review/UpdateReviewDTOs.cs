using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.DTO.Review
{
    public class UpdateReviewDTOs
    {
        public int ReviewId { get; set; }
        public Rating? Rating { get; set; }
        public List<string>? WhatWasGreat { get; set; }

        public string? Comment { get; set; }
        public string? imageUrl { get; set; }

    }
}
