using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.Article
{
    public class UploadImageModel : PageModel
    {
        private readonly IFileUploader _fileUploader;

        public UploadImageModel(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public IActionResult OnGet([FromQuery] IFormFile upload, string CKEditor, int CKEditorFuncNum,
            string langCode)
        {
            var fileUrl = _fileUploader.Upload(upload, $"uploads/{upload.FileName.ToLower()}");
            return new JsonResult(new {uploaded = true, fileUrl});
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPost([FromQuery] IFormFile upload, string CKEditor, int CKEditorFuncNum,
            string langCode)
        {
            var fileUrl = _fileUploader.Upload(upload, $"uploads/{upload.FileName.ToLower()}");
            return new JsonResult(new {uploaded = true, fileUrl});
        }
    }
}
