using Microsoft.AspNetCore.Mvc;

namespace Yacaba.Web.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {

        [HttpGet]
        public IEnumerable<Int32> Get() {
            return [1, 3, 4];
        }

    }
}
