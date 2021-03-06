﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidWorks.Interop.cosworks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.sw3dprinter;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swdimxpert;
using SolidWorks.Interop.swdocumentmgr;
using SolidWorks.Interop.swmotionstudy;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.SWRoutingLib;

namespace Design_Automation_Basic
{
    class CreateModel_Class
    {
        SldWorks swApp = new SldWorks();

        ModelDoc2 swModel;
        PartDoc myPart = default(PartDoc);
        MaterialVisualPropertiesData myMatVisProps = default(MaterialVisualPropertiesData);
        Feature swFeature;
        Configuration config;
        CustomPropertyManager cusPropMgr;
        // 모델링을 위한 객체

        DocumentSpecification swDocSpecification = default(DocumentSpecification);

        ModelDocExtension swModelDocExt;
        MotionStudyManager swMotionMgr;
        MotionStudy swMotionStudy1;
        SimulationGravityFeatureData swGravityFeat;

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;

        public void CreateHC(ref double dout, ref double di, ref double h) //중공축 생성
        {
            bool status;

            swApp.INewPart();

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);
            Console.WriteLine("status 확인 : " + status);
            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, dout / 2); //외경(반지름) m단위
            swModel.CreateCircleByRadius2(0, 0, 0, di / 2); //내경(반지름)

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Hollow_Clynder_Sketch";

            status = swModel.Extension.SelectByID2("Hollow_Clynder_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);
            Console.WriteLine("status 확인 : " + status);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Hollow_Clynder_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Hollow_clynder / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            Console.WriteLine("Dimensions Done");
        }

