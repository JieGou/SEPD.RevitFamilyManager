
using Autodesk.Revit.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using SEPD.RevitFamilyPlatform;
using SEPD.CommunicationModule;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Shell;
using Autodesk.Revit.UI.Selection;

namespace SEPD.RevitFamilyManager
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainSTF : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
             

            MyFamilyLoadOptions option = new MyFamilyLoadOptions();
            Document Revit_Doc = commandData.Application.ActiveUIDocument.Document;

            RevitFamilyManagerFormSTF RevitFamilyManagerFormSTF = new RevitFamilyManagerFormSTF();
            RevitFamilyManagerFormSTF.ShowDialog();
 
            List<string> fPathList = new List<string>();
            List<string> fNameList = new List<string>();
            fPathList = RevitFamilyManagerFormSTF.fPathList;
            fNameList = RevitFamilyManagerFormSTF.fNameList;
            //Document rfadoc = app.Application.OpenDocumentFile(familyFilePaths[k]);
            List<Family> families = new List<Family>(); ;
            for (int i = 0; i < fPathList.Count(); i++)
            {
            
                try
                {
                    Document rfadoc = commandData.Application.Application.OpenDocumentFile(fPathList[i]);
                    Transaction openRFA = new Transaction(rfadoc, "openRFA");
                    openRFA.Start();
                   
                    Settings documentSettings = rfadoc.Settings;
                    Category categoryGM = documentSettings.Categories.get_Item(BuiltInCategory.OST_GenericModel);
                    rfadoc.OwnerFamily.FamilyCategory = categoryGM;
                    openRFA.Commit();
  
                    rfadoc.SaveAs(fPathList[i] + "_changedCat.rvt");
                    rfadoc.Close();
                }
                catch (Exception defe)
                {
                    MessageBox.Show(defe.ToString());
                }
            }
            return Result.Succeeded;
        }
    }
}
