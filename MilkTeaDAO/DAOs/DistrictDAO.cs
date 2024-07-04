using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class DistrictDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public DistrictDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<District> GetAllDistrict()
        {
            try
            {
                return _context.Districts!.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewDistrict(District district)
        {
            try
            {
                _context.Add(district);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public District GetDistrictByID(int id)
        {
            try
            {
                var district = _context.Districts!.FirstOrDefault(c => c.DistrictID.Equals(id));
                return district;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDistrict(District district)
        {
            try
            {
                var a = _context.Districts!.SingleOrDefault(c => c.DistrictID == district.DistrictID);

                _context.Entry(a).CurrentValues.SetValues(district);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusDistrict(District district)
        {
            var _district = _context.Districts!.FirstOrDefault(c => c.DistrictID.Equals(district.DistrictID));


            if (_district == null)
            {
                return false;
            }
            else
            {
                _context.Entry(_district).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
