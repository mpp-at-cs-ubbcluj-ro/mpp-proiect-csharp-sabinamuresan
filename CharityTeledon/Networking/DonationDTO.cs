using System;

namespace Networking
{
    [Serializable]
    public class DonationDTO
    {
        public int id { get; }
        public int caseId { get; set; }
        public int donorId { get; set; }
        public float amount { get; set; }

        public DonationDTO(int id, int caseId, int donorId, float amount)
        {
            this.id = id;
            this.caseId = caseId;
            this.donorId = donorId;
            this.amount = amount;
        }
        
    }
}