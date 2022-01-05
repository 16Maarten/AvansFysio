using Domain;
using DomainServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class VektisAPIRepository : IVektisRepository
    {
        HttpClient client = new HttpClient();
        private readonly string UrlDiagnoses = "https://vektis.azurewebsites.net/api/diagnoses/";
        private readonly string UrlTreatments = "https://vektis.azurewebsites.net/api/treatments/";
        public IEnumerable<VektisDiagnosis> GetAllDiagnoses()
        {
            HttpResponseMessage response = client.GetAsync(UrlDiagnoses).Result;
            return JsonConvert.DeserializeObject<IEnumerable<VektisDiagnosis>>(response.Content.ReadAsStringAsync().Result);
        }

        public IEnumerable<VektisTreatment> GetAllTreatments()
        {
            HttpResponseMessage response = client.GetAsync(UrlTreatments).Result;
            return JsonConvert.DeserializeObject<IEnumerable<VektisTreatment>>(response.Content.ReadAsStringAsync().Result);
        }

        public VektisDiagnosis GetDiagnosisByCode(int code)
        {
            HttpResponseMessage response = client.GetAsync($"{UrlDiagnoses}{code}").Result;
            return JsonConvert.DeserializeObject<VektisDiagnosis>(response.Content.ReadAsStringAsync().Result);
        }

        public VektisTreatment GetTreatmentByCode(string code)
        {
            HttpResponseMessage response = client.GetAsync($"{UrlTreatments}{code}").Result;
            return JsonConvert.DeserializeObject<VektisTreatment>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
