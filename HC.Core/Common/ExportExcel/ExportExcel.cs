using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;

namespace HC.Core.Common.ExportExcel
{
    public class ExportExcel
    {

        #region 导出Excel
        /// <summary>
        /// 导出学生报名基本信息
        /// </summary>
        /// <param name="info"></param>
        /// <param name="temp"></param>
        /// <param name="dt"></param>
        public void ExportExcel(FileInfo info, FileInfo temp, DataTable dt)
        {
            using (var xlPackage = new ExcelPackage(info, temp))
            {
                var worksheet = xlPackage.Workbook.Worksheets["Sheet1"];
                worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                worksheet.Column(1).Width = 11.25;//设置列宽
                worksheet.Column(4).Width = 21.25;//设置列宽
                worksheet.Column(5).Width = 20.75;//设置列宽
                worksheet.Column(6).Width = 13.75;//设置列宽
                worksheet.Column(18).Width = 15;//设置列宽
                for (var i = 1; i <= 20; i++)
                {
                    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//设置列宽
                }

                var properties = new string[]
              {                 
                   "姓名 ",
                   "性别",
                   "身份证号 ",
                   "高考证号",
                   "学生类型",
                   "科别",
                   "生源地",
                   "中学名称",
                   "本人手机号",
                   "父亲手机号",
                   "母亲手机号",
                   "座机",
                   "专业",
                   "录取通知书邮寄地址",
                   "邮编",
                   "收件人",
                   "收件人电话"
              };
                //worksheet.Cells[1, 5].Value = starttime + "--" + endtime + "工资" + sumerizingBy + "汇总表" + "(" + attibute.Alias + ")";
                worksheet.Cells[1, 6].Value = "人口统计表(" + DateTime.Now.ToString("yyyy-MM-dd") + ")";
                //worksheet.Cells[1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Cells[1, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                //worksheet.Cells[1, 5].Style.Font.Bold = true;


                //worksheet.Row(2).Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Row(2).Style.Font.Bold = true;
                //worksheet.Row(2).Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                for (int i = 1; i <= properties.Length; i++)
                {
                    worksheet.Cells[2, i].Value = properties[i - 1];
                }
                int row = 3;
                foreach (DataRow sumer in dt.Rows)
                {
                    int col = 1;
                    worksheet.Cells[row, col].Value = sumer["FamilyRelationName"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["FullName"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["SexName"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["IdentityNo"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["FamilyRegisterNo"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["Birthday"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["NationText"].ToString(); col++;

                    worksheet.Cells[row, col].Value = sumer["EducationalLevelText"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["PropertyText"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["PoliticalLandscapeText"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["MaritalStatusText"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["FirstMarriageDate"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["NativePlace"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["DomicilePlace"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["HomeAddress"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["CauseSeparation"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["ResidentialStatus"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["Mobile"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["EscuageStatusText"].ToString(); col++;
                    worksheet.Cells[row, col].Value = sumer["AccountStatusName"].ToString(); col++;
                    row++;
                }
                xlPackage.Save();
            }
        }
        #endregion

    }
}
