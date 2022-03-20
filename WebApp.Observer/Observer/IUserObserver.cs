using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public interface IUserObserver
    {
        void UserCreated(AppUser appUser);
    }
}
