﻿using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class MaterialDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public MaterialDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<Material> GetAllMaterial()
        {
            try
            {
                return _context.Materials!.Include(a => a.DetailsMaterials).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewMaterial(Material material)
        {
            try
            {
                _context.Add(material);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Material GetMaterialByID(int id)
        {
            try
            {
                var material = _context.Materials!.Include(a => a.DetailsMaterials).SingleOrDefault(c => c.MaterialID == id);
                return material;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMaterial(Material material)
        {
            try
            {
                var a = _context.Materials!.SingleOrDefault(c => c.MaterialID == material.MaterialID);

                _context.Entry(a).CurrentValues.SetValues(material);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusMaterial(Material material)
        {
            var _material = _context.Materials!.FirstOrDefault(c => c.MaterialID.Equals(material.MaterialID));


            if (_material == null)
            {
                return false;
            }
            else
            {
                _material.Status = false;
                _context.Entry(_material).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }

    }
}
