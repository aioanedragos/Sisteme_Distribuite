using System;
using System.Collections;

namespace TemaEBS
{
    class populate
    {
        public void populateFields(ArrayList simbol)
        {
            simbol.Add("stattionid");
            simbol.Add("city");
            simbol.Add("temp");
            simbol.Add("rain");
            simbol.Add("wind");
            simbol.Add("direction");
            simbol.Add("date");
        }
    }
}
