using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recrutation_exercise.Model;
using Recrutation_exercise.Repository;

namespace Recrutation_exercise.Controllers
{

    
    [EnableCors("CORS")]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private IProductRepository _ProductRepo;

        public ProductController(IProductRepository productRepository)
        {
            _ProductRepo = productRepository;
        }
        // GET: api/Product
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _ProductRepo.GetAll();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var product = _ProductRepo.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _ProductRepo.Add(product);
            if(!_ProductRepo.Save())
            {
                throw new Exception("Creating product failed on save");
            }
            return Ok(product);

        }
        
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if( id != product.ProductId)
            {
                return BadRequest();
            }
            _ProductRepo.UpdateItem(product);
            
            if(!_ProductRepo.Save())
            {
                throw new Exception("Updating product failed on save");
            }
            return Ok(product);
        }
        
        
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)

        {
            Product product = _ProductRepo.GetSingle(id);
            if(product == null)
            {
                return NotFound();
            }

            _ProductRepo.Delete(id);
            if(!_ProductRepo.Save())
            {
                throw new Exception("Deleting product failed on save");
            }
            return NoContent();
        }
    }
}
