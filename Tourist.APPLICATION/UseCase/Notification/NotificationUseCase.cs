using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.APPLICATION.Interface;

namespace Tourist.APPLICATION.UseCase.Notification
{
    public class NotificationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
