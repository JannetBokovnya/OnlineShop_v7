using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.UserManagementService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous] //без авторизации при вызове
    public class HealthCheckController : ControllerBase
    {
        //для проверки работает ли наша услуга
        [HttpGet]

        public string Check() => "Service is online";

    }
}
