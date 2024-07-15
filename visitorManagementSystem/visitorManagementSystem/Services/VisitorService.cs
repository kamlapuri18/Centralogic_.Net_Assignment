using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using visitorManagementSystem.CosmosDB;
using visitorManagementSystem.Dto;
using visitorManagementSystem.Entities;
using visitorManagementSystem.Interfaces;

namespace visitorManagementSystem.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IMapper _mapper;

        public VisitorService(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDbService = cosmosDbService;
            _mapper = mapper;
        }

        public async Task<VisitorDto> AddVisitor(VisitorDto visitorDto)
        {
            var visitor = _mapper.Map<VisitorEntity>(visitorDto);
            visitor.Initialize(true, "visitor", "Sudhanshu", "Sudhanshu");
            var response = await _cosmosDbService.AddVisitor(visitor);
            var responseModel =  _mapper.Map<VisitorDto>(response);

            //Sending Email

            string subject = "Visitor Pass";
            
            string username = visitorDto.Name;
            string message = $@"
            Hello Sir/Ma'am,
            We have a new Visitor registered for Approval. Please Find the Information below:
            # Name : {visitorDto.Name}
            # Email : {visitorDto.Email}
            # Phone : {visitor.PhoneNumber}
            # Company : {visitor.CompanyName}
            # Purpose of Visit : {visitor.Purpose}
            # Entry Time : {visitor.EntryTime:MMMM dd, yyyy - HH:mm tt}
            # Expected Exit Time : {visitor.ExitTime:MMMM dd, yyyy - HH:mm tt}
            
            Please Approved or Decline as per Appointment.
            
            Best regards,
            [Sudhanshu Kumar]";
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(subject, "ksudhanshu0704@gmail.com" , username, message).Wait();
            return responseModel;
        }

      
        public async Task<VisitorDto> GetVisitorByUId(string uId)
        {
            var visitor = await _cosmosDbService.GetVisitorByUId(uId);
            if (visitor == null)
            {
                throw new Exception("Visitor Not Found");
            }
            var visitorDto = _mapper.Map<VisitorDto>(visitor);
            return visitorDto;
        }

        public async Task<VisitorDto> UpdateVisitor(VisitorDto visitorDto)
        {
            var existing = await _cosmosDbService.GetVisitorByUId(visitorDto.UId);
            if(existing == null)
            {
                throw new Exception("Visitor Not Found");
            }
            existing.Active = false;
            await _cosmosDbService.ReplaceAsync(visitorDto);
            existing.Initialize(false, "visitor", "Sudhanshu", "Sudhanshu");
            existing.Name = visitorDto.Name;
            existing.Email = visitorDto.Email;
            existing.PhoneNumber = visitorDto.PhoneNumber;
            existing.Address= visitorDto.Address;
            existing.CompanyName = visitorDto.CompanyName;
            existing.Purpose = visitorDto.Purpose;
            existing.PassStatus= visitorDto.PassStatus;

            var response = await _cosmosDbService.AddVisitor(existing);
            var responseModel = new VisitorDto
            {
                UId = existing.UId,
                Name = existing.Name,
                Email = existing.Email,
                PhoneNumber = existing.PhoneNumber,
                Address = existing.Address,
                CompanyName = existing.CompanyName,
                Purpose = existing.Purpose,
                PassStatus = existing.PassStatus,
            };
            return responseModel;
        }
        public async Task<string> DeleteVisitor(string uId)
        {
            var visitor = await _cosmosDbService.GetVisitorByUId(uId);
            if (visitor == null)
            {
                throw new Exception("Visitor not Found");
            }
            visitor.Active = false;
            visitor.Archived = true;
            await _cosmosDbService.ReplaceAsync(visitor);
            visitor.Initialize(false, "visitor", "Sudhanshu", "Sudhanshu");
            visitor.Active = false;
            return "Visitor Deleted";
        }
        public async Task<List<VisitorDto>> GetAllVisitor()
        {
            var visitors = await _cosmosDbService.GetAllVisitor();
            var visitorModels = new List<VisitorDto>();
            foreach (var visitor in visitors)
            {
                var visitorModel = new VisitorDto();
                visitorModel.UId = visitor.UId;
                visitorModel.Name = visitor.Name;
                visitorModel.Email = visitor.Email;
                visitorModel.PhoneNumber = visitor.PhoneNumber;
                visitorModel.Purpose = visitor.Purpose;
                visitorModel.CompanyName = visitor.CompanyName;
                visitorModel.PassStatus = visitor.PassStatus;
                visitorModel.Address = visitor.Address;

                visitorModels.Add(visitorModel);
            }
            return visitorModels;
        }
        public async Task<List<VisitorDto>> GetVisitorByStatus(string passStatus)
        {
            try
            {
                bool status = bool.Parse(passStatus);
                var allVisitors = await GetAllVisitor();
                if (allVisitors == null)
                {
                    return new List<VisitorDto>();
                }

                var response = allVisitors.Where(a => a.PassStatus == status).ToList();
                return response;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid passStatus value. It must be 'true' or 'false'.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetVisitorByStatus", ex);
            }
        }

    }
}
