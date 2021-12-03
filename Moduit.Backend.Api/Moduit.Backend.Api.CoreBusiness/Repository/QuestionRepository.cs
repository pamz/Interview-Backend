using Moduit.Backend.Api.CoreBusiness.Interfaces;
using Moduit.Backend.Api.CoreBusiness.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduit.Backend.Api.CoreBusiness.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        public async Task<BackendQuestionOneResponse> GetQuestionOneAsync()
        {
            using (RestService service = new RestService())
            {
                return await service.GetQuestionOneAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionThreeAsync(BackendQuestionThreeResponse entity)
        {
            using (RestService service = new RestService())
            {
                return await service.GetQuestionThreeAsync(entity).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionTwoAsync(string title, string tags, int pageSize)
        {
            using (RestService servivce = new RestService())
            {
                return await servivce.GetQuestionTwoAsync(title, tags, pageSize).ConfigureAwait(false);
            }
        }
    }
}
