using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
namespace Design_Automation_Machinery
{
    class CreateDrafting_Class
    {
        SldWorks swApp = new SldWorks();

        ModelDoc2 swModel;
        PartDoc myPart = default(PartDoc);
        MaterialVisualPropertiesData myMatVisProps = default(MaterialVisualPropertiesData);
        Feature swFeature;
        Configuration config;
        CustomPropertyManager cusPropMgr;

        DocumentSpecification swDocSpecification = default(DocumentSpecification);

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

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;

        public void Drafting_Vbelt(ref string filename1, ref string filename2, ref double dp,ref double f)
        {
            Save(ref filename1); // 저장 후 불러오기

            string assemblyDrawing = null;
            bool status = false;
            int lineWeight = 0;
            double lineThickness = 0;
            object excludedComponents = null;

            swModel = (ModelDoc2)swApp.ActiveDoc;

            swDraw = (DrawingDoc)swApp.INewDrawing(8); //A0:11, A1:10, A2:9, A3:8, A4:6 , 도면 새로 만들기
            swDrawModel = (ModelDoc2)swDraw;
            Sheet swSheet;

            status = swDraw.Create3rdAngleViews2(filename1); //도면의 3면도 생성
            swSheet = (Sheet)swDraw.GetCurrentSheet();
            swSheet.SetScale(1.0, 1.0, true, true); // 1:1비율

            swView = (View)swDraw.GetFirstView();
            swView = (View)swView.GetNextView();
            
            status = swView.SelectEntity(swEnt, false);
            swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);

            //평면도 삭제
            swModel = (ModelDoc2)swApp.ActiveDoc;           
            status = swModel.Extension.SelectByID2("도면뷰2", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swModel.DeleteSelection(status);
                   
            status = swModel.Extension.SelectByID2("도면뷰1", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.ViewDisplayHiddengreyed();
            swDraw.Dimensions(); //모델링의 지능형 치수 전부 가져오기   
            //swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);

            status = swDraw.ActivateView("도면뷰1");
            swSketchMgr = (SketchManager)swModel.SketchManager;
            swSketchSegment = (SketchSegment)swSketchMgr.CreateCircleByRadius(0.0025, 0.0046+dp , 0, 2*f);
            swDraw.CreateDetailViewAt4(0.34305342706156, 0.2292140266484527, 0, (int)swDetViewStyle_e.swDetViewSTANDARD, 2, 1, "A", (int)swDetCircleShowType_e.swDetCircleCIRCLE, true, true, false, 5);
            //swModel.ClearSelection2(true);

            status = swModel.Extension.SelectByID2("V벨트풀리_Sketch@" + filename2 + "<4>@상세도 A (2 : 1)", "SKETCH", 0, 0, 0, false, 0, null, 0);
            swModel.UnblankSketch();// 스케치 보이기
            swDraw.Dimensions();
            swSelMgr = (SelectionMgr)swModel.SelectionManager;
            swSelData = (SelectData)swSelMgr.CreateSelectData();
            //swModel.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swDisplayAnnotations, true);
            swModel.ClearSelection2(true);

            status = swModel.Extension.SelectByID2("도면뷰3", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.ViewDisplayHidden();

            swModel.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swUnitSystem,(int)swUnitSystem_e.swUnitSystem_MKS);
            swModel.SetUserPreferenceIntegerValue((int)swUserPreferenceIntegerValue_e.swUnitSystem, (int)swUnitSystem_e.swUnitSystem_MMGS);
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

        private void LinkConfiguration()
        {
            // Print to the Immediate window whether
            // the projected view is linked to the parent
            // configuration
            bool status;

            status = swView.LinkParentConfiguration;
            if (status)
            {
                Console.WriteLine("Projected view now linked to parent configuration.");
                swModel.EditRebuild3();
            }
            else
            {
                Console.WriteLine("Projected view not linked to parent configuration.");
            }
        }
    }
}
