using CharityTeledon.model;

namespace Services
{
    public interface IObserver
    {
        void notifyCaseUpdated(Case var1);

        void notifyDonorAdded(Donor var1);
    }
}