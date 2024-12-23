namespace Uex.ContactBook.Domain.Model.DTOs.External
{
    public readonly record struct ViaCepResponse(
        string Cep,
        string logradouro,
        string complemento,
        string unidade,
        string bairro,
        string localidade,
        string uf,
        string estado,
        string regiao,
        string ibge,
        string gia,
        string ddd,
        string siafi
    );
}
