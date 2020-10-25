using System;
using System.Collections.Generic;
using System.Text;

namespace Git.InputModels
{
    public class CreateRepositoryInputModel
    {
        public string Name { get; set; }
        public string RepositoryType { get; set; }
        public string OwnerId { get; set; }
    }
}
