using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class RoleDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public RoleDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<Role> GetAllRole()
        {
            try
            {
                return _context.Roles!.Include(a => a.Users).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
