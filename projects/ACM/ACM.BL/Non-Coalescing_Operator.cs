using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
   public  class Non_Coalescing_Operator
    {
        public void MyProperty () {

            var name = "Sarah";

            var result = name ?? "No name is provided";

            name = null;

            result = name ?? "No name provided";

        }
        
        

    }
}
