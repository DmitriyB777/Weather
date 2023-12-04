namespace Weather.Interfaces.Services
{
    public interface IUserErrorService
    {
        List<string> Errors { get; }

        void AddError(string error);
    }
}
