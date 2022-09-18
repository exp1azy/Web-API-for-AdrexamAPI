using Microsoft.AspNetCore.Mvc;

namespace AdrexamAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly Service _service;

        public HomeController(Service service)
        {
            _service = service;
        }

        [HttpGet("tree")]
        public async Task<IActionResult> QuestionTreeAsync(CancellationToken cancellationToken) =>
            Ok(await _service.GetSections(cancellationToken));

        [HttpGet("questions/{id}")]
        public async Task<IActionResult> OneSectionQuestions(int id, int from, int size, CancellationToken cancellationToken) =>
            Ok(await _service.GetQuestions(id, from, size, cancellationToken));

        [HttpGet("random")]
        public async Task<IActionResult> RandomQuestionAsync([FromQuery] List<int> id, CancellationToken cancellationToken) =>
            Ok(await _service.GetRandomQuestionAsync(id, cancellationToken));
            
    }
}