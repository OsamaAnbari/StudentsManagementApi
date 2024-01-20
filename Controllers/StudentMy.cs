//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Bcpg.Sig;
//using System.Security.Cryptography;
//using Students_Management_Api.Models;
//using Microsoft.Extensions.Caching.Distributed;
//using Newtonsoft.Json;
//using System.Text;
//using Microsoft.Extensions.Logging.Console;

//namespace Students_Management_Api.Controllers
//{
//    [Authorize(Roles = "3")]
//    [Route("api/Students/My")]
//    [ApiController]
//    public class StudentMy : ControllerBase
//    {
//        private readonly LibraryContext _context;
//        IDistributedCache _cache;
//        ILogger _logger;

//        public StudentMy(LibraryContext context, IDistributedCache cache, ILogger<StudentMy> logger)
//        {
//            _context = context;
//            _cache = cache;
//            _logger = logger;
//        }

//        [HttpGet("infos")]
//        public async Task<ActionResult<Student>> GetMyInfos()
//        {
//            int _id = Convert.ToInt32(HttpContext.Items["userId"]);
            
//            string cacheKey = $"Item_{_id}";
//            byte[] cachedItemBytes = await _cache.GetAsync(cacheKey);

//            if (cachedItemBytes == null)
//            {
//                if (_context.Student == null)
//                {
//                    return NotFound();
//                }

//                var student = await _context.Student.FindAsync(_id);

//                if (student == null)
//                {
//                    return NotFound();
//                }

//                cachedItemBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(student));
//                await _cache.SetAsync(cacheKey, cachedItemBytes, new DistributedCacheEntryOptions
//                {
//                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Cache for 30 minutes
//                });

//                _logger.LogInformation("This data from database");
//                return Ok(student);
//            }

//            var student1 = JsonConvert.DeserializeObject<Student>(Encoding.UTF8.GetString(cachedItemBytes));
            
//            _logger.LogInformation("This data from cache");
//            return Ok(student1);
//        }

//        [HttpGet("clearcache")]
//        public async Task<ActionResult> ClearCache()
//        {
//            int _id = Convert.ToInt32(HttpContext.Items["userId"]);
//            string cacheKey = $"Item_{_id}";
//            await _cache.RemoveAsync(cacheKey);
//            _logger.LogInformation("Chache is deleted");
//            return Ok();
//        }

//            [HttpGet("lectures")]
//        public async Task<ActionResult<List<Lecture>>> GetMyLectures()
//        {
//            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

//            if (_context.Student == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student.Include(e => e.Lectures).FirstAsync(e => e.Id == _id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return student.Lectures;
//        }

//        [HttpGet("sents")]
//        public async Task<ActionResult<List<StudentMessages>>> GetMySents()
//        {
//            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

//            if (_context.Student == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student.Include(e => e.Sents).FirstAsync(e => e.Id == _id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return student.Sents;
//        }

//        [HttpGet("received")]
//        public async Task<ActionResult<List<TeacherMessage>>> GetMyReceived()
//        {
//            int _id = Convert.ToInt32(HttpContext.Items["userId"]);

//            if (_context.Student == null)
//            {
//                return NotFound();
//            }

//            var student = await _context.Student.Include(e => e.Received).FirstAsync(e => e.Id == _id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return student.Received;
//        }
//    }
//}
