using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace MappingProfile;

public MappingProfile()
{
    CreateMap<Owner, OwnerDto>();
    CreateMap<Account, AccountDto>();
    CreateMap<OwnerForCreationDto, Owner>();

    CreateMap<OwnerForUpdateDto, Owner>();
}