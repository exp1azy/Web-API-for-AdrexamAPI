using AdrexamAPI.Data;
using AdrexamAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdrexamAPI
{
    public class Service
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public Service(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            _config = config;
        }

        private void GetParents(List<NavigationItems> result, NavigationItems navItem)
        {
            if (navItem.ParentId == 0)
                return;

            var item = _dataContext.NavigationItems.FirstOrDefault(d => d.Id == navItem.ParentId);
            if (item == null)
                return;

            result.Add(item);
            GetParents(result, item);
        }

        public async Task<List<SectionModel?>> GetSectionsAsync(CancellationToken cancellationToken)
        {
            var navItems =  await _dataContext.NavigationItems
                .FromSqlRaw("select * from NavigationItems where id in (SELECT NavigationItemId from Questions group by NavigationItemId)")
                .ToListAsync(cancellationToken);

            List<NavigationItems> result = new List<NavigationItems>();
            foreach (var navItem in navItems)
            {
                GetParents(result, navItem);
            }

            return navItems.Concat(result.DistinctBy(i => i.Id)).Select(s => SectionModel.Map(s)).ToList();
        }

        public async Task<List<QuestionModel?>> GetQuestionsAsync(int sectionId, int from, int size, CancellationToken cancellationToken)
        {
            var query = _dataContext.Questions
                .Include(i => i.Answers).Include(i => i.Comments)
                .Where(e => e.NavigationItemId == sectionId).Skip(from)
                .OrderBy(o => o.Id).AsQueryable();

            int maxSize = _config.GetValue<int>("MaxQueryPageSize");

            if(size <= maxSize)
                query = query.Take(size);
            else
                query = query.Take(maxSize);

            return (await query.ToListAsync(cancellationToken)).Select(s => QuestionModel.Map(s)).ToList();
        }

        public async Task<QuestionModel?> GetRandomQuestionAsync(List<int> sectionId, CancellationToken cancellationToken)
        {
            Random random = new Random();

            var query = _dataContext.Questions.AsNoTracking().Include(m => m.Answers).Include(m => m.Comments)
                .Where(q => q.IsDeleted != true).AsQueryable();
            if (sectionId != null && sectionId.Any())
                query = query.Where(i => sectionId.Contains(i.NavigationItemId));

            var countOfQuestions = await query.CountAsync(cancellationToken);
            var questions = await query.Skip(random.Next(countOfQuestions-1)).Take(1).FirstAsync(cancellationToken);

            return QuestionModel.Map(questions);
        }
    }
}