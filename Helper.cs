using System;

namespace Kros.KORM.PerformanceTests
{
    internal class Helper
    {
        public const string TemplateMDBConnectionString =
            "Provider={0};" +
            "Data Source={1};" +
            "Locale Identifier=1051;" +
            "Persist Security Info=TRUE;";

        public const string InsertUpdateTestTempFolder = "TempInsertUpdateTest";
        public const string TemplateInsertMDBName = "TemplateInsertEmployee";
        public const string TemplateUpdateMDBName = "TemplateUpdateEmployee";


        public static void CheckMsAccessProvider()
        {
            if (string.IsNullOrEmpty(Kros.Data.MsAccess.MsAccessDataHelper.MsAccessAceProvider))
            {
                throw new InvalidOperationException("MS Access ACE provider not found. 64-bit ACE provider must be present.");
            }
        }
    }
}
