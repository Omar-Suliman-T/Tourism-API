using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model;

namespace Tourist.APPLICATION.Interface
{
    public interface IMonumentRepository:IRepository<Monument>
    {
        public void Update(Monument monument);
    }
}
