namespace eClassBook.Services.UserServices
{
    public interface IUserService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();
    }
}
