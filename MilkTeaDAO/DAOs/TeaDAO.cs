using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaDAO.DAOs
{
    public class TeaDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public TeaDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<Tea> GetAllTea()
        {
            try
            {
                return _context.Teas.Include(c => c.Comments)
                                    .Include(a => a.OrderDetails)
                                    .Include(a => a.DetailsMaterials)
                                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewTea(Tea tea)
        {
            try
            {
                _context.Add(tea);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Tea GetTeaByID(int id)
        {
            try
            {
                var tea = _context.Teas!.Include(c => c.Comments)
                                           .Include(a => a.OrderDetails)
                                           .Include(a => a.DetailsMaterials)
                                           .SingleOrDefault(c => c.TeaID == id);
                return tea;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTea(Tea tea)
        {
            try
            {
                var a = _context.Teas!.SingleOrDefault(c => c.TeaID == tea.TeaID);

                _context.Entry(a).CurrentValues.SetValues(tea);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusTea(Tea tea)
        {
            var _tea = _context.Teas.FirstOrDefault(c => c.TeaID.Equals(tea.TeaID));


            if (_tea == null)
            {
                return false;
            }
            else
            {
                _tea.Status = false;
                _context.Entry(_tea).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
