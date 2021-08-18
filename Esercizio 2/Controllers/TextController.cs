using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Esercizio_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextController : ControllerBase
    {
        private TextElaborator _context;

        public TextController()
        {
            _context = new TextElaborator();
        }


        [HttpPost("UploadText")]
        public IActionResult ElaborateText([FromForm] string Text)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Text)) return BadRequest();
                return Ok(new { count = Text.Length, words = _context.CountAsync(Text).ToString() });
            }
            catch (Exception e) { return StatusCode((int)HttpStatusCode.InternalServerError, e); }
        }

        [HttpPost("UploadFiles")]
        public IActionResult ElaborateFile(List<IFormFile> Files)
        {
            try
            {
                long size = Files.Sum(f => f.Length);
                string result = String.Empty;

                foreach (var formFile in Files)
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                            formFile.CopyTo(stream);

                        using (var reader = new StreamReader(formFile.OpenReadStream()))
                            result += " " + reader.ReadToEnd(); ;
                    }

                return Ok(new { count = Files.Count, words = _context.CountAsync(result).ToString() });
            }
            catch (Exception e) { return StatusCode((int)HttpStatusCode.InternalServerError, e); }
        }
        [HttpPost("BasicAuth/UploadText")]
        [Authorize]
        public IActionResult ElaborateTextBasicAuth([FromForm] string Text)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Text)) return BadRequest();
                return Ok(new { count = Text.Length, words = _context.CountAsync(Text).ToString() });
            }
            catch (Exception e) { return StatusCode((int)HttpStatusCode.InternalServerError, e); }
        }
        [HttpPost("BasicAuth/UploadFiles")]
        [Authorize]
        public IActionResult ElaborateFileBasicAuth(List<IFormFile> Files)
        {
            try
            {
                long size = Files.Sum(f => f.Length);
                string result = String.Empty;

                foreach (var formFile in Files)
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(filePath))
                            formFile.CopyTo(stream);

                        using (var reader = new StreamReader(formFile.OpenReadStream()))
                            result += " " + reader.ReadToEnd(); ;
                    }

                return Ok(new { count = Files.Count, words = _context.CountAsync(result).ToString() });
            }
            catch (Exception e) { return StatusCode((int)HttpStatusCode.InternalServerError, e); }
        }
    }
}
