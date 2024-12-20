namespace Uex.ContactBook.Domain.Model.DTOs.User
{
    public readonly record struct UserGetListResponse(
        List<UserGetAllResponse> Items
    );
}
