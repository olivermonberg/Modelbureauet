using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Modelbureauet
{
    public class DataAccessLayer
    {
        private HttpClient client = new HttpClient();
        Uri _uri = new Uri("http://localhost:64291");

        public DataAccessLayer()
        {
            client.BaseAddress = _uri;
        }

        public async Task<List<ModelDTO>> GetAllModels()
        {
            var str = await client.GetStringAsync($"api/Models");
            var toReturn = JsonConvert.DeserializeObject<List<ModelDTO>>(str);
            return toReturn;
        }

        public async Task<bool> AddModel(ModelDTO _model)
        {
            var str = await client.PostAsJsonAsync($"api/Models", _model);

            if (str.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateModel(ModelDTO _model)
        {
            var str = await client.PutAsJsonAsync($"api/Models/{_model.Id}", _model);

            if (str.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteModel(long modelId)
        {
            var str = await client.DeleteAsync($"api/Models/{modelId}");

            if (str.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<List<JobsDTO>> GetAllJobs()
        {
            var str = await client.GetStringAsync($"api/Jobs");
            var toReturn = JsonConvert.DeserializeObject<List<JobsDTO>>(str);
            return toReturn;
        }

        public async Task<bool> AddJob(JobsDTO _job)
        {
            var str = await client.PostAsJsonAsync($"api/Jobs", _job);

            if (str.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateJob(JobsDTO _job)
        {
            var str = await client.PutAsJsonAsync($"api/Jobs/{_job.Id}", _job);

            if (str.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteJob(long jobId)
        {
            var str = await client.DeleteAsync($"api/Jobs/{jobId}");

            if (str.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddModelToJob(ModelToJobAssignmentDTO dto)
        {
            var str = await client.PostAsJsonAsync($"api/ModelToJobAssignments", dto);

            if (str.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<List<JobsDTO>> GetAllAssignedJobs()
        {
            var str = await client.GetStringAsync($"api/ModelToJobAssignments/GetAllJobsAssignedToModels");
            var toReturn = JsonConvert.DeserializeObject<List<JobsDTO>>(str);
            return toReturn;
        }
    }
}
