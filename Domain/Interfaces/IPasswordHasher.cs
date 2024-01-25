namespace Domain.Interfaces
{
    public interface IPasswordHasher
    {
        bool Check(string hash, string password);
        string Hash(string password);
    }
}
