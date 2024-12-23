﻿namespace Uex.ContactBook.Domain.Model.DTOs.Contact
{
    public readonly record struct ContactCreateResponse(
        Guid Id,
        string Name,
        string Email,
        string Cpf,
        string PhoneNumber,
        string Address,
        string Cep,
        string City,
        string Uf,
        string Complement,
        string GeographicalPosition
    );
}