        public void CreateC(ref double d, ref double h) // 중실축 생성
        {
            bool status;
            swApp.INewPart();

            Console.WriteLine("swApp " + swApp);
            if (swApp != null)
            {
                swApp.ResetUntitledCount(0, 0, 0);

                ModelDoc2 swModel;
                swModel = (ModelDoc2)swApp.ActiveDoc;
                Console.WriteLine("swModel : " + swModel);
                swModel.ClearSelection2(true);

                swFeature = swModel.FeatureByPositionReverse(3);
                swFeature.Name = "Front";


                swFeature = swModel.FeatureByPositionReverse(2);
                swFeature.Name = "Top";

                swFeature = swModel.FeatureByPositionReverse(1);
                swFeature.Name = "Right";

                status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

                swModel.InsertSketch2(true);

                swModel.CreateCircleByRadius2(0, 0, 0, d / 2); //외경(반지름) m단위

                swModel.InsertSketch2(true);

                swFeature = swModel.FeatureByPositionReverse(0);
                swFeature.Name = "Clynder_Sketch";

                status = swModel.Extension.SelectByID2("Clynder_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

                swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

                swFeature = swModel.FeatureByPositionReverse(0);
                swFeature.Name = "Clynder_Model";

                swModel.ForceRebuild3(true);
                swModel.ViewZoomtofit2();

                config = swModel.GetActiveConfiguration();
                cusPropMgr = config.CustomPropertyManager;

                cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "clynder", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

                cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            }
            Console.WriteLine("Dimensions Done");
        }

        public void CreateB(ref double x, ref double y, ref double h) //각주 생성
        {
            //swDocSpecification = (DocumentSpecification)swApp.INewPart();

            bool status;

            swApp.INewPart();

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2, -y / 2, 0, x / 2, y / 2, 0, true);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Block_Sketch";

            status = swModel.Extension.SelectByID2("Block_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Block_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Block / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
        }

        public void Material(ref string material)
        {
            string databaseName = null;
            string newPropName = null;
            bool orgBlend = false;
            bool orgApply = false;
            double orgAngle = 0;
            double orgScale = 0;
            long longstatus = 0;

            swModel = (ModelDoc2)swApp.ActiveDoc;
            myPart = (PartDoc)swModel;
            myMatVisProps = myPart.GetMaterialVisualProperties();
            Console.WriteLine("myMatVisProps : " + myMatVisProps);
            Console.WriteLine("");
            Console.WriteLine("===== Material Visual Properties Example =====");

            if ((myMatVisProps != null))
            {
                dump_material_visual_properties(myMatVisProps, myPart);

                // Set the material to something else, so that the display changes
                databaseName = "solidworks materials.sldmat";   // or "SOLIDWORKS Materials";
                newPropName = material;
                myPart.SetMaterialPropertyName(databaseName, newPropName);
                //myPart.SetMaterialPropertyName2(configName, databaseName, newPropName);
                dump_material_visual_properties(myMatVisProps, myPart);
            }

            // Set the material visual properties to be just color, no advanced graphics
            myMatVisProps = myPart.GetMaterialVisualProperties();
            Console.WriteLine("myMatVisProps : " + myMatVisProps);
            Console.WriteLine("");

            if ((myMatVisProps != null))
            {
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                // Set the material visual properties to be RealView
                myMatVisProps.RealView = true;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                // Set the material visual properties to be SOLIDWORKS standard textures
                myMatVisProps.RealView = false;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
            }

            myMatVisProps = myPart.GetMaterialVisualProperties();
            Console.WriteLine("myMatVisProps : " + myMatVisProps);
            Console.WriteLine("");

            if ((myMatVisProps != null))
            {
                orgAngle = myMatVisProps.Angle;
                myMatVisProps.Angle = orgAngle + 1.0;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                orgScale = myMatVisProps.Scale2;
                myMatVisProps.Scale2 = orgScale * 1.25;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                // Toggle the standard texture to blend with the part color
                if (myMatVisProps.BlendColor == false)
                {
                    orgBlend = false;
                }
                else
                {
                    orgBlend = true;
                }

                myMatVisProps.BlendColor = !orgBlend;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                myMatVisProps.BlendColor = orgBlend;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                Console.WriteLine("myMatVisProps.ApplyMaterialColorToPart : " + myMatVisProps.ApplyMaterialColorToPart);
                Console.WriteLine("");

                // Toggle the apply material color to part flag
                if (myMatVisProps.ApplyMaterialColorToPart == false)
                {
                    orgApply = false;
                }
                else
                {
                    orgApply = true;
                }

                myMatVisProps.ApplyMaterialColorToPart = !orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                myMatVisProps.ApplyMaterialColorToPart = orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                // Toggle the apply material hatch to drawing section view flag
                if (myMatVisProps.ApplyMaterialHatchToSection == false)
                {
                    orgApply = false;
                }
                else
                {
                    orgApply = true;
                }

                myMatVisProps.ApplyMaterialHatchToSection = !orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                myMatVisProps.ApplyMaterialHatchToSection = orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);

                // Toggle the apply appearance flag
                if (myMatVisProps.ApplyAppearance == false)
                {
                    orgApply = false;
                }
                else
                {
                    orgApply = true;
                }

                myMatVisProps.ApplyAppearance = !orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
                myMatVisProps.ApplyAppearance = orgApply;
                longstatus = myPart.SetMaterialVisualProperties(myMatVisProps, (int)swInConfigurationOpts_e.swThisConfiguration, null);
                dump_material_visual_properties(myMatVisProps, myPart);
            }
        }

        //재질 적용 하기 위한 내부 메소드
        private void dump_material_visual_properties(MaterialVisualPropertiesData myMatVisProps, PartDoc myPart)
        {
            string configName = null;
            string databaseName = null;
            string propName = null;
            bool bRealView = false;
            double dScale = 0;
            double dAngle = 0;
            bool bBlendColor = false;
            bool bApplyColor = false;
            bool bApplyHatch = false;
            bool bApplyAppearance = false;
            configName = "default";
            databaseName = null;
            propName = myPart.GetMaterialPropertyName2(configName, out databaseName);
            Console.WriteLine("");
            Console.WriteLine("Config: \"" + configName + "\", Database: \"" + databaseName + "\", Material: \"" + propName + "\"");

            if ((myMatVisProps != null))
            {
                bRealView = myMatVisProps.RealView;
                dScale = myMatVisProps.Scale2;
                dAngle = myMatVisProps.Angle;
                bBlendColor = myMatVisProps.BlendColor;
                bApplyColor = myMatVisProps.ApplyMaterialColorToPart;
                bApplyHatch = myMatVisProps.ApplyMaterialHatchToSection;
                bApplyAppearance = myMatVisProps.ApplyAppearance;


                if (bRealView == false)
                {
                    Console.WriteLine("Advanced graphics - SOLIDWORKS standard textures.");
                }
                else
                {
                    Console.WriteLine("Advanced graphics - RealView textures.");
                }
                Console.WriteLine("   SOLIDWORKS standard texture scale = " + dScale + ", Angle = " + dAngle);
                if (bBlendColor == false)
                {
                    Console.WriteLine("   Do not blend part color with SOLIDWORKS standard texture.");
                }
                else
                {
                    Console.WriteLine("   Blend part color with SOLIDWORKS standard texture.");
                }

                if (bApplyColor == false)
                {
                    Console.WriteLine("Do not apply material color to part.");
                }
                else
                {
                    Console.WriteLine("Apply material color to part.");
                }

                if (bApplyHatch == false)
                {
                    Console.WriteLine("Do not apply material hatch to drawing section.");
                }
                else
                {
                    Console.WriteLine("Apply material hatch to drawing section.");

                }
                if (bApplyAppearance == false)
                {
                    Console.WriteLine("Do not apply appearance.");
                }
                else
                {
                    Console.WriteLine("Apply appearance.");
                }

            }

        }

