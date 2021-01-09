using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tests.Assets.Referenced.Traits;

namespace Tests.Assets.Referenced.DTO
{
    [DebuggerDisplay("Eat me!")]
    public class OrangeDTO : ICanBeEaten
    {
    }
}
