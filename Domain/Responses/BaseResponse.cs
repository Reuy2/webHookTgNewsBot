using Microsoft.AspNetCore.Http;
using WorkOutWebHookBot.Domain.Enums;

namespace WorkOutWebHookBot.Domain.Responses
{
    public class BaseResponse<T>
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
