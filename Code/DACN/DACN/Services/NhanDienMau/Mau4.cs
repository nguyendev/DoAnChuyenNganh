using DACN.Models.NhanDienMauViewModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Services.NhanDienMau
{
    //dan tri
    public static class Mau4
    {
        public static bool Check(HtmlDocument doc)
        {
            var nodeH1 = doc.DocumentNode.SelectSingleNode(".//h1");
            if (nodeH1 == null)
                return false;
            var parentNodeH1 = nodeH1.ParentNode.ParentNode;
            var nodeText = doc.DocumentNode.SelectNodes(".//div/p");
            var checknote = nodeText.Where(p => p.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode == parentNodeH1).ToList();
            if (checknote.Count <= Global.MIN_MAIN_CONTENT)
                return false;
            //foreach (var item in nodeText)
            //{
            //     if (item.ParentNode.ParentNode != parentNodeH1)
            //        return false;
            //}
            return true;
        }
        public static NhanDienMauViewModel GetContent(HtmlDocument doc, string url)
        {
            string H1 = "";
            string[] pTemp = new string[100];
            var nodeH1 = doc.DocumentNode.SelectSingleNode(".//h1");
            H1 = nodeH1.InnerText;
            var parentNodeH1 = nodeH1.ParentNode.ParentNode;
            var nodeText = doc.DocumentNode.SelectNodes(".//div/p");
            var checknote = nodeText.Where(t => t.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode == parentNodeH1).ToList();
            int count = 0;
            foreach (var item in checknote)
            {
                pTemp[count] = item.InnerText;
                count++;
            }
            string[] p = new string[count];
            for (int i = 0; i < p.Count(); i++)
            {
                p[i] = pTemp[i];
            }
            return new NhanDienMauViewModel(url, H1, p, "Template 4");
        }
    }
}
