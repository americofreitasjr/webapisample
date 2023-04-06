using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogonController : ControllerBase
    {
        private static readonly LogonRequest[] Logons = new LogonRequest[]
        {
            new LogonRequest{ Username="jr",Password="123456"},
            new LogonRequest{ Username="thayna",Password="123"},
        };

        private readonly ILogger<LogonController> _logger;

        public LogonController(ILogger<LogonController> logger)
        {
            _logger = logger;
        }

        [HttpPost] 
        public ActionResult<LogonResponse> Post(LogonRequest req)
        {
            var logado = Logons.Any(w => w.Username == req.Username && w.Password == req.Password);

            var response = new LogonResponse() {
                Username = req.Username,
                Logado = logado,
                Data = DateTime.Now,
            };

            return Ok(response);
        }
    }
}