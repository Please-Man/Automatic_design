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

        //1줄 V벨트 생성
        public void CreateV1(ref double dp, ref double a, ref double l0, ref double k, ref double k0, ref double f, ref double r1, ref double r2, ref double r3)
        { 
            bool status;

            swApp.INewPart(); //파트 새로열기

            swModel = swApp.ActiveDoc; //실행 중인 창을 선택

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

            Console.WriteLine("f = " + f); 
            swModel.CreateLine2(0, 0, 0, f, 0, 0);
            swModel.AddDimension2(f/2.0, dp+5*k, 0);
            swModel.ClearSelection2(true);//f, 디멘션2

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
            swModel.SketchAddConstraints("sgCOINCIDENT"); //일치
            swModel.ClearSelection2(true);

            

            //a각 적용
            swSelectionMgr = (SelectionMgr)swModel.SelectionManager;
            status = swModel.Extension.SelectByID2("Line1", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0); //스케치 요소 선택
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension(0.007, 0.007, 0); //치수 생성(선과 선을 선택 했으므로 angle)
            swDimension = (Dimension)swModel.Parameter("D5@스케치1"); //스케치1의 5번째 치수 선택
            Console.WriteLine("a = " + a);
            errors = swDimension.SetSystemValue3(a*Math.PI/180, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration,null); //V벨트의 각도
            swModel.ClearSelection2(true); //치수 메세지 닫기

            //l0(중심거리) 적용
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension(0.01, 0.55, 0);
            swDimension = (Dimension)swModel.Parameter("D6@스케치1");
            errors = swDimension.SetSystemValue3(l0, (int)swSetValueInConfiguration_e.swSetValue_InThisConfiguration, null);
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

            swModel.CreateCenterLineVB(0, -dp, 0, 0.05, -dp, 0); //도면화 작업 편리용 피치선 

            //도면화 작업을 위한 치수선 삭제 작업
            status = swModel.Extension.SelectByID2("D1@스케치1", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);
            status = swModel.Extension.SelectByID2("D3@스케치1", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);
            status = swModel.Extension.SelectByID2("D4@스케치1", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);
            status = swModel.Extension.SelectByID2("D5@스케치1", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);
            status = swModel.Extension.SelectByID2("D6@스케치1", "DIMENSION", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);

            //도면화 작업을 위한 규격 치수 표시
            status = swModel.Extension.SelectByID2("Line14", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(2.0*f,0,0); //호칭지름(dp)
            status = swModel.Extension.SelectByID2("Line6", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line9", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(0, dp+5.1*k, 0); //각도(a)
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line11", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-2.7*f, dp+k, 0); // k
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            status = swModel.Extension.SelectByID2("Line5", "SKETCHSEGMENT", 0, 0, 0, true, 0, null, 0);
            swModel.AddDimension2(-2.7*f, dp-k0/1.4, 0); // k0
            status = swModel.Extension.SelectByID2("Line7", "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            swModel.AddDimension2(0, dp + 2.8*k , 0); // l0

            //도면화 작업을 위한 표면 거칠기 생성
            swModel.Extension.InsertSurfaceFinishSymbol3((int)swSFSymType_e.swSFMachining_Req, (int)swLeaderStyle_e.swSTRAIGHT, -1.7 * f, dp + k, 0, (int)swSFLaySym_e.swSFNone, (int)swArrowStyle_e.swDOT_ARROWHEAD, "", "", "", "", "x", "", "");
            swModel.Extension.InsertSurfaceFinishSymbol3((int)swSFSymType_e.swSFMachining_Req, (int)swLeaderStyle_e.swSTRAIGHT, -1.7 * f, dp - k0, 0, (int)swSFLaySym_e.swSFNone, (int)swArrowStyle_e.swDOT_ARROWHEAD, "", "", "", "", "x", "", "");
   
            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "V벨트풀리_Sketch";
       
            status = swModel.Extension.SelectByID2("Line2", "SKETCHSEGMENT", 0, 0, 0, true, 16, null, 0);
            
            Console.WriteLine("status 확인 : " + status);

            swFeature = swModel.FeatureManager.FeatureRevolve2(true,true,false,false,false,false,0,0, 6.2831853071796, 0, false, false, 0.01, 0.01, 0, 0, 0, true, true, true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "V벨트풀리";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;
        }

        public void CutKeyHole(ref double di,ref double t,ref double b)
        {
            bool status;

            //샤프트 홀 생성
            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Right", "PLANE", 0, 0, 0, false, 0, null, 0);

            swModel.InsertSketch2(true);

            swModel.CreateCircleByRadius2(0, 0, 0, di/2000.0);
            swModel.AddDimension2(-0.01, 0.01, 0);
            swModel.ClearSelection2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "샤프트 홀_Sketch";

            status = swModel.Extension.SelectByID2("샤프트 홀_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureCut4(false, false, false, 1, 1, 0, 0, false, false, false,
            false, 0,0, false, false, false, false,false,false,false,false,false,false,0,0,false,false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "샤프트 홀";


            //키 홈 생성
            swModel = swApp.ActiveDoc;

            swFeature = swModel.FeatureByPositionReverse(3);
            swFeature.Name = "Front";

            swFeature = swModel.FeatureByPositionReverse(2);
            swFeature.Name = "Top";

            swFeature = swModel.FeatureByPositionReverse(1);
            swFeature.Name = "Right";

            status = swModel.Extension.SelectByID2("Right", "PLANE", 0, 0, 0, false, 0, null, 0);


            swModel.InsertSketch2(true);

            swModel.SketchRectangle(-b/2, di/2000.0+t, 0, b/2, di/2000.0-t, 0, true);
            swModel.AddDimension2(0.03, -0.02, 0);
            swModel.ClearSelection2(true);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "키 홈_Sketch";
            
            status = swModel.Extension.SelectByID2("키홈_Sketch", "SKETCH", 0, 0, 0, false, 0, null, 0);

            swFeature = swModel.FeatureManager.FeatureCut4(false, false, false, 1, 1, 0, 0, false, false, false,
            false, 0, 0, false, false, false, false, false, false, false, false, false, false, 0, 0, false, false);

            swFeature = swModel.FeatureByPositionReverse(0);
            swFeature.Name = "키 홈";

            swModel.ForceRebuild3(true);
            swModel.ViewZoomtofit2();

            config = swModel.GetActiveConfiguration();
            cusPropMgr = config.CustomPropertyManager;         
        }

        public void Create_Bearing()
        {

        }
    }
}
