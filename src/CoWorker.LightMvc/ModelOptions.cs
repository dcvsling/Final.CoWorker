using System.Xml.Linq;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace CoWorker.LightMvc
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Options;
    using System.Linq.Expressions;

    public class ModelOptions
    {
        public List<Type> Models { get; set; }
    }
}
