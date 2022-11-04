using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameExtendedParticleSandbox.src
{
    public class EmitterIndex
    {
        private static List<EmitterIndex> indexes = new List<EmitterIndex>();
        private int value = 0;
        private int index = 0;
        public EmitterIndex(int i)
        {
            value = i;
            indexes.Add(this);
            index = indexes.Count - 1;
        }

        public int get()
        {
            return value;
        }

        public void set(int i)
        {
            value = i;
        }

        public void delete()
        {
            for (int i = index + 1; i < indexes.Count; i++)
            {
                var emitter = indexes[i];
                emitter.value--;
                emitter.index--;
            }

            indexes.RemoveAt(index);
            value = -1;
            index = -1;
        }
    }
}
