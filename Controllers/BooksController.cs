using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using home_libraryAPI.Models;
using AutoMapper;
using home_libraryAPI.DTOs;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Extensions.Hosting.Internal;

namespace home_libraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly HomeLibraryContext _context;
        private readonly IMapper _mapper;

        public BooksController(HomeLibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {

            if (_context.Books == null)
            {
                return NotFound();
            }

            //return await _context.Books.ToListAsync();

            var bookDtos = _context.Books
                .Select(book => new BookDto
                {
                    Id = book.Id,
                    BookTitle = book.BookTitle,
                    PublisherId = book.Publisher.Id,
                    PublisherName = book.Publisher.PublisherName,
                    AuthorId = book.Author.Id,
                    AuthorName = book.Author.AuthorName,
                    ImagePath = book.ImagePath,
                    BookSummary = book.BookSummary,
                    Categories = book.Categories.Select(category => new Categories
                    {
                        Id = category.Id,
                        CategoryName = category.CategoryName
                    }).ToList(),
                }).ToList();

            return Ok(bookDtos);
            if (_context.Books == null)
            {
                return NotFound();
            }
            return await _context.Books.ToListAsync();

        }

        [HttpGet("GetHomePageChoose")]
        public async Task<ActionResult<IEnumerable<Book>>> GetHomePageChoose()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var chooseDtos = _context.Books.Select(book => new ChooseDTO
            {
                Id = book.Id,
                BookTitle = book.BookTitle,
                ImagePath = book.ImagePath,
                BookSummary = book.BookSummary
            }).ToList();

            return Ok(chooseDtos);
        }

        [HttpGet("GetHomePageAbout")]
        public async Task<ActionResult<Book>> GetHomePageAbout()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = _context.Books.Where(b => b.StatusId != 6).Select(b => b.Id).ToList();
            if (books != null && books.Any())
            {
                var latestLog = _context.Logs
                    .Where(log => log.EventTypeId == 25 && books.Contains((int)log.BookId)).OrderByDescending(log => log.EventDate)
            .FirstOrDefault();

                if (latestLog != null)
                {
                    var lastInsertBook = await _context.Books.Include(b => b.Publisher).Include(b => b.Categories).Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == latestLog.BookId);

                    if (lastInsertBook != null)
                    {
                        var bookDto = new BookDto
                        {
                            Id = lastInsertBook.Id,
                            BookTitle = lastInsertBook.BookTitle,
                            ImagePath = lastInsertBook.ImagePath,
                            BookSummary = lastInsertBook.BookSummary,
                            PublisherName = lastInsertBook.Publisher.PublisherName,
                            Categories = lastInsertBook.Categories.Select(category => new Categories
                            {
                                Id = category.Id,
                                CategoryName = category.CategoryName,
                            }).ToList(),
                            PublisherId = lastInsertBook.PublisherId,
                            AuthorName = lastInsertBook.Author.AuthorName + " " + lastInsertBook.Author.AuthorSurname,
                            AuthorId = lastInsertBook.AuthorId,
                        };

                        return Ok(bookDto);
                    }
                }
            }
            return NotFound();
        }


        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'HomeLibraryContext.Books'  is null.");
            }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("GetImage")]
        public IActionResult GetImage([FromServices] IWebHostEnvironment hostingEnvironment, [FromQuery] string imageName)
        {
            try
            {
                if (string.IsNullOrEmpty(imageName))
                {
                    // Handle the case when imageName is not provided.
                    return BadRequest();
                }

                string imagePath = Path.Combine(hostingEnvironment.ContentRootPath, "Content", "img", imageName);

                if (!System.IO.File.Exists(imagePath))
                {
                    // Handle the case when the image file does not exist.
                    return NotFound();
                }

                // Read the file into a byte array
                byte[] fileBytes = System.IO.File.ReadAllBytes(imagePath);

                // Return the image file directly from the byte array
                return File(fileBytes, "image/jpeg");
            }
            catch (Exception ex)
            {
                // Handle other exceptions if necessary
                return StatusCode(500);
            }
        }







    }
}
