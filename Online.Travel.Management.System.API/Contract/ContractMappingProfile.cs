namespace Online.Travel.Management.System.API.Contract
{
    using AutoMapper;

    public class ContractMappingProfile : Profile
    {
        /// <summary>
        /// ContractMappingProfile Constructor
        /// </summary>
        public ContractMappingProfile()
        {
            CreateMap<Model.Booking, Entities.Booking>();
            CreateMap<Model.UserModel, Entities.UserDetail>();

            CreateMap<Entities.Booking, Model.Booking>();
            CreateMap<Entities.UserDetail, Model.UserModel>();
        }
    }
}
