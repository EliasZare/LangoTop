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
    }
}
