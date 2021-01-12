using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace DDTeamBuilderDatabaseGenerator
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Declare variables
            Application excelApp;
            _Workbook excelWorkBook;
            _Worksheet excelWorkSheet;

            //Start Excel and get Application object.
            excelApp = new Application();
            excelApp.Visible = false;

            //Set workbook and worksheet
            excelWorkBook = excelApp.Workbooks.Open(@"Darkest Dungeon_ Darkest Statics (Base Stats Assuming Level Appropriate Gear).xlsx");
            excelWorkSheet = excelWorkBook.Sheets[1];

            DarkestDungeonCharacterStatsDBEntities darkestDungeonCharacterStatsDBEntities = new DarkestDungeonCharacterStatsDBEntities();

            //Close stuff out
            excelWorkBook.Close();
            excelApp.Quit();

            if (excelWorkSheet != null) Marshal.ReleaseComObject(excelWorkSheet);
            if (excelWorkBook != null) Marshal.ReleaseComObject(excelWorkBook);
            if (excelApp != null) Marshal.ReleaseComObject(excelApp);


        }
    }
}
