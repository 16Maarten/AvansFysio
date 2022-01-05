using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysioWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly IVektisRepository _vektisRepository;
        public TreatmentsController(IVektisRepository vektisRepository)
        {
            _vektisRepository = vektisRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VektisTreatment>> GetAll()
        {
            return Ok(_vektisRepository.GetAllTreatments());
        }

        [HttpGet("{id}")]
        public ActionResult<VektisTreatment> GetSingle(string id)
        {
            var result = _vektisRepository.GetTreatmentByCode(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
