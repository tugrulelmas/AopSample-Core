using AopSample.ApplicationServices;
using AopSample.Entities;
using AopSample.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AopSample.Controllers
{
    [Route("api/[controller]")]
    [CustomActionFilter]
    public class ValuesController : Controller
    {
        private readonly IUserService userService;

        public ValuesController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var result = userService.GetAll();
            return new ObjectResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]User user)
        {
            userService.Add(user);
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
