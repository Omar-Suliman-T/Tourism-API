using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;
using Tourist.APPLICATION.UseCase.Monument;

namespace Tourist.PERSISTENCE.Repository
{
    public class MonumentRepository : Repository<DOMAIN.model.Monument>, IMonumentRepository
    {
        public ApplicationDbContext _context;
        public MonumentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(DOMAIN.model.Monument monument)
        {
            _context.Update(monument);
        }
    }
}
