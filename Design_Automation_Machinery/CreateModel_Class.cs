using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.sw3dprinter;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swdimxpert;
using SolidWorks.Interop.swdocumentmgr;
using SolidWorks.Interop.swmotionstudy;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.SWRoutingLib;

namespace Design_Automation_Machinery
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
        ModelDocExtension swModelDocExt = default(ModelDocExtension);
        SelectionMgr swSelectionMgr = default(SelectionMgr);
        SketchManager sketchMgr = default(SketchManager);
        Dimension swDimension;

        // 모델링을 위한 객체

        DocumentSpecification swDocSpecification = default(DocumentSpecification);

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;

        public void CreateV1(ref double dp, ref double a, ref double l0, ref double k, ref double k0, ref double f, ref double r1, ref double r2, ref double r3)
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

            status = swModel.Extension.SelectByID2("Front", "PLANE", 0, 0, 0, false, 0, null, 0);
            Console.WriteLine("status 확인 : " + status);
            swModel.InsertSketch2(true);
            swApp.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swInputDimValOnCreate, false);

            //스케치 시작
            swModel.CreateCenterLineVB(0, 0, 0, 0, dp, 0); // 중심선
            swModel.AddDimension2(-0.01, 0.01, 0);
            swModel.ClearSelection2(true);

            swModel.CreateLine2(0, 0, 0, f, 0, 0);
            swModel.AddDimension2(0.02, -0.02, 0);
            swModel.ClearSelection2(true);

            swModel.CreateLine2(f,0,0,f,dp+k,0); //오른쪽 선(높이는 호칭지름+k)
            swModel.AddDimension2(0.03, 0.03, 0);
            swModel.ClearSelection2(true);

            swModel.CreateLine2(f, dp + k, 0, 0.005, dp + k, 0);

            swModel.CreateLine2(0,dp-k0,0,0.002,dp-k0,0); 
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line5", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.SketchAddConstraints("sgHORIZONTAL2D");
            swModel.AddDimension2(0.03, 0.02, 0);
            swModel.ClearSelection2(true);

            swModel.CreateLine2(0.002, dp - k0, 0, 0.005, dp + k, 0);

            swModel.CreateCenterLineVB(0,dp,0,0.005,dp,0); // line7
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.SketchAddConstraints("sgHORIZONTAL2D");
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Point9", "SKETCHPOINT", 0, 0, 0, true, 0, null, 0);
            swModel.SketchAddConstraints("sgCOINCIDENT");
            swModel.ClearSelection2(true);

            

            //a각 적용
            swSelectionMgr = (SelectionMgr)swModel.SelectionManager;
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0); //스케치 요소 선택
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension(0.007, 0.007, 0); //치수 생성(선과 선을 선택 했으므로 angle)
            swDimension = (Dimension)swModel.Parameter("D5@스케치1"); //스케치1의 5번째 치수 선택
            errors = swDimension.SetSystemValue3(a*Math.PI/180, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration,null); //V벨트의 각도
            swModel.ClearSelection2(true); //치수 메세지 닫기

            //l0(중심거리) 적용
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension(0.01, 0.55, 0);
            swDimension = (Dimension)swModel.Parameter("D6@스케치1");
            errors = swDimension.SetSystemValue3(0.004, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration, null);
            swModel.ClearSelection2(true);

            //필렛
            //r1
            status = swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            swModel.SketchFillet1(r1);
            //r2
            status = swModel.Extension.SelectByID2("Line5", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            swModel.SketchFillet1(r2);
            //r3
            status = swModel.Extension.SelectByID2("Line3", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.SketchFillet1(r3); 


            //대칭복사
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line3", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line4", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line5", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Arc1", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Arc2", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Arc3", "SKETCHSEGMENT", 0, 0, 0, true, 1, null, 0);
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, true, 2, null, 0);
            swModel.SketchMirror();

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "V벨트풀리_Sketch";
       
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            
            Console.WriteLine("status 확인 : " + status);

            swFeature = swModel.FeatureManager.FeatureRevolve2(true,true,false,false,false,false,0,0, 6.2831853071796, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "V벨트풀리_Model";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;
           
        }
    }
}
