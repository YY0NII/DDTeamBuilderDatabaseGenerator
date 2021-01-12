using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace DDTeamBuilderDatabaseGenerator
{
    public class Program
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
            darkestDungeonCharacterStatsDBEntities.Units.Load();

            // Got these 2 directly from the slides provided in class
            // This one removes the data from the table (the local one I think)
            darkestDungeonCharacterStatsDBEntities.Units.RemoveRange(darkestDungeonCharacterStatsDBEntities.Units);
            // This one commits the changes to actual SQL database 
            darkestDungeonCharacterStatsDBEntities.SaveChanges();

            //**************************************************** 
            // Kinda proud of this one while I was googling how to 
            // Reset the auto incremented Id property I came across
            // this link:
            //
            // https://www.mysqltutorial.org/mysql-reset-auto-increment/#:~:text=The%20syntax%20of%20the%20ALTER,in%20the%20expression%20AUTO_INCREMENT%3Dvalue%20.
            //
            // which just lists some of the ways the operation can be 
            // done using SQL command than I remembered a method called 
            // ExecuteSqlCommand in the slides and it clicked. 
            //**************************************************** 
            darkestDungeonCharacterStatsDBEntities.Database.ExecuteSqlCommand("TRUNCATE TABLE UNITS");

            for ( int i = 2; i < 103; i++ ) 
            {
                for (int j = 1; j < 23; j++ ) 
                {
                    darkestDungeonCharacterStatsDBEntities.Units.Add( 
                        new Unit 
                        { 
                            Name_Of_Class = Convert.ToString(excelWorkSheet.Cells[i, j].text), 
                            Resolve_Level = Convert.ToInt32(excelWorkSheet.Cells[i, j + 1].text),
                            Max_HP = Convert.ToInt32(excelWorkSheet.Cells[i, j + 2].text), 
                            Dodge_Percentage = Convert.ToDecimal(excelWorkSheet.Cells[i, j + 3].text), 
                            Protect = Convert.ToInt32(excelWorkSheet.Cells[i, j + 4].text), 
                            Speed = Convert.ToInt32(excelWorkSheet.Cells[i, j + 5].text), 
                            Accuracy_Modifier = Convert.ToInt32(excelWorkSheet.Cells[i, j + 6].text), 
                            Critical_Chance_Percentage = Convert.ToDecimal(excelWorkSheet.Cells[i, j + 7].text),
                            Damage_Maximum = Convert.ToInt32(excelWorkSheet.Cells[i, j + 8].text),
                            Damage_Minimum = Convert.ToInt32(excelWorkSheet.Cells[i, j + 9].text),
                            Stun_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 10].text),
                            Move_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 11].text),
                            Blight_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 12].text),
                            Bleed_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 13].text),
                            Debuff_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 14].text),
                            Trap_Disarm_Chance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 15].text),
                            Disease_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 16].text),
                            Deathblow_Resistance = Convert.ToInt32(excelWorkSheet.Cells[i, j + 17].text),
                            Movement_Forward = Convert.ToInt32(excelWorkSheet.Cells[i, j + 18].text),
                            Movement_Backwards = Convert.ToInt32(excelWorkSheet.Cells[i, j + 19].text),
                            Religious = Convert.ToString(excelWorkSheet.Cells[i, j + 20].text),
                            Provisions = Convert.ToString(excelWorkSheet.Cells[i, j + 21].text) 
                        });
   
                    j = 23;
                }
            }

            darkestDungeonCharacterStatsDBEntities.SaveChanges();

            //Close stuff out
            excelWorkBook.Close();
            excelApp.Quit();

            if (excelWorkSheet != null) Marshal.ReleaseComObject(excelWorkSheet);
            if (excelWorkBook != null) Marshal.ReleaseComObject(excelWorkBook);
            if (excelApp != null) Marshal.ReleaseComObject(excelApp);


        }
    }
}
