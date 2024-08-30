using Microsoft.AspNetCore.Mvc;

namespace WSInformatica.Models.Response
{
    [Route("api")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly InfoContext _context;

        public BaseController(InfoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string Get()
        {
            return "Informatica API funcionando OK";
        }

        /*[HttpGet("TestDatabase")]
        public async Task<ActionResult<BaseResponse<string>>> TestDatabase()
        {
            object result = await _context.ExecuteQuery("SELECT 1 FROM INFORMATION_SCHEMA.TABLES");
            string response = result is not null ? "Conexion con la base OK" : "";
            return new ObjectResult(response);
        }*/
    }

    public class BaseResponse<T> : BaseResponse
    {
        public new T Data { get; private set; } = default;

        public BaseResponse(bool success, int resultCode, string msg = "", T data = default) : base(success, resultCode, msg)
        {
            Data = data;
        }
    }

    public class BaseResponse
    {
        public virtual bool Error { get; set; } = false;
        public virtual bool Success { get; set; } = false;
        public virtual string Message { get; set; } = string.Empty;
        public virtual object? Data { get; set; } = null;
        public virtual int ResultCode { get; set; } = 0;

        public BaseResponse(bool success, int resultCode = 0, string msg = "")
        {
            Success = success;
            Error = !success;
            ResultCode = resultCode;
            Message = msg;
        }
    }
}

