using Microsoft.AspNetCore.Mvc;

namespace AdrexamAPI.Controllers
{
    public class ApiController : Controller
    {
        private readonly Service _service;

        public ApiController(Service service)
        {
            _service = service;
        }

        /// <summary>
        /// ��������� ������ ��������, � ������ �� ������� ����� ���� ���� ��� ��������� ��������
        /// </summary>
        /// <param name="cancellationToken">����� ������ ��������</param>
        /// <returns>������ ��������</returns>
        [HttpGet("tree")]
        public async Task<IActionResult> QuestionTreeAsync(CancellationToken cancellationToken) =>
            Ok(await _service.GetSectionsAsync(cancellationToken));

        /// <summary>
        /// ��������� �������� ������ �������� �� ���������� �������
        /// </summary>
        /// <param name="id">������������� �������</param>
        /// <param name="from">���������� ������ ������ ��������</param>
        /// <param name="size">������ ��������</param>
        /// <param name="cancellationToken">����� ������ ��������</param>
        /// <returns>������ �������� �� ������ �������</returns>
        [HttpGet("questions/{id}")]
        public async Task<IActionResult> OneSectionQuestions(int id, int from, int size, CancellationToken cancellationToken) =>
            Ok(await _service.GetQuestionsAsync(id, from, size, cancellationToken));

        /// <summary>
        /// ��������� ������ ���������� ������� �� ��������� ��������, ���� �� ������ ���� ��������, ���� ������ �������� ����
        /// </summary>
        /// <param name="id">������ ��������������� ��������</param>
        /// <param name="cancellationToken">����� ������ ��������</param>
        /// <returns>��������� ������</returns>
        [HttpGet("random")]
        public async Task<IActionResult> RandomQuestionAsync([FromQuery] List<int> id, CancellationToken cancellationToken) =>
            Ok(await _service.GetRandomQuestionAsync(id, cancellationToken));
            
    }
}