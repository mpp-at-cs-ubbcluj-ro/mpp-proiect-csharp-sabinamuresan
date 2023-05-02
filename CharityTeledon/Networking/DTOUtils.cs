using CharityTeledon.model;

namespace Networking
{
    public class DTOUtils
    {
        
        public static Case getFromDTO(CaseDTO caseDto)
        {
            return new Case(caseDto.id, caseDto.name, caseDto.sum);

        }
        public static CaseDTO getDTO(Case myCase)
        {
            return new CaseDTO(myCase.Id, myCase.CaseName, myCase.Sum);
        }
        public static Donor getFromDTO(DonorDTO donorDto)
        {
            return new Donor(donorDto.id, donorDto.name, donorDto.address, donorDto.phone);

        }
        public static DonorDTO getDTO(Donor donor)
        {
            return new DonorDTO(donor.Id, donor.DonorName, donor.DonorAddress, donor.DonorPhoneNumber);
        }
        
        public static Donation getFromDTO(DonationDTO donationDto)
        {
            return new Donation(donationDto.id, donationDto.amount, donationDto.caseId, donationDto.donorId);

        }

        public static DonationDTO getDTO(Donation donation)
        {
            return new DonationDTO(donation.Id, donation.IdCase, donation.IdDonor, donation.Amount);
        }

        public static Volunteer getFromDTO(VolunteerDTO volunteerDto)
        {
            return new Volunteer(volunteerDto.id, volunteerDto.username, volunteerDto.password);

        }

        public static VolunteerDTO getDTO(Volunteer volunteer)
        {
            return new VolunteerDTO(volunteer.Id, volunteer.Username, volunteer.Password);
        }

        public static DonorDTO[] getDTO(Donor[] donors)
        {
            DonorDTO[] donorsDTO = new DonorDTO[donors.Length];
            for(int i=0;i<donors.Length;i++)
            {
                donorsDTO[i]=getDTO(donors[i]);
            }
            return donorsDTO;
        }

        public static Donor[] getFromDTO(DonorDTO[] donorsDTO)
        {
            Donor[] donors =new Donor[donorsDTO.Length];
            for(int i=0;i<donorsDTO.Length;i++)
            {
                donors[i]=getFromDTO(donorsDTO[i]);
            }
            return donors;
        }
        
        public static CaseDTO[] getDTO(Case[] cases)
        {
            CaseDTO[] casesDTO = new CaseDTO[cases.Length];
            for(int i=0;i<cases.Length;i++)
            {
                casesDTO[i]=getDTO(cases[i]);
            }
            return casesDTO;
        }

        public static Case[] getFromDTO(CaseDTO[] casesDTO)
        {
            Case[] cases =new Case[casesDTO.Length];
            for(int i=0;i<casesDTO.Length;i++)
            {
                cases[i]=getFromDTO(casesDTO[i]);
            }
            return cases;
        }
    }
}