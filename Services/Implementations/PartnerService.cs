using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.DAL.Repository.Implementations;
using WorkOutWebHookBot.DAL.Repository.Interfaces;
using WorkOutWebHookBot.Domain.Enums;
using WorkOutWebHookBot.Domain.Responses;
using WorkOutWebHookBot.Services.Interfaces;

namespace WorkOutWebHookBot.Services.Implementations
{
    public class PartnerService : IPartnerService
    {
        IBaseRepository<Partner> _partnerRepository = new PartnerRepository();
        public async Task<BaseResponse<Partner>> CreatePartner(Partner partner)
        {
            var response = new BaseResponse<Partner>();
            try
            {
                var partnerFromDb = await _partnerRepository.GetAll().FirstOrDefaultAsync(x => x.Url == partner.Url && x.Caption == partner.Caption);
                if (partnerFromDb is not null)
                {
                    response.StatusCode = StatusCode.Ok;
                    response.Message = "partnerAlreadyExist";
                    return response;
                }
                await _partnerRepository.Create(partner);
                response.Data = await _partnerRepository.GetAll().FirstOrDefaultAsync(x =>  x.Url == partner.Url && x.Caption == partner.Caption);
                response.StatusCode = StatusCode.Ok;
                return response;


            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[createPartner]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<Partner>> DeletePartner(Partner partner)
        {
            var response = new BaseResponse<Partner>();
            try
            {
                var arr = await _partnerRepository.GetAll().ToListAsync();
                var partnerFromDb = await _partnerRepository.GetAll().FirstOrDefaultAsync(x => x.Caption == partner.Caption && x.Url == partner.Url);
                if (partnerFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found Partner";
                    return response;
                }
                await _partnerRepository.Delete(partnerFromDb);
                response.Data = partnerFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;


            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[DeletePartner]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<Partner>> GetPartner(int Id)
        {
            var response = new BaseResponse<Partner>();
            try
            {
                var userFromDb = await _partnerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
                if (userFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found Partner";
                    return response;
                }

                response.Data = userFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[GetPartner]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<IEnumerable<Partner>>> GetPartners()
        {
            var response = new BaseResponse<IEnumerable<Partner>>();
            try
            {
                var partners = await _partnerRepository.GetAll().ToListAsync();
                if (partners is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found partners";
                    return response;
                }

                response.Data = partners;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[GetPartners]: {ex.Message}";
                return response;
            }
        }

        public async Task<BaseResponse<Partner>> UpdatePartner(int id, Partner user)
        {
            var response = new BaseResponse<Partner>();
            try
            {
                var partnerFromDb = await _partnerRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (partnerFromDb is null)
                {
                    response.StatusCode = StatusCode.NotFound;
                    response.Message = "Unable To Found Partner";
                    return response;
                }

                await _partnerRepository.Update(user);
                response.Data = partnerFromDb;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCode.InternalServerError;
                response.Message = $"[UpdatePartner]: {ex.Message}";
                return response;
            }
        }
    }
}
