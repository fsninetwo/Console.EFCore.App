using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EfCore.Domain.Helpers
{
    public static class ObjectComparerHelper
    {
        public static bool JsonCompare(this object expected, object actual)
        {
            if (ReferenceEquals(expected, actual))
            {
                return true;
            }

            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);

            return expectedJson == actualJson;
        }

    }
}
