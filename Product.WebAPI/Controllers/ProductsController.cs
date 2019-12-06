using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Product.WebAPI.Models;

namespace Product.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        POSStoreDbContext context;

        public ProductsController(POSStoreDbContext sc)
        {
            context = sc;
        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var productList = context.Products.ToList();
            return Ok(productList);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //[Route("jsonbody")]
        public ActionResult Post([FromBody]JObject content)
        {
            JArray a = (JArray)content["SoldProduct"];
            var items = a.ToObject<IList<SoldProduct>>();
            if (items == null)
                return BadRequest("Can not deserialize JSON object");
            foreach (var x in items)
            {
                int res = context.RemoveFromStock(x);
                if(res!=0)
                    return NotFound("Can't update stock, please check product id or units.");
            }
            context.SaveChanges();
            return Ok("Success");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
