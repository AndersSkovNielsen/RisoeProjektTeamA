using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Model
{
    public class TestOpgave
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _beskrivelse;

        public string Beskrivelse
        {
            get { return _beskrivelse; }
            set { _beskrivelse = value; }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _ventetidIDage;

        public int VentetidIDage
        {
            get { return _ventetidIDage; }
            set { _ventetidIDage = value; }
        }

        public TestOpgave()
        {

        }

        public TestOpgave(int id, string beskrivelse, string status, int ventetidIDage)
        {
            _id = id;
            _beskrivelse = beskrivelse;
            _status = status;
            _ventetidIDage = ventetidIDage;

        }
    }
}
