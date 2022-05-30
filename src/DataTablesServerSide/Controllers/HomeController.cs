using DataTablesServerSide.Data;
using DataTablesServerSide.Helpers;
using DataTablesServerSide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataTablesServerSide.Controllers
{
    public class HomeController : Controller
    {
        protected readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DataHandler(DtRequest dtRequest)
        {
            // Use IQueryable to defer the query until query building finished
            IQueryable<User> data = _context.Set<User>();

            var countAll = data.Count();
            var countFiltered = countAll;

            // Filter if where is null
            if (!string.IsNullOrEmpty(dtRequest.Search.Value))
            {
                Expression<Func<User, bool>> _where = m =>
                   m.Id.ToString().Contains(dtRequest.Search.Value)
                   || m.Name.ToLower().Contains(dtRequest.Search.Value)
                   || m.Surname.ToLower().Contains(dtRequest.Search.Value)
                   || m.Email.ToLower().Contains(dtRequest.Search.Value)
                   || m.Street.ToLower().Contains(dtRequest.Search.Value)
                   || m.City.ToLower().Contains(dtRequest.Search.Value);

                data = data.Where(_where);
                countFiltered = data.Where(_where).Count();
            }

            // No need to filter order, start, and length because the value is always available
            var tOrder = dtRequest.Order[0];
            var tOrderCol = dtRequest.Columns[tOrder.Column];

            bool _orderDesc = true;
            if (tOrder.Dir.ToLower() == "asc")
            {
                _orderDesc = false;
            }

            data = data.OrderBy(tOrderCol.Data, _orderDesc).Skip(dtRequest.Start).Take(dtRequest.Length);

            // Get data async
            var dataResult = await data.ToListAsync();

            // Format return to required DataTables response
            DtResponse result = DtResponse.CreateResponse(dtRequest.Draw, countFiltered, countAll, dataResult);

            return Json(result);
        }
    }
}