﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using home_libraryAPI.Models;
using AutoMapper;
using home_libraryAPI.DTOs;

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
    }
}
