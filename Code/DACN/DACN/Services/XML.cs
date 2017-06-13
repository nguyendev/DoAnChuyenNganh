using DACN.Models;
using DACN.Models.XmlViewModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DACN.Services
{
    public class XML
    {
        public async Task<DACN.Models.XmlViewModels.ParserViewModel> convertXML(SearchViewModel Search)
        {
            try
            {
                HttpClient http = new HttpClient();
                //Format UTF 8
                var link = System.Uri.UnescapeDataString(Search.Link);

                var response = await http.GetByteArrayAsync(link);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(source);

                htmlDoc.OptionOutputAsXml = true;
                //StringWriter sw = new StringWriter();
                //var xw = XmlWriter.Create(sw);
                // write xml, nicely indented but it's XML!
                //StringBuilder sb = new StringBuilder();
                //XmlWriterSettings xws = new XmlWriterSettings();
                //xws.OmitXmlDeclaration = true;
                //xws.Indent = true;

                //using (XmlWriter xw = XmlWriter.Create(sb, xws))
                //{
                //    XDocument doc = new XDocument(
                //        new XElement("Child",
                //            new XElement("GrandChild", "some content")
                //        )
                //    );
                //    doc.Save(xw);
                //}

                //htmlDoc.Save(xw);

                //xmlContents = xw.ToString();
                TextReader sr = new StringReader(source);
                CenterCLR.Sgml.SgmlReader sgmlReader = new CenterCLR.Sgml.SgmlReader();
                sgmlReader.DocType = "HTML";
                //sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                sgmlReader.CaseFolding = CenterCLR.Sgml.CaseFolding.ToLower;
                sgmlReader.InputStream = sr;

                // create document
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.Load(sgmlReader);
             

                return new DACN.Models.XmlViewModels.ParserViewModel(Search.Title, Search.Link, doc.InnerXml);
            }
            catch (Exception e) {
                string error = e.StackTrace;
            }
            return new DACN.Models.XmlViewModels.ParserViewModel(Search.Title, Search.Link, "");
            
        }
    }
}
