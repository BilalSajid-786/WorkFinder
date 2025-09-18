using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFinder.Api.Controllers.Base;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.Enums;

namespace WorkFinder.Api.Controllers
{
    /// <summary>
    /// File Controller to handle requests related to file uploads
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicantService _applicantService;
        public FilesController(IWebHostEnvironment webHostEnvironment, IApplicantService applicantService)
        {
            _webHostEnvironment = webHostEnvironment;
            _applicantService = applicantService;
        }

        /// <summary>
        /// Upload the files for the applicants
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        
        [AllowAnonymous]
        [HttpPost("{fileType}/{userId:guid}")]
        public async Task<ActionResult<ResponseDto>> UploadFile(IFormFile formFile,FileType fileType, Guid userId)
        {

            try
            {
                bool isUserExist = await _applicantService.IsApplicantExistAsync(userId);
                if (!isUserExist)
                    return BadRequest(new ResponseDto()
                    {
                        IsSuccess = false,
                        Message = "Invalid UserId"
                    });

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

                string folderName = fileType switch
                {
                    FileType.Resume => "resumes",
                    FileType.Certificate => "certificates",
                    _ => String.Empty
                };


                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = string.Concat(userId,
                    Path.GetExtension(formFile.FileName));
                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return Ok(new ResponseDto()
                {
                    Result = fileName,
                    IsSuccess = true,
                    Message = $"{fileType} Uploaded successfull"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto()
                {
                    IsSuccess = false,
                    Message = $"{ex.Message}"
                });
            }
            
        }
    }
}
