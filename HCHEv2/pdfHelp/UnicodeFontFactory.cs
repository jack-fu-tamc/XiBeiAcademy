using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace HCHEv2.pdfHelp
{
    public class UnicodeFontFactory : FontFactoryImp 
    {
        private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "arialuni.ttf");//arial unicode MS是完整的unicode字型。
       // private static readonly string bktPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),"KAIU.TTF");//標楷體
        //C:\\WINDOWS\\Fonts\\simsun.ttc
        private static readonly string bktPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "simsun.ttc,1");//宋体
        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
            bool cached)
        {
            //可用Arial或標楷體，自己選一個
           // BaseFont baseFont = BaseFont.CreateFont(bktPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont baseFont = BaseFont.CreateFont(bktPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            
            return new Font(baseFont, size, style, color);
        }
    }
}