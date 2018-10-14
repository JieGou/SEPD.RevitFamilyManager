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
using System.Diagnostics;
using System.Text.RegularExpressions;

using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.IFC;

namespace SEPD.IFCtoRVT
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    public class IFCtoRVTMain : IExternalCommand
    {
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //Application.OpenIFCDocument Method (String, IFCImportOptions)
            //public Document OpenIFCDocument(string fileName,IFCImportOptions importOptions)
            List<string> ifcList = new List<string>();
            IFCtoRVTForm IFCtoRVTForm = new IFCtoRVTForm();

            IFCtoRVTForm.ShowDialog();

            if (IFCtoRVTForm.ifcPaths != null)
            {
                ifcList = IFCtoRVTForm.ifcPaths;
            }

            IFCImportOptions option = new IFCImportOptions();

            for (int i = 0; i < ifcList.Count(); i++)
            {
                Transaction openIFC = new Transaction(doc, "openIFC");
                openIFC.Start();
                Document ifcDoc = commandData.Application.Application.OpenIFCDocument(ifcList[i], option);
                openIFC.Commit();
                ifcDoc.SaveAs(ifcList[i] +".rvt");
                ifcDoc.Close();
            }

            return Result.Succeeded;
  
        }
    }
}
