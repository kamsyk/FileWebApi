using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticHtmlPageGenerator
{
    public class PageGenerator
    {
        public void GenerateSlideShowHtml() {
            string htmlPageFilePath = ConfigurationManager.AppSettings["HtmlPageFilePath"];

            //using (StreamReader sr = new StreamReader(htmlPageFilePath)) {
            //    using (StreamWriter sw = new StreamWriter(htmlPageFilePath + ".txt", true)) {
            //        while (sr.Peek() >= 0) {
            //            string strLine = sr.ReadLine();
            //            string strWrileLine = "sw.WriteLine(\"" + strLine + "\");";
            //            sw.WriteLine(strWrileLine);
            //        }
            //    }
            //}

            File.Delete(htmlPageFilePath);
            using (StreamWriter sw = new StreamWriter(htmlPageFilePath, true)) {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("    <meta charset=\"utf-8\" />");
                sw.WriteLine("    <title>Slide Show Static</title>");
                sw.WriteLine("    <script>");
                sw.WriteLine("        function getFiles() {");
                sw.WriteLine("            let files = [];");

                AddFiles(sw);

                sw.WriteLine("");
                sw.WriteLine("            runSlideShow(files);");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("        function runSlideShow(files) {");
                sw.WriteLine("");
                sw.WriteLine("            if (files != null && files[0].startsWith(\"SlideShowApiError: \")) {");
                sw.WriteLine("                displayMessage(files[0]);");
                sw.WriteLine("                return;");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("            if (files === null || files.length === 0) {");
                sw.WriteLine("                alert(\"No Files Were Found\");");
                sw.WriteLine("                return;");
                sw.WriteLine("            }");
                sw.WriteLine("");
                sw.WriteLine("            hideMessage();");
                sw.WriteLine("            let img = document.getElementById('imgPhoto');");
                sw.WriteLine("            imgPhoto.style.display = \"block\";");
                sw.WriteLine("            imgPhoto.src = files[0];");
                sw.WriteLine("");
                sw.WriteLine("            runSlideShowAsync(files);");
                sw.WriteLine("");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        function sleep(ms) {");
                sw.WriteLine("            return new Promise(resolve => setTimeout(resolve, ms));");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        async function runSlideShowAsync(files) {");
                sw.WriteLine("            for (let i = 1; i < files.length; i++) {");
                sw.WriteLine("                await sleep(3000);");
                sw.WriteLine("                imgPhoto.src = files[i];");
                sw.WriteLine("            }");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        function displayMessage(strMsg) {");
                sw.WriteLine("            let spanInfo = document.getElementById('spanInfo');");
                sw.WriteLine("            spanInfo.style.display = \"block\";");
                sw.WriteLine("            spanInfo.innerHTML = strMsg;");
                sw.WriteLine("        }");
                sw.WriteLine("");
                sw.WriteLine("        function hideMessage() {");
                sw.WriteLine("            let spanInfo = document.getElementById('spanInfo');");
                sw.WriteLine("            spanInfo.style.display = \"none\";");
                sw.WriteLine("            spanInfo.innerHTML = \"\";");
                sw.WriteLine("        }");
                sw.WriteLine("    </script>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");
                sw.WriteLine("    <h1>Slide Show</h1>");
                sw.WriteLine("    <input type=\"button\" value=\"Start Slide Show\" onclick=\"getFiles()\">");
                sw.WriteLine("    <span id=\"spanInfo\"></span>");
                sw.WriteLine("    <img id=\"imgPhoto\" style=\"display:none;width:450px;\" />");
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");

            }

            Process.Start(htmlPageFilePath);
        }

        private void AddFiles(StreamWriter sw) {
            string photoFolder = ConfigurationManager.AppSettings["PhotoFolder"];

            var photoFiles = Directory.GetFiles(photoFolder, "*.*", SearchOption.AllDirectories)
                .Where(s => s.ToLower().EndsWith(".jpg")
                || s.ToLower().EndsWith(".bmp")
                || s.ToLower().EndsWith(".gif")
                || s.ToLower().EndsWith(".png")).ToList();


            for (int i=0; i< photoFiles.Count; i++) {
                sw.WriteLine("files[" + i + "] = \"" + photoFiles[i].Replace(@"\", @"\\") + "\"");
            }
        }
    }
}
