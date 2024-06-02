using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaDAO.DAOs
{
    public class DetailsMeterialDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public DetailsMeterialDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }
        public List<DetailsMaterial> GetAllDetailsMaterial()
        {
            try
            {
                return _context.DetailsMaterials!.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DetailsMaterial> GetDetailsMaterialByTeaID(int id)
        {
            try
            {
                return _context.DetailsMaterials!.Where(a => a.TeaID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewDetailDetailsMaterial(DetailsMaterial detailsMaterial)
        {
            try
            {
                _context.Add(detailsMaterial);
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
                var detailsMaterial = _context.DetailsMaterials!.SingleOrDefault(c => c.DetailsMaterialID == id);
                return detailsMaterial;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateDetailsMaterial(DetailsMaterial detailsMaterial)
        {
            try
            {
                var a = _context.DetailsMaterials!.SingleOrDefault(c => c.DetailsMaterialID == detailsMaterial.DetailsMaterialID);

                _context.Entry(a).CurrentValues.SetValues(detailsMaterial);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteDetailsMaterial(int id)
        {
            try
            {
                var detailsMaterial = _context.DetailsMaterials.SingleOrDefault(a => a.DetailsMaterialID == id);
                if (detailsMaterial != null)
                {
                    _context.DetailsMaterials.Remove(detailsMaterial);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("DetailsMaterial not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
