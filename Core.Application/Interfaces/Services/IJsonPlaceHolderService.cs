using Core.Application.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Services
{
    public interface IJsonPlaceHolderService
    {
        Task<List<JsonPlaceHolderModel>> GetData();

        Task<JsonPlaceHolderModel> Create(JsonPlaceHolderModel request);
    }
}
