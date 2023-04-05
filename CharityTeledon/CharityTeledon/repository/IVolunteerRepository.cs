using CharityTeledon.model;

namespace CharityTeledon.repository
{

    public interface IVolunteerRepository : IRepository<int, Volunteer>
    {
        Volunteer FindVolunteerAccount(string username, string parola);
    }
}