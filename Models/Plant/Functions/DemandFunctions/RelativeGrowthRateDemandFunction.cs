using System;
using Models.Core;
using Models.PMF.Phen;

namespace Models.PMF.Functions.DemandFunctions
{
    [Description("This must be renamed DMDemandFunction for the source code to recoginise it!!!!  This function calculates DM demand beyond the start stage as the product of current organ wt (g), relative growth rate and the specified organ number.")]
    public class RelativeGrowthRateDemandFunction : Function
    {
        public double InitialWt = 0;

        public string InitialStageName = "";

        public Function RelativeGrowthRate { get; set; }

        public Function OrganNumber { get; set; }

        //[Link]
        //Phenology Phenology = null;
        [Link]
        Phenology Phenology = null;

        [Link]
        Biomass Live = null;

        double StartWt = 0;

        public override double FunctionValue
        {
            get
            {
                if (Phenology.OnDayOf(InitialStageName) && StartWt == 0)
                    StartWt = InitialWt;                                   //This is to initiate mass so relative growth rate can kick in
                double CurrentOrganWt = Math.Max(StartWt, Live.Wt / OrganNumber.FunctionValue);
                double OrganDemand = CurrentOrganWt * RelativeGrowthRate.FunctionValue;
                return OrganDemand * OrganNumber.FunctionValue;
            }
        }

    }
}

