using DermPOCAppML.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Predict
{
    public class Result
    {

        public Result()
        {
            Results = new List<ModelOutput>();
        }

        bool Error { get; set; }

        string ErrorMessage { get; set; }
        
        public List<ModelOutput> Results { get; set; }
    }
}
