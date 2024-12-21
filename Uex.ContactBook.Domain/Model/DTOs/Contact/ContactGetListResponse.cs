namespace Uex.ContactBook.Domain.Model.DTOs.Contact
{
    public readonly record struct ContactGetListResponse(
        List<ContactGetAllResponse> Items
    );
}
