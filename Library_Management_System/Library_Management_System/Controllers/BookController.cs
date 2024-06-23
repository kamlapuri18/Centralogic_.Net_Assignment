using Library_Management_System.DTO;
using Library_Management_System.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;



namespace Library_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class BookController : Controller
    {
        public BookController()
        {
            Container = GetContainer();
        }

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Library_Management_System";
        public string ContainerName = "IssuesBook";


        private Container Container;
        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }

        [HttpPost]
        public async Task<BookModel> AddBook(BookModel bookModel)
        {
            BookEntity book = new BookEntity();
            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.ISBN = bookModel.ISBN;
            book.PublishedDate = bookModel.PublishedDate;
            book.IsIssued= bookModel.IsIssued;

            // Assign Value in EntityBase
            book.Id = Guid.NewGuid().ToString();
            book.UId = book.Id;
            book.DocumentType = "book";
            book.CreatedBy = "Sudhanshu";
            book.CreatedOn = DateTime.Now;
            book.UpdatedBy = "";
            book.UpdatedOn = DateTime.Now;
            book.Version = 1;
            book.Active= true;
            book.Archived = false;

            BookEntity response = await Container.CreateItemAsync(book);

            // return model

            BookModel responseModel = new BookModel();
            responseModel.UID = response.UId;
            responseModel.Title = response.Title;
            responseModel.Author = response.Author;
            responseModel.ISBN= response.ISBN;
            responseModel.PublishedDate = response.PublishedDate;
            responseModel.IsIssued= response.IsIssued;

            return responseModel;

        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBook()
        {
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "book").ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel bookModel = new BookModel();
                bookModel.UID = book.UId;
                bookModel.Title = book.Title;
                bookModel.Author = book.Author;
                bookModel.PublishedDate= book.PublishedDate;
                bookModel.ISBN= book.ISBN;
                bookModel.IsIssued = book.IsIssued;

                bookModels.Add(bookModel);
            }
            return bookModels;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByUid(string UId)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            BookModel bookModel = new BookModel();
            bookModel.UID = book.UId;
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.ISBN = book.ISBN;
            bookModel.IsIssued = book.IsIssued;
            return bookModel;
        }

        [HttpPut]

        public async Task<BookModel> UpdateBook(BookModel bookModel, string UId)
        {
            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == bookModel.UID && q.Active == true && q.Archived == false).FirstOrDefault();


                existingBook.Archived = true;
                existingBook.Active = false;
                await Container.ReplaceItemAsync(existingBook, existingBook.Id);

                existingBook.UId = Guid.NewGuid().ToString();
                existingBook.UpdatedBy = "Kumar";
                existingBook.UpdatedOn = DateTime.Now;
                existingBook.Version = existingBook.Version++;
                existingBook.Active = true;
                existingBook.Archived = false;

                existingBook.Title = bookModel.Title;
                existingBook.Author = bookModel.Author;
                existingBook.ISBN = bookModel.ISBN;
                existingBook.IsIssued = bookModel.IsIssued;
                existingBook.PublishedDate = bookModel.PublishedDate;

            existingBook = await Container.CreateItemAsync(existingBook);

                BookModel response = new BookModel();
                response.UID = existingBook.UId;
                response.Title = existingBook.Title;
                response.Author = existingBook.Author;
                response.PublishedDate = existingBook.PublishedDate;
                response.ISBN = existingBook.ISBN;
                response.IsIssued = existingBook.IsIssued;

                return response;

            }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string UID)
        {
            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == q.Id && q.DocumentType == "book" && q.Archived == false && q.Active == true).AsEnumerable().FirstOrDefault();
            await Container.ReplaceItemAsync(books, books.Id);

            return Ok("Book Deleted");
        }



    }

}
