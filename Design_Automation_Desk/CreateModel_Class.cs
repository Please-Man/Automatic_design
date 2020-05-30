using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.dsgnchk;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.sw3dprinter;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swdimxpert;
using SolidWorks.Interop.swdocumentmgr;
using SolidWorks.Interop.swmotionstudy;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.SWRoutingLib;

namespace Design_Automation_Desk
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
        DisplayDimension myDisplayDim;
        EquationMgr swEqnMgr = default(EquationMgr); //도구 모음
        Dimension swDimension;
        // 모델링을 위한 객체

        ModelDocExtension swModelDocExt = default(ModelDocExtension);
        DocumentSpecification swDocSpecification = default(DocumentSpecification);

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;


        public void CreateDesk(ref int desk_type,ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string fileName2, ref string fileName3,ref string fileName4,ref string fileName5, ref string material)
        {
            switch (desk_type)
            {
                case 0: //기본
                    CreateDesk1(ref x, ref y, ref t, ref h, ref fileName1, ref fileName2, ref fileName3, ref fileName5, ref material);
                    break;
                case 1: //서랍추가
                    CreateDesk2(ref x, ref y, ref t, ref h, ref fileName1, ref fileName2, ref fileName3, ref fileName5, ref material);
                    break;
                case 2: //책장추가
                    CreateDesk3(ref x, ref y, ref t, ref h, ref fileName1, ref fileName2, ref fileName3, ref fileName4, ref fileName5, ref material);
                    break;
            }
        }

        private void CreateDesk1(ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string fileName2, ref string fileName3,ref string fileName5, ref string material) //책상 생성
        {
            swApp.INewPart(); //파트 새로열기

            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            //swModel.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swInputDimValOnCreate, false);
            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2, -y / 2, 0, x / 2, y / 2, 0, true);//사각형 생성

            swModel.CreateCircleByRadius2(-x / 2 + 0.12, -y / 2 + 0.12, 0, 100.0 / 2000.0); //기둥이 들어갈 원
            swModel.AddDimension2(0, -x / 2 + 0.12, -y / 2 + 0.12);
            swModel.CreateCircleByRadius2(-x / 2 + 0.12, y / 2 - 0.12, 0, 100.0 / 2000.0);
            swModel.AddDimension2(0, -x / 2 + 0.12, -y / 2 + 0.12);
            swModel.CreateCircleByRadius2(x / 2 - 0.12, -y / 2 + 0.12, 0, 100.0 / 2000.0);
            swModel.AddDimension2(0, -x / 2 + 0.12, -y / 2 + 0.12);
            swModel.CreateCircleByRadius2(x / 2 - 0.12, y / 2 - 0.12, 0, 100.0 / 2000.0);
            swModel.AddDimension2(0, -x / 2 + 0.12, -y / 2 + 0.12);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.AddDimension2(0, 0, -y / 2 - 0.05);
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.1, 0, 0);
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 + 0.06, 0, -y / 2 - 0.05);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.05, 0, -y / 2 + 0.06);
            status = swModel.Extension.SelectByID2("Line3", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point8", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.05, 0, y / 2 - 0.06);
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point8", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 + 0.06, 0, -y / 2 + 0.06);
            status = swModel.Extension.SelectByID2("Line3", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point12", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(x / 2 - 0.05, 0, y / 2 - 0.06);
            status = swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point12", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(x / 2 - 0.05, 0, y / 2 - 0.06);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point10", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(x / 2 - 0.05, 0, y / 2 - 0.06);
            status = swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point10", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(x / 2 - 0.05, 0, y / 2 - 0.06);

            swModel.CreateCenterLineVB(0, 0, 0, x / 2, 0, 0);
            swModel.CreateCenterLineVB(0, 0, 0, 0, y / 2, 0);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Sketch";

            status = swModel.Extension.SelectByID2("Desk_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, t, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Desk / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (t * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            ApplyMaterial(ref material);
            Save(ref fileName1);

            Create_Pillar(ref h);
            ApplyMaterial(ref material);
            Save(ref fileName2);

            Assembly_desk1(ref fileName1, ref fileName2,ref fileName3, ref x, ref y, ref t, ref h);
            Save(ref fileName3);

            double a = x * y * h;
            Create_Drafting(ref fileName3, ref a); ;
            Save(ref fileName5);
        }

        private void CreateDesk2(ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string fileName2, ref string fileName3, ref string fileName5, ref string material)
        {
            swApp.INewPart(); //파트 새로열기

            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2, -y / 2, 0, x / 2, y / 2, 0, true);//사각형 생성
            
            swModel.CreateCircleByRadius2(-x / 2 + 0.12, -y / 2 + 0.12, 0, 100.0 / 2000.0); //기둥이 들어갈 원
            swModel.CreateCircleByRadius2(-x / 2 + 0.12, y / 2 - 0.12, 0, 100.0 / 2000.0);
            swModel.CreateCircleByRadius2(x / 2 - 0.12, -y / 2 + 0.12, 0, 100.0 / 2000.0);
            swModel.CreateCircleByRadius2(x / 2 - 0.12, y / 2 - 0.12, 0, 100.0 / 2000.0);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.AddDimension2(0, 0, -y / 2 - 0.05);
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.05, 0, 0);

            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 + 0.06, -y / 2 - 0.05, 0);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.05, -y / 2 + 0.06, 0);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Sketch";

            status = swModel.Extension.SelectByID2("Desk_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, t, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Desk", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (t * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            ApplyMaterial(ref material);

            //서랍
            status = swModel.Extension.SelectByID2("Front", "PLANE", 0, 0, 0, false, 0, null, 0);
            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2 + 0.175, -t-0.08, 0, x / 2 - 0.175, -t, 0, true);//사각형 생성

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Drawer_Sketch";

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondMidPlane, 0, y - 0.15, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);
            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Drawer_Model";

            status = swModel.Extension.SelectByID2("", "FACE", 0,-t/2 -0.04, (y - 0.15) / 2, false, 1, null, 0);
            swModel.InsertFeatureShell(0.005, false);


            Save(ref fileName1);

            Create_Pillar(ref h);
            ApplyMaterial(ref material);
            Save(ref fileName2);

            Assembly_desk1(ref fileName1, ref fileName2, ref fileName3, ref x, ref y, ref t, ref h);
            Save(ref fileName3);

            double a = x * y * h;
            Create_Drafting(ref fileName3, ref a);
            Save(ref fileName5);
        }

        private void CreateDesk3(ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string fileName2, ref string fileName3, ref string fileName4, ref string fileName5, ref string material)
        {
            swApp.INewPart(); //파트 새로열기

            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            //swModel.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swInputDimValOnCreate, false);
            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-x / 2, -y / 2, 0, x / 2, y / 2, 0, true);//사각형 생성

            swModel.CreateCircleByRadius2(-x / 2 + 0.12, -y / 2 + 0.12, 0, 100.0 / 2000.0); //기둥이 들어갈 원
            swModel.CreateCircleByRadius2(-x / 2 + 0.12, y / 2 - 0.12, 0, 100.0 / 2000.0);

            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 + 0.06, -y / 2 - 0.05, 0);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point6", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-x / 2 - 0.05, -y / 2 + 0.06, 0);

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Sketch";

            status = swModel.Extension.SelectByID2("Desk_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, t, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Desk / Boru", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

            cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (t * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            ApplyMaterial(ref material);
            Save(ref fileName1);

            Create_Pillar(ref h);
            ApplyMaterial(ref material);
            Save(ref fileName2);

            Create_BookCase(ref x, ref y, ref t, ref h);
            ApplyMaterial(ref material);
            Save(ref fileName4);

            Assembly_desk2(ref fileName1, ref fileName2, ref fileName3, ref fileName4, ref x, ref y, ref t, ref h);
            Save(ref fileName3);

            double a = x * y * h;
            Create_Drafting(ref fileName3, ref a);
            Save(ref fileName5);
        }

        private void Create_Pillar(ref double h)
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

                swModel.CreateCircleByRadius2(0, 0, 0, 100.0 / 2000); //외경(반지름) m단위

                swModel.InsertSketch2(true);

                swFeature = swModel.FeatureByPositionReverse(0);
                swFeature.Name = "Pillar_Sketch";

                status = swModel.Extension.SelectByID2("Pillar_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

                swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, h, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

                swFeature = swModel.FeatureByPositionReverse(0);
                swFeature.Name = "Pillar_Model";

                swModel.ForceRebuild3(true);
                swModel.ViewZoomtofit2();

                config = swModel.GetActiveConfiguration();
                cusPropMgr = config.CustomPropertyManager;

                cusPropMgr.Add3("Description", (int)swCustomInfoType_e.swCustomInfoText, "Pillar", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);

                cusPropMgr.Add3("Dimensions", (int)swCustomInfoType_e.swCustomInfoText, (h * 1000).ToString() + "mm", (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            }
            Console.WriteLine("Dimensions Done");
        }

        private void Save(ref string filename)
        {
            bool status;
            int lErrors = 0;
            int lWarnings = 0;

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swApp.Visible = true;

            // Make a change
            swModel.ViewZoomtofit2();
            status = swModel.SaveAs(filename);

            // Errors
            Console.WriteLine("Errors as defined in swFileSaveError_e: " + lErrors);

            // Warnings
            Console.WriteLine("Warnings as defined in swFileSaveWarning_e: " + lWarnings);
        }

        private void Create_Drafting(ref string fileName3, ref double a)
        {
            //도면화를 위한 객체
            DrawingDoc swDraw = default(DrawingDoc);
            SelectionMgr swSelMgr = default(SelectionMgr);
            SelectData swSelData = default(SelectData);
            ModelDocExtension swModelDocExt = default(ModelDocExtension);
            View swView = default(View);
            DrawingComponent swRootDrawComp = default(DrawingComponent);
            object[] vDrawChildCompArr = null;
            DrawingComponent swDrawComp = default(DrawingComponent);
            Component2 swComp = default(Component2);
            ModelDoc2 swCompModel = default(ModelDoc2);
            ModelDoc2 swDrawModel = default(ModelDoc2);
            Entity swEnt = default(Entity);
            SketchSegment swSketchSegment;
            SketchManager swSketchMgr;

            string assemblyDrawing = null;
            bool status = false;
            int lineWeight = 0;
            double lineThickness = 0;
            object annObj = null;

            int sizer;
            double locx, locy, txtHeight;
            if (a>0.48) { sizer = 11; locx = 0.96; locy = 0.65; txtHeight = 0.014; }
            else { sizer = 10; locx = 0.657; locy = 0.435; txtHeight = 0.007; }
            swDraw = (DrawingDoc)swApp.INewDrawing(sizer); //A0:11, A1:10, A2:9, A3:8, A4:6 , 도면 새로 만들기
            swModel = (ModelDoc2)swApp.ActiveDoc;
            Sheet swSheet;

            status = swDraw.Create3rdAngleViews2(fileName3); //도면의 3면도 생성
            swSheet = (Sheet)swDraw.GetCurrentSheet();
            swSheet.SetScale(1.0, 5.0, true, true); // 1:5비율

            swView = (View)swDraw.GetFirstView();
            swView = (View)swView.GetNextView();

            status = swView.SelectEntity(swEnt, false);
            swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);           

            swDraw.CreateDrawViewFromModelView3(fileName3, "*등각 보기", locx, locy, 0);
            swDraw.ViewDisplayShaded(); // 음영 보기

            status = swModel.Extension.SelectByID2("도면뷰1", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.Dimensions(); // 치수생성
            string TableTemplate = "D:\\Program File\\SolidWorks2019\\SOLIDWORKS\\lang\\korean\\bom-material.sldbomtbt"; //pc마다 저장된 위치가 다르므로 수정 필요

            BomTableAnnotation swBOMAnnotation;
            status = swDraw.ActivateView("도면뷰1");
            swView = (View)swDraw.ActiveDrawingView;
            swBOMAnnotation = swView.InsertBomTable4(true, 0.4, 0.3, (int)swBOMConfigurationAnchorType_e.swBOMConfigurationAnchor_TopLeft, (int)swBomType_e.swBomType_Indented, "", TableTemplate, false, (int)swNumberingType_e.swNumberingType_Detailed, true);
            
            TextFormat textFormat = default(TextFormat); 
            textFormat = swModel.Extension.GetUserPreferenceTextFormat((int)swUserPreferenceTextFormat_e.swDetailingDimensionTextFormat, (int)swUserPreferenceOption_e.swDetailingDimension);
            textFormat.CharHeight = txtHeight;
            status = swModel.Extension.SetUserPreferenceTextFormat((int)swUserPreferenceTextFormat_e.swDetailingDimensionTextFormat, (int)swUserPreferenceOption_e.swDetailingDimension, textFormat);
            
        }

        private void ApplyMaterial(ref string material)
        {
            string databaseName = null;
            string newPropName = null;
            bool orgBlend = false;
            bool orgApply = false;
            double orgAngle = 0;
            double orgScale = 0;
            long longstatus = 0;

            swModel = swApp.ActiveDoc;
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

        private void Create_BookCase(ref double x, ref double y, ref double t, ref double h)
        {
            swApp.INewPart(); //파트 새로열기

            bool status;

            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Top", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(0, 0, 0, 0.3, y+2*t, 0, true);//사각형 생성

            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "BookCase_Sketch";

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, h+0.6, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);
            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "BookCase_Model";

            //쉘적용
            status = swModel.Extension.SelectByID2("", "FACE", 0, y/2, -0.15, false, 1, null, 0);
            swModel.InsertFeatureShell(t, false);
            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "shell";

            //선반 만들기
            status = swModel.Extension.SelectByID2("Right", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.SketchRectangle(y+t, h-2*t,0, t, h-t, 0, true);//사각형 생성
            swModel.InsertSketch2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "plate_Sketch";

            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, false, (int)swEndConditions_e.swEndCondBlind, 0, 0.29, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);
            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "plate_Model";
        }

        
        // 어셈블리 메소드
        PartDoc swPart1 = default(PartDoc);
        PartDoc swPart2 = default(PartDoc);
        //ModelDoc2 swModel = default(ModelDoc2);
        AssemblyDoc swAssemblyDoc = default(AssemblyDoc);
        Component2 swComponent1 = default(Component2);
        Component2 swComponent2 = default(Component2);
        Component2 swComponent3 = default(Component2);
        Component2 swComponent4 = default(Component2);
        Component2 swComponent5 = default(Component2);

        Mate2 swMate = default(Mate2);
        bool status = false;
        DispatchWrapper[] dispWrapperComponentArray = new DispatchWrapper[1];
        Component2[] swComponentArray = new Component2[1];
        bool[] repeatArray = new bool[1];
        DispatchWrapper[] dispWrapperMateReferencesArray = new DispatchWrapper[1];
        double[] valueArray = new double[1];
        bool[] flipAlignmentArray = new bool[1];
        bool[] flipDimensionArray = new bool[1];
        bool[] lockRotationArray = new bool[1];
        int[] orientationArray = new int[1];

        //책장을 포함하지 않는 책상
        private void Assembly_desk1(ref string fileName1, ref string fileName2,ref string fileName3, ref double x, ref double y, ref double t, ref double h)
        {
            
            swApp.INewAssembly();

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swAssemblyDoc = (AssemblyDoc)swModel;

            // Add components to assembly document
            swComponent1 = (Component2)swAssemblyDoc.AddComponent5(fileName1, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", 0, 0, 0);
            swComponent2 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -x, -h, -y );
            swComponent3 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -x , -h , y );
            swComponent4 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", x , -h , -y );
            swComponent5 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", x , -h , y );
            swModel.ViewZoomtofit2();

            Save(ref fileName3);

            // Add profile center mate
            swModel = (ModelDoc2)swApp.ActiveDoc;
            swModelDocExt = (ModelDocExtension)swModel.Extension;

            status = swModelDocExt.SelectByID2("Top@"+ swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@"+ swComponent2.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", -x, -t , y, true, 1, null, 0); //기둥
            status = swModelDocExt.SelectByID2("", "FACE", -x/2 + 0.17, t/8, -y/2 + 0.12, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);

            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@" + swComponent3.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", -x, -t , y, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("", "FACE", -x / 2 + 0.17, t / 8, y / 2 - 0.12, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);

            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@" + swComponent4.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", x, -t , -y, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("", "FACE", x / 2 - 0.12, t / 8, -y / 2 + 0.17, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);

            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@" + swComponent5.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", x, -t , y, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("", "FACE", x / 2 - 0.12, t / 8, y / 2 - 0.17, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
        }

        //책장을 포함하는 책상
        private void Assembly_desk2(ref string fileName1,ref string fileName2,ref string fileName3, ref string fileName4, ref double x, ref double y, ref double t,ref double h)
        {
            swApp.INewAssembly();

            swModel = (ModelDoc2)swApp.ActiveDoc;
            swAssemblyDoc = (AssemblyDoc)swModel;

            // Add components to assembly document
            swComponent1 = (Component2)swAssemblyDoc.AddComponent5(fileName1, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", 0, 0, 0);
            swComponent2 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -x, -h, -y);
            swComponent3 = (Component2)swAssemblyDoc.AddComponent5(fileName2, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -x, -h, y);
            swComponent4 = (Component2)swAssemblyDoc.AddComponent5(fileName4, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", x , 0 , 0);
            swModel.ViewZoomtofit2();

            Save(ref fileName3);

            // Add profile center mate
            swModel = (ModelDoc2)swApp.ActiveDoc;
            swModelDocExt = (ModelDocExtension)swModel.Extension;

            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@" + swComponent2.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", -x, -t, -y, true, 1, null, 0); //기둥
            status = swModelDocExt.SelectByID2("", "FACE", -x / 2 + 0.17, 0, -y / 2 + 0.12, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);

            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); //기둥1
            status = swModelDocExt.SelectByID2("Top@" + swComponent3.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("", "FACE", -x, -t, y, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("", "FACE", -x / 2 + 0.17, 0, y / 2 - 0.12, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateCONCENTRIC, (int)swMateAlign_e.swMateAlignALIGNED, true, 0, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);

            status = swModelDocExt.SelectByID2("Front@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0); 
            status = swModelDocExt.SelectByID2("Front@" + swComponent4.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, false, y / 2 + t,0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("Right@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("Right@" + swComponent4.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, false, x / 2 - 0.3 + t, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
            status = swModelDocExt.SelectByID2("Top@" + swComponent1.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            status = swModelDocExt.SelectByID2("Top@" + swComponent4.Name2 + "@assembly_desk", "PLANE", 0, 0, 0, true, 1, null, 0);
            swAssemblyDoc.AddMate5((int)swMateType_e.swMateDISTANCE, (int)swMateAlign_e.swMateAlignALIGNED, true, h, 0, 0, 0, 0, 0, 0, 0, false, true, (int)swMateWidthOptions_e.swMateWidth_Centered, out errors);
            Console.WriteLine("Mate Error!!! = " + errors);
            swModel.ClearSelection2(true);
        }

        public void Edit(ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string fileName2, ref string fileName4, ref string material)
        {
            Edit_desk(ref x, ref y, ref t, ref h, ref fileName1, ref material);
            Edit_pillar(ref x, ref y, ref t, ref h, ref fileName2, ref material);
        }

        private void Edit_desk(ref double x, ref double y, ref double t, ref double h, ref string fileName1, ref string material)
        {
            swModel = (ModelDoc2)swApp.OpenDoc6(fileName1, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);

            bool status;

            swModel = swApp.ActiveDoc;

            status = swModel.Extension.SelectByID2("Desk_Sketch@desk.sldprt", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swModel.EditSketch();
            swModel.ClearSelection2(true);

            //status = swModel.Extension.SelectByID2("D5@Desk_Sketch@desk.sldprt", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swDimension = (Dimension)swModel.Parameter("D5@Desk_Sketch@desk.sldprt");
            errors = swDimension.SetSystemValue3(x, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration, null);

            //status = swModel.Extension.SelectByID2("D6@Desk_Sketch@desk.sldprt", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swDimension = (Dimension)swModel.Parameter("D6@Desk_Sketch@desk.sldprt");
            errors = swDimension.SetSystemValue3(y, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration, null);

            swModel.InsertSketch2(true);

            status = swModel.Extension.SelectByID2("Desk_Model@desk.sldprt", "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(true);


            status = swModel.Extension.SelectByID2("Desk_Sketch@desk.sldprt", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, t, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Desk_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            ApplyMaterial(ref material);
        }

        private void Edit_pillar(ref double x, ref double y, ref double t, ref double h, ref string fileName2, ref string material)
        {
            swModel = (ModelDoc2)swApp.OpenDoc6(fileName2, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);

            bool status;

            swModel = swApp.ActiveDoc;

            status = swModel.Extension.SelectByID2("Pillar_Model@pillar.sldprt", "BODYFEATURE", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(true);


            status = swModel.Extension.SelectByID2("Pillar_Sketch@pillar.sldprt", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swFeature = swModel.FeatureManager.FeatureExtrusion3(true, false, true, (int)swEndConditions_e.swEndCondBlind, 0, t, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false, 0, 0, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "Pillar_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;

            ApplyMaterial(ref material);
        }
    }
}
