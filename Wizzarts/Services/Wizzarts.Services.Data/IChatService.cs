using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizzarts.Services.Data
{
    public interface  IChatService
    {
        IEnumerable<T> GetAllGeneralChatMessages<T>(int id);
    }
}
