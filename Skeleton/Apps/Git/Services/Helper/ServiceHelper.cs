using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services.Helper
{
    public static class ServiceHelper
    {
        public static string CreateModelId()
        {
            var modelId = Guid.NewGuid();
            return modelId.ToString();
        }
    }
}
