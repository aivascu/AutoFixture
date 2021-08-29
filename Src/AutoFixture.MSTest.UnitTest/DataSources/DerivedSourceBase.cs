using System;
using System.Collections;
using AutoFixture.MSTest.DataSources;

namespace AutoFixture.MSTest.UnitTest.DataSources
{
    public class DerivedSourceBase : SourceBase
    {
        public Func<IEnumerator> OnGetEnumerator { get; set; }
        public override IEnumerator GetEnumerator()
        {
            return this.OnGetEnumerator();
        }
    }
}