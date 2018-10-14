
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

/// <summary>
/// 批量提取RFA文件的通用属性
/// </summary>
/// 
namespace SEPD.RevitFamilyManager
{

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class RevitFamilyManagerMainEP : IExternalCommand
    {
        public bool clearpara = false;
        public bool clearsymbol = false;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DotNet.Utilities.LogHelper.LogMonitor("table_Log", "族属性批量修改功能");
            //创建datatable
            DataTable dtListView = new DataTable("Table_Excel");
            //提取属性
            //RevitFamilyManagerFormIO RevitFamilyManagerFormIO = new RevitFamilyManagerFormIO();
            //RevitFamilyManagerFormIO.Show();

            Document Revit_Doc = commandData.Application.ActiveUIDocument.Document;
            Selection sel = commandData.Application.ActiveUIDocument.Selection;
            IList<Reference> listRef = new List<Reference>();
            IList<Element> listEle = new List<Element>();
            List<string> listElementNames = new List<string>();//选中的元素名字传递
            List<string> listFamilyNames = new List<string>();

            //选取需要检测的图元
            MessageBox.Show("选取图元");
            //将所选取的element转换为reference
            listRef = sel.PickObjects(ObjectType.Element);
            //将reference转换为element
            foreach (var Ref in listRef)
            {
                Element element = Revit_Doc.GetElement(Ref.ElementId);
                listEle.Add(element);
                //MessageBox.Show(element.Name);
                listElementNames.Add(element.Name.ToString());
                listFamilyNames.Add((element as FamilyInstance).Symbol.Family.Name.ToString());
                MessageBox.Show((element as FamilyInstance).Symbol.Family.Name.ToString());
            }
            /*-------------------------------------------------------------------------------------------------------*/
            RevitFamilyManagerFormEP RevitFamilyManagerFormEP = new RevitFamilyManagerFormEP();
            RevitFamilyManagerFormEP.listElementNames = listElementNames;
            RevitFamilyManagerFormEP.listFamilyNames = listFamilyNames;
            RevitFamilyManagerFormEP.ShowDialog();
            clearpara = RevitFamilyManagerFormEP.clearpara;
            clearsymbol = RevitFamilyManagerFormEP.clearsymbol;
            /*-------------------------------------------------------------------------------------------------------*/
            MyFamilyLoadOptions option = new MyFamilyLoadOptions();
            dtListView = RevitFamilyManagerFormEP.dtListView;
            for (int i = 0; i < dtListView.Rows.Count; i++)
            {
                foreach (Element ele in listEle)
                {
                    if (ele.Name == dtListView.Rows[i]["eleName"].ToString())
                    {
                        string eleName = dtListView.Rows[i]["eleName"].ToString();
                        string eleXPath = dtListView.Rows[i]["eleXPath"].ToString();
                        string elefName = dtListView.Rows[i]["elefName"].ToString();

                        if (eleXPath != "...")
                        {
                            Document familyDoc = null;
                            //Transaction editFamily = new Transaction(Revit_Doc, "openFamily");
                            //editFamily.Commit();
                            familyDoc = settingParamenters(eleName, eleXPath, elefName, Revit_Doc);
                            Family Family = familyDoc.LoadFamily(Revit_Doc, option);
                        }
                    }
                }
            }

            return Result.Succeeded;

        }

