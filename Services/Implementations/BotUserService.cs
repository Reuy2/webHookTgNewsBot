using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.DAL.Repository.Interfaces;
using WorkOutWebHookBot.Domain.Enums;
using WorkOutWebHookBot.Domain.Responses;
using WorkOutWebHookBot.Services.Interfaces;

namespace WorkOutWebHookBot.Services.Implementations
{
    public class BotUserService : IBotUserService
    {
        IBaseRepository<BotUser> _userRepository;

        public BotUserService(IBaseRepository<BotUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<BotUser>> CreateUser(BotUser user)
        {
            var response = new BaseResponse<BotUser>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.TelegramId == user.TelegramId);
                if (userFromDb is not null)
                {
                    response.StatusCode = StatusCode.Ok;
                    response.Message = "UserAlreadyExist";
                    return response;
                }
                await _userRepository.Create(user);
                response.Data = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.TelegramId == user.TelegramId);
                response.StatusCode = StatusCode.Ok;
                return response;


            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[createUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<BotUser>> DeleteUser(long TelegramId)
        {
            var response = new BaseResponse<BotUser>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.TelegramId == TelegramId);
                if (userFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found User";
                    return response;
                }
                await _userRepository.Delete(userFromDb);
                response.Data = userFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[DeleteUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<BotUser>> GetUser(int Id)
        {
            var response = new BaseResponse<BotUser>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
                if (userFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found User";
                    return response;
                }

                response.Data = userFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[GetUser]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<BotUser>> GetUserByTelegramId(long TelegramId)
        {
            var response = new BaseResponse<BotUser>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.TelegramId == TelegramId);
                if (userFromDb is null)
                {
                    response.StatusCode = StatusCode.Ok;
                    response.Message = "Unable To Found User";
                    return response;
                }

                response.Data = userFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[GetUserByTelegramId]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<IEnumerable<BotUser>>> GetUsers()
        {
            var response = new BaseResponse<IEnumerable<BotUser>>();
            try
            {
                var users = await _userRepository.GetAll().ToListAsync();
                if (users is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found Users";
                    return response;
                }

                response.Data = users;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[GetUsers]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<BotUser>> UpdateUser(long TelegramId, BotUser user)
        {
            var response = new BaseResponse<BotUser>();
            try
            {
                var userFromDb = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.TelegramId == TelegramId);
                if (userFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found User";
                    return response;
                }

                await _userRepository.Update(user);
                response.Data = userFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[UpdateUser]: {ex.Message}";
                return response;
            }
        }
    }
}
