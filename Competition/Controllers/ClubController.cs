using Competition.Context;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class ClubController : BaseAPIController
    {
        /** Grąžina visų klubų sąrašą;*/
        [Route("api/club")]
        public HttpResponseMessage Get()
        {
            if (CompetitionDB.TblClubs.AsEnumerable() != null)
            {
                return ToJsonOK(CompetitionDB.TblClubs.AsEnumerable());
            }

            return ToJsonNotFound("Sąrašas tuščias.");
        }
    }
}