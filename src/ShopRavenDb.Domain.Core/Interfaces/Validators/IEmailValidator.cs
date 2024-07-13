namespace ShopRavenDb.Domain.Core.Interfaces.Validators
{
    public interface IEmailValidator
    {
        bool IsValid(string email);
    }
}
