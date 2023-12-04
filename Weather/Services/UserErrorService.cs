using Weather.Interfaces.Services;

namespace Weather.Services
{
    public class UserErrorService : IUserErrorService
    {
        public List<string> Errors { get; }

        public UserErrorService() 
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
