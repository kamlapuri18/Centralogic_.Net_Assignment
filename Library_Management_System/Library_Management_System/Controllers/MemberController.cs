using Library_Management_System.DTO;
using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Library_Management_System.Controllers
{


    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class MembersController : Controller
    {
        public MembersController()
        {
            container = GetContainer();
        }
        
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

        [HttpPost]
        public async Task<MemberEntity> AddMember(MemberEntity member)
        {
            {
                MemberEntity entity = new MemberEntity();
                entity.Name = member.Name;
                entity.DateOfBirth = member.DateOfBirth;
                entity.Email = member.Email;

                entity.Id = Guid.NewGuid().ToString();
                entity.UId = entity.Id;
                entity.DocumentType = "Member";
                entity.CreatedBy = "Sudhanshu";
                entity.CreatedOn = DateTime.Now;
                entity.UpdatedBy = "";
                entity.UpdatedOn = DateTime.Now;
                entity.Version = 1;
                entity.Active = true;
                entity.Archived = false;

                MemberEntity response = await container.CreateItemAsync(entity);

                MemberEntity responseMember = new MemberEntity();
                responseMember.Name = response.Name;
                responseMember.DateOfBirth = response.DateOfBirth;
                responseMember.Email = response.Email;

                return responseMember;
            }
        }

        [HttpGet]
        public async Task<List<MemberModel>> GetAllMembers()
        {
            var members = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "Member").ToList();
            List<MemberModel> member_Model = new List<MemberModel>();
            foreach (var member in members)
                {
                    MemberModel memberModel = new MemberModel();
                    memberModel.UID = member.UId;
                    memberModel.Name = member.Name;
                    memberModel.DateOfBirth = member.DateOfBirth;
                    memberModel.Email = member.Email;
                    member_Model.Add(memberModel);
                }
            return member_Model;

        }

        [HttpGet]
        public async Task<MemberModel> GetMemberByUId(string UID)
        {
            var member = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == UID && q.Active == true && q.Archived == false).FirstOrDefault();
            MemberModel response = new MemberModel();
            response.Name = member.Name;
            response.DateOfBirth = member.DateOfBirth;
            response.Email = member.Email;

            return response;
        }


        [HttpPut]
        public async Task<MemberModel> UpdateMember(MemberModel memberModel)
        {
            var existingMember = container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == memberModel.UID && q.Active == true && q.Archived == false).FirstOrDefault();

            existingMember.Archived = true;
            existingMember.Active = false;

            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "Sudhanshu";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version++;
            existingMember.Active = true;
            existingMember.Archived = false;
            existingMember.Name = memberModel.Name;
            existingMember.Email = memberModel.Email;
            existingMember.DateOfBirth = memberModel.DateOfBirth;

            existingMember = await container.CreateItemAsync(existingMember);

            MemberModel response = new MemberModel();
            response.Name = existingMember.Name;
            response.Email= existingMember.Email;
            response.DateOfBirth = existingMember.DateOfBirth;

            return response;
        }
    }
}