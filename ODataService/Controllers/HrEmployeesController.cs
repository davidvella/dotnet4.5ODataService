using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using ODataService.Models;


namespace ODataService.Controllers
{
    public class HrEmployeesController : ODataController
    {
        private readonly DatabaseEntities _db = new DatabaseEntities();

        // GET: odata/HrEmployees
        [EnableQuery(PageSize = 30)]
        public IQueryable<HrEmployee> GetHrEmployees()
        {
            return _db.HrEmployees;
        }

        // GET: odata/HrEmployees(5)
        [EnableQuery]
        public SingleResult<HrEmployee> GetHrEmployee([FromODataUri] int key)
        {
            return SingleResult.Create(_db.HrEmployees.Where(hrEmployee => hrEmployee.ID == key));
        }

        // PUT: odata/HrEmployees(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<HrEmployee> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HrEmployee hrEmployee = await _db.HrEmployees.FindAsync(key);
            if (hrEmployee == null)
            {
                return NotFound();
            }

            patch.Put(hrEmployee);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HrEmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hrEmployee);
        }

        // POST: odata/HrEmployees
        public async Task<IHttpActionResult> Post(HrEmployee hrEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.HrEmployees.Add(hrEmployee);
            await _db.SaveChangesAsync();

            return Created(hrEmployee);
        }

        // PATCH: odata/HrEmployees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<HrEmployee> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            HrEmployee hrEmployee = await _db.HrEmployees.FindAsync(key);
            if (hrEmployee == null)
            {
                return NotFound();
            }

            patch.Patch(hrEmployee);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HrEmployeeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hrEmployee);
        }

        // DELETE: odata/HrEmployees(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            HrEmployee hrEmployee = await _db.HrEmployees.FindAsync(key);
            if (hrEmployee == null)
            {
                return NotFound();
            }

            _db.HrEmployees.Remove(hrEmployee);
            await _db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HrEmployeeExists(int key)
        {
            return _db.HrEmployees.Count(e => e.ID == key) > 0;
        }
    }
}
