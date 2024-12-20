using Uex.ContactBook.Domain.Model.DTOs.User;
using Uex.ContactBook.Domain.Model.Entities;
using Mapster;

namespace Uex.ContactBook.Api.Extensions
{
    public static partial class MappingConfiguration
    {
        private static void RegisterCheckListMap()
        {
            TypeAdapterConfig<User, UserGetAllResponse>
                .NewConfig()
                .Map(destiny => destiny.UserName, source => source.UserName);
        }
    }
}
