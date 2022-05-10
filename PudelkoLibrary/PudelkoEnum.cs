using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PudelkoLibrary {
    class PudelkoEnum : IEnumerator<double> {
        private readonly Pudelko pudelko;
        private int index = 0;

        public PudelkoEnum(Pudelko pudelko) {
            this.pudelko = pudelko;
        }

        public double Current => pudelko[index++];

        object IEnumerator.Current => pudelko[index++];

        public void Dispose() {
        }

        public bool MoveNext() {
            return index < 3;
        }

        public void Reset() {
            index = 0;
        }
    }
}
