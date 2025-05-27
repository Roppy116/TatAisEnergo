using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TatAisEnergo.Core.DTOs;
using TatAisEnergo.Core.DTOs.Results;
using TatAisEnergo.Core.Entities;
using TatAisEnergo.Core.Models;
using TatAisEnergo.DataAccess;
using TatAisEnergo.WebApi.Requests;

namespace TatAisEnergo.WebApi.Controllers
{
    [ApiController]
    [Route("history")]
    public class HistoryController : ControllerBase
    {
        [HttpPost]
        public async Task<Result<PagedResult<HistoryModel>>> Get(
            [FromBody] HistoryFilterRequest filter,
            [FromServices] AppDbContext _db)
        {
            var query = _db.Set<History>().Include(x => x.User).AsQueryable();

            // Фильтрация по ID
            if (!string.IsNullOrWhiteSpace(filter.Id))
            {
                if (long.TryParse(filter.Id, out var id))
                {
                    query = query.Where(x => x.Id == id);
                }
            }

            // Фильтрация по тексту
            if (!string.IsNullOrWhiteSpace(filter.Text))
                query = query.Where(x => x.Text != null && x.Text.ToLower().Contains(filter.Text.ToLower()));

            // Фильтрация по имени
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(x => x.User.FullName.ToLower().Contains(filter.Name.ToLower()));

            // Фильтрация по типу события
            if (filter.EventType.HasValue)
                query = query.Where(x => x.EventTypeId == filter.EventType.Value);

            // Фильтрация по дате
            if (filter.DateFrom.HasValue)
                query = query.Where(x => x.Dt >= filter.DateFrom.Value);

            if (filter.DateTo.HasValue)
                query = query.Where(x => x.Dt <= filter.DateTo.Value.AddDays(1));

            // Сортировка
            if (filter.Sort != null && filter.Sort.Count > 0)
            {
                IOrderedQueryable<History> orderedQuery = null!;

                foreach (var (field, order) in filter.Sort.Select(s => (s.Field.ToLower(), s.Order.ToLower())))
                {
                    Expression<Func<History, object>> keySelector = field switch
                    {
                        "id" => x => x.Id,
                        "name" => x => x.User.FullName,
                        "text" => x => x.Text!,
                        "date" => x => x.Dt,
                        "eventtype" => x => x.EventType.Name,
                        _ => x => x.Id
                    };

                    if (orderedQuery == null)
                    {
                        orderedQuery = order == "ascend"
                            ? query.OrderBy(keySelector)
                            : query.OrderByDescending(keySelector);
                    }
                    else
                    {
                        orderedQuery = order == "ascend"
                            ? orderedQuery.ThenBy(keySelector)
                            : orderedQuery.ThenByDescending(keySelector);
                    }
                }

                query = orderedQuery ?? query;
            }
            else
            {
                // Сортировка по умолчанию
                query = query.OrderBy(x => x.Id);
            }

            var total = await query.CountAsync();
            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(item => new HistoryModel
                {
                    Id = item.Id,
                    Text = item.Text ?? string.Empty,
                    Date = item.Dt,
                    Name = item.User.FullName,
                    EventType = item.EventTypeId
                })
                .ToListAsync();

            return new Result<PagedResult<HistoryModel>>()
                .WithData(new PagedResult<HistoryModel> { Items = items, Total = total });
        }

        [HttpGet("event-type")]
        public async Task<Result<Dictionary<long, string>>> GetFilterValues(
            [FromServices] AppDbContext _db)
        {
            var result = new Result<Dictionary<long, string>>();

            var data = await _db.Set<EventType>()
                .ToDictionaryAsync(x => x.Id, y => y.Name) ?? [];

            return result.WithData(data);
        }
    }
}
