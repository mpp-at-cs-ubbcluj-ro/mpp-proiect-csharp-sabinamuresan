using System;
using CharityTeledon.model;

namespace Networking
{
    public interface Response 
    {
    }
    
    [Serializable]
    public class OkResponse : Response
    {
		
    }

    [Serializable]
    public class OkLoginResponse : Response
    {
        public VolunteerDTO volunteerDto { get; set; }

        public OkLoginResponse(VolunteerDTO volunteerDto)
        {
            this.volunteerDto = volunteerDto;
        }
    }
    
    [Serializable]
    public class ErrorResponse : Response
    {
        private string message;

        public ErrorResponse(string message)
        {
            this.message = message;
        }

        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }

    [Serializable]
    public class GetCasesResponse : Response
    {
        public CaseDTO[] cases;

        public GetCasesResponse(CaseDTO[] cases)
        {
            this.cases = cases;
        }
    }
    
    [Serializable]
    public class GetDonorsResponse : Response
    {
        public DonorDTO[] donors;

        public GetDonorsResponse(DonorDTO[] donors)
        {
            this.donors = donors;
        }
    }
    
    [Serializable]
    public class GetDonorByNameResponse : Response
    {
        public DonorDTO donor { get; }

        public GetDonorByNameResponse(DonorDTO donor)
        {
            this.donor = donor;
        }
    }
    
    [Serializable]
    public class UpdatedCaseResponse : Response
    {
        public CaseDTO caseDto { get; }

        public UpdatedCaseResponse(CaseDTO caseDto)
        {
            this.caseDto = caseDto;
        }
    }
    
    [Serializable]
    public class AddedDonorResponse : Response
    {
        public DonorDTO donor;

        public AddedDonorResponse(DonorDTO donor)
        {
            this.donor = donor;
        }
    }
}