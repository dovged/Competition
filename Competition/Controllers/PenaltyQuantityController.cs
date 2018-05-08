using Competition.Context;
using Competition.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Competition.Controllers
{
    public class PenaltyQuantityController : BaseAPIController
    {
        /** Grąžina visas baudas pagal teisėjo lapą*/
        [Route("api/penaltyquantity/{paperId}")]
        public HttpResponseMessage Get(int paperId)
        {
            if (CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == paperId).Select(x => new PenaltyQuantityModel(x)).ToList().Count != 0)
            {
                List<PenaltyQuantityModel> penalties = CompetitionDB.TblPenaltyQuantities.ToArray().Where(x => x.JudgesPaperId == paperId).Select(x => new PenaltyQuantityModel(x)).ToList();

                foreach(PenaltyQuantityModel p in penalties)
                {
                    p.PenaltyName = CompetitionDB.TblPenalties.Find(p.PenaltyId).Name;
                }

                return ToJsonOK(penalties);
            }
            return ToJsonNotFound("Objektas nerastas.");
        }

        /**Pridedama nauja bauda*/
        [Route("api/penaltyquantity/{paperId}/{penaltyId}")]
        public HttpResponseMessage Post(int paperId, int penaltyId)
        {
            TblPenaltyQuantity value = new TblPenaltyQuantity();
            value.JudgesPaperId = paperId;
            value.PenaltyId = penaltyId;
            value.Quantity = 1;

            CompetitionDB.TblPenaltyQuantities.Add(value);
            return ToJsonOK(CompetitionDB.SaveChanges());
        }

        /** Padidinama (n = 1) arba sumažinama (n = 2) baudos vertė 1*/
        [Route("api/penaltyquantity/{penaltyId}/{n}")]
        public HttpResponseMessage Put(int penaltyId, int n)
        {
            TblPenaltyQuantity value = CompetitionDB.TblPenaltyQuantities.Find(penaltyId);
            switch (n)
            {
                case 1:
                    value.Quantity++;
                    break;
                case 2:
                    value.Quantity--;
                    break;
            }

            CompetitionDB.Entry(value).State = EntityState.Modified;
            return ToJsonOK(CompetitionDB.SaveChanges());
            
        }

        /** Ištrinama duota bauda*/
        [Route("api/penaltyquantity/{penaltyId}")]
        public HttpResponseMessage Delete(int penaltyId)
        {
            if (CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == penaltyId) != null)
            {
                CompetitionDB.TblPenaltyQuantities.Remove(CompetitionDB.TblPenaltyQuantities.FirstOrDefault(x => x.Id == penaltyId));
                return ToJsonOK(CompetitionDB.SaveChanges());
            }
            return ToJsonNotFound("Objektas nerastas.");
        }
    }
}
