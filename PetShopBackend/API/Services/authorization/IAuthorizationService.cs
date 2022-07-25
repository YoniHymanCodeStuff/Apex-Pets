using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.authorization
{
    public interface IAuthorizationService
    {
        public bool GetIsCurrentUserAdmin();
    }
}