using System.Web.Mvc;
using CMajor.Infrastructure;
using CMajor.Models;
using System.Linq;
using System.Data.Entity;

namespace CMajor.Controllers {
    [UnitOfWork]
    public abstract class ApplicationController : Controller {
        private CMajorDbContext _context;

        // this is useful for non db related controller such as ErrorsController
        public ApplicationController() {

        }

        public ApplicationController(IUnitOfWork unitOfWork) {
            this._context = unitOfWork as CMajorDbContext;
        }

        protected CMajorDbContext DbContext {
            get {
                return this._context;
            }
        }

        protected DbSet<Artist> Artists {
            get {
                return this._context.Artists;
            }
        }

    }
}