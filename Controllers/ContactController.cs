using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

[ApiController]
[Route("api/contact")]
public class ContactController : ControllerBase
{
private readonly ApiContactContext _context;
public ContactController(ApiContactContext context)
{
_context = context;
}

// GET: api/contact
[HttpGet]
public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
{
// Get contacts
var contacts = _context.ApiContact;
return await contacts.ToListAsync();
}

// GET: api/contact/2
[HttpGet("{id}")]
public async Task<ActionResult<Contact>> GetContact(int id)
{
// Find a specific item
// SingleAsync() throws an exception if no item is found (which is possible, depending on id)
// SingleOrDefaultAsync() is a safer choice here
var contact = await _context.ApiContact.SingleOrDefaultAsync(t => t.Id == id);
if (contact == null)
return NotFound();

return contact;
}
// POST: api/contact
[HttpPost]
public async Task<ActionResult<Contact>> PostContact(Contact contact)
{
_context.ApiContact.Add(contact);
await _context.SaveChangesAsync();




return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
}

// PUT: api/contact/2
[HttpPut("{id}")]
public async Task<IActionResult> PutContact(int id, Contact contact)
{
if (id != contact.Id)
return BadRequest();

_context.Entry(contact).State = EntityState.Modified;

try
{
await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
if (!_context.ApiContact.Any(m => m.Id == id))
return NotFound();
else
throw;
}

return NoContent();
}

// DELETE: api/contact/2
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteContact(int id)
{
var contact = await _context.ApiContact.FindAsync(id);

if (contact == null)
return NotFound();

_context.ApiContact.Remove(contact);
await _context.SaveChangesAsync();

return NoContent();
}

[HttpPost("send-email")]
public async Task<ActionResult<string>> SendEmail(Contact contact)
{
    // Validates the contact form data
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Prepare the email message body
    var messageBody = $"From: {contact.Name}<br/>Email: {contact.Email}<br/>Message: {contact.Message}";

    try
    {
        // Sends the email with the contact object
        await SendEmail("noreply@example.com", contact);
        
        // You can include additional logic here, such as saving the contact form data to a database

        return Ok(new { message = "Email sent successfully" });
    }
    catch (Exception ex)
    {
        // Handle any errors that occur during email sending
        return StatusCode(500, $"Error sending email: {ex.Message}");
    }
}


private async Task SendEmail(string recipient, Contact contact)
{
    // Configuration du client SMTP
    var smtpClient = new SmtpClient("smtp.gmail.com")
    {
        Port = 587,
        Credentials = new NetworkCredential("webprofilesimontvr@gmail.com", "etlj omli zfil lgtg"),
        EnableSsl = true,
    };

    // Constructs the email message
    var mailMessage = new MailMessage
    {
        From = new MailAddress("noreply@example.com"),
        Subject = $"Nouveau message de : {contact.Name}",
        Body = $"Email: {contact.Email}<br/>Message: {contact.Message}",
        IsBodyHtml = true,
    };

    mailMessage.To.Add(recipient);

    // Sends the email
    await smtpClient.SendMailAsync(mailMessage);
}
}

