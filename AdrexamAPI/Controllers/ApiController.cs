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
        /// Получение дерева разделов, в каждом из которых может быть один или несколько вопросов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Дерево разделов</returns>
        [HttpGet("tree")]
        public async Task<IActionResult> QuestionTreeAsync(CancellationToken cancellationToken) =>
            Ok(await _service.GetSectionsAsync(cancellationToken));

        /// <summary>
        /// Получение страницы списка вопросов из указанного раздела
        /// </summary>
        /// <param name="id">Идентификатор раздела</param>
        /// <param name="from">Порядковый индекс начала страницы</param>
        /// <param name="size">Размер страницы</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список вопросов из одного раздела</returns>
        [HttpGet("questions/{id}")]
        public async Task<IActionResult> OneSectionQuestions(int id, int from, int size, CancellationToken cancellationToken) =>
            Ok(await _service.GetQuestionsAsync(id, from, size, cancellationToken));

        /// <summary>
        /// Получение одного случайного вопроса из указанных разделов, либо из набора всех вопросов, если список разделов пуст
        /// </summary>
        /// <param name="id">Список идентификаторов разделов</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Случайный вопрос</returns>
        [HttpGet("random")]
        public async Task<IActionResult> RandomQuestionAsync([FromQuery] List<int> id, CancellationToken cancellationToken) =>
            Ok(await _service.GetRandomQuestionAsync(id, cancellationToken));
            
    }
}