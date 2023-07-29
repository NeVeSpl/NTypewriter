using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    public enum RegionStatTypeEnum
    {        
        [StatisticsGroup(StatisticsGroupTypeEnum.Restriction, FederalLawEnum.FZ44)]
        AuctionContractCount44,
        
        [StatisticsGroup(StatisticsGroupTypeEnum.Variation, 2, FederalLawEnum.FZ69)]
        ContractSum,
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StatisticsGroupAttribute : Attribute
    {
        public StatisticsGroupAttribute(StatisticsGroupTypeEnum type, params object[] values)
        {
           
        }       

        public StatisticsGroupAttribute(StatisticsGroupTypeEnum type, params FederalLawEnum[] values)
            : this(type, values.Cast<object>().ToArray())
        {
        }


        public StatisticsGroupTypeEnum Type { get; set; }

        public IEnumerable<Guid> Values { get; }
    }

    public enum StatisticsGroupTypeEnum
    {
        Restriction = 1,
        Variation = 3,
    }

    public enum FederalLawEnum
    {
        FZ44 = 44,
        FZ69 = 69,
    }
}
