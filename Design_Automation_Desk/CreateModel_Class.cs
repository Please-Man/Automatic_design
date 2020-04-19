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
        // 모델링을 위한 객체

        DocumentSpecification swDocSpecification = default(DocumentSpecification);

        string[] componentsArray = new string[1];
        object[] components = null;
        string name = null;
        int errors = 0;
        int warnings = 0;

        public void CreateB(ref double x, ref double y, ref double h) //각주 생성
        {
            //swDocSpecification = (DocumentSpecification)swApp.INewPart();

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
    }
}
