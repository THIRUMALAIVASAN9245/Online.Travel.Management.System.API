using AutoMapper;

namespace Online.Travel.AuthService.API.Contract
{
    public class ContractMappingProfile : Profile
    {
        /// <summary>
        /// ContractMappingProfile Constructor
        /// </summary>
        public ContractMappingProfile()
        {
            CreateMap<Model.UserModel, Entities.UserDetail>();

            CreateMap<Entities.UserDetail, Model.UserModel>();
        }
    }
}