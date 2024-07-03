using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class DetailsMaterialDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public DetailsMaterialDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<DetailsMaterial> GetAllDetailsMaterial()
        {
            try
            {
                return _context.DetailsMaterials!.Include(a => a.Tea)
                                                 .Include(a => a.Material)
                                                 .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewDetailsMaterial(DetailsMaterial detail)
        {
            try
            {
                _context.Add(detail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public DetailsMaterial GetDetailsMaterialByID(int id)
        {
            try
            {
                var detail = _context.DetailsMaterials!.Include(a => a.Tea)
                                                       .Include(a => a.Material)
                                                       .SingleOrDefault(c => c.DetailsMaterialID == id);
                return detail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDetailsMaterial(DetailsMaterial detail)
        {
            try
            {
                var a = _context.DetailsMaterials!.SingleOrDefault(c => c.DetailsMaterialID == detail.DetailsMaterialID);

                _context.Entry(a).CurrentValues.SetValues(detail);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteDetailsMaterialById(int id)
        {
            var _detail = _context.DetailsMaterials!.SingleOrDefault(lo => lo.DetailsMaterialID == id);
            if (_detail != null)
            {
                _context.Remove(_detail);
                _context.SaveChanges();
            }
        }



    }
}
