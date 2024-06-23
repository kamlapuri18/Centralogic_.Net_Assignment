
using iTextSharp.text;
using iTextSharp.text.pdf;
using SendGrid;
using SendGrid.Helpers.Mail;
using Visitor_Security_Clearance_System.Common;
using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Entity;
using Visitor_Security_Clearance_System.Interface;
using Document = iTextSharp.text.Document;

namespace Visitor_Security_Clearance_System.Service
{
    public class PassService : IPassService
    {
        private readonly ICosmosDBService _cosmosDBService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PassService(ICosmosDBService cosmosDBService, IWebHostEnvironment webHostEnvironment)
        {
            _cosmosDBService = cosmosDBService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<PassDTO> CreatePass(PassDTO passDTO)
        {
            
            PassEntity passEntity = new PassEntity
            {
                UId = passDTO.UId,
                VisitorID = passDTO.VisitorID,
                OfficeID = passDTO.OfficeID,
                Status = passDTO.Status,
                ValidFrom = passDTO.ValidFrom,
                ValidUntil = passDTO.ValidUntil,
                IssuedBy = passDTO.IssuedBy,
                CreatedDate = passDTO.CreatedDate
            };

            passEntity.Intialize(true, Credentials.PassDocumentType, "Sudh", "Sudh");

            var response = await _cosmosDBService.CreatePass(passEntity);

            byte[] pdfBytes = GeneratePassPDF(passDTO);

            await SendPassEmail(passDTO.VisitorEmail, pdfBytes);

            var responseModel = new PassDTO
            {
                UId = response.UId,
                VisitorID = response.VisitorID,
                OfficeID = response.OfficeID,
                Status = response.Status,
                ValidFrom = response.ValidFrom,
                ValidUntil = response.ValidUntil,
                IssuedBy = response.IssuedBy,
                CreatedDate = response.CreatedDate
            };

            return responseModel;
        }

        private byte[] GeneratePassPDF(PassDTO passDTO)
        {
            byte[] pdfBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                document.Add(new Paragraph("Visitor Pass"));
                document.Add(new Paragraph($"Visitor ID: {passDTO.VisitorID}"));
                document.Add(new Paragraph($"Office ID: {passDTO.OfficeID}"));
                document.Add(new Paragraph($"Status : {passDTO.Status}"));
                document.Add(new Paragraph($"ValidFrom : {passDTO.ValidFrom}"));
                document.Add(new Paragraph($"ValidUntil : {passDTO.ValidUntil}"));

                document.Close();
                pdfBytes = memoryStream.ToArray();
            }
            return pdfBytes;
        }

        private async Task SendPassEmail(string recipientEmail, byte[] pdfBytes)
        {
           
            string apiKey = "";

            var client = new SendGridClient(apiKey);

      
            var msg = new SendGridMessage
            {
                From = new EmailAddress("sudhanshu9939@gmail.com", "Sudhanshu"),
                Subject = "Visitor Pass",
                HtmlContent = "Please find your Visitor Pass Here.",
                PlainTextContent = " "

            };
            msg.AddTo(new EmailAddress("sudhanshu123@gmail.com"));

            
         msg.AddAttachment("Visitor_Pass.pdf", Convert.ToBase64String(pdfBytes), "application/pdf", "attachment");
         

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);
            Console.WriteLine(response.ToString());
        }




        public async Task<PassDTO> GetPassById(string uId)
        {
            
            var pass = await _cosmosDBService.GetPassById(uId);

            var passDTO = new PassDTO();
            passDTO.UId = pass.UId;
            passDTO.Status = pass.Status;
           
            return passDTO;
        }

      public async Task<PassDTO> UpdatePassStatus(string uId, string newStatus)
        {
            var existingPass = await _cosmosDBService.GetPassById(uId);

            if (existingPass == null)
            {
              
                return null;
            }

            existingPass.Status = newStatus;

            var updatedPass = await _cosmosDBService.UpdatePass(existingPass);

            var responseModel = new PassDTO
            {
                UId = updatedPass.UId,
                VisitorID= updatedPass.VisitorID,
                OfficeID= updatedPass.OfficeID,
                ValidFrom= updatedPass.ValidFrom,
                ValidUntil= updatedPass.ValidUntil,
                IssuedBy= updatedPass.IssuedBy,
                CreatedDate= updatedPass.CreatedDate,
                Status = updatedPass.Status,
            };

            return responseModel;
        }

    }
}

