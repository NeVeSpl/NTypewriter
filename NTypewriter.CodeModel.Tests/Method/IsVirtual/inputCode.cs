using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTypewriter.Tests.CodeModel
{
    abstract class SampleBaseClass
    {
        void Foo_Normal()
        {

        }

        public virtual void Foo_Virtual()
        {

        }

        public abstract void Foo_Abstract();
    }


    class SampleDerivedClass : SampleBaseClass
    {
        public override void Foo_Virtual()
        {

        }

        public override void Foo_Abstract()
        {

        }
    }

    abstract class SampleDerivedClassTwo : SampleBaseClass
    {
        public new void Foo_Virtual()
        {

        }

        public void Foo_Abstract()
        {

        }
    }
}