        public Document settingParamenters(string eleName, string eleXPath,string elefName, Document Revit_Doc)
        {
            //MessageBox.Show(elefName);
            //根据名称在工程文件中查找出对应族
            FilteredElementCollector allElements = new FilteredElementCollector(Revit_Doc);         
            ElementClassFilter familyFilter = new ElementClassFilter(typeof(Family));
            allElements = allElements.WherePasses(familyFilter);
            var filterFamilyList = from f in allElements
                                   where f.Name.ToString() == elefName
                                   select f as Family;
            //MessageBox.Show(filterFamilyList.ToList<Family>()[0].ToString());

            //allElements = new FilteredElementCollector(Revit_Doc);
            //ElementClassFilter familySymbolFilter = new ElementClassFilter(typeof(FamilySymbol));
            //allElements = allElements.WherePasses(familySymbolFilter);
            //var filterList = from f in allElements
            //                 where f.Name.ToString() == eleName && (f as FamilySymbol).Family.ToString() == elefName
            //                 select (f as FamilySymbol).Family ;  
            //Family filtedFamily = filterList.ToList<Family>().FirstOrDefault();
            ////if (filterList != null)
            ////{ MessageBox.Show(filterList.ToList<Family>()[0].ToString()); }

            Family filtedFamily = filterFamilyList.ToList<Family>().FirstOrDefault(); ;
            MessageBox.Show(filtedFamily.Name.ToString());
            //---------------------------------------------------
            //MessageBox.Show(eleName);
            //if (filtedFamily == null)
            //{ MessageBox.Show("filtedFamily is null!!"); }
            //---------------------------------------------------
            //
            Document familyDoc = Revit_Doc.EditFamily(filtedFamily);
            if (null != familyDoc && familyDoc.IsFamilyDocument == true)
            {
                DataTable xdte = new DataTable();
                //读取xls文件
                ExcelHelper ExcelHelper = new ExcelHelper();
                DataTable xdt = ExcelHelper.Reading_Excel_Information(eleXPath);
                //获取参数集
                FamilyParameterSet rfadocParas = familyDoc.FamilyManager.Parameters;
                List<string> rfadocParasListName = new List<string>();
                foreach (FamilyParameter rfadocPara in rfadocParas)
                {
                    rfadocParasListName.Add(rfadocPara.Definition.Name);
                }

                FamilyManager familyMgr = familyDoc.FamilyManager;

                if (clearsymbol == true)
                {
                    //清空族内所有类型 仅保留默认族类型
                    int typesizes = familyMgr.Types.Size;
                    if (familyMgr.Types.Size > 1 && familyMgr.Types.Size != 0)
                    {
                        for (int typenumber = 0; typenumber < typesizes - 1; typenumber++)
                        {
                            if (familyMgr.CurrentType != null)
                            {
                                Transaction DeleteType = new Transaction(familyDoc, "DeleteType");
                                DeleteType.Start();
                                familyMgr.DeleteCurrentType();
                                DeleteType.Commit();
                            }
                        }
                    }
                }

                if (clearpara == true)
                {
                    //清空族内所有参数条目
                    foreach (FamilyParameter fp in familyMgr.Parameters)
                    {
                        if (fp.Definition.ParameterGroup == BuiltInParameterGroup.PG_ELECTRICAL)
                        {
                            Transaction RemoveParameter = new Transaction(familyDoc, "RemoveParameter");
                            RemoveParameter.Start();
                            familyMgr.RemoveParameter(fp);
                            RemoveParameter.Commit();
                        }
                    }
                }
                //开始添加

                Transaction addParameter = new Transaction(familyDoc, "AddParameters");
                addParameter.Start();

                string paraname = null;
                BuiltInParameterGroup paragroup = BuiltInParameterGroup.PG_ELECTRICAL; ;
                ParameterType paraType = ParameterType.Text; ;
                bool isInstance = false;

                string paravalue = null;
                List<string> distinctparanames = new List<string>();

                //判断xls表中与原有rfa文件内重复的条目  放入distinctparanames列表
                for (int i = 0; i < xdt.Rows.Count; i++)
                {
                    foreach (FamilyParameter fp in familyMgr.Parameters)
                    {
                        if (fp.Definition.Name == xdt.Rows[i]["paraname"].ToString())
                        {
                            distinctparanames.Add(fp.Definition.Name);
                            MessageBox.Show(fp.Definition.Name);
                        }
                    }
                }
                //遍历xls添加属性条目
                for (int i = 0; i < xdt.Rows.Count; i++)
                {
                    //获取表中条目名称判断是否重复 重复则继续下一次循环
                    paraname = xdt.Rows[i]["paraname"].ToString();

                    foreach (string disstr in distinctparanames)
                    {
                        if (disstr == paraname)
                        {
                            continue;
                        }
                    }

                    //通过的条目名称  
                    if (xdt.Rows[i]["paragroup"] == null)
                    { paragroup = BuiltInParameterGroup.PG_ELECTRICAL; }
                    else
                    {
                        #region  参数分组对照  用于RevitAPI2016A
                        switch (xdt.Rows[i]["paragroup"].ToString())
                        {
                            case "PG_RELEASES_MEMBER_FORCES": paragroup = BuiltInParameterGroup.PG_RELEASES_MEMBER_FORCES; break;
                            case "PG_SECONDARY_END": paragroup = BuiltInParameterGroup.PG_SECONDARY_END; break;
                            case "PG_PRIMARY_END": paragroup = BuiltInParameterGroup.PG_PRIMARY_END; break;
                            case "PG_MOMENTS": paragroup = BuiltInParameterGroup.PG_MOMENTS; break;
                            case "PG_FORCES": paragroup = BuiltInParameterGroup.PG_FORCES; break;
                            case "PG_FABRICATION_PRODUCT_DATA": paragroup = BuiltInParameterGroup.PG_GEOMETRY_POSITIONING; break;
                            case "PG_REFERENCE": paragroup = BuiltInParameterGroup.PG_REFERENCE; break;
                            case "PG_GEOMETRY_POSITIONING": paragroup = BuiltInParameterGroup.PG_GEOMETRY_POSITIONING; break;
                            case "PG_DIVISION_GEOMETRY": paragroup = BuiltInParameterGroup.PG_DIVISION_GEOMETRY; break;
                            case "PG_SEGMENTS_FITTINGS": paragroup = BuiltInParameterGroup.PG_SEGMENTS_FITTINGS; break;
                            case "PG_CONTINUOUSRAIL_END_TOP_EXTENSION": paragroup = BuiltInParameterGroup.PG_CONTINUOUSRAIL_END_TOP_EXTENSION; break;
                            case "PG_CONTINUOUSRAIL_BEGIN_BOTTOM_EXTENSION": paragroup = BuiltInParameterGroup.PG_CONTINUOUSRAIL_BEGIN_BOTTOM_EXTENSION; break;
                            case "PG_STAIRS_WINDERS": paragroup = BuiltInParameterGroup.PG_STAIRS_WINDERS; break;
                            case "PG_STAIRS_SUPPORTS": paragroup = BuiltInParameterGroup.PG_STAIRS_SUPPORTS; break;
                            case "PG_STAIRS_OPEN_END_CONNECTION": paragroup = BuiltInParameterGroup.PG_STAIRS_OPEN_END_CONNECTION; break;
                            case "PG_RAILING_SYSTEM_SECONDARY_FAMILY_HANDRAILS": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SECONDARY_FAMILY_HANDRAILS; break;
                            case "PG_TERMINTATION": paragroup = BuiltInParameterGroup.PG_TERMINTATION; break;
                            case "PG_STAIRS_TREADS_RISERS": paragroup = BuiltInParameterGroup.PG_STAIRS_TREADS_RISERS; break;
                            case "PG_STAIRS_CALCULATOR_RULES": paragroup = BuiltInParameterGroup.PG_STAIRS_CALCULATOR_RULES; break;
                            case "PG_SPLIT_PROFILE_DIMENSIONS": paragroup = BuiltInParameterGroup.PG_SPLIT_PROFILE_DIMENSIONS; break;
                            case "PG_LENGTH": paragroup = BuiltInParameterGroup.PG_LENGTH; break;
                            case "PG_NODES": paragroup = BuiltInParameterGroup.PG_NODES; break;
                            case "PG_ANALYTICAL_PROPERTIES": paragroup = BuiltInParameterGroup.PG_ANALYTICAL_PROPERTIES; break;
                            case "PG_ANALYTICAL_ALIGNMENT": paragroup = BuiltInParameterGroup.PG_ANALYTICAL_ALIGNMENT; break;
                            case "PG_SYSTEMTYPE_RISEDROP": paragroup = BuiltInParameterGroup.PG_SYSTEMTYPE_RISEDROP; break;
                            case "PG_LINING": paragroup = BuiltInParameterGroup.PG_LINING; break;
                            case "PG_INSULATION": paragroup = BuiltInParameterGroup.PG_INSULATION; break;
                            case "PG_OVERALL_LEGEND": paragroup = BuiltInParameterGroup.PG_OVERALL_LEGEND; break;
                            case "PG_VISIBILITY": paragroup = BuiltInParameterGroup.PG_VISIBILITY; break;
                            case "PG_SUPPORT": paragroup = BuiltInParameterGroup.PG_SUPPORT; break;
                            case "PG_RAILING_SYSTEM_SEGMENT_V_GRID": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SEGMENT_V_GRID; break;
                            case "PG_RAILING_SYSTEM_SEGMENT_U_GRID": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SEGMENT_U_GRID; break;
                            case "PG_RAILING_SYSTEM_SEGMENT_POSTS": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SEGMENT_POSTS; break;
                            case "PG_RAILING_SYSTEM_SEGMENT_PATTERN_REMAINDER": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SEGMENT_PATTERN_REMAINDER; break;
                            case "PG_RAILING_SYSTEM_SEGMENT_PATTERN_REPEAT": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_SEGMENT_PATTERN_REPEAT; break;
                            case "PG_RAILING_SYSTEM_FAMILY_SEGMENT_PATTERN": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_FAMILY_SEGMENT_PATTERN; break;
                            case "PG_RAILING_SYSTEM_FAMILY_HANDRAILS": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_FAMILY_HANDRAILS; break;
                            case "PG_RAILING_SYSTEM_FAMILY_TOP_RAIL": paragroup = BuiltInParameterGroup.PG_RAILING_SYSTEM_FAMILY_TOP_RAIL; break;
                            case "PG_CONCEPTUAL_ENERGY_DATA_BUILDING_SERVICES": paragroup = BuiltInParameterGroup.PG_CONCEPTUAL_ENERGY_DATA_BUILDING_SERVICES; break;
                            case "PG_DATA": paragroup = BuiltInParameterGroup.PG_DATA; break;
                            case "PG_ELECTRICAL_CIRCUITING": paragroup = BuiltInParameterGroup.PG_ELECTRICAL_CIRCUITING; break;
                            case "PG_GENERAL": paragroup = BuiltInParameterGroup.PG_GENERAL; break;
                            case "PG_FLEXIBLE": paragroup = BuiltInParameterGroup.PG_FLEXIBLE; break;
                            case "PG_ENERGY_ANALYSIS_CONCEPTUAL_MODEL": paragroup = BuiltInParameterGroup.PG_ENERGY_ANALYSIS_CONCEPTUAL_MODEL; break;
                            case "PG_ENERGY_ANALYSIS_DETAILED_MODEL": paragroup = BuiltInParameterGroup.PG_ENERGY_ANALYSIS_DETAILED_MODEL; break;
                            case "PG_ENERGY_ANALYSIS_DETAILED_AND_CONCEPTUAL_MODELS": paragroup = BuiltInParameterGroup.PG_ENERGY_ANALYSIS_DETAILED_AND_CONCEPTUAL_MODELS; break;
                            case "PG_FITTING": paragroup = BuiltInParameterGroup.PG_FITTING; break;
                            case "PG_CONCEPTUAL_ENERGY_DATA": paragroup = BuiltInParameterGroup.PG_CONCEPTUAL_ENERGY_DATA; break;
                            case "PG_AREA": paragroup = BuiltInParameterGroup.PG_AREA; break;
                            case "PG_ADSK_MODEL_PROPERTIES": paragroup = BuiltInParameterGroup.PG_ADSK_MODEL_PROPERTIES; break;
                            case "PG_CURTAIN_GRID_V": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_V; break;
                            case "PG_CURTAIN_GRID_U": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_U; break;
                            case "PG_DISPLAY": paragroup = BuiltInParameterGroup.PG_DISPLAY; break;
                            case "PG_ANALYSIS_RESULTS": paragroup = BuiltInParameterGroup.PG_ANALYSIS_RESULTS; break;
                            case "PG_SLAB_SHAPE_EDIT": paragroup = BuiltInParameterGroup.PG_SLAB_SHAPE_EDIT; break;
                            case "PG_LIGHT_PHOTOMETRICS": paragroup = BuiltInParameterGroup.PG_LIGHT_PHOTOMETRICS; break;
                            case "PG_PATTERN_APPLICATION": paragroup = BuiltInParameterGroup.PG_PATTERN_APPLICATION; break;
                            case "PG_GREEN_BUILDING": paragroup = BuiltInParameterGroup.PG_GREEN_BUILDING; break;
                            case "PG_PROFILE_2": paragroup = BuiltInParameterGroup.PG_PROFILE_2; break;
                            case "PG_PROFILE_1": paragroup = BuiltInParameterGroup.PG_PROFILE_1; break;
                            case "PG_PROFILE": paragroup = BuiltInParameterGroup.PG_PROFILE; break;
                            case "PG_TRUSS_FAMILY_BOTTOM_CHORD": paragroup = BuiltInParameterGroup.PG_TRUSS_FAMILY_BOTTOM_CHORD; break;
                            case "PG_TRUSS_FAMILY_TOP_CHORD": paragroup = BuiltInParameterGroup.PG_TRUSS_FAMILY_TOP_CHORD; break;
                            case "PG_TRUSS_FAMILY_DIAG_WEB": paragroup = BuiltInParameterGroup.PG_TRUSS_FAMILY_DIAG_WEB; break;
                            case "PG_TRUSS_FAMILY_VERT_WEB": paragroup = BuiltInParameterGroup.PG_TRUSS_FAMILY_VERT_WEB; break;
                            case "PG_TITLE": paragroup = BuiltInParameterGroup.PG_TITLE; break;
                            case "PG_FIRE_PROTECTION": paragroup = BuiltInParameterGroup.PG_FIRE_PROTECTION; break;
                            case "PG_ROTATION_ABOUT": paragroup = BuiltInParameterGroup.PG_ROTATION_ABOUT; break;
                            case "PG_TRANSLATION_IN": paragroup = BuiltInParameterGroup.PG_TRANSLATION_IN; break;
                            case "PG_ANALYTICAL_MODEL": paragroup = BuiltInParameterGroup.PG_ANALYTICAL_MODEL; break;
                            case "PG_REBAR_ARRAY": paragroup = BuiltInParameterGroup.PG_REBAR_ARRAY; break;
                            case "PG_REBAR_SYSTEM_LAYERS": paragroup = BuiltInParameterGroup.PG_REBAR_SYSTEM_LAYERS; break;
                            case "PG_CURTAIN_GRID": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID; break;
                            case "PG_CURTAIN_MULLION_2": paragroup = BuiltInParameterGroup.PG_CURTAIN_MULLION_2; break;
                            case "PG_CURTAIN_MULLION_HORIZ": paragroup = BuiltInParameterGroup.PG_CURTAIN_MULLION_HORIZ; break;
                            case "PG_CURTAIN_MULLION_1": paragroup = BuiltInParameterGroup.PG_CURTAIN_MULLION_1; break;
                            case "PG_CURTAIN_MULLION_VERT": paragroup = BuiltInParameterGroup.PG_CURTAIN_MULLION_VERT; break;
                            case "PG_CURTAIN_GRID_2": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_2; break;
                            case "PG_CURTAIN_GRID_HORIZ": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_HORIZ; break;
                            case "PG_CURTAIN_GRID_1": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_1; break;
                            case "PG_CURTAIN_GRID_VERT": paragroup = BuiltInParameterGroup.PG_CURTAIN_GRID_VERT; break;
                            case "PG_IFC": paragroup = BuiltInParameterGroup.PG_IFC; break;
                            case "PG_AELECTRICAL": paragroup = BuiltInParameterGroup.PG_AELECTRICAL; break;
                            case "PG_ENERGY_ANALYSIS": paragroup = BuiltInParameterGroup.PG_ENERGY_ANALYSIS; break;
                            case "PG_STRUCTURAL_ANALYSIS": paragroup = BuiltInParameterGroup.PG_STRUCTURAL_ANALYSIS; break;
                            case "PG_MECHANICAL_AIRFLOW": paragroup = BuiltInParameterGroup.PG_MECHANICAL_AIRFLOW; break;
                            case "PG_MECHANICAL_LOADS": paragroup = BuiltInParameterGroup.PG_MECHANICAL_LOADS; break;
                            case "PG_ELECTRICAL_LOADS": paragroup = BuiltInParameterGroup.PG_ELECTRICAL_LOADS; break;
                            case "PG_ELECTRICAL_LIGHTING": paragroup = BuiltInParameterGroup.PG_ELECTRICAL_LIGHTING; break;
                            case "PG_TEXT": paragroup = BuiltInParameterGroup.PG_TEXT; break;
                            case "PG_VIEW_CAMERA": paragroup = BuiltInParameterGroup.PG_VIEW_CAMERA; break;
                            case "PG_VIEW_EXTENTS": paragroup = BuiltInParameterGroup.PG_VIEW_EXTENTS; break;
                            case "PG_PATTERN": paragroup = BuiltInParameterGroup.PG_PATTERN; break;
                            case "PG_CONSTRAINTS": paragroup = BuiltInParameterGroup.PG_CONSTRAINTS; break;
                            case "PG_PHASING": paragroup = BuiltInParameterGroup.PG_PHASING; break;
                            case "PG_MECHANICAL": paragroup = BuiltInParameterGroup.PG_MECHANICAL; break;
                            case "PG_STRUCTURAL": paragroup = BuiltInParameterGroup.PG_STRUCTURAL; break;
                            case "PG_PLUMBING": paragroup = BuiltInParameterGroup.PG_PLUMBING; break;
                            case "PG_ELECTRICAL": paragroup = BuiltInParameterGroup.PG_ELECTRICAL; break;
                            case "PG_STAIR_STRINGERS": paragroup = BuiltInParameterGroup.PG_STAIR_STRINGERS; break;
                            case "PG_STAIR_RISERS": paragroup = BuiltInParameterGroup.PG_STAIR_RISERS; break;
                            case "PG_STAIR_TREADS": paragroup = BuiltInParameterGroup.PG_STAIR_TREADS; break;
                            case "PG_MATERIALS": paragroup = BuiltInParameterGroup.PG_MATERIALS; break;
                            case "PG_GRAPHICS": paragroup = BuiltInParameterGroup.PG_GRAPHICS; break;
                            case "PG_CONSTRUCTION": paragroup = BuiltInParameterGroup.PG_CONSTRUCTION; break;
                            case "PG_GEOMETRY": paragroup = BuiltInParameterGroup.PG_GEOMETRY; break;
                            case "PG_IDENTITY_DATA": paragroup = BuiltInParameterGroup.PG_IDENTITY_DATA; break;
                            case "INVALID": paragroup = BuiltInParameterGroup.INVALID; break;
                        }
                        #endregion
                    }
                    if (xdt.Rows[i]["paratype"] == null)
                    { paraType = ParameterType.Text; }
                    else
                    {
                        #region 参数类型对照 用于RevitAPI2016
                        switch (xdt.Rows[i]["paratype"].ToString())
                        {
                            case "Text": paraType = ParameterType.Text; break;

                                //case "Invalid": paraType = ParameterType.Invalid; break;
                                //case "Integer": paraType = ParameterType.Integer; break;

                                //case "Number": paraType = ParameterType.Number; break;
                                //case "Length": paraType = ParameterType.Length; break;
                                //case "Volume": paraType = ParameterType.Volume; break;
                                //case "Area": paraType = ParameterType.Area; break;
                                //case "Angle": paraType = ParameterType.Angle; break;
                                //case "URL": paraType = ParameterType.URL; break;
                                //case "Material": paraType = ParameterType.Material; break;
                                //case "YesNo": paraType = ParameterType.YesNo; break;
                                //case "Force": paraType = ParameterType.Force; break;
                                //case "NumberOfPoles": paraType = ParameterType.NumberOfPoles; break;
                                //case "AreaForce": paraType = ParameterType.AreaForce; break;
                                //case "Moment": paraType = ParameterType.Moment; break;
                                //case "FixtureUnit": paraType = ParameterType.FixtureUnit; break;
                                //case "FamilyType": paraType = ParameterType.FamilyType; break;
                                //case "LoadClassification": paraType = ParameterType.LoadClassification; break;
                                //case "Image": paraType = ParameterType.Image; break;
                                //case "HVACDensity": paraType = ParameterType.HVACDensity; break;
                                //case "HVACEnergy": paraType = ParameterType.HVACEnergy; break;
                                //case "HVACFriction": paraType = ParameterType.HVACFriction; break;
                                //case "HVACPower": paraType = ParameterType.HVACPower; break;
                                //case "HVACPowerDensity": paraType = ParameterType.HVACPowerDensity; break;
                                //case "HVACPressure": paraType = ParameterType.HVACPressure; break;
                                //case "HVACTemperature": paraType = ParameterType.HVACTemperature; break;
                                //case "HVACVelocity": paraType = ParameterType.HVACVelocity; break;
                                //case "HVACAirflow": paraType = ParameterType.HVACAirflow; break;
                                //case "HVACDuctSize": paraType = ParameterType.HVACDuctSize; break;
                                //case "HVACCrossSection": paraType = ParameterType.HVACCrossSection; break;
                                //case "HVACHeatGain": paraType = ParameterType.HVACHeatGain; break;
                                //case "ElectricalCurrent": paraType = ParameterType.ElectricalCurrent; break;
                                //case "ElectricalPotential": paraType = ParameterType.ElectricalPotential; break;
                                //case "ElectricalFrequency": paraType = ParameterType.ElectricalFrequency; break;
                                //case "ElectricalIlluminance": paraType = ParameterType.ElectricalIlluminance; break;
                                //case "ElectricalLuminousFlux": paraType = ParameterType.ElectricalLuminousFlux; break;
                                //case "ElectricalPower": paraType = ParameterType.ElectricalPower; break;
                                //case "HVACRoughness": paraType = ParameterType.HVACRoughness; break;
                                //case "ElectricalApparentPower": paraType = ParameterType.ElectricalApparentPower; break;
                                //case "ElectricalPowerDensity": paraType = ParameterType.ElectricalPowerDensity; break;
                                //case "PipingDensity": paraType = ParameterType.PipingDensity; break;
                                //case "PipingFlow": paraType = ParameterType.PipingFlow; break;
                                //case "PipingFriction": paraType = ParameterType.PipingFriction; break;
                                //case "PipingPressure": paraType = ParameterType.PipingPressure; break;
                                //case "PipingTemperature": paraType = ParameterType.PipingTemperature; break;
                                //case "PipingVelocity": paraType = ParameterType.PipingVelocity; break;
                                //case "PipingViscosity": paraType = ParameterType.PipingViscosity; break;
                                //case "PipeSize": paraType = ParameterType.PipeSize; break;
                                //case "PipingRoughness": paraType = ParameterType.PipingRoughness; break;
                                //case "Stress": paraType = ParameterType.Stress; break;
                                //case "UnitWeight": paraType = ParameterType.UnitWeight; break;
                                //case "ThermalExpansion": paraType = ParameterType.ThermalExpansion; break;
                                //case "LinearMoment": paraType = ParameterType.LinearMoment; break;
                                //case "ForcePerLength": paraType = ParameterType.ForcePerLength; break;
                                //case "ForceLengthPerAngle": paraType = ParameterType.ForceLengthPerAngle; break;
                                //case "LinearForcePerLength": paraType = ParameterType.LinearForcePerLength; break;
                                //case "LinearForceLengthPerAngle": paraType = ParameterType.LinearForceLengthPerAngle; break;
                                //case "AreaForcePerLength": paraType = ParameterType.AreaForcePerLength; break;
                                //case "PipingVolume": paraType = ParameterType.PipingVolume; break;
                                //case "HVACViscosity": paraType = ParameterType.HVACViscosity; break;
                                //case "HVACCoefficientOfHeatTransfer": paraType = ParameterType.HVACCoefficientOfHeatTransfer; break;
                                //case "HVACAirflowDensity": paraType = ParameterType.HVACAirflowDensity; break;
                                //case "Slope": paraType = ParameterType.Slope; break;
                                //case "HVACCoolingLoad": paraType = ParameterType.HVACCoolingLoad; break;
                                //case "HVACCoolingLoadDividedByArea": paraType = ParameterType.HVACCoolingLoadDividedByArea; break;
                                //case "HVACCoolingLoadDividedByVolume": paraType = ParameterType.HVACCoolingLoadDividedByVolume; break;
                                //case "HVACHeatingLoad": paraType = ParameterType.HVACHeatingLoad; break;
                                //case "HVACHeatingLoadDividedByArea": paraType = ParameterType.HVACHeatingLoadDividedByArea; break;
                                //case "Weight": paraType = ParameterType.Weight; break;

                        }
                        #endregion
                    }
                    if (xdt.Rows[i]["paratag"].ToString() == "是")
                    { isInstance = true; }
                    else if (xdt.Rows[i]["paratag"].ToString() == "否")
                    { isInstance = false; }
                    if (xdt.Rows[i]["paravalue"].ToString() == null)
                    {
                        paravalue = "NA";
                    }
                    else
                    {
                        paravalue = xdt.Rows[i]["paravalue"].ToString();
                    }

                    //bool checkDistinct = false;
                    //foreach (FamilyParameter fp in familyMgr.Parameters)
                    //{
                    //    if (fp.Definition.Name == xdt.Rows[i]["paraname"].ToString())
                    //    {
                    //        checkDistinct = true;
                    //    }
                    //}
                    //if (checkDistinct == true)
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    try
                    {
                        FamilyParameter newParameter = familyMgr.AddParameter(paraname, paragroup, paraType, isInstance);
                    }
                    catch(Exception efec)
                    {
                        MessageBox.Show(efec.ToString());
                        continue;
                    }                     
                        //创建族参数(每个参数两秒)
                        //familyMgr.Set(newParameter, paravalue);                
                    //}
                }
 
                try
                {
                    for (int i = 0; i < xdt.Rows.Count; i++)
                    {
                        paraname = xdt.Rows[i]["paraname"].ToString();
                        foreach (FamilyParameter fp in familyMgr.Parameters)
                        {
                            StorageType fst = fp.StorageType;
                            if (fp.Definition.Name == xdt.Rows[i]["paraname"].ToString())
                            {
                                if (xdt.Rows[i]["paravalue"].ToString() == null && fst.ToString() == "String")
                                {
                                    paravalue = "NA";
                                    familyMgr.Set(fp , paravalue);
                                }
                                else
                                {
                                    //paravalue = xdt.Rows[i]["paravalue"].ToString();   
                                    #region
                                    switch (fst.ToString())
                                    {
                                        case "Integer": int paravint = Convert.ToInt32(xdt.Rows[i]["paravalue"].ToString()); familyMgr.Set(fp, paravint); ; break;
                                        case "Double": double paravdouble = Convert.ToDouble(xdt.Rows[i]["paravalue"].ToString()); familyMgr.Set(fp, paravdouble); break;
                                        case "String": string paravstring = xdt.Rows[i]["paravalue"].ToString(); familyMgr.Set(fp, paravstring); break;
                                        case "ElementId": ElementId paravid = new ElementId(Convert.ToInt32(xdt.Rows[i]["paravalue"].ToString())); familyMgr.Set(fp, paravid); break;
                                        case "None":   break;
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
                catch (Exception eeef )
                { /*MessageBox.Show(eeef.ToString());*/ }

                SaveOptions opt = new SaveOptions();
                addParameter.Commit();
             }

            return familyDoc;
        }
    }
}
