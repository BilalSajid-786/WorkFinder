using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts.DTOs.Response;

namespace WorkFinder.Api.Controllers
{
    /// <summary>
    /// File Controller to handle requests related to file uploads
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseApiController
    {
        private IWebHostEnvironment _webHostEnvironment;
        public FilesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Upload the resumes for the applicants
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("uploadResume")]
        public async Task<ActionResult<ResponseDto>> UploadResume(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                return BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Message = "No file exist to upload"
                });

            var allowedExtensions = new[] { ".pdf", ".docx" };
            if (!allowedExtensions.Contains(Path.GetExtension(formFile.FileName)))
                return BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Message = "File extension not supported"
                });


            var resumePath = Path.Combine(_webHostEnvironment.WebRootPath, "resumes");

            if (!Directory.Exists(resumePath))
                Directory.CreateDirectory(resumePath);

            var fileName = string.Concat(CurrentUser.UserId,
                Path.GetExtension(formFile.FileName));
            var filePath = Path.Combine(resumePath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return Ok(new ResponseDto()
            {
                Result = fileName,
                IsSuccess = true,
                Message = "Resume Uploaded successfull"
            });
        }
    }
}
