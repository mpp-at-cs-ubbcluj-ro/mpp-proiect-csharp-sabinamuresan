using System.Collections.Generic;
using CharityTeledon.model;

namespace CharityTeledon.repository
{

    public interface IDonorRepository : IRepository<int, Donor>
    {
        IEnumerable<Donor> GetDonorsForCase(Case c);
        Donor FindByName(string name);
    }
}