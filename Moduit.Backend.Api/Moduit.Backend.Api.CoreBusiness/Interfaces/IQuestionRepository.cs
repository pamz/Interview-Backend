using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moduit.Backend.Api.CoreBusiness.Interfaces
{
    public interface IQuestionRepository
    {
        Task<BackendQuestionOneResponse> GetQuestionOneAsync();
        Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionTwoAsync(string title, string tags, int pageSize);
        Task<IEnumerable<BackendQuestionOneResponse>> GetQuestionThreeAsync(BackendQuestionThreeResponse entity);
    }
}
