using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
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
    //[EnableCors("FileWebApiPolicy")]
    public class SlideShowApiController : ControllerBase
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
        public SlideShowApiController(IConfiguration iConfig) {
            _iConfig = iConfig;
        }
        #endregion

        [HttpGet]
        public IEnumerable<string> Get() {
            string path = AppContext.BaseDirectory;

            try {

                return Directory.GetFiles(_slideShowFolder, "*.*", SearchOption.AllDirectories)
                .Where(s => s.ToLower().EndsWith(".jpg")
                || s.ToLower().EndsWith(".bmp")
                || s.ToLower().EndsWith(".gif")
                || s.ToLower().EndsWith(".png")).ToList();
            } catch (Exception ex) {
                return new string[] { "SlideShowApiError:" + ex.ToString() };
            }
        }
               
    }
}
