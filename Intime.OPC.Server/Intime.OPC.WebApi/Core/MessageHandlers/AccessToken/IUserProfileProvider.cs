namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken
{
    public interface IUserProfileProvider
    {
        UserProfile Get(int userId);
    }
}
