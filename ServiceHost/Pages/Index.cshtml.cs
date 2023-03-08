using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IFileUploader _fileUploader;

        public IndexModel(ILogger<IndexModel> logger, IFileUploader fileUploader)
        {
            _logger = logger;
            _fileUploader = fileUploader;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public async Task<JsonResult> UploadImage([FromForm] IFormFile upload, string CKEditor,
            int CKEditorFuncNum,
            string langCode)
        {
            //var url = _fileUploader.Upload(upload, "Uploaded/");

            //var success = new UploadSuccess
            //{
            //    Uploaded = true,
            //    FileName = upload.FileName,
            //    Url = url
            //};
            //return new JsonResult(success);
            return new(1);
        }
    }

    public class UploadSuccess
    {
        public bool Uploaded { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
