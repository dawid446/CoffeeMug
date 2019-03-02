using Microsoft.EntityFrameworkCore;
using Recrutation_exercise.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutation_exercise.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetSingle(Guid id);
        void Add(Product item);
        void UpdateItem(Product item);
        void Delete(Guid id);
        bool Save();

    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;

        }

        public void Add(Product item)
        {
            _context.Products.Add(item);
        }

        public void Delete(Guid id)
        {
            Product productItem = GetSingle(id);
            _context.Products.Remove(productItem);
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {

                return _context.Products.ToList();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Product GetSingle(Guid id)
        {
            try
            {
                return _context.Products.Where(s => s.ProductId == id).FirstOrDefault();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateItem(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
            
        }

       
    }
}
