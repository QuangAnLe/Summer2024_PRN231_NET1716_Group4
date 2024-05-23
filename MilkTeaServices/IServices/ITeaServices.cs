using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface ITeaServices
    {
        List<Tea> GetAllTea();
        void AddNewTea(Tea tea);
        Tea GetTeaByID(int id);
        void UpdateTea(Tea tea);
        bool ChangeStatusTea(Tea tea);
    }
}
