using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.DOMAIN.model;

namespace Tourist.PERSISTENCE.Repository
{
    public class TourRepository:Repository<Tour>,ITourRepository
    {
        private readonly ApplicationDbContext _context;
        public TourRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Tour tour)
        {
            _context.Update(tour);
        }
    }
}
