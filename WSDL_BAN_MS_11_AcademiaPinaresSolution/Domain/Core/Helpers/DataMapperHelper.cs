using EasyTemplateSolution.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Helpers
{
    public class DataMapperHelper
    {
        public static Data GetData(string Field, string Value)
        {
            var data = new Data();
            data.Field = Field;
            data.Value = "";
            if (Value != null)
            {
                data.HasData = true;
                data.Value = Value;
            }
            return data;
        }
    }
}
