using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SlideShowController : ControllerBase
    {
        #region Properties
        private IConfiguration _iConfig = null;
        private string _slideShowFolder {
            get {
                string strValue = _iConfig.GetSection("AppSettings").GetSection("PhotoFolder").Value;
                
                return strValue;
            }
        }
        #endregion

        #region Constructor
        public SlideShowController(IConfiguration iConfig) {
            _iConfig = iConfig;
        }
        #endregion

        [HttpGet]
        public String[] Get() {
            return Directory.GetFiles(_slideShowFolder);
        }

       

    }
}
