using CharityTeledon.model;

namespace CharityTeledon.repository
{

    public interface ICaseRepository : IRepository<int, Case>
    {
        void UpdateSum(int id, float sum);
    }
}