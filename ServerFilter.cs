using System;

namespace fdm_gamify2
{
    public class ServerFilter
    {
        public static Boolean Filter(String Value)
        {
            String[] BadInputs = {"<",">","!","{","}","insert","where","script","delete","input"};
            foreach (String Input in BadInputs)
            {
                if (Input == Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}