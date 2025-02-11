using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MessagesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(long id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(long id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(MessageModel message)
        {
            //_context.Users.Attach(message.User);
            Message newMessage = new Message();
            newMessage.Body = message.Body;
            newMessage.UserId = message.UserId;
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = newMessage.Id }, newMessage);
        }

        // DELETE: Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(long id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(long id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }

        [HttpGet("/api/GetMessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(DateTime periodFrom, DateTime periodTo, string username=null)
        {
            periodFrom = periodFrom.ToUniversalTime();
            periodTo = periodTo.ToUniversalTime();

            IQueryable<Message> resultMessages = _context.Messages;

            if (periodFrom != DateTime.MinValue)
            {
                resultMessages = resultMessages.Where(msg => msg.DateTime >= periodFrom);
            }

            if (username != null)
            {
                resultMessages = resultMessages.Where(msg => msg.User.Username == username);
            }

            if (periodTo != DateTime.MinValue)
            {
                resultMessages = resultMessages.Where(msg => msg.DateTime <= periodTo);
            }

            return await resultMessages.ToListAsync();
        }
    }
}
