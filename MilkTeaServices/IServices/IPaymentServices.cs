using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface IPaymentServices
    {
        Task CreateOrder(int id);
    }
}