        public void Calculate_model()
        {
            ModelDocExtension swModelExt = default(ModelDocExtension);
            SelectionMgr swSelMgr = default(SelectionMgr);
            Component2 swComp = default(Component2);
            int nStatus = 0;
            double[] vMassProp = null;
            int i = 0;
            int nbrSelections = 0;

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swModelExt = swModel.Extension;
            swSelMgr = (SelectionMgr)swModel.SelectionManager;

            nbrSelections = swSelMgr.GetSelectedObjectCount2(-1);

            if (nbrSelections == 0)
            {

                Console.WriteLine("Please select one or more components and rerun the macro.");
                return;

            }


            nbrSelections = nbrSelections - 1;

            Console.WriteLine("Getting mass properties for components: ");
            for (i = 0; i <= nbrSelections; i++)
            {
                swComp = (Component2)swSelMgr.GetSelectedObject6(i + 1, -1);
                Console.WriteLine("  " + swComp.Name2);
            }

            vMassProp = (double[])swModelExt.GetMassProperties2(1, out nStatus, true);

            Console.WriteLine("Status as defined in swMassPropertiesStatus_e (0 = Mass properties calculation successful) = " + nStatus);


            if ((vMassProp != null))
            {
                Console.WriteLine("Center of mass:");
                Console.WriteLine("  X-coordinate = " + vMassProp[0]);
                Console.WriteLine("  Y-coordinate = " + vMassProp[1]);
                Console.WriteLine("  Z-coordinate = " + vMassProp[2]);
                Console.WriteLine("Volume = " + vMassProp[3]);
                Console.WriteLine("Surface area = " + vMassProp[4]);
                Console.WriteLine("Mass = " + vMassProp[5]);
                Console.WriteLine("Density = " + vMassProp[5] / vMassProp[3]);
                Console.WriteLine("Moments of inertia taken at the center of mass and aligned with the output coordinate system:");
                Console.WriteLine("  Lxx = " + vMassProp[6]);
                Console.WriteLine("  Lyy = " + vMassProp[7]);
                Console.WriteLine("  Lzz = " + vMassProp[8]);
                Console.WriteLine("  Lxy = " + vMassProp[9]);
                Console.WriteLine("  Lzx = " + vMassProp[10]);
                Console.WriteLine("  Lyz = " + vMassProp[11]);

                for (int q = 0; q < 12; q++)
                {
                    if (vMassProp[q] < 0.00000000000001)
                    {
                        vMassProp[q] = 0;
                    }
                }

                Result_cal += "중심(mm) : \n" + "X = " + vMassProp[0] * 1000 + "\nY = " + vMassProp[1] * 1000 + "\nZ = " + vMassProp[2] * 1000
                               + "\n볼륨(mm^3) = " + vMassProp[3] * 1000 * 1000 * 1000 + "\n면적(mm^2) = " + vMassProp[4] * 1000 * 1000 + "\n질량(g) = " + vMassProp[5] * 1000 +
                               "\n밀도(g/mm^3) = " + vMassProp[5] / vMassProp[3] + "\n질량 중심에서의 관성 모멘트(kg*m^2) = " +
                               "\nLxx = " + vMassProp[6] * 1000 * 1000 * 1000 + "\nLyy = " + vMassProp[7] * 1000 * 1000 * 1000 + "\nLzz = " + vMassProp[8] * 1000 * 1000 * 1000 +
                               "\nLxy = " + vMassProp[9] * 1000 * 1000 * 1000 + "\nLzx = " + vMassProp[10] * 1000 * 1000 * 1000 +
                               "\nLyz = " + vMassProp[11] * 1000 * 1000 * 1000;
            }
        }

