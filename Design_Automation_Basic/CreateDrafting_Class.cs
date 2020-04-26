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
namespace Design_Automation_Basic
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

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;

        public void Drafting_create(ref string filename)
        {
            string assemblyDrawing = null;
            bool status = false;
            int lineWeight = 0;
            double lineThickness = 0;

            swModel = (ModelDoc2)swApp.ActiveDoc;

            swDraw = (DrawingDoc)swApp.INewDrawing(8); //A0:11, A1:10, A2:9, A3:8, A4:6 , 도면 새로 만들기
            swDrawModel = (ModelDoc2)swDraw;

            status = swDraw.Create3rdAngleViews2(filename);

            swView = (View)swDraw.GetFirstView();
            swView = (View)swView.GetNextView();

            status = swView.SelectEntity(swEnt, false);
            //status = swModelDocExt.SelectByID2("도면뷰1", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);
            
            swModel = (ModelDoc2)swApp.ActiveDoc;
            status = swModel.Extension.SelectByID2("도면뷰1", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);

            status = swModel.Extension.SelectByID2("도면뷰2", "DRAWINGVIEW", 0, 0, 0, false, 0, null, 0);
            swDraw.AutoDimension((int)swAutodimEntities_e.swAutodimEntitiesBasedOnPreselect, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimHorizontalPlacement_e.swAutodimHorizontalPlacementAbove, (int)swAutodimScheme_e.swAutodimSchemeBaseline, (int)swAutodimVerticalPlacement_e.swAutodimVerticalPlacementRight);
        }

        public void Save(ref string filename)
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
    }
}
