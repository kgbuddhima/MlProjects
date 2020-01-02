using DermPOC.Shared.Predict;
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
            Results = new List<SingleLabelResult>();
        }

        bool Error { get; set; }

        string ErrorMessage { get; set; }
        
        public List<SingleLabelResult> Results { get; set; }
    }
}
