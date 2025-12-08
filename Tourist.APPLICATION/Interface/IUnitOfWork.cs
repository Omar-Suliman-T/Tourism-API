using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.Interface
{
    public interface IUnitOfWork:IDisposable
    {
        IAuth Auth { get; }
        IUser User { get; }
        Task<int> CompleteAsync();
        public Task SaveChangesAsync();
    }
}
