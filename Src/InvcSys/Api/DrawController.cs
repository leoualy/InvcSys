using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace InvcSys
{
    public class DrawController:ApiController
    {
        [HttpGet]
        public string Date()
        {
            return "Current:"+ DateTime.Now.ToString();
        }
        [HttpGet]
        public void Update(int id, int window)
        {
            DataManager.Update(id, window);
        }
    }
}
