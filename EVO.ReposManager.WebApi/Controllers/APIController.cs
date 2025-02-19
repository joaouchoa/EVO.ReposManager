﻿using Microsoft.AspNetCore.Mvc;

namespace EVO.ReposManager.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        protected ActionResult CustomResponse(int status, bool sucess, object data = null)
        {
            return (status, sucess) switch
            {
                (200, true) => Ok(new BaseResponse { StatusCode = status, Sucess = sucess, Message = "Sucess", Data = data }),
                (201, true) => Ok(new BaseResponse { StatusCode = status, Sucess = sucess, Message = "Created", Data = data }),
                (204, true) => Ok(new BaseResponse { StatusCode = status, Sucess = sucess, Message = "Deleted", Data = data }),
                (400, false) => BadRequest(new BaseResponse { StatusCode = status, Sucess = sucess, Message = "Errors during the transaction.", Data = data }), // Mensagem para erros de validação
                (404, false) => NotFound(new BaseResponse { StatusCode = status, Sucess = sucess, Message = "No elements found.", Data = data })
            };
        }
    }
}
