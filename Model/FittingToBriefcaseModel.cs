using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fittings.Model
{
    public class FittingToBriefcaseModel
    {
        public int Id { get; set; }
        public int IdBriefcase { get; set; }
        public int IdFitting { get; set; }
        public string FittingName { get; set; }
        public double FittingCount { get; set; }

        public bool IsAdded { get; set; } = false;
        public bool IsEdited { get; set; } = false;
        public bool IsRemoved { get; set; } = false;

        public FittingToBriefcaseModel()
        {
            
        }
    }
}
