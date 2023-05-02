using System;
using CharityTeledon.model;

namespace Networking
{
    public interface Request 
    {
    }

    [Serializable]
    public class LoginRequest : Request
    {
        private VolunteerDTO volunteer;

        public LoginRequest(VolunteerDTO volunteer)
        {
            this.volunteer = volunteer;
        }

        public virtual VolunteerDTO Volunteer
        {
            get
            {
                return volunteer;
            }
        }
    }
    
    [Serializable]
    public class LogoutRequest : Request
    {
        private VolunteerDTO volunteer;

        public LogoutRequest(VolunteerDTO volunteer)
        {
            this.volunteer = volunteer;
        }

        public virtual VolunteerDTO Volunteer
        {
            get
            {
                return volunteer;
            }
        }
    }
    [Serializable]
    public class GetCasesRequest : Request
    {
        
    }
    [Serializable]
    public class GetDonorsRequest : Request
    {
        
    }
    [Serializable]
    public class GetDonationsRequest : Request
    {
        
    }
    [Serializable]
    public class AddDonationRequest : Request
    {
        public DonationDTO donation { get; }

        public AddDonationRequest(DonationDTO donation)
        {
            this.donation = donation;
        }
    }
    [Serializable]
    public class AddDonorRequest : Request
    {
        public DonorDTO donor { get; }

        public AddDonorRequest(DonorDTO donor)
        {
            this.donor = donor;
        }
    }
    [Serializable]
    public class GetCaseRequest : Request
    {
        
    }
    [Serializable]
    public class GetDonorByNameRequest : Request
    {
        public string name { get; }

        public GetDonorByNameRequest(string name)
        {
            this.name = name;
        }
    }
    [Serializable]
    public class UpdateCaseRequest : Request
    {
        public int idCase {get;}
        public float amount { get; }

        public UpdateCaseRequest(int idCase, float amount)
        {
            this.idCase = idCase;
            this.amount = amount;
        }
    }
}