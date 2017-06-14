using DACN.Models;
using DACN.Models.CRFsViewModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DACN.Services
{
    public class CRFSServices
    {
        public static int SO_LUONG_TU_TOI_THIEU = 10;
        public static async Task<ParserViewModel> getParserAsync(SearchViewModel Search)
        {
            try
            {
                HttpClient http = new HttpClient();
                //Format UTF 8
                var link = System.Uri.UnescapeDataString(Search.Link);

                var response = await http.GetByteArrayAsync(link);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);

                HtmlDocument document = new HtmlDocument();
                //Load trang web, nạp html vào document
                document.LoadHtml(source);


                var threadItems = document.DocumentNode.SelectNodes(".//div").ToList();
                Content content = new Content();

                //int nH2, nH3, nH4, nP, nB;
                //nH2 = nH3 = nH4 = nP = nB = 0;
                //foreach (var item in threadItems)
                //{
                //    var
                //    var h2Note = item.SelectNodes(".//h2");
                //    if (h2Note != null)
                //    {
                //        foreach (var h2Item in h2Note)
                //        {
                //            content.H2[4] = h2Item.InnerText;
                //            h2++;
                //            h3 = 1;
                //            var h3Note = item.SelectNodes(".//h3");
                //            if (h3Note != null)
                //            {
                //                // Khoi tao gia tri h3 mac dinh ban dau

                //                foreach (var h3Item in h3Note)
                //                {
                //                    Content[h2, h3, 0, 0] = h3Item.InnerText;
                //                    h3++;
                //                    h4 = 2;
                //                    var h4Note = item.SelectNodes(".//h4");
                //                    if (h4Note != null)
                //                    {
                //                        // Khoi tao gia tri h3 mac dinh ban dau
                //                        foreach (var h4Item in h4Note)
                //                        {
                //                            Content[h2, h3, h4, 0] = h4Item.InnerText;
                //                            h4++;
                //                            var ph4Note = item.SelectNodes(".//p");
                //                            text = 3;
                //                            if (ph4Note != null)
                //                            {

                //                                foreach (var textItem in ph4Note)
                //                                {
                //                                    Content[h2, h3, h4, text] = h3Item.InnerText;
                //                                    text++;
                //                                }
                //                            }
                //                            var bh4Note = item.SelectNodes(".//b");
                //                            if (bh4Note != null)
                //                            {
                //                                foreach (var textItem in bh4Note)
                //                                {
                //                                    Content[h2, h3, h4, text] = h3Item.InnerText;
                //                                    text++;
                //                                }

                //                            }
                //                        }

                //                    }
                //                    else
                //                    {
                //                        text = 3;
                //                        var ptextNote = item.SelectNodes(".//p");
                //                        if (ptextNote != null)
                //                        {
                //                            foreach (var pItem in ptextNote)
                //                            {
                //                                Content[h2, h3, h4, text] = pItem.InnerText;
                //                                text++;
                //                            }
                //                        }
                //                        var btextNote = item.SelectNodes(".//b");
                //                        if (btextNote != null)
                //                        {
                //                            foreach (var bItem in btextNote)
                //                            {
                //                                Content[h2, h3, h4, text] = bItem.InnerText;
                //                                text++;
                //                            }
                //                        }
                //                    }

                //                }
                //            }
                //            else
                //            {
                //                text = 3;
                //                var ptextNote = item.SelectNodes(".//p");
                //                if (ptextNote != null)
                //                {
                //                    foreach (var pItem in ptextNote)
                //                    {
                //                        Content[h2, h3, 0, text] = pItem.InnerText;
                //                        text++;
                //                    }
                //                }
                //                var btextNote = item.SelectNodes(".//b");
                //                if (btextNote != null)
                //                {
                //                    foreach (var bItem in btextNote)
                //                    {
                //                        Content[h2, h3, 0, text] = bItem.InnerText;
                //                        text++;
                //                    }
                //                }
                //            }


                //        }
                //    }
                //    else
                //    {
                //        text = 3;
                //        var ptextNote = item.SelectNodes(".//p");
                //        if (ptextNote != null)
                //        {
                //            foreach (var pItem in ptextNote)
                //            {
                //                Content[h2, 0, 0, text] = pItem.InnerText;
                //                text++;
                //            }
                //        }
                //        var btextNote = item.SelectNodes(".//b");
                //        if (btextNote != null)
                //        {
                //            foreach (var bItem in btextNote)
                //            {
                //                Content[h2, 0, 0, text] = bItem.InnerText;
                //                text++;
                //            }
                //        }
                //    }

                //}
                await ParserExtension.GetText(Search.Link);
                string[] text = new string[ParserExtension.Count];
                text = ParserExtension.Text;
                return new ParserViewModel(Search.Title, Search.Link, text);

            }
            catch (Exception e){
                String error = e.StackTrace; 
            }
            return null;
        }

        public static CXacDinhDongDLNDViewModel XacDinhDongDuLieuNoiDung(ParserViewModel oldModel)
        {
            //kiem tra so luong tu de xac dinh do co phai noi dung khong
            string[] temp = new string[2000];
            int iTemp = 0;
            for (int i = 0; i < oldModel.Text.Count(); i++)
            {
                if (StringExtension.DemChuoi(oldModel.Text[i]) > SO_LUONG_TU_TOI_THIEU)
                {
                    temp[iTemp] = oldModel.Text[i];
                    iTemp++;
                }
            }
            //copy chuoi da xu ly vao mang chinh
            string[] Text = new string[iTemp];
            for(int  i = 0; i < iTemp; i++)
            {
                Text[i] = temp[i];
            }
            return new CXacDinhDongDLNDViewModel(oldModel.Title, oldModel.Link, Text) ;
        }


    }
}