        public string Result_cal; // 물성치 메세지 출력용      

        CosmosWorks COSMOSWORKS = default(CosmosWorks);
        CwAddincallback CWAddinCallBack = default(CwAddincallback);
        CWModelDoc ActDoc = default(CWModelDoc);
        CWStudyManager StudyMngr = default(CWStudyManager);
        CWStudy Study = default(CWStudy);
        CWMesh CWMesh = default(CWMesh);
        CWResults CWResult = default(CWResults);
        CWPlot CWCFf = default(CWPlot);
        CWResultsProbeManager CWProbeResultsManager = default(CWResultsProbeManager);
        SelectionMgr swSelMgr = default(SelectionMgr);
        CwAddincallback COSMOSObject = default(CwAddincallback);
        CWModelDoc swsActDoc = default(CWModelDoc);
        CWStudyManager swsStudyMngr = default(CWStudyManager);
        CWStudy swsStudy = default(CWStudy);
        CWLoadsAndRestraintsManager swsLBCMgr = default(CWLoadsAndRestraintsManager);
        CWForce swsCWForce = default(CWForce);

        object selBeam = null;
        object selFace = null;
        double[] data = new double[6];
        int rowNum = 0;
        double[] distValue = new double[3];
        double[] forceValue = new double[3];
        int errCode = 0;
        string forceType = null;

        public void SimulateB()
        {
            CWAddinCallBack = (CwAddincallback)swApp.GetAddInObject("CosmosWorks.CosmosWorks");
            if (CWAddinCallBack == null)
                ErrorMsg(swApp, "No CWAddinCallBack object", true);
            COSMOSWORKS = CWAddinCallBack.CosmosWorks;
            if (COSMOSWORKS == null)
                ErrorMsg(swApp, "No CosmosWorks object", true);

            //Get active document
            ActDoc = COSMOSWORKS.ActiveDoc;
            if (ActDoc == null)
                ErrorMsg(swApp, "No active document", true);

            //Delete all default static study plots from this model
            ActDoc.DeleteAllDefaultStaticStudyPlots();

            StudyMngr = ActDoc.StudyManager;

            Study = StudyMngr.CreateNewStudy3("Study", (int)swsAnalysisStudyType_e.swsAnalysisStudyTypeStatic, 0, out errCode);

            //Get Ready study
            if (StudyMngr == null)
                ErrorMsg(swApp, "No study manager object", true);
            StudyMngr.ActiveStudy = 0;
            Study = StudyMngr.GetStudy(0);
            if (Study == null)
                ErrorMsg(swApp, "Study not created", true);

            Study.CreateMesh(0, 15, 0.75);

        }

        private void ErrorMsg(object swApp, string Message)
        {
            MessageBox.Show(Message);
            MessageBox.Show("'*** WARNING - General");
            MessageBox.Show("'*** " + Message);
            MessageBox.Show("");
        }

        private void ErrorMsg(SldWorks SwApp, string Message, bool EndTest)
        {
            SwApp.SendMsgToUser2(Message, 0, 0);
            SwApp.RecordLine("'*** WARNING - General");
            SwApp.RecordLine("'*** " + Message);
            SwApp.RecordLine("");
        }

        private void LoadError(object swApp, string force, long errorCode)
        {
            switch (errorCode)
            {
                case 18:
                    ErrorMsg(swApp, "You cannot apply triangular and centered load distribution on multiple beams");
                    break;
                case 19:
                    ErrorMsg(swApp, "You cannot apply a zero intensity load");
                    break;
                case 20:
                    ErrorMsg(swApp, "Invalid selection");
                    break;
                case 21:
                    ErrorMsg(swApp, "The table-driven data is invalid");
                    break;
                case 22:
                    ErrorMsg(swApp, "In the table-driven distance data, the distance value from the previous row cannot be greater than the distance value in the next row");
                    break;
                case 0:
                    Debug.Print(force);
                    break;
                default:
                    ErrorMsg(swApp, "No forces applied");
                    break;
            }
        }
    }

}
