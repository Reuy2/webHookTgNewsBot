using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.Domain.Responses;

namespace WorkOutWebHookBot.Services.Interfaces
{
    public interface IBotUserService
    {
        Task<BaseResponse<IEnumerable<BotUser>>> GetUsers();
        Task<BaseResponse<BotUser>> GetUser(int Id);
        Task<BaseResponse<BotUser>> GetUserByTelegramId(long TelegramId);
        Task<BaseResponse<BotUser>> CreateUser(BotUser user);
        Task<BaseResponse<BotUser>> UpdateUser(long TelegramId, BotUser user);
        Task<BaseResponse<BotUser>> DeleteUser(long TelegramId);
    }
}
