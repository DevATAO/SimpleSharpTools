using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SimpleSharpTool.ConceptDescription
{
    public class SelfEnumerator:IEnumerator<int>
    {
        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current => Current;

        public int Current { get; }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
