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
    public class DiagnosesController : ControllerBase
    {
        private readonly IVektisRepository _vektisRepository;
        public DiagnosesController(IVektisRepository vektisRepository)
        {
            _vektisRepository = vektisRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VektisDiagnosis>> GetAll()
        {
            return Ok(_vektisRepository.GetAllDiagnoses());
        }

        [HttpGet("{id}")]
        public ActionResult<VektisDiagnosis> GetSingle(int id)
        {
            var result = _vektisRepository.GetDiagnosisByCode(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

    }
}
