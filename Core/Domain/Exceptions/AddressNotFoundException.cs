namespace Domain.Exceptions
{
    public class AddressNotFoundException(string userName) : Exception($"The user ' {userName} ' does not have an address yet.")
    {
    }
}
