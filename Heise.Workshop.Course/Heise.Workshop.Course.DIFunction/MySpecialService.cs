using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heise.Workshop.Course.DIFunction
{
  public class MySpecialService
  {

    private Random random;

    public MySpecialService()
    {
      random = new Random();
    }

    public int CreateRandomNumber()
    {
      
      return random.Next(0, 100);
    }
  }
}
