using System;
using System.Collections.Generic;
using Teledon.Protocol;
using CharityTeledon.model;
using proto = Teledon.Protocol;

namespace Networking
{
    public class ProtoUtils
    {
        public static TeledonResponse createOKResponse()
        {
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.Ok };
            return response;
        }

        public static TeledonResponse createErrorResponse(String text)
        {
            TeledonResponse response = new TeledonResponse
            {
                Type = TeledonResponse.Types.Type.Error,
                Error = text
            };
            return response;
        }

        public static TeledonResponse createLoginResponse(CharityTeledon.model.Volunteer volunteer)
        {
            proto.Volunteer volunteerDto = new proto.Volunteer
                { Id = volunteer.Id, Username = volunteer.Username, Password = volunteer.Password };
            TeledonResponse response =
                new TeledonResponse{ Type = TeledonResponse.Types.Type.Ok, Volunteer = volunteerDto};
            return response;
        }

        public static TeledonResponse createGetCaseResponse(CharityTeledon.model.Case myCase)
        {
            proto.Case caseDto = new proto.Case { CaseId = myCase.Id, Name = myCase.CaseName, Sum = myCase.Sum };
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.GetCase, Case = caseDto};
            return response;
        }
        
        public static TeledonResponse createGetDonorResponse(CharityTeledon.model.Donor donor)
        {
            proto.Donor donorDto = new proto.Donor { DonorId = donor.Id, Name = donor.DonorName, Address = donor.DonorAddress, PhoneNumber = donor.DonorPhoneNumber};
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.GetDonor, Donor = donorDto};
            return response;
        }
        
        public static TeledonResponse createGetCasesResponse(List<CharityTeledon.model.Case> cases)
        {
            List<proto.Case> casesDto = new List<proto.Case>();
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.GetCases };
            foreach (CharityTeledon.model.Case myCase in cases)
            {
                proto.Case caseDto = new proto.Case { CaseId = myCase.Id, Name = myCase.CaseName, Sum = myCase.Sum };
                response.Cases.Add(caseDto);
            }
            return response;
        }
        
        public static TeledonResponse createGetDonorsResponse(List<CharityTeledon.model.Donor> donors)
        {
            List<proto.Donor> donorsDto = new List<proto.Donor>();
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.GetDonors };
            foreach (CharityTeledon.model.Donor donor in donors)
            {
                proto.Donor donorDto = new proto.Donor { DonorId = donor.Id, Name = donor.DonorName, Address = donor.DonorAddress, PhoneNumber = donor.DonorPhoneNumber};
                response.Donors.Add(donorDto);
            }
            return response;
        }

        public static TeledonResponse createCaseUpdatedResponse(CharityTeledon.model.Case myCase)
        {
            proto.Case caseDto = new proto.Case { CaseId = myCase.Id, Name = myCase.CaseName, Sum = myCase.Sum};
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.MadeDonation, Case = caseDto};
            return response;
        }
        
        public static TeledonResponse createAddedDonorResponse(CharityTeledon.model.Donor donor)
        {
            proto.Donor donorDto = new proto.Donor { DonorId = donor.Id, Name = donor.DonorName, Address = donor.DonorAddress, PhoneNumber = donor.DonorPhoneNumber};
            TeledonResponse response = new TeledonResponse { Type = TeledonResponse.Types.Type.AddedDonor, Donor = donorDto};
            return response;
        }

        public static CharityTeledon.model.Volunteer getUser(TeledonRequest request)
        {
            CharityTeledon.model.Volunteer volunteer =
                new CharityTeledon.model.Volunteer(request.User.Id, request.User.Username, request.User.Password);
            return volunteer;
        }
        
        public static CharityTeledon.model.Case getCase(TeledonRequest request)
        {
            CharityTeledon.model.Case myCase =
                new CharityTeledon.model.Case(request.Case.CaseId, request.Case.Name, request.Case.Sum);
            return myCase;
        }

        public static string getVolunteerUsername(TeledonRequest request)
        {
            return request.Username;
        }
        
        public static string getVolunteerPassword(TeledonRequest request)
        {
            return request.Password;
        }

        public static int getCaseId(TeledonRequest request)
        {
            return request.CaseId;
        }

        public static string getDonorName(TeledonRequest request)
        {
            return request.DonorName;
        }

        public static float getAmount(TeledonRequest request)
        {
            return request.Amount;
        }

        public static CharityTeledon.model.Donation getDonation(TeledonRequest request)
        {
            CharityTeledon.model.Donation donation =
                new CharityTeledon.model.Donation(request.Donation.DonationId, request.Donation.Amount, request.Donation.CaseId, request.Donation.DonorId);
            return donation;
        }

        public static CharityTeledon.model.Donor getDonor(TeledonRequest request)
        {
            CharityTeledon.model.Donor donor = new CharityTeledon.model.Donor(request.Donor.DonorId, request.Donor.Name,
                request.Donor.Address, request.Donor.PhoneNumber);
            return donor;
        }
    }
}