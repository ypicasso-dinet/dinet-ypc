using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Toshi.Backend.Utilities
{
    public static class ExcelHelper
    {
        public static void SetWhiteBorders(ExcelRange cell, Color borderColor, ExcelBorderStyle style = ExcelBorderStyle.Thin)
        {
            var border = cell.Style.Border;
            border.Top.Style = border.Bottom.Style = border.Left.Style = border.Right.Style = style;
            border.Top.Color.SetColor(borderColor);
            border.Bottom.Color.SetColor(borderColor);
            border.Left.Color.SetColor(borderColor);
            border.Right.Color.SetColor(borderColor);
        }
    }
}
