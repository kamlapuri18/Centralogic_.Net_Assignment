using Library_Management_System.DTO;
using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Library_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class IssuesController : Controller
    {

        
        private string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library_Management_System";
        public string ContainerName = "IssuesBook";

        public Container container;


        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
        public IssuesController()
        {
            container = GetContainer();
        }

        [HttpPost]
        public async Task<IssueModel> AddIssuedBook(IssueModel issueModel)
        {
            IssueEntity issue = new IssueEntity();
            issue.BookId = issueModel.BookId;
            issue.MemberId = issueModel.MemberId;
            issue.IssueDate = issueModel.IssueDate;
            issue.ReturnDate = issueModel.ReturnDate;
            issue.IsReturned = issueModel.IsReturned;

            issue.Id = Guid.NewGuid().ToString();
            issue.UId = issue.Id;
            issue.DocumentType = "issuebook";
            issue.CreatedBy = "Sudhanshu";
            issue.CreatedOn = DateTime.Now;
            issue.UpdatedBy = "";
            issue.UpdatedOn = DateTime.Now;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            IssueEntity response = await container.CreateItemAsync(issue);
            
            IssueModel responses = new IssueModel();
            responses.Id = response.Id;
            responses.UId = response.UId;
            responses.BookId = response.BookId;
            responses.MemberId = response.MemberId;
            responses.IssueDate = response.IssueDate;
            responses.ReturnDate = response.ReturnDate;
            responses.IsReturned = response.IsReturned;
            return responses;

        }

        [HttpGet]
        public async Task<IssueModel> GetIssuedBookByUid(string UID)
        {
            var book = container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == UID && q.Active == true && q.Archived == false).FirstOrDefault();

            IssueModel issue = new IssueModel();
            issue.UId = book.UId;
            issue.BookId = book.Id;
            issue.MemberId = book.MemberId;
            issue.IssueDate = book.IssueDate;
            issue.ReturnDate = book.ReturnDate;
            issue.IsReturned = book.IsReturned;
            return issue;
        }

        [HttpPut]
        public async Task<IssueModel> UpdateIssued(IssueModel issueModel)
        {
            var existingIssue = container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == issueModel.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingIssue.Archived = true;
            existingIssue.Active = false;

            existingIssue.Id = Guid.NewGuid().ToString();
            existingIssue.UpdatedBy = "Sudhanshu";
            existingIssue.UpdatedOn = DateTime.Now;
            existingIssue.Version = existingIssue.Version + 1;
            existingIssue.Active = true;
            existingIssue.Archived = false;
            existingIssue.BookId = issueModel.BookId;
            existingIssue.MemberId = issueModel.MemberId;
            existingIssue.IssueDate = issueModel.IssueDate;
            existingIssue.ReturnDate = issueModel.ReturnDate;
            existingIssue.IsReturned = issueModel.IsReturned;

            existingIssue = await container.CreateItemAsync(existingIssue);
            IssueModel responseModel = new IssueModel();
            responseModel.BookId = existingIssue.BookId;
            responseModel.MemberId = existingIssue.MemberId;
            responseModel.IssueDate = existingIssue.IssueDate;
            responseModel.ReturnDate = existingIssue.ReturnDate;
            responseModel.IsReturned = existingIssue.IsReturned;
            return responseModel;
        }
    }
}



