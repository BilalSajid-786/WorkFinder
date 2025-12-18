using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
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
        private readonly ResponseDto _responseDto;
        public FilesController(IWebHostEnvironment webHostEnvironment, IApplicantService applicantService)
        {
            _webHostEnvironment = webHostEnvironment;
            _applicantService = applicantService;
            _responseDto = new();
        }

        /// <summary>
        /// Upload the files for the applicants. Supported filetype: Resume, Certificate. Allowed Extensions: pdf, docx
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="fileType"></param>
        /// <param name="applicantId"></param>
        /// <returns></returns>

        [AllowAnonymous]
        [HttpPost("{fileType}/{applicantId:guid}")]
        public async Task<ActionResult<ResponseDto>> UploadFile(IFormFile formFile, FileType fileType, Guid applicantId)
        {

            try
            {
                bool isUserExist = await _applicantService.IsApplicantExistAsync(applicantId);
                if (!isUserExist)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Invalid ApplicantId";
                    return BadRequest(_responseDto);
                }

                if (formFile == null || formFile.Length == 0)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "No file exist to upload";
                    return BadRequest(_responseDto);
                }

                var allowedExtensions = new[] { ".pdf", ".docx" };
                if (!allowedExtensions.Contains(Path.GetExtension(formFile.FileName)))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "File extension not supported";
                    return BadRequest(_responseDto);
                }

                string fileName = string.Empty;

                string folderName = fileType switch
                {
                    FileType.Resume => "resumes",
                    FileType.Certificate => "certificates",
                    _ => String.Empty
                };


                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                if (fileType == FileType.Resume)
                    fileName = string.Concat(applicantId,
                        Path.GetExtension(formFile.FileName));
                else
                    fileName = string.Concat(Guid.NewGuid(),
                        Path.GetExtension(formFile.FileName));


                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                if (fileType == FileType.Resume)
                    await _applicantService.UpdateApplicantResume(formFile.FileName, applicantId);

                _responseDto.IsSuccess = true;
                _responseDto.Message = $"{fileType} upload Successfull";
                _responseDto.Result = fileName;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Upload Profile for the users.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost("uploadProfile")]
        public async Task<ActionResult<ResponseDto>> UploadProfile(IFormFile formFile)
        {
            try
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Profile upload success";

                //var allowedExtensions = new[] { ".png", ".jpg" };
                //if (!allowedExtensions.Contains(Path.GetExtension(formFile.FileName)))
                //{
                //    _responseDto.IsSuccess = false;
                //    _responseDto.Message = "File extension not supported";
                //    return BadRequest(_responseDto);
                //}
                string fileName = string.Empty;

                string folderName = "profiles";


                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                    fileName = string.Concat(CurrentUser.UserId,
                        Path.GetExtension(formFile.FileName));


                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                _responseDto.Result = fileName;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
