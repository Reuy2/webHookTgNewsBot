using System.Reflection.Emit;
using WorkOutWebHookBot.DAL.Entities;
using WorkOutWebHookBot.Domain.Responses;

namespace WorkOutWebHookBot.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<BaseResponse<IEnumerable<Partner>>> GetPartners();
        Task<BaseResponse<Partner>> GetPartner(int Id);
        Task<BaseResponse<Partner>> CreatePartner(Partner user);
        Task<BaseResponse<Partner>> UpdatePartner(int id, Partner user);
        Task<BaseResponse<Partner>> DeletePartner(Partner partner);
    }
}
