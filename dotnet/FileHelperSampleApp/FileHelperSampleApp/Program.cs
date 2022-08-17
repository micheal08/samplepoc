using FileHelpers;
using FileHelpers.ExcelNPOIStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHelperSampleApp
{
    [DelimitedRecord("|")]
    public class GenericLesson
    {
        #region Fields

        public string FacilitySection;
        public string Title;
        public string Description;
        public string PotentialCause;
        public string Recommendation;
        

        #endregion Fields
    }

    [DelimitedRecord("|")]
    public class VersionSheet
    {
        public string CurrentVersion;
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericLesson[] genLess;
            ExcelNPOIStorage provider = new ExcelNPOIStorage(typeof(GenericLesson))
            {
                SheetName = "LessonsLearned",
                FileName = @"D:\Chevron Project Docs\Smart Engineering\LessonsLearned\testing_Template.xlsx",
                StartRow = 1,
                StartColumn = 0
            };

            genLess = (GenericLesson[])provider.ExtractRecords();

            VersionSheet[] versionShee;
            provider = new ExcelNPOIStorage(typeof(VersionSheet))
            {
                SheetName = "Version",
                FileName = @"D:\Chevron Project Docs\Smart Engineering\LessonsLearned\testing_Template.xlsx",
                StartRow = 1,
                StartColumn = 0
            };

            versionShee = (VersionSheet[])provider.ExtractRecords();
        }
    }
}
