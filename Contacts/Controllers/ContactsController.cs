using Contacts.Data;
using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
           return Ok(await dbContext.Contacts.ToListAsync());
           
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest add)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = add.Name,
                Phno = add.Phno
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);

        }
    }
}